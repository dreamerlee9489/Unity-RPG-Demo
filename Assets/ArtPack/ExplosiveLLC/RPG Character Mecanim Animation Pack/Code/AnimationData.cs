using UnityEngine;

namespace RPGCharacterAnims
{
    public enum AttackSide
    {
        None = 0,
        Left = 1,
        Right = 2,
        Dual = 3,
    }

    public enum Weapon
    {
        Relax = -1,
        Unarmed = 0,
        TwoHandSword = 1,
        TwoHandSpear = 2,
        TwoHandAxe = 3,
        TwoHandBow = 4,
        TwoHandCrossbow = 5,
        TwoHandStaff = 6,
        Shield = 7,
        LeftSword = 8,
        RightSword = 9,
        LeftMace = 10,
        RightMace = 11,
        LeftDagger = 12,
        RightDagger = 13,
        LeftItem = 14,
        RightItem = 15,
        LeftPistol = 16,
        RightPistol = 17,
        Rifle = 18,
        RightSpear = 19,
    }

    /// <summary>
    /// Enum to use with the "Weapon" parameter of the animator. To convert from a Weapon number,
    /// use AnimationData.ConvertToAnimatorWeapon.
    ///
    /// Two-handed weapons have a 1:1 relationship with this enum, but all one-handed weapons use
    /// ARMED.
    /// </summary>
    public enum AnimatorWeapon
    {
        UNARMED = 0,
        TWOHANDSWORD = 1,
        TWOHANDSPEAR = 2,
        TWOHANDAXE = 3,
        TWOHANDBOW = 4,
        TWOHANDCROSSBOW = 5,
        STAFF = 6,
        ARMED = 7,
        RELAX = 8,
        RIFLE = 9,
        SHIELD = 11,
        ARMEDSHIELD = 12
    }

    /// <summary>
    /// Enum to use with the "TriggerNumber" parameter of the animator. Convert to (int) to set.
    /// </summary>
    public enum AnimatorTrigger
    {
        NoTrigger = 0,
        IdleTrigger = 1,
        ActionTrigger = 2,
        ClimbLadderTrigger = 3,
        AttackTrigger = 4,
        AttackKickTrigger = 5,
        AttackDualTrigger = 6,
        AttackCastTrigger = 7,
        SpecialAttackTrigger = 8,
        SpecialEndTrigger = 9,
        CastTrigger = 10,
        CastEndTrigger = 11,
        GetHitTrigger = 12,
        RollTrigger = 13,
        TurnTrigger = 14,
        WeaponSheathTrigger = 15,
        WeaponUnsheathTrigger = 16,
        DodgeTrigger = 17,
        JumpTrigger = 18,
        BlockTrigger = 19,
        DeathTrigger = 20,
        ReviveTrigger = 21,
        BlockBreakTrigger = 22,
        SwimTrigger = 23,
        ReloadTrigger = 24,
        InstantSwitchTrigger = 25,
        KnockbackTrigger = 26,
        KnockdownTrigger = 27,
        DiveRollTrigger = 28,
		CrawlTrigger = 29
    }

    /// <summary>
    /// Static class which contains hardcoded animation constants and helper functions.
    /// </summary>
    public class AnimationData
    {
        /// <summary>
        /// Converts left and right-hand weapon numbers into the legacy weapon number usable by the
        /// animator's "Weapon" parameter.
        /// </summary>
        /// <param name="leftWeapon">Left-hand weapon.</param>
        /// <param name="rightWeapon">Right-hand weapon.</param>
        public static int ConvertToAnimatorWeapon(int leftWeapon, int rightWeapon)
        {
			if (Is2HandedWeapon(rightWeapon)) { return rightWeapon; }                               // 2-handed weapon.
			else if (IsNoWeapon(rightWeapon) && IsNoWeapon(leftWeapon)) { return rightWeapon; }     // Unarmed or Relax.
			else { return ( int )AnimatorWeapon.ARMED; }                                            // Armed.
		}

