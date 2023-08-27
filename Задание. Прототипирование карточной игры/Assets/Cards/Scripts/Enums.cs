using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cards
{
	public enum CardUnitType : byte
	{
		None = 0,
		Murloc = 1,
		Beast = 2,
		Elemental = 3,
		Mech = 4
	}

	public enum SideType : byte
	{
		Common = 0,
		Mage = 1,
		Warrior = 2,
		Hunter = 3,
		Priest = 4
	}

	public enum CardStateType : byte
    {
		InDeck,
		InHand,
		OnTable,
		Discard
    }

	public enum StatType : byte
    {
		MurlocHaveBonusAttack = 0,
		DealDamage = 1,
		RestoreHealth = 2,
		DestroyWeapon = 3,
		BonusDamage = 4,
		GiveMinionBonuses = 5,
		GainAttack = 6,
		SimpleAttack = 7
	}

	public enum PlayerType: byte
    {
		Player1,
		Player2
    }

}
