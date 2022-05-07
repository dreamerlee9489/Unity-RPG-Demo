using System;
using UnityEngine;
using RPGCharacterAnims.Actions;

namespace RPGCharacterAnims
{
    public class GUIControls : MonoBehaviour
    {
        private RPGCharacterController rpgCharacterController;
        private RPGCharacterWeaponController rpgCharacterWeaponController;
        private float charge = 0f;
        private bool useHips;
        private bool useDual;
        private bool useInstant;
        private bool hipsToggle;
        private bool dualToggle;
        private bool instantToggle;
        private bool crouchToggle;
        private bool isTalking;
        private bool isAiming;
        private bool hipShooting;
        private bool useNavigation;
        private float swimTimeout = 0;
        private Vector3 jumpInput;
        public GameObject nav;

        private void Start()
        {
            // Get other RPG Character components.
            rpgCharacterController = GetComponent<RPGCharacterController>();
            rpgCharacterWeaponController = GetComponent<RPGCharacterWeaponController>();
        }

        private void OnGUI()
        {
            // Character is not dead.
            if (!rpgCharacterController.isDead) {

                // Character not climbing or swimming.
                if (!rpgCharacterController.isClimbing && !rpgCharacterController.isSwimming) {

                    // Character is on the ground.
                    if (rpgCharacterController.maintainingGround) {
                        Blocking();

                        // Character is not Blocking.
                        if (!rpgCharacterController.isBlocking) {

							// Character is not Casting.
							if (!rpgCharacterController.isCasting) {
								Crouching();
								Sprinting();
								Charging();
								Navigation();
								Emotes();
								WeaponSwitching();
								Attacks();
								Damage();
								RollDodgeTurn();
							}
                            Casting();
                        }
                    }
                    Jumping();
                }
                Swimming();
                Climbing();
            }
            Misc();
        }

        private void Sprinting()
        {
			// Check to make sure Sprint Action exists.
			if (!rpgCharacterController.HandlerExists("Sprint")) { return; }

			if (rpgCharacterController.hasNoWeapon) {
				bool useSprint = GUI.Toggle(new Rect(640, 115, 100, 30), rpgCharacterController.isSprinting, "Sprint");
				if (useSprint)
					if (rpgCharacterController.CanStartAction("Sprint")) { rpgCharacterController.StartAction("Sprint"); }
				else if (!useSprint) {
					if (rpgCharacterController.CanEndAction("Sprint")) { rpgCharacterController.EndAction("Sprint"); }
				}
			}
        }

        private void Charging()
        {
            if (rpgCharacterController.hasShield) {
                GUI.Button(new Rect(620, 140, 100, 30), "Charge");
                charge = GUI.HorizontalSlider(new Rect(620, 180, 100, 30), charge, 0.0F, 1f);
                rpgCharacterController.animator.SetFloat("Charge", charge);
            }
        }

