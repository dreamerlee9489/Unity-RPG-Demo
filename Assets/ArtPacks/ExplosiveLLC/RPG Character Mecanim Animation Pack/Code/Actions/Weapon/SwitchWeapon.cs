using UnityEngine;

namespace RPGCharacterAnims.Actions
{
    public class SwitchWeaponContext
    {
        public string type;
        public string side;

        // "back" or "hips".
        public string sheathLocation;

        public int rightWeapon;
        public int leftWeapon;

        public SwitchWeaponContext()
        {
            this.type = "Instant";
            this.side = "None";
            this.sheathLocation = "Back";
            this.rightWeapon = (int)Weapon.Relax;
            this.leftWeapon = (int)Weapon.Relax;
        }

        public SwitchWeaponContext(string type, string side, string sheathLocation = "Back", int rightWeapon = -1, int leftWeapon = -1)
        {
            this.type = type;
            this.side = side;
            this.sheathLocation = sheathLocation;
            this.rightWeapon = rightWeapon;
            this.leftWeapon = leftWeapon;
        }

        public void LowercaseStrings()
        {
            type = type.ToLower();
            side = side.ToLower();
            sheathLocation = sheathLocation.ToLower();
        }
    }

    public class SwitchWeapon : BaseActionHandler<SwitchWeaponContext>
    {
        public override bool CanStartAction(RPGCharacterController controller)
        {
            return !IsActive() && !controller.isCasting;
        }

        public override bool CanEndAction(RPGCharacterController controller)
        {
            return IsActive();
        }