        public static int ConvertToAnimatorLeftRight(int leftWeapon, int rightWeapon)
        {
            if (Is2HandedWeapon(rightWeapon)) { return (int)AttackSide.None; }
			else if (leftWeapon > -1 && rightWeapon > -1) { return (int)AttackSide.Dual; }
			else if (leftWeapon > 0 && rightWeapon < 1) { return (int)AttackSide.Left; }
			else if (leftWeapon < 1 && rightWeapon > 0) { return (int)AttackSide.Right; }
            return -1;
        }

        /// <summary>
        /// Returns true if the weapon number is Unarmed or a placeholder for the Relax state (i.e. -1).
        /// </summary>
        /// <param name="weaponNumber">Weapon number to test.</param>
        public static bool IsNoWeapon(int weaponNumber)
        {
            return weaponNumber < 1;
        }

        /// <summary>
        /// Returns true if the weapon number is a weapon held in the left hand.
        /// </summary>
        /// <param name="weaponNumber">Weapon number to test.</param>
        public static bool IsLeftWeapon(int weaponNumber)
        {
            //weaponNumber 7 = Shield
            //weaponNumber 8 = L Sword
            //weaponNumber 10 = L Mace
            //weaponNumber 12 = L Dagger
            //weaponNumber 14 = L Item
            //weaponNumber 16 = L Pistol
            return weaponNumber == (int)Weapon.Shield
				|| weaponNumber == (int)Weapon.LeftSword
				|| weaponNumber == (int)Weapon.LeftMace
				|| weaponNumber == (int)Weapon.LeftDagger
				|| weaponNumber == (int)Weapon.LeftItem
				|| weaponNumber == (int)Weapon.LeftPistol;
        }

        /// <summary>
        /// Returns true if the weapon number is a weapon held in the right hand.
        /// </summary>
        /// <param name="weaponNumber">Weapon number to test.</param>
        public static bool IsRightWeapon(int weaponNumber)
        {
            //weaponNumber 9 = R Sword
            //weaponNumber 11 = R Mace
            //weaponNumber 13 = R Dagger
            //weaponNumber 15 = R Item
            //weaponNumber 17 = R Pistol
            //weaponNumber 19 = Right Spear
            return weaponNumber == (int)Weapon.RightSword
				|| weaponNumber == (int)Weapon.RightMace
				|| weaponNumber == (int)Weapon.RightDagger
				|| weaponNumber == (int)Weapon.RightItem
				|| weaponNumber == (int)Weapon.RightPistol
				|| weaponNumber == (int)Weapon.RightSpear;
        }

        /// <summary>
        /// Returns true if the weapon number is a 1-handed weapon.
        /// </summary>
        /// <param name="weaponNumber">Weapon number to test.</param>
        public static bool Is1HandedWeapon(int weaponNumber)
        {
            //weaponNumber 7 = Shield
            //weaponNumber 8 = L Sword
            //weaponNumber 9 = R Sword
            //weaponNumber 10 = L Mace
            //weaponNumber 11 = R Mace
            //weaponNumber 12 = L Dagger
            //weaponNumber 13 = R Dagger
            //weaponNumber 14 = L Item
            //weaponNumber 15 = R Item
            //weaponNumber 16 = L Pistol
            //weaponNumber 17 = R Pistol
            //weaponNumber 19 = Right Spear
            return IsLeftWeapon(weaponNumber)
				|| IsRightWeapon(weaponNumber);
        }

