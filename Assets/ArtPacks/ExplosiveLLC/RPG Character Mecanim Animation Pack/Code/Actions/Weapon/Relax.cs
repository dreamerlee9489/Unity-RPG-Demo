namespace RPGCharacterAnims.Actions
{
    public class Relax : BaseActionHandler<bool?>
    {
        public override bool CanStartAction(RPGCharacterController controller)
        {
            return !IsActive();
        }

        public override bool CanEndAction(RPGCharacterController controller)
        {
            return IsActive();
        }

        protected override void _StartAction(RPGCharacterController controller, bool? instant)
        {
            bool useInstant = instant.HasValue && instant.Value == true;

            RPGCharacterWeaponController weaponController = controller.GetComponent<RPGCharacterWeaponController>();
            if (weaponController == null) {
                EndAction(controller);
                return;
            }

            // If a weapon is equipped, we must sheathe it.
            if (controller.leftWeapon != (int)Weapon.Relax || controller.rightWeapon != (int)Weapon.Relax) {
                if (useInstant) {
                    weaponController.InstantWeaponSwitch((int)Weapon.Relax);
                }
				else { weaponController.SheathWeapon(controller.rightWeapon, (int)Weapon.Relax, true); }
                weaponController.AddCallback(() => {
                    controller.leftWeapon = (int)Weapon.Relax;
                    controller.rightWeapon = (int)Weapon.Relax;
                    weaponController.SyncWeaponVisibility();
                });
            }
        }

        protected override void _EndAction(RPGCharacterController controller)
        {
            // If switching directly from the relaxed state, switch to unarmed.
            if (controller.leftWeapon == (int)Weapon.Relax || controller.rightWeapon == (int)Weapon.Relax) {
                controller.StartAction("SwitchWeapon", new SwitchWeaponContext("Unsheath", "Dual", "Hips", (int)Weapon.Unarmed, (int)Weapon.Unarmed));
            }
        }
    }
}