        protected override void _StartAction(RPGCharacterController controller, SwitchWeaponContext context)
        {
            RPGCharacterWeaponController weaponController = controller.GetComponent<RPGCharacterWeaponController>();

            if (weaponController == null) {
                EndAction(controller);
                return;
            }

            context.LowercaseStrings();

            bool changeRight = false;
            bool sheathRight = false;
            bool unsheathRight = false;
            int fromRight = controller.rightWeapon;
            int toRight = context.rightWeapon;

            bool changeLeft = false;
            bool sheathLeft = false;
            bool unsheathLeft = false;
            int fromLeft = controller.leftWeapon;
            int toLeft = context.leftWeapon;

            bool dualWielding = AnimationData.Is1HandedWeapon(fromRight) && AnimationData.Is1HandedWeapon(fromLeft);
            bool dualUnsheath = context.side == "dual";
            bool dualSheath = false;

            int toAnimatorWeapon = 0;

            // Filter which side is changing.
            switch (context.side) {
                case "none":
                case "right":
                    changeRight = true;
                    if (AnimationData.Is2HandedWeapon(toRight) && !AnimationData.IsNoWeapon(fromLeft)) {
                        changeLeft = true;
                        toLeft = (int)Weapon.Unarmed;
                        dualSheath = dualWielding;
                    }
                    break;
                case "left":
                    changeLeft = true;
                    if (AnimationData.Is2HandedWeapon(fromRight)) {
                        changeRight = true;
                        toRight = (int)Weapon.Unarmed;
                    }
                    break;
                case "dual":
                    changeLeft = true;
                    changeRight = true;
                    dualSheath = dualWielding;
                    break;
                case "both":
                    changeLeft = true;
                    changeRight = true;
                    break;
            }

            // Set sheath location.
            switch (context.sheathLocation) {
                case "back":
                    controller.animator.SetInteger("SheathLocation", 0);
                    break;
                case "hips":
                    controller.animator.SetInteger("SheathLocation", 1);
                    break;
            }

            // If relaxing, just use the Relax action.
            if (context.type == "relax") {
                controller.StartAction("Relax");
                return;
            }

            // Force unarmed if sheathing weapons.
            if (context.type == "sheath") {
                if (context.side == "left" || context.side == "dual" || context.side == "both") { toLeft = (int)Weapon.Unarmed;}
				else { toLeft = fromLeft; }
                if (context.side == "none" || context.side == "right" || context.side == "dual" || context.side == "both") { toRight = (int)Weapon.Unarmed; }
				else { toRight = fromRight; }
            }

            // Sheath weapons first if our starting weapon is different from our desired weapon and we're
            // not starting from an unarmed position.
            if (context.type == "sheath" || context.type == "switch") {
                sheathLeft = changeLeft && fromLeft != toLeft && !AnimationData.IsNoWeapon(fromLeft);
                sheathRight = changeRight && fromRight != toRight && !AnimationData.IsNoWeapon(fromRight);
                toAnimatorWeapon = AnimationData.ConvertToAnimatorWeapon(toLeft, toRight);
            }

            // Unsheath a weapon if our starting weapon is different from our desired weapon and we're
            // not ending on an unarmed position.
            if (context.type == "unsheath" || context.type == "switch") {
                unsheathLeft = changeLeft && fromLeft != toLeft && !AnimationData.IsNoWeapon(toLeft);
                unsheathRight = changeRight && fromRight != toRight && !AnimationData.IsNoWeapon(toRight);

                // If you're switching from the relaxed state, you can "unsheath" your fists.
                if (controller.isRelaxed && (toLeft == (int)Weapon.Unarmed || toRight == (int)Weapon.Unarmed)) {
                    fromLeft = (int)Weapon.Relax;
                    fromRight = (int)Weapon.Relax;
                    sheathLeft = false;
                    sheathRight = false;
                    unsheathLeft = false;
                    unsheathRight = true;
                    dualSheath = false;
                    dualUnsheath = false;
                }
            }

            ///
            /// Actually make changes to the weapon controller.
            ///

			// If Instant Switch.
            if (context.type == "instant") {
				Debug.Log("Instant Switch");
				if (changeLeft) { weaponController.InstantWeaponSwitch(toLeft); }
                else if (changeRight) { weaponController.InstantWeaponSwitch(toRight); }
            }
			// Normal, non-instant weapon switch.
			else {
                // Sheath weapons first if that's necessary.
                if (dualSheath && (sheathRight || sheathLeft)) {

					// Dual sheathing requires at most one call.
					Debug.Log("Dual Sheathing");
					weaponController.SheathWeapon(fromRight, toAnimatorWeapon, dualSheath);
                }
				else {
					Debug.Log("Sheath Left: " + fromLeft + " > " + toLeft);
					if (sheathLeft) { weaponController.SheathWeapon(fromLeft, toAnimatorWeapon, dualSheath); }

					Debug.Log("Sheath Right (dual: " + dualSheath + "): " + fromRight + " > " + toRight);
					if (sheathRight) { weaponController.SheathWeapon(fromRight, toAnimatorWeapon, dualSheath); }
                }

				//--------------------------------------

                // Finally, unsheath the desired weapons!
                if (dualUnsheath && (unsheathRight || unsheathLeft)) {
					Debug.Log("Dual Unsheathing");

                    // Dual unsheathing requires at most one call.
                    weaponController.UnsheathWeapon(toRight, dualUnsheath);
                }
				else {
					if (unsheathLeft) {
						Debug.Log("Unsheath Left: " + toLeft);
						weaponController.UnsheathWeapon(toLeft, dualUnsheath);
					}
					else if (unsheathRight) {
						Debug.Log("Unsheath Right (dual: " + dualUnsheath + "): " + toRight);
						weaponController.UnsheathWeapon(toRight, dualUnsheath);
					}
                }
            }

            // This callback will update the weapons in character controller after all other
            // coroutines finish.
            weaponController.AddCallback(() => {
                if (changeLeft) { controller.leftWeapon = toLeft; }
                if (changeRight) { controller.rightWeapon = toRight; }

                // Turn off the isWeaponSwitching flag and sync weapon object visibility.
                weaponController.SyncWeaponVisibility();
                controller.EndAction("Relax");
                EndAction(controller);
            });
        }

        protected override void _EndAction(RPGCharacterController controller)
        {
        }
    }
}