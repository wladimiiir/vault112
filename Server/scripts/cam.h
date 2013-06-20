#include "fonline_tla.h"

#ifndef __CRITTER_ACTION_MANAGER__
#define __CRITTER_ACTION_MANAGER__

std::vector<asIScriptContext *> pool;
asIScriptContext *GetContextFromPool()
{
  // Get a context from the pool, or create a new
  asIScriptContext *ctx = 0;
  if( pool.size() )
  {
    ctx = *pool.rbegin();
    pool.pop_back();
  }
  else
    ctx = ASEngine->CreateContext();
  return ctx;
}
void ReturnContextToPool(asIScriptContext *ctx)
{
  pool.push_back(ctx);
  
  // Unprepare the context to free non-reusable resources
  ctx->Unprepare();
}

class IAction
{
	public:
		IAction(){}
		~IAction(){}
	
		virtual bool Start(Critter* critter) = 0;
		virtual void Cancel(Critter* critter) = 0;
		virtual vector<IAction*> GetListeningActions() = 0;
		virtual const char* GetInfo() = 0;
		
		virtual void IdleEvent(Critter* critter) = 0;
		virtual void ShowCritterEvent(Critter* critter, Critter* showCritter) = 0;
		virtual void HideCritterEvent(Critter* critter, Critter* hideCritter) = 0;
		virtual bool AttackEvent(Critter* critter, Critter* target) = 0;
		virtual bool AttackedEvent(Critter* critter, Critter* attacker) = 0;
		virtual void DeadEvent(Critter* critter, Critter* killer) = 0;
		virtual void MessageEvent(Critter* critter, Critter* messenger, int message, int value) = 0;
		virtual void SeeSomeoneAttackEvent(Critter* critter, Critter* attacker, Critter* attacked) = 0;
		virtual void SeeSomeoneDeadEvent(Critter* critter, Critter* killed, Critter* killer) = 0;
		virtual void SeeSomeoneAttackedEvent(Critter* critter, Critter* attacked, Critter* attacker) = 0;
		virtual void SeeSomeoneStealingEvent(Critter* critter, Critter* victim, Critter* thief, bool success, Item* item, uint count) = 0;
		virtual void SeeSomeoneUseSkillEvent(Critter* critter, Critter* skillCritter, int skill, Critter* onCritter, Item* onItem, MapObject* onScenery) = 0;
		virtual int PlaneBeginEvent(Critter* critter, NpcPlane* plane, int reason, Critter* someCr, Item* someItem) = 0;
		virtual int PlaneRunEvent(Critter* critter, NpcPlane* plane, int reason, uint& result0, uint& result1, uint& result2) = 0;
		virtual int PlaneEndEvent(Critter* critter, NpcPlane* plane, int reason, Critter* someCr, Item* someItem) = 0;
};

class ScriptActionWrapper : public IAction
{
	private:
		map<asIScriptObject*, ScriptActionWrapper*> wrapperMap;
		map<char*, asIScriptFunction*> functionMap;
	
		asIScriptContext* PrepareContext(char* methodName)
		{
			asIScriptContext* ctx = GetContextFromPool();
		
			//~ ctx->PushState();
			
			asIScriptFunction* func = functionMap[methodName];
			
			if(func == NULL)
			{
				func = scriptAction->GetObjectType()->GetMethodByName(methodName);
				functionMap[methodName] = func;
			}
			
			if(func == NULL)
			{
				Log("Cannot find function '%s'\n", methodName);
				return NULL;
			}
			ctx->Prepare(func);
			ctx->SetObject(scriptAction);
			
			return ctx;
		}
		
		bool Execute(asIScriptContext* context)
		{
			int state = context->Execute();

			if(state == asEXECUTION_EXCEPTION)
			{
				asIScriptFunction* ex = context->GetExceptionFunction();
				Log("Exception occured: %s\n", context->GetExceptionString());
				if(ex != NULL)
					Log("Function exception: %s@%s\n", ex->GetModuleName(), ex->GetName());
				Log("Line of exception: %d\n", context->GetExceptionLineNumber());
				
				return false;
			}
			
			return true;
		}
		
		void Finish(asIScriptContext* context)
		{
			ReturnContextToPool(context);
		}
	
	protected:
		asIScriptObject* scriptAction;
		
	public:
		ScriptActionWrapper(asIScriptObject* scriptActionObj) : scriptAction(scriptActionObj) { scriptAction->AddRef(); }
		~ScriptActionWrapper() { scriptAction->Release(); }
	
		asIScriptObject* GetScriptAction() { return scriptAction; }
		
