using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FOnline
{
	public partial class NpcPlane : IManagedWrapper
	{
		IntPtr thisptr;

		public NpcPlane (IntPtr ptr)
		{
			thisptr = ptr;
			AddRef ();
		}

		~NpcPlane ()
		{
			Release ();
		}

		public IntPtr ThisPtr { get { return thisptr; } }
	}

	public enum PlaneType
	{
		Misc = 0,
		Attack = 1,
		Walk = 2,
		Pick = 3,

		// WIP
		Patrol = 4,
		// WIP
		Courier = 5,
		CustomAI = 6
	}

	public static class Priorities
	{
		public const uint Misc = 10;
		public const uint Attack = 50;
		public const uint Walk = 20;
		public const uint Pick = 35;
		public const uint Patrol = 25;
		public const uint Courier = 30;
	}

	public static class NpcPlaneReason
	{
		public const int GoHome = 10;
		public const int FoundInEnemyStack = 11;
		public const int FromDialog = 12;
		public const int FromScript = 13;
		public const int RunAway = 14;

		public const int Success = 30;
		public const int HexTooFar = 31;
		public const int HexBusy = 32;
		public const int HexBusyRing = 33;
		public const int Deadlock = 34;
		public const int CantWalk = 38;
		public const int TargetDissapeared = 39;
		public const int GagCritter = 41;
		public const int GagItem = 42;
		public const int NoUnarmed = 43;

		public const int AttackTarget = 50;
		public const int AttackWeapon = 51;
		public const int AttackDistantion = 52;
		public const int AttackUseAim = 53;
	}

	public sealed class NpcPlaneArray : HandleArray<NpcPlane>
	{
		static readonly IntPtr type;

		public NpcPlaneArray ()
			: base (type)
		{

		}

		internal NpcPlaneArray (IntPtr ptr)
			: base (ptr, true)
		{
		}

		static NpcPlaneArray ()
		{
			type = ScriptArray.GetType ("array<NpcPlane@>");
		}

		public override NpcPlane FromNative (IntPtr ptr)
		{
			return new NpcPlane (GetObjectAddress (ptr));
		}
	}
}