        /// <summary>
        /// Returns true if the weapon number is a 2-handed weapon.
        /// </summary>
        /// <param name="weaponNumber">Weapon number to test.</param>
        public static bool Is2HandedWeapon(int weaponNumber)
        {
            //weaponNumber 1 = 2H Sword
            //weaponNumber 2 = 2H Spear
            //weaponNumber 3 = 2H Axe
            //weaponNumber 4 = 2H Bow
            //weaponNumber 5 = 2H Crossbow
            //weaponNumber 6 = 2H Staff
            //weaponNumber 18 = Rifle
            return weaponNumber == (int)Weapon.TwoHandSword
				|| weaponNumber == (int)Weapon.TwoHandSpear
				|| weaponNumber == (int)Weapon.TwoHandAxe
				||  weaponNumber == (int)Weapon.TwoHandBow
				|| weaponNumber == (int)Weapon.TwoHandCrossbow
				|| weaponNumber == (int)Weapon.TwoHandStaff
				|| weaponNumber == (int)Weapon.Rifle;
        }

		/// <summary>
		/// Returns true if the weapon number can use IKHands.
		/// </summary>
		/// <param name="weaponNumber">Weapon number to test.</param>
		public static bool IsIKWeapon(int weaponNumber)
		{
			//weaponNumber 1 = 2H Sword
			//weaponNumber 2 = 2H Spear
			//weaponNumber 3 = 2H Axe
			//weaponNumber 5 = 2H Crossbow
			//weaponNumber 18 = Rifle
			return weaponNumber == ( int )Weapon.TwoHandSword
				|| weaponNumber == ( int )Weapon.TwoHandSpear
				|| weaponNumber == ( int )Weapon.TwoHandAxe
				|| weaponNumber == ( int )Weapon.TwoHandCrossbow
				|| weaponNumber == ( int )Weapon.Rifle;
		}

		/// <summary>
		/// Returns true if the weapon number can use casting actions.
		/// </summary>
		/// <param name="weaponNumber">Weapon number to test.</param>
		public static bool IsCastableWeapon(int weaponNumber)
		{
			//weaponNumber 0 = Unarmed
			//weaponNumber 7 = Armed, ArmedShield
			//weaponNumber 6 = 2H Staff
			return weaponNumber == ( int )AnimatorWeapon.UNARMED
				|| weaponNumber == ( int )AnimatorWeapon.ARMED
				|| weaponNumber == ( int )AnimatorWeapon.ARMEDSHIELD
				|| weaponNumber == ( int )AnimatorWeapon.STAFF;
		}

		/// <summary>
		/// Returns the duration of an attack animation. Use side 0 (none) for two-handed weapons.
		/// </summary>
		/// <param name="attackSide">Side of the attack: 0- None, 1- Left, 2- Right, 3- Dual.</param>
		/// <param name="weaponNumber">Weapon that's attacking.</param>
		/// <param name="attackNumber">Attack animation number.</param>
		/// <returns>Duration in seconds of attack animation.</returns>
		public static float AttackDuration(int attackSide, int weaponNumber, int attackNumber)
        {
            float duration = 1f;

            switch (attackSide) {
                case 0:										// Unspecified (2-Handed Weapons)
                    switch (weaponNumber) {
                        case 1: duration = 1.1f; break;     // 2H Sword
                        case 2: duration = 1.1f; break;     // 2H Spear
                        case 3: duration = 1.5f; break;     // 2H Axe
                        case 4: duration = 0.75f; break;    // 2H Bow
                        case 5: duration = 0.75f; break;    // 2H Crossbow
                        case 6: duration = 1f; break;       // 2H Staff
                        case 18: duration = 1.1f; break;    // Rifle
                        default:
                            Debug.LogError("RPG Character: no weapon number " + weaponNumber + " for Side 0");
                            break;
                    }
                    break;

                case 1:										// Left Side
                    switch (weaponNumber) {
                        case 0: duration = 0.75f; break;    // Unarmed  (1-3)
                        case 7: duration = 1.1f; break;     // Shield   (1-1)
                        case 8: duration = 0.75f; break;    // L Sword  (1-7)
                        case 10: duration = 0.75f; break;   // L Mace   (1-3)
                        case 12: duration = 1f; break;      // L Dagger (1-3)
                        case 14: duration = 1f; break;      // L Item   (1-4)
                        case 16: duration = 0.75f; break;   // L Pistol (1-3)
                        default:
                            Debug.LogError("RPG Character: no weapon number " + weaponNumber + " for Side 1 (Left)");
                            break;
                    }
                    break;
                case 2:										// Right Side
                    switch (weaponNumber) {
                        case 0: duration = 0.75f; break;    // Unarmed  (4-6)
                        case 9: duration = 0.75f; break;    // R Sword  (8-14)
                        case 11: duration = 0.75f; break;   // R Mace   (4-6)
                        case 13: duration = 1f; break;      // R Dagger (4-6)
                        case 15: duration = 1f; break;      // R Item   (5-8)
                        case 17: duration = 0.75f; break;   // R Pistol (4-6)
                        case 19: duration = 0.75f; break;   // R Spear  (1-7)
                        default:
                            Debug.LogError("RPG Character: no weapon number " + weaponNumber + " for Side 2 (Right)");
                            break;
                    }
                    break;
                case 3: duration = 0.75f; break;			// Dual Attacks (1-3)
            }

            return duration;
        }