		const char* GetInfo()
		{
			asIScriptContext* ctx = PrepareContext("GetInfo");
			
			if(ctx == NULL)
				return "error";
				
			Execute(ctx);
			ScriptString* result = (ScriptString*) ctx->GetReturnObject();
			Finish(ctx);
			
			return result->c_str();
		}
		
		vector<IAction*> GetListeningActions()
		{
			asIScriptContext* ctx = PrepareContext("CollectListeningActions");
			if(ctx == NULL)
				return vector<IAction*>(0);
			
			ScriptArray& actionArray = ScriptArray::Create("Action@");
			
			//~ actionArray.AddRef();
			
			ctx->SetArgAddress(0, &actionArray);
			Execute(ctx);

			//~ Log("Collected %d listening actions from AS\n", actionArray.GetSize());
			
			vector<IAction*> actions(actionArray.GetSize());
			for(uint i = 0; i < actionArray.GetSize(); i++)
			{
				asIScriptObject** obj = static_cast<asIScriptObject**>(actionArray.At(i));
				//~ Log("Found listening action: %s\n", (*obj)->GetObjectType()->GetName());
				ScriptActionWrapper* wrapper = wrapperMap[*obj];
				if(wrapper == NULL)
				{
					wrapper = new ScriptActionWrapper(*obj);
					wrapperMap[*obj] = wrapper;
				}
				//~ Log("GetInfo of listening action: %s\n", wrapper->GetInfo());
				actions[i] = wrapper;
			}
			//~ actionArray.Release();
			Finish(ctx);
			
			return actions;
		}
		
		bool Start(Critter* critter)
		{
			asIScriptContext* ctx = PrepareContext("Start");
			
			if(ctx == NULL)
				return false;
				
			ctx->SetArgObject(0, critter);
			Execute(ctx);
			
			asBYTE result = ctx->GetReturnByte();
			Finish(ctx);
			return result != 0;
		}
		
		void Cancel(Critter* critter)
		{
			asIScriptContext* ctx = PrepareContext("Cancel");
			
			if(ctx == NULL)
				return;
				
			ctx->SetArgObject(0, critter);
			Execute(ctx);
			Finish(ctx);
		} 
		
		void IdleEvent(Critter* critter)
		{
			asIScriptContext* ctx = PrepareContext("IdleEvent");
			if(ctx == NULL)
				return;
				
			ctx->SetArgObject(0, critter);
			Execute(ctx);
			Finish(ctx);
		}
		
		void ShowCritterEvent(Critter* critter, Critter* showCritter)
		{
			asIScriptContext* ctx = PrepareContext("ShowCritterEvent");
			if(ctx == NULL)
				return;
				
			ctx->SetArgObject(0, critter);
			ctx->SetArgObject(1, showCritter);
			Execute(ctx);
			Finish(ctx);
		}
		
		void HideCritterEvent(Critter* critter, Critter* hideCritter)
		{
			asIScriptContext* ctx = PrepareContext("HideCritterEvent");
			if(ctx == NULL)
				return;
				
			ctx->SetArgObject(0, critter);
			ctx->SetArgObject(1, hideCritter);
			Execute(ctx);
			Finish(ctx);
		}
		
		bool AttackEvent(Critter* critter, Critter* target)
		{
			asIScriptContext* ctx = PrepareContext("AttackEvent");
			
			if(ctx == NULL)
				return false;
				
			ctx->SetArgObject(0, critter);
			ctx->SetArgObject(1, target);
			Execute(ctx);
			
			asBYTE result = ctx->GetReturnByte();
			Finish(ctx);
			return result != 0;
		}
		
		bool AttackedEvent(Critter* critter, Critter* attacker)
		{
			asIScriptContext* ctx = PrepareContext("AttackedEvent");
			
			if(ctx == NULL)
				return false;
				
			ctx->SetArgObject(0, critter);
			ctx->SetArgObject(1, attacker);
			Execute(ctx);
			
			asBYTE result = ctx->GetReturnByte();
			Finish(ctx);
			return result != 0;
		}
		
		void DeadEvent(Critter* critter, Critter* killer)
		{
			asIScriptContext* ctx = PrepareContext("DeadEvent");
			if(ctx == NULL)
				return;
				
			ctx->SetArgObject(0, critter);
			ctx->SetArgObject(1, killer);
			Execute(ctx);
			Finish(ctx);
		}
		
		void MessageEvent(Critter* critter, Critter* messenger, int message, int value)
		{
			asIScriptContext* ctx = PrepareContext("MessageEvent");
			if(ctx == NULL)
				return;
				
			ctx->SetArgObject(0, critter);
			ctx->SetArgObject(1, messenger);
			ctx->SetArgDWord(2, message);
			ctx->SetArgDWord(3, value);
			Execute(ctx);
			Finish(ctx);
		}
		