        private void Navigation()
        {
			// Check to make sure Navigation Action exists.
            if (!rpgCharacterController.HandlerExists("Navigation")) { return; }

            useNavigation = GUI.Toggle(new Rect(550, 105, 100, 30), useNavigation, "Navigation");

            if (useNavigation) {
                nav.transform.GetChild(0).GetComponent<MeshRenderer>().enabled = true;
                nav.transform.GetChild(0).GetChild(0).GetComponent<MeshRenderer>().enabled = true;
                RaycastHit hit;
                if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100)) {
                    nav.transform.position = hit.point;
                    if (Input.GetMouseButtonDown(0)) { rpgCharacterController.StartAction("Navigation", hit.point); }
                }
            }
			else {
                if (rpgCharacterController.CanEndAction("Navigation")) {
                    nav.transform.GetChild(0).GetComponent<MeshRenderer>().enabled = false;
                    nav.transform.GetChild(0).GetChild(0).GetComponent<MeshRenderer>().enabled = false;
                    rpgCharacterController.EndAction("Navigation");
                }
            }
        }

        private void Attacks()
        {
			// Check if Attack Action exists.
			if (!rpgCharacterController.HandlerExists("Attack")) { return; }

			if (rpgCharacterController.CanEndAction("Attack") && rpgCharacterController.isSpecial) {
                if (GUI.Button(new Rect(235, 85, 100, 30), "End Special")) { rpgCharacterController.EndAction("Attack"); }
            }

            if (!rpgCharacterController.CanStartAction("Attack")) { return; }

            if (rpgCharacterController.hasLeftWeapon || rpgCharacterController.leftWeapon == (int)Weapon.Unarmed) {
                if (GUI.Button(new Rect(25, 85, 100, 30), "Attack L")) {
                    rpgCharacterController.StartAction("Attack", new Actions.AttackContext("Attack", "Left"));
                }
            }
            if (rpgCharacterController.hasRightWeapon || rpgCharacterController.rightWeapon == (int)Weapon.Unarmed) {
                if (GUI.Button(new Rect(130, 85, 100, 30), "Attack R")) {
                    rpgCharacterController.StartAction("Attack", new Actions.AttackContext("Attack", "Right"));
                }
            }
            if (rpgCharacterController.hasTwoHandedWeapon) {
                if (GUI.Button(new Rect(130, 85, 100, 30), "Attack")) {
                    rpgCharacterController.StartAction("Attack", new Actions.AttackContext("Attack", "None"));
                }
            }
            if (rpgCharacterController.hasDualWeapons) {

                // Can't Dual Attack with Item weapons.
                if (rpgCharacterController.rightWeapon != 14 && rpgCharacterController.leftWeapon != 15) {
                    if (GUI.Button(new Rect(235, 85, 100, 30), "Attack Dual")) {
                        rpgCharacterController.StartAction("Attack", new Actions.AttackContext("Attack", "Dual"));
                    }
                }
            }
            //Special Attack.
            if (rpgCharacterController.hasTwoHandedWeapon) {
                if (GUI.Button(new Rect(335, 85, 100, 30), "Special Attack1")) {
                    rpgCharacterController.StartAction("Attack", new Actions.AttackContext("Special", "None"));
                }
            }
            // Sword + Mace Special Attack.
            if ((rpgCharacterController.leftWeapon == 8 || rpgCharacterController.leftWeapon == 10)
                && (rpgCharacterController.rightWeapon == 9 || rpgCharacterController.rightWeapon == 11)) {
                if (GUI.Button(new Rect(335, 85, 100, 30), "Special Attack1")) {
                    rpgCharacterController.StartAction("Attack", new Actions.AttackContext("Special", "Right"));
                }
            }
            // Kicking.
            if (GUI.Button(new Rect(25, 115, 100, 30), "Left Kick")) { rpgCharacterController.StartAction("Attack", new Actions.AttackContext("Kick", "Left", 1)); }
            if (GUI.Button(new Rect(25, 145, 100, 30), "Left Kick2")) { rpgCharacterController.StartAction("Attack", new Actions.AttackContext("Kick", "Left", 2)); }
            if (GUI.Button(new Rect(130, 115, 100, 30), "Right Kick")) { rpgCharacterController.StartAction("Attack", new Actions.AttackContext("Kick", "Right", 3)); }
            if (GUI.Button(new Rect(130, 145, 100, 30), "Right Kick2")) { rpgCharacterController.StartAction("Attack", new Actions.AttackContext("Kick", "Right", 4)); }
        }

        private void Damage()
        {
			// Check if Get Hit Action exists.
			if (rpgCharacterController.HandlerExists("GetHit")) {
				if (GUI.Button(new Rect(30, 240, 100, 30), "Get Hit")) { rpgCharacterController.StartAction("GetHit", new HitContext()); }
			}
			// Check if Knockback Action exists.
			if (rpgCharacterController.HandlerExists("Knockback")) {
				if (GUI.Button(new Rect(130, 240, 100, 30), "Knockback1")) { rpgCharacterController.StartAction("Knockback", new HitContext(1, Vector3.back)); }
				if (GUI.Button(new Rect(230, 240, 100, 30), "Knockback2")) { rpgCharacterController.StartAction("Knockback", new HitContext(2, Vector3.back)); }
			}
			// Check if Knockdown Action exists.
			if (rpgCharacterController.HandlerExists("Knockdown")) {
				if (GUI.Button(new Rect(130, 270, 100, 30), "Knockdown")) { rpgCharacterController.StartAction("Knockdown", new HitContext(1, Vector3.back)); }
			}
        }

        private void Crouching()
        {
			// Check if Crouch Action exists.
			if (rpgCharacterController.HandlerExists("Crouch")) {
				bool useCrouch = GUI.Toggle(new Rect(640, 95, 100, 30), rpgCharacterController.isCrouching, "Crouch");
				if (useCrouch && rpgCharacterController.CanStartAction("Crouch")) { rpgCharacterController.StartAction("Crouch"); } else if (!useCrouch && rpgCharacterController.CanEndAction("Crouch")) { rpgCharacterController.EndAction("Crouch"); }

				// Check if Crawl Action exists.
				if (!rpgCharacterController.HandlerExists("Crawl")) { return; }

				bool useCrawl = rpgCharacterController.isCrawling;
				if (useCrouch) {
					if (GUI.Button(new Rect(640, 140, 100, 30), "Crawl")) {
						rpgCharacterController.Crawl();
						if (rpgCharacterController.CanStartAction("Crawl")) { rpgCharacterController.StartAction("Crawl"); }
						if (rpgCharacterController.CanEndAction("Crouch")) { rpgCharacterController.EndAction("Crouch"); }
					}
				}
				if (useCrawl) {
					if (GUI.Button(new Rect(640, 140, 100, 30), "Crawl")) {
						rpgCharacterController.StartAction("Idle");
						if (rpgCharacterController.CanStartAction("Crouch")) { rpgCharacterController.StartAction("Crouch"); }
					}
				}
			}
        }

        private void Blocking()
        {
			// Check if Block Action exists.
			if (!rpgCharacterController.HandlerExists("Block")) { return; }

			if (!rpgCharacterController.isCasting && !rpgCharacterController.isSitting && !rpgCharacterController.IsActive("Relax")) {
                bool blockGui = GUI.Toggle(new Rect(25, 215, 100, 30), rpgCharacterController.isBlocking, "Block");

                if (blockGui && rpgCharacterController.CanStartAction("Block")) { rpgCharacterController.StartAction("Block"); }
				else if (!blockGui && rpgCharacterController.CanEndAction("Block")) { rpgCharacterController.EndAction("Block"); }

                if (blockGui) {

					// Check if Get Hit Action exists.
					if (!rpgCharacterController.HandlerExists("Get Hit")) { return; }

					if (GUI.Button(new Rect(30, 240, 100, 30), "Get Hit")) { rpgCharacterController.StartAction("GetHit", new HitContext()); }
                }
            }
        }

		private void RollDodgeTurn()
		{
			// Check if Roll Action exists.
			if (rpgCharacterController.HandlerExists("Block")) {
				if (rpgCharacterController.CanStartAction("Roll")) {
					if (GUI.Button(new Rect(25, 15, 100, 30), "Roll Forward")) { rpgCharacterController.StartAction("Roll", 1); }
					if (GUI.Button(new Rect(130, 15, 100, 30), "Roll Backward")) { rpgCharacterController.StartAction("Roll", 3); }
					if (GUI.Button(new Rect(25, 45, 100, 30), "Roll Left")) { rpgCharacterController.StartAction("Roll", 4); }
					if (GUI.Button(new Rect(130, 45, 100, 30), "Roll Right")) { rpgCharacterController.StartAction("Roll", 2); }
				}
			}
			// Check if Dodge Action exists.
			if (rpgCharacterController.HandlerExists("Dodge")) {
				if (rpgCharacterController.CanStartAction("Dodge")) {
					if (GUI.Button(new Rect(235, 15, 100, 30), "Dodge Left")) { rpgCharacterController.StartAction("Dodge", 1); }
					if (GUI.Button(new Rect(235, 45, 100, 30), "Dodge Right")) { rpgCharacterController.StartAction("Dodge", 2); }
				}
			}
			// Check if Turn Action exists.
			if (rpgCharacterController.HandlerExists("Turn")) {
				if (rpgCharacterController.CanStartAction("Turn")) {
					if (GUI.Button(new Rect(340, 15, 100, 30), "Turn Left")) { rpgCharacterController.StartAction("Turn", 1); }
					if (GUI.Button(new Rect(340, 45, 100, 30), "Turn Right")) { rpgCharacterController.StartAction("Turn", 2); }
					if (GUI.Button(new Rect(445, 15, 100, 30), "Turn Left 180")) { rpgCharacterController.StartAction("Turn", 3); }
					if (GUI.Button(new Rect(445, 45, 100, 30), "Turn Right 180")) { rpgCharacterController.StartAction("Turn", 4); }
				}
			}
			// Check if DiveRoll Action exists.
			if (rpgCharacterController.HandlerExists("DiveRoll")) {
				if (rpgCharacterController.CanStartAction("DiveRoll")) {
					if (GUI.Button(new Rect(445, 75, 100, 30), "Dive Roll")) { rpgCharacterController.StartAction("DiveRoll", 1); }
				}
			}
		}

        private void Casting()
        {
			// Check if Cast Action exists.
			if (!rpgCharacterController.HandlerExists("Cast")) { return; }

			if (rpgCharacterController.CanEndAction("Cast") && rpgCharacterController.isCasting) {
                if (GUI.Button(new Rect(25, 330, 100, 30), "Stop Casting")) { rpgCharacterController.EndAction("Cast"); }
            }

            if (!rpgCharacterController.CanStartAction("Cast")) { return; }

            bool leftUnarmed = rpgCharacterController.leftWeapon == (int)Weapon.Unarmed;
            bool rightUnarmed = rpgCharacterController.rightWeapon == (int)Weapon.Unarmed;
            bool wieldStaff = rpgCharacterController.rightWeapon == (int)Weapon.TwoHandStaff;

            if (leftUnarmed && GUI.Button(new Rect(25, 330, 100, 30), "Cast Atk Left")) {
                rpgCharacterController.StartAction("Cast", new Actions.CastContext("Cast", "Left"));
            }
            if (rightUnarmed && GUI.Button(new Rect(125, 330, 100, 30), "Cast Atk Right")) {
                rpgCharacterController.StartAction("Cast", new Actions.CastContext("Cast", "Right"));
            }
            if (leftUnarmed && rightUnarmed && GUI.Button(new Rect(80, 365, 100, 30), "Cast Atk Dual")) {
                rpgCharacterController.StartAction("Cast", new Actions.CastContext("Cast", "Dual"));
            }
            if (!rpgCharacterController.hasDualWeapons
				&& (AnimationData.IsCastableWeapon(rpgCharacterController.leftWeapon) || AnimationData.IsCastableWeapon(rpgCharacterController.rightWeapon))) {
                if (GUI.Button(new Rect(25, 425, 100, 30), "Cast AOE")) { rpgCharacterController.StartAction("Cast", new Actions.CastContext("AOE", "Dual")); }
                if (GUI.Button(new Rect(25, 400, 100, 30), "Cast Buff")) { rpgCharacterController.StartAction("Cast", new Actions.CastContext("Buff", "Dual")); }
                if (GUI.Button(new Rect(25, 450, 100, 30), "Cast Summon")) { rpgCharacterController.StartAction("Cast", new Actions.CastContext("Summon", "Dual")); }
            }
        }

        private void Jumping()
        {
			// Check if Jump Action exists.
			if (!rpgCharacterController.HandlerExists("Jump")) { return; }

			if (rpgCharacterController.CanStartAction("Jump")) {
                if (GUI.Button(new Rect(25, 175, 100, 30), "Jump")) {
                    rpgCharacterController.SetJumpInput(Vector3.up);
                    rpgCharacterController.StartAction("Jump");
                }
            }
            if (rpgCharacterController.CanStartAction("DoubleJump")) {
                if (GUI.Button(new Rect(25, 175, 100, 30), "Jump Flip")) {
                    rpgCharacterController.SetJumpInput(Vector3.up);
                    rpgCharacterController.StartAction("DoubleJump");
                }
            }
        }

        private void Emotes()
        {
			// Check if Emote Action exists.
			if (rpgCharacterController.HandlerExists("Emote")) {
				if (rpgCharacterController.CanStartAction("Emote")) {
					string emote = "";
					if (GUI.Button(new Rect(665, 680, 100, 30), "Sleep")) { emote = "Sleep"; }
					if (GUI.Button(new Rect(770, 680, 100, 30), "Sit")) { emote = "Sit"; }
					if (GUI.Button(new Rect(770, 650, 100, 30), "Drink")) { emote = "Drink"; }
					if (GUI.Button(new Rect(665, 650, 100, 30), "Bow")) { emote = "Bow"; }
					if (GUI.Button(new Rect(560, 650, 100, 30), "Yes")) { emote = "Yes"; }
					if (GUI.Button(new Rect(455, 650, 100, 30), "No")) { emote = "No"; }
					if (GUI.Button(new Rect(560, 680, 100, 30), "Start Talking")) { emote = "Talk"; }

					if (emote != "") { rpgCharacterController.StartAction("Emote", emote); }
				}
				if (rpgCharacterController.CanEndAction("Emote")) {
					if (rpgCharacterController.isSitting) {
						if (GUI.Button(new Rect(795, 680, 100, 30), "Stand")) { rpgCharacterController.EndAction("Emote"); }
					}
					if (rpgCharacterController.isTalking) {
						if (GUI.Button(new Rect(795, 680, 100, 30), "Stop Talking")) { rpgCharacterController.EndAction("Emote"); }
					}
				}
			}

			// Check if Emote Combat Action exists.
			if (rpgCharacterController.HandlerExists("EmoteCombat")) {
				if (rpgCharacterController.CanStartAction("EmoteCombat")) {
					string emote = "";
					if (GUI.Button(new Rect(130, 175, 100, 30), "Pickup")) { emote = "Pickup"; }
					if (GUI.Button(new Rect(235, 175, 100, 30), "Activate")) { emote = "Activate"; }
					if (GUI.Button(new Rect(480, 650, 100, 30), "Boost")) { emote = "Boost"; }

					if (emote != "") { rpgCharacterController.StartAction("EmoteCombat", emote); }
				}
			}
        }

        private void Climbing()
        {
			// Check if Climb Ladder  Action exists.
			if (rpgCharacterController.HandlerExists("ClimbLadder")) {
				if (rpgCharacterController.CanStartAction("ClimbLadder")) {
					if (GUI.Button(new Rect(640, 360, 100, 30), "Climb Ladder")) { rpgCharacterController.StartAction("ClimbLadder"); }
				}
			}
        }

        private void Swimming()
        {
            if (rpgCharacterController.isSwimming) {
                float swimTime = 0.5f;

                if (GUI.Button(new Rect(25, 175, 100, 30), "Swim Up")) {
                    swimTimeout = Time.time + swimTime;
                    jumpInput = Vector3.up;

                    // Override the jump input for a half second to simulate a button press.
                    RPGCharacterInputController inputController = rpgCharacterController.GetComponent<RPGCharacterInputController>();
                    if (inputController != null) { inputController.PauseInput(swimTime); }
                }
                if (GUI.Button(new Rect(25, 225, 100, 30), "Swim Down")) {
                    swimTimeout = Time.time + swimTime;
                    jumpInput = Vector3.down;

                    // Override the jump input for a half second to simulate a button press.
                    RPGCharacterInputController inputController = rpgCharacterController.GetComponent<RPGCharacterInputController>();
                    if (inputController != null) { inputController.PauseInput(swimTime); }
                }

                if (Time.time < swimTimeout) { rpgCharacterController.SetJumpInput(jumpInput); }
            }
        }

        // Death / Debug.
        private void Misc()
        {
            string deathReviveLabel = rpgCharacterController.isDead ? "Revive" : "Death";
            if (!rpgCharacterController.isClimbing && !rpgCharacterController.isCasting && !rpgCharacterController.isSitting && rpgCharacterController.maintainingGround) {

				// Check if Climb Ladder  Action exists.
				if (rpgCharacterController.HandlerExists("Death")) {
					if (GUI.Button(new Rect(30, 270, 100, 30), deathReviveLabel)) {
						if (rpgCharacterController.CanStartAction("Death")) { rpgCharacterController.StartAction("Death"); } else if (rpgCharacterController.CanEndAction("Death")) { rpgCharacterController.EndAction("Death"); }
					}
				}
            }
            // Debug.
            if (GUI.Button(new Rect(600, 20, 120, 30), "Debug Controller")) { rpgCharacterController.ControllerDebug(); }
            if (GUI.Button(new Rect(600, 50, 120, 30), "Debug Animator")) { rpgCharacterController.AnimatorDebug(); }
        }


		private void WeaponSwitching()
		{
			// Check if SwitchWeapon Action exists.
			if (!rpgCharacterController.HandlerExists("SwitchWeapon")) { return; }

			bool doSwitch = false;
			SwitchWeaponContext context = new SwitchWeaponContext();

			// Check if Relax Action exists.
			if (rpgCharacterController.HandlerExists("Relax")) {
				if (!rpgCharacterController.isRelaxed) {
					if (GUI.Button(new Rect(1115, 240, 100, 30), "Relax")) {
						if (useInstant) { rpgCharacterController.StartAction("Relax", true); }
						else { rpgCharacterController.StartAction("Relax"); }
					}
				}
			}
			if (rpgCharacterController.rightWeapon != ( int )Weapon.Unarmed || rpgCharacterController.leftWeapon != ( int )Weapon.Unarmed) {
				if (GUI.Button(new Rect(1115, 280, 100, 30), "Unarmed")) {
					doSwitch = true;
					context.type = "Switch";
					context.side = "Dual";
					context.leftWeapon = ( int )Weapon.Unarmed;
					context.rightWeapon = ( int )Weapon.Unarmed;
				}
			}

			// Two-handed weapons.
			Weapon[] weapons = new Weapon[] {
				Weapon.TwoHandSword,
				Weapon.TwoHandSpear,
				Weapon.TwoHandAxe,
				Weapon.TwoHandStaff,
				Weapon.TwoHandBow,
				Weapon.TwoHandCrossbow,
				Weapon.Rifle,
			};

			int offset = 310;

			foreach (Weapon weapon in weapons) {
				if (rpgCharacterController.rightWeapon != ( int )weapon) {
					string label = weapon.ToString();
					if (label.StartsWith("TwoHand")) { label = label.Replace("TwoHand", "2H "); }
					if (GUI.Button(new Rect(1115, offset, 100, 30), label)) {
						doSwitch = true;
						context.type = "Switch";
						context.side = "None";
						context.leftWeapon = -1;
						context.rightWeapon = ( int )weapon;
					}
				}
				offset += 30;
			}

			// Left/Right weapon pairs.
			Tuple<Weapon, Weapon>[] leftRightPairs = new Tuple<Weapon, Weapon>[] {
				Tuple.Create(Weapon.LeftSword, Weapon.RightSword),
				Tuple.Create(Weapon.LeftMace, Weapon.RightMace),
				Tuple.Create(Weapon.LeftDagger, Weapon.RightDagger),
				Tuple.Create(Weapon.LeftItem, Weapon.RightItem),
				Tuple.Create(Weapon.LeftPistol, Weapon.RightPistol),
				Tuple.Create(Weapon.Shield, Weapon.RightSpear),
			};

			offset = 530;

			foreach (Tuple<Weapon, Weapon> pair in leftRightPairs) {
				bool missingOneSide = false;

				// Left weapons.
				if (rpgCharacterController.leftWeapon != ( int )pair.Item1) {
					missingOneSide = true;
					if (GUI.Button(new Rect(1065, offset, 100, 30), pair.Item1.ToString())) {
						doSwitch = true;
						context.type = "Switch";
						context.side = "Left";
						context.leftWeapon = ( int )pair.Item1;
						context.rightWeapon = -1;
					}
				}
				// Right weapons.
				if (rpgCharacterController.rightWeapon != ( int )pair.Item2) {
					missingOneSide = true;
					if (GUI.Button(new Rect(1165, offset, 100, 30), pair.Item2.ToString())) {
						doSwitch = true;
						context.type = "Switch";
						context.side = "Right";
						context.leftWeapon = -1;
						context.rightWeapon = ( int )pair.Item2;
					}
				}
				// If at least one side isn't carrying this weapon, show the Dual switch.
				if (missingOneSide) {
					string label = pair.Item1.ToString();
					if (!label.Contains("Shield")) {
						label = label.Replace("Left", "Dual ") + "s";
						if (GUI.Button(new Rect(965, offset, 100, 30), label)) {
							doSwitch = true;
							context.type = "Switch";
							context.side = "Dual";
							context.leftWeapon = ( int )pair.Item1;
							context.rightWeapon = ( int )pair.Item2;
						}
					}
				}

				offset += 30;
			}
			if (rpgCharacterController.leftWeapon > ( int )Weapon.Unarmed) {
				if (GUI.Button(new Rect(750, offset - 150, 100, 30), "Sheath Left")) {
					doSwitch = true;
					context.type = "Sheath";
					context.side = "Left";
					context.leftWeapon = ( int )Weapon.Unarmed;
					context.rightWeapon = -1;
				}
			}
			if (rpgCharacterController.rightWeapon > ( int )Weapon.Unarmed) {
				if (GUI.Button(new Rect(850, offset - 150, 100, 30), "Sheath Right")) {
					doSwitch = true;
					context.type = "Sheath";
					context.side = "Right";
					context.leftWeapon = -1;
					context.rightWeapon = ( int )Weapon.Unarmed;
				}
			}

			offset += 30;

			if (GUI.Button(new Rect(965, 680, 100, 30), "Shuffle")) {
				int lefty = ( int )leftRightPairs[UnityEngine.Random.Range(0, leftRightPairs.Length)].Item1;
				int righty = ( int )leftRightPairs[UnityEngine.Random.Range(0, leftRightPairs.Length)].Item2;

				doSwitch = true;
				context.type = "Switch";
				context.side = "Both";
				context.leftWeapon = lefty;
				context.rightWeapon = righty;
			}
			// Check if HipShoot Action exists.
			if (rpgCharacterController.HandlerExists("HipShoot")) {
				hipShooting = GUI.Toggle(new Rect(1000, 495, 100, 30), hipShooting, "Hip Shooting");
				if (hipShooting) {
					if (rpgCharacterController.CanStartAction("HipShoot")) { rpgCharacterController.StartAction("HipShoot"); }
					else {
						if (rpgCharacterController.CanEndAction("HipShoot")) { rpgCharacterController.EndAction("HipShoot"); }
					}
				}
			}

			// Sheath/Unsheath Hips.
			useHips = GUI.Toggle(new Rect(1000, 260, 100, 30), useHips, "Hips");
			if (useHips) { context.sheathLocation = "Hips"; } else { context.sheathLocation = "Back"; }

			// Instant weapon toggle.
			useInstant = GUI.Toggle(new Rect(1000, 310, 100, 30), useInstant, "Instant");
			if (useInstant) { context.type = "Instant"; }

			// Perform the weapon switch.
			if (doSwitch) {
				if (rpgCharacterController.CanStartAction("SwitchWeapon")) { rpgCharacterController.StartAction("SwitchWeapon", context); }
			}
		}
	}
}