        /// <summary>
        /// Returns the duration of the weapon sheath animation.
        /// </summary>
        /// <param name="attackSide">Side of the attack: 0- None, 1- Left, 2- Right, 3- Dual.</param>
        /// <param name="weaponNumber">Weapon being sheathed.</param>
        /// <returns>Duration in seconds of sheath animation.</returns>
        public static float SheathDuration(int attackSide, int weaponNumber)
        {
            float duration = 1f;

            if (IsNoWeapon(weaponNumber)) {duration = 0f; }
			else if (Is2HandedWeapon(weaponNumber)) { duration = 1.2f; }
			else if (attackSide == 3) { duration = 1f; }
			else { duration = 1.05f; }

            return duration;
        }

        /// <summary>
        /// Returns a random attack number usable as the animator's Action parameter.
        /// </summary>
        /// <param name="attackSide">Side of the attack: 0- None, 1- Left, 2- Right, 3- Dual.</param>
        /// <param name="weaponNumber">Weapon attacking.</param>
        /// <returns>Attack animation number.</returns>
        public static int RandomAttackNumber(int attackSide, int weaponNumber)
        {
            int offset = 1;
            int numAttacks = 3;

            switch (attackSide) {
                case 0:									// Unspecified (2-Handed Weapons)
                    switch (weaponNumber) {
                        case 1: numAttacks = 10; break; // 2H Sword     (1-10)
                        case 2: numAttacks = 9; break;  // 2H Spear     (1-9)
                        case 3: numAttacks = 5; break;  // 2H Axe       (1-5)
                        case 4: numAttacks = 5; break;  // 2H Bow       (1-5)
                        case 5: numAttacks = 5; break;  // 2H Crossbow  (1-5)
                        case 6: numAttacks = 5; break;  // 2H Staff     (1-5)
                        case 18: break;                 // Rifle        (???)
                        default:
                            Debug.LogError("RPG Character: no weapon number " + weaponNumber + " for Side 0");
                            break;
                    }
                    break;

                case 1:									// Left Side
                    switch (weaponNumber) {
                        case 0: break;                  // Unarmed  (1-3)
                        case 7: numAttacks = 1; break;  // Shield   (1-1)
                        case 8: numAttacks = 7; break;  // L Sword  (1-7)
                        case 10: break;                 // L Mace   (1-3)
                        case 12: break;                 // L Dagger (1-3)
                        case 14: numAttacks = 4; break; // L Item   (1-4)
                        case 16: break;                 // L Pistol (1-3)
                        default:
                            Debug.LogError("RPG Character: no weapon number " + weaponNumber + " for Side 1 (Left)");
                            break;
                    }
                    break;
                case 2:												// Right Side
                    switch (weaponNumber) {
                        case 0: offset = 4; break;                  // Unarmed  (4-6)
                        case 9: offset = 8; numAttacks = 7; break;  // R Sword  (8-14)
                        case 11: offset = 4; break;                 // R Mace   (4-6)
                        case 13: offset = 4; break;                 // R Dagger (4-6)
                        case 15: offset = 5; numAttacks = 4; break; // R Item   (5-8)
                        case 17: offset = 4; break;                 // R Pistol (4-6)
                        case 19: numAttacks = 7; break;             // R Spear  (1-7)
                        default:
                            Debug.LogError("RPG Character: no weapon number " + weaponNumber + " for Side 2 (Right)");
                            break;
                    }
                    break;
                case 3: break;										// Dual Attacks (1-3)
            }

            return Random.Range(offset, numAttacks + offset);
        }