		void SeeSomeoneAttackEvent(Critter* critter, Critter* attacker, Critter* attacked)
		{
			asIScriptContext* ctx = PrepareContext("SeeSomeoneAttackEvent");
			if(ctx == NULL)
				return;
				
			ctx->SetArgObject(0, critter);
			ctx->SetArgObject(1, attacker);
			ctx->SetArgObject(2, attacked);
			Execute(ctx);
			Finish(ctx);
		}
		
		void SeeSomeoneDeadEvent(Critter* critter, Critter* killed, Critter* killer)
		{
			asIScriptContext* ctx = PrepareContext("SeeSomeoneDeadEvent");
			if(ctx == NULL)
				return;
				
			ctx->SetArgObject(0, critter);
			ctx->SetArgObject(1, killed);
			ctx->SetArgObject(2, killer);
			Execute(ctx);
			Finish(ctx);
		}
		
		void SeeSomeoneAttackedEvent(Critter* critter, Critter* attacked, Critter* attacker)
		{
			asIScriptContext* ctx = PrepareContext("SeeSomeoneAttackedEvent");
			if(ctx == NULL)
				return;
				
			ctx->SetArgObject(0, critter);
			ctx->SetArgObject(1, attacked);
			ctx->SetArgObject(2, attacker);
			Execute(ctx);
			Finish(ctx);
		}
		
		void SeeSomeoneStealingEvent(Critter* critter, Critter* victim, Critter* thief, bool success, Item* item, uint count)
		{
			asIScriptContext* ctx = PrepareContext("SeeSomeoneStealingEvent");
			if(ctx == NULL)
				return;
				
			ctx->SetArgObject(0, critter);
			ctx->SetArgObject(1, victim);
			ctx->SetArgObject(2, thief);
			ctx->SetArgByte(3, success);
			ctx->SetArgObject(4, item);
			ctx->SetArgDWord(5, count);
			Execute(ctx);
			Finish(ctx);
		}
		
		void SeeSomeoneUseSkillEvent(Critter* critter, Critter* skillCritter, int skill, Critter* onCritter, Item* onItem, MapObject* onScenery)
		{
			asIScriptContext* ctx = PrepareContext("SeeSomeoneUseSkill");
			if(ctx == NULL)
				return;
				
			ctx->SetArgObject(0, critter);
			ctx->SetArgObject(1, skillCritter);
			ctx->SetArgDWord(2, skill);
			ctx->SetArgObject(3, onCritter);
			ctx->SetArgObject(4, onItem);
			ctx->SetArgObject(5, onScenery);
			Execute(ctx);
			Finish(ctx);
		}
		
		int PlaneBeginEvent(Critter* critter, NpcPlane* plane, int reason, Critter* someCr, Item* someItem)
		{
			asIScriptContext* ctx = PrepareContext("PlaneBeginEvent");
			
			if(ctx == NULL)
				return false;
				
			ctx->SetArgObject(0, critter);
			ctx->SetArgObject(1, plane);
			ctx->SetArgDWord(2, reason);
			ctx->SetArgObject(3, someCr);
			ctx->SetArgObject(4, someItem);
			Execute(ctx);
			
			int result = ctx->GetReturnDWord();
			Finish(ctx);
			return result;
		}
		
		int PlaneRunEvent(Critter* critter, NpcPlane* plane, int reason, uint& result0, uint& result1, uint& result2)
		{
			asIScriptContext* ctx = PrepareContext("PlaneRunEvent");
			
			if(ctx == NULL)
				return false;
				
			ctx->SetArgObject(0, critter);
			ctx->SetArgObject(1, plane);
			ctx->SetArgDWord(2, reason);
			ctx->SetArgAddress(3, &result0);
			ctx->SetArgAddress(4, &result1);
			ctx->SetArgAddress(5, &result2);
			Execute(ctx);
			
			int result = ctx->GetReturnDWord();
			Finish(ctx);
			return result;
		}
		
		int PlaneEndEvent(Critter* critter, NpcPlane* plane, int reason, Critter* someCr, Item* someItem)
		{
			asIScriptContext* ctx = PrepareContext("PlaneEndEvent");
			
			if(ctx == NULL)
				return false;
				
			ctx->SetArgObject(0, critter);
			ctx->SetArgObject(1, plane);
			ctx->SetArgDWord(2, reason);
			ctx->SetArgObject(3, someCr);
			ctx->SetArgObject(4, someItem);
			Execute(ctx);
			
			int result = ctx->GetReturnDWord();
			Finish(ctx);
			return result;
		}
};

#endif     // __CRITTER_ACTION_MANAGER__
