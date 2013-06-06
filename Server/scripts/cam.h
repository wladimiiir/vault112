#include "fonline_tla.h"

#ifndef __CRITTER_ACTION_MANAGER__
#define __CRITTER_ACTION_MANAGER__

class IAction
{
	public:
		virtual IAction* GetSuperAction();
		virtual void SetSuperAction(IAction* superAction);
		virtual void CollectListeningActions(ScriptAny& collection);
		virtual void StartNextAction(Critter& critter);
		virtual bool Check(Critter& critter, Critter* someCritter, Item* someItem);
		virtual bool Start(Critter& critter);
		virtual void Cancel(Critter& critter);
		virtual string GetInfo();
		
		virtual void Idle(Critter& critter);
		virtual bool Attack(Critter& critter, Critter& target);
};

class ICondition
{
	public:
		virtual bool Check(Critter& critter, Critter someCritter, Item someItem);
};

#endif     // __CRITTER_ACTION_MANAGER__