        /// <summary>
        /// Returns the number of a random kick animation.
        /// </summary>
        /// <param name="attackSide">Side of the kick: 1- Left, 2- Right.</param>
        /// <returns>Kick animation number.</returns>
        public static int RandomKickNumber(int attackSide)
        {
            int offset = 1;
            int numAttacks = 2;

            switch (attackSide) {
                case 1:					// Left Side Kicks (1-2)
                    break;
                case 2:					// Right Side Kicks (3-4)
                    offset = 3;
                    break;
            }

            return Random.Range(offset, numAttacks + offset);
        }

        /// <summary>
        /// Returns the number of a random spellcasting animation.
        /// </summary>
        /// <param name="castType">Type of cast being performed: ("Cast" | "Buff" | "AOE" | "Summon").</param>
        /// <returns>Cast animation number.</returns>
        public static int RandomCastNumber(string castType)
        {
            int offset = 1;
            int numAttacks = 3;

            switch (castType) {
                case "Cast":			// Regular Casting (1-3)
                    break;
                case "Buff":			// Buffs (1-2)
                    numAttacks = 2;
                    break;
                case "AOE":				// AOE (3-4)
                    offset = 3;
                    numAttacks = 2;
                    break;
                case "Summon":			// Summon (5-6)
                    offset = 5;
                    numAttacks = 2;
                    break;
            }

            return Random.Range(offset, numAttacks + offset);
        }

        /// <summary>
        /// Returns the number of a random conversation animation.
        /// </summary>
        public static int RandomConversationNumber()
        {
            return Random.Range(1, 9);
        }

        /// <summary>
        /// Returns the number of a random hit animation.
        /// </summary>
        /// <param name="hitType">Type of hit taken: ("Hit" | "BlockHit" | "Knockback" | "Knockdown")</param>
        public static int RandomHitNumber(string hitType)
        {
            int hits = 5;

            switch (hitType) {
                case "Hit":
                    hits = 5;			// Regular hits (1-5)
                    break;
                case "BlockHit":
                    hits = 2;			// Blocked hits (1-2)
                    break;
                case "Knockback":
                    hits = 3;			// Knockback hits (1-3)
                    break;
                case "Knockdown":
                    hits = 1;			// Knockdown hits (1)
                    break;
            }

            return Random.Range(1, hits + 1);
        }

        /// <summary>
        /// Returns the relative direction of knockback force for a hit animation.
        /// </summary>
        /// <param name="hitType">Type of hit taken: ("Hit" | "BlockHit" | "Knockback" | "Knockdown").</param>
        /// <param name="hitNumber">Hit animation number.</param>
        /// <returns>Direction of hit. (relative to character)</returns>
        public static Vector3 HitDirection(string hitType, int hitNumber)
        {
            switch (hitType) {
                case "Hit":
                    switch (hitNumber) {
                        case 1:
                        case 2:
                            return Vector3.back;
                        case 3:
                            return Vector3.forward;
                        case 4:
                            return Vector3.right;
                        case 5:
                            return Vector3.left;
                    }
                    break;
                case "BlockHit":
                    return Vector3.back;
                case "Knockback":
                    switch (hitNumber) {
                        case 1:
                        case 2:
                            return Vector3.back;
                        case 3:
                            return Vector3.forward;
                    }
                    break;
                case "Knockdown":
                    return Vector3.back;
            }

            return Vector3.back;
        }
    }
}