#include "cam.h"

void Start(Critter* critter, asIScriptObject* actionObj, bool immediately);

EXPORT void InitLibrary();

// Entry point
FONLINE_DLL_ENTRY( isCompiler )
{
    if( isCompiler )
        return;

	Log("Registering CritterActionManager types...\n");
	ASEngine->RegisterInterface("Condition");
	ASEngine->RegisterInterfaceMethod("Condition", "bool Check(Critter&, Critter@, Item@)");
	
	ASEngine->RegisterInterface("Action");
	ASEngine->RegisterInterfaceMethod("Action", "Action@ GetSuperAction()");
	ASEngine->RegisterInterfaceMethod("Action", "Action@ AddSubAction(Action@ subAction)");
	ASEngine->RegisterInterfaceMethod("Action", "Action@ If(Condition@ condition)");
	ASEngine->RegisterInterfaceMethod("Action", "Action@ IfNot(Condition@ condition)");
	ASEngine->RegisterInterfaceMethod("Action", "Action@ And(Action@ andAction)");
	
	ASEngine->RegisterInterfaceMethod("Action", "void SetSuperAction(Action@ superAction)");
	ASEngine->RegisterInterfaceMethod("Action", "void CollectListeningActions(array<Action@>& collection)");
	ASEngine->RegisterInterfaceMethod("Action", "void StartNextAction(Critter& critter)");
	ASEngine->RegisterInterfaceMethod("Action", "bool Check(Critter& critter, Critter@ someCritter, Item@ someItem)");
	ASEngine->RegisterInterfaceMethod("Action", "bool Start(Critter& critter)");
	ASEngine->RegisterInterfaceMethod("Action", "void Cancel(Critter& critter)");
	ASEngine->RegisterInterfaceMethod("Action", "string GetInfo()");
	
	ASEngine->RegisterInterfaceMethod("Action", "void IdleEvent(Critter& critter)");
	ASEngine->RegisterInterfaceMethod("Action", "void ShowCritterEvent(Critter& critter, Critter& showCritter)");
	ASEngine->RegisterInterfaceMethod("Action", "void HideCritterEvent(Critter& critter, Critter& hideCritter)");
	ASEngine->RegisterInterfaceMethod("Action", "bool AttackEvent(Critter& critter, Critter& target)");
	ASEngine->RegisterInterfaceMethod("Action", "bool AttackedEvent(Critter& critter, Critter& attacker)");
	ASEngine->RegisterInterfaceMethod("Action", "void DeadEvent(Critter& critter, Critter@ killer)");
	ASEngine->RegisterInterfaceMethod("Action", "void MessageEvent(Critter& critter, Critter& messenger, int message, int value)");
	ASEngine->RegisterInterfaceMethod("Action", "void SeeSomeoneAttackEvent(Critter& critter, Critter& attacker, Critter& attacked)");
	ASEngine->RegisterInterfaceMethod("Action", "void SeeSomeoneDeadEvent(Critter& critter, Critter& killed, Critter@ killer)");
	ASEngine->RegisterInterfaceMethod("Action", "void SeeSomeoneAttackedEvent(Critter& critter, Critter& attacked, Critter& attacker)");
	ASEngine->RegisterInterfaceMethod("Action", "void SeeSomeoneStealingEvent(Critter& critter, Critter& victim, Critter& thief, bool success, Item& item, uint count)");
	ASEngine->RegisterInterfaceMethod("Action", "void SeeSomeoneUseSkillEvent(Critter& critter, Critter& skillCritter, int skill, Critter@ onCritter, Item@ onItem, Scenery@ onScenery)");
	ASEngine->RegisterInterfaceMethod("Action", "int PlaneBeginEvent(Critter& critter, NpcPlane& plane, int reason, Critter@ someCr, Item@ someItem)");
	ASEngine->RegisterInterfaceMethod("Action", "int PlaneRunEvent(Critter& critter, NpcPlane& plane, int reason, uint& result0, uint& result1, uint& result2)");
	ASEngine->RegisterInterfaceMethod("Action", "int PlaneEndEvent(Critter& critter, NpcPlane& plane, int reason, Critter@ someCr, Item@ someItem)");
	
	ASEngine->RegisterGlobalFunction("void StartCritterAction(Critter&, Action&, bool)", asFUNCTION(Start), asCALL_CDECL);
	
    // Test Memory Level 3 for loaded DLLs
    for( uint i = 0; i < 666; i++ )
        volatile char* leak = new char[ 2 ];
}

