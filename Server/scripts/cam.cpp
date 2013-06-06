#include "cam.h"

// Entry point
FONLINE_DLL_ENTRY( isCompiler )
{
    if( isCompiler )
        return;

	Log("Hello");
	
	ASEngine->RegisterInterface("IAction");
	ASEngine->RegisterInterfaceMethod("IAction", "IAction@ GetSuperAction()");
	ASEngine->RegisterInterfaceMethod("IAction", "void SetSuperAction(IAction@ superAction)");
	ASEngine->RegisterInterfaceMethod("IAction", "void CollectListeningActions(any& collection)");
	ASEngine->RegisterInterfaceMethod("IAction", "void StartNextAction(Critter& critter)");
	ASEngine->RegisterInterfaceMethod("IAction", "bool Check(Critter& critter, Critter@ someCritter, Item@ someItem)");
	ASEngine->RegisterInterfaceMethod("IAction", "bool Start(Critter& critter)");
	ASEngine->RegisterInterfaceMethod("IAction", "void Cancel(Critter& critter)");
	ASEngine->RegisterInterfaceMethod("IAction", "string GetInfo()");
	ASEngine->RegisterInterfaceMethod("IAction", "void Idle(Critter& critter)");
	ASEngine->RegisterInterfaceMethod("IAction", "bool Attack(Critter& critter, Critter& target)");
	
	ASEngine->RegisterInterface("ICondition");
	ASEngine->RegisterInterfaceMethod("ICondition", "bool Check(Critter&, Critter@, Item@)");
	
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
		void Start(Critter& critter, IAction* action)
		{
			queuedActions[critter.Id] = action;
		}
		
		void StartQueuedAction(Critter& critter)
		{
			IAction* action = queuedActions[critter.Id];
			if(action != NULL)
			{
				Log("Found non-null action");
				Log("Starting");
				action->Start(critter);
				Log("Setting NULL to queued actions");
				queuedActions[critter.Id] = NULL;
				Log("Setting action as runnig");
				runningActions[critter.Id] = action;
			}
			Log("End of start queued action");
		}
		
		ScriptAny& GetListeningActions(Critter& critter)
		{
			ScriptAny& actions = ScriptAny::Create();
			IAction* runningAction = runningActions[critter.Id];
			
			if(runningAction == NULL)
				return actions;
			
			Log("Calling collect of listening actions");
			runningAction->CollectListeningActions(actions);
			Log("End of calling collect of listening actions");
			
			return actions;
		}
};

map<uint, IAction*> actionMap;
CritterActionManager manager;

EXPORT void Start(Critter& critter, IAction& action)
{
	manager.Start(critter, &action);
}

EXPORT void Idle(Critter& critter)
{
	Log("Trying Idle action");
	manager.StartQueuedAction(critter);

	Log("Getting listening actions");
	ScriptAny& listeningActions = manager.GetListeningActions(critter);
	
	Log("Found some listening actions");
	//~ for(int i = 0; i != listeningActions.GetElementSize(); i++) {
		//~ ((IAction*) listeningActions.At(i))->Idle(critter);
	//~ }
}

EXPORT bool Attack(Critter& critter, Critter& target)
{
	ScriptAny& listeningActions = manager.GetListeningActions(critter);
		
	//~ for(int i = 0; i != listeningActions.GetElementSize(); i++) {
		//~ bool result = ((IAction*) listeningActions.At(i))->Attack(critter, target);
		//~ if(result)
			//~ return true;
	//~ }
	return false;
}

EXPORT bool Attacked(Critter& critter, Critter& attacker)
{
	return false;
}

EXPORT void Dead(Critter& critter, Critter* killer)
{
}

EXPORT void Finish(Critter& critter)
{
}

EXPORT int PlaneBegin(Critter& critter, NpcPlane& plane, int reason, Critter* someCr, Item* someItem)
{
	return PLANE_KEEP;
}

EXPORT int PlaneRun(Critter& critter, NpcPlane& plane, int reason, uint& result0, uint& result1, uint& result2)
{
	return PLANE_KEEP;
}

EXPORT int PlaneEnd(Critter& critter, NpcPlane& plane, int reason, Critter* someCr, Item* someItem)
{
	return PLANE_KEEP;
}

EXPORT void SeeSomeoneAttack(Critter& critter, Critter& attacker, Critter& attacked)
{
	
}

EXPORT void SeeSomeoneDead(Critter& critter, Critter& killed, Critter* killer)
{
}

EXPORT void SeeSomeoneAttacked(Critter& critter, Critter& attacked, Critter& attacker)
{
}

EXPORT void SeeSomeoneStealing(Critter& critter, Critter& victim, Critter& thief, bool success, Item& item, uint count)
{
}

EXPORT void ShowCritter(Critter& critter, Critter& showCritter)
{
}

EXPORT void HideCritter(Critter& critter, Critter& hideCritter)
{
}

EXPORT void Message(Critter& critter, Critter& messenger, int message, int value)
{
}

EXPORT void Respawn(Critter& critter)
{
}

EXPORT void SeeSomeoneUseSkill(Critter& critter, Critter& skillCritter, int skill, Critter* onCritter, Item* onItem, MapObject* onScenery)
{
}