//used to load the library
EXPORT void InitLibrary()
{
}

class CritterActionManager
{
	private:
		map<uint, IAction*> runningActions;
		map<uint, IAction*> queuedActions;
	
	public:
		void Start(Critter* critter, IAction* action, bool immediatelly)
		{
			if(immediatelly)
			{
				runningActions[critter->Id] = action;
				action->Start(critter);
			}
			else
			{
				queuedActions[critter->Id] = action;
			}
		}
		
		void StartQueuedAction(Critter* critter)
		{
			IAction* action = queuedActions[critter->Id];
			if(action != NULL)
			{
				queuedActions[critter->Id] = NULL;
				runningActions[critter->Id] = action;
				action->Start(critter);
			}
		}
		
		void Finish(Critter* critter)
		{
			IAction* action = queuedActions[critter->Id];
			if(action != NULL)
				queuedActions[critter->Id] = NULL;
				
			action = runningActions[critter->Id];
			if(action != NULL)
			{
				action->Cancel(critter);
				runningActions[critter->Id] = NULL;
			}
		}
		
		void Respawn(Critter* critter)
		{
			IAction* action = runningActions[critter->Id];
			if(action != NULL)
			{
				action->Cancel(critter);
				runningActions[critter->Id] = NULL;
				queuedActions[critter->Id] = action;
			}
		}
		
		vector<IAction*> GetListeningActions(Critter* critter)
		{
			IAction* runningAction = runningActions[critter->Id];
			
			if(runningAction == NULL)
				return vector<IAction*>(0);
			
			return runningAction->GetListeningActions();
		}
};

map<uint, IAction*> actionMap;
CritterActionManager manager;

void Start(Critter* critter, asIScriptObject* actionObj, bool immediately)
{
	if(actionObj == NULL)
		return;
		
	IAction* action = new ScriptActionWrapper(actionObj);
	
	manager.Start(critter, action, immediately);
}

EXPORT void Idle(Critter* critter)
{
	manager.StartQueuedAction(critter);
	vector<IAction*> listeningActions = manager.GetListeningActions(critter);
	
	for(uint i = 0; i < listeningActions.size(); i++)
		listeningActions[i]->IdleEvent(critter);
}

EXPORT bool Attack(Critter* critter, Critter* target)
{
	vector<IAction*> listeningActions = manager.GetListeningActions(critter);
		
	bool result = false;
	for(uint i = 0; i < listeningActions.size(); i++)
		result |= listeningActions[i]->AttackEvent(critter, target);
		
	return result;
}

EXPORT bool Attacked(Critter* critter, Critter* attacker)
{
	vector<IAction*> listeningActions = manager.GetListeningActions(critter);
	
	bool result = false;
	for(uint i = 0; i < listeningActions.size(); i++)
		result |= listeningActions[i]->AttackedEvent(critter, attacker);
		
	return result;
}

EXPORT void Dead(Critter* critter, Critter* killer)
{
	vector<IAction*> listeningActions = manager.GetListeningActions(critter);
	
	for(uint i = 0; i < listeningActions.size(); i++)
		listeningActions[i]->DeadEvent(critter, killer);
}

EXPORT void Finish(Critter* critter, bool deleted)
{
	if(!deleted)
		return;
			
	manager.Finish(critter);
}

EXPORT int PlaneBegin(Critter* critter, NpcPlane* plane, int reason, Critter* someCr, Item* someItem)
{
	vector<IAction*> listeningActions = manager.GetListeningActions(critter);
		
	int result = PLANE_RUN_GLOBAL;
	for(uint i = 0; i < listeningActions.size(); i++) {
		int actionResult = listeningActions[i]->PlaneBeginEvent(critter, plane, reason, someCr, someItem);
		if(actionResult != PLANE_RUN_GLOBAL)
			result = actionResult;
	}
	return result;
}

EXPORT int PlaneRun(Critter* critter, NpcPlane* plane, int reason, uint& result0, uint& result1, uint& result2)
{
	vector<IAction*> listeningActions = manager.GetListeningActions(critter);
		
	int result = PLANE_RUN_GLOBAL;
	for(uint i = 0; i < listeningActions.size(); i++) {
		int actionResult = listeningActions[i]->PlaneRunEvent(critter, plane, reason, result0, result1, result2);
		if(actionResult != PLANE_RUN_GLOBAL)
			result = actionResult;
	}
	return result;
}

EXPORT int PlaneEnd(Critter* critter, NpcPlane* plane, int reason, Critter* someCr, Item* someItem)
{
	vector<IAction*> listeningActions = manager.GetListeningActions(critter);
		
	int result = PLANE_RUN_GLOBAL;
	for(uint i = 0; i < listeningActions.size(); i++) {
		int actionResult = listeningActions[i]->PlaneEndEvent(critter, plane, reason, someCr, someItem);
		if(actionResult != PLANE_RUN_GLOBAL)
			result = actionResult;
	}
	return result;
}

EXPORT void SeeSomeoneAttack(Critter* critter, Critter* attacker, Critter* attacked)
{
	vector<IAction*> listeningActions = manager.GetListeningActions(critter);
	
	for(uint i = 0; i < listeningActions.size(); i++)
		listeningActions[i]->SeeSomeoneAttackEvent(critter, attacker, attacked);
}

EXPORT void SeeSomeoneDead(Critter* critter, Critter* killed, Critter* killer)
{
	vector<IAction*> listeningActions = manager.GetListeningActions(critter);
	
	for(uint i = 0; i < listeningActions.size(); i++)
		listeningActions[i]->SeeSomeoneDeadEvent(critter, killed, killer);
}

EXPORT void SeeSomeoneAttacked(Critter* critter, Critter* attacked, Critter* attacker)
{
	vector<IAction*> listeningActions = manager.GetListeningActions(critter);
	
	for(uint i = 0; i < listeningActions.size(); i++)
		listeningActions[i]->SeeSomeoneAttackedEvent(critter, attacked, attacker);
}

EXPORT void SeeSomeoneStealing(Critter* critter, Critter* victim, Critter* thief, bool success, Item* item, uint count)
{
	vector<IAction*> listeningActions = manager.GetListeningActions(critter);
	
	for(uint i = 0; i < listeningActions.size(); i++)
		listeningActions[i]->SeeSomeoneStealingEvent(critter, victim, thief, success, item, count);
}

EXPORT void ShowCritter(Critter* critter, Critter* showCritter)
{
	vector<IAction*> listeningActions = manager.GetListeningActions(critter);
	
	for(uint i = 0; i < listeningActions.size(); i++)
		listeningActions[i]->ShowCritterEvent(critter, showCritter);
}

EXPORT void HideCritter(Critter* critter, Critter* hideCritter)
{
	vector<IAction*> listeningActions = manager.GetListeningActions(critter);
	
	for(uint i = 0; i < listeningActions.size(); i++)
		listeningActions[i]->HideCritterEvent(critter, hideCritter);
}

EXPORT void Message(Critter* critter, Critter* messenger, int message, int value)
{
	vector<IAction*> listeningActions = manager.GetListeningActions(critter);
	
	for(uint i = 0; i < listeningActions.size(); i++)
		listeningActions[i]->MessageEvent(critter, messenger, message, value);
}

EXPORT void Respawn(Critter* critter)
{
	manager.Respawn(critter);
}

EXPORT void SeeSomeoneUseSkill(Critter* critter, Critter* skillCritter, int skill, Critter* onCritter, Item* onItem, MapObject* onScenery)
{
	vector<IAction*> listeningActions = manager.GetListeningActions(critter);
	
	for(uint i = 0; i < listeningActions.size(); i++)
		listeningActions[i]->SeeSomeoneUseSkillEvent(critter, skillCritter, skill, onCritter, onItem, onScenery);
}

