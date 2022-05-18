namespace RPGCharacterAnims.Actions
{
    public class Reload : InstantActionHandler<EmptyContext>
    {
        public override bool CanStartAction(RPGCharacterController controller)
        {
            return !controller.isRelaxed &&
                   (controller.rightWeapon == (int)Weapon.TwoHandCrossbow ||
                    controller.rightWeapon == (int)Weapon.Rifle ||
                    controller.rightWeapon == (int)Weapon.RightPistol ||
                    controller.leftWeapon == (int)Weapon.LeftPistol);
        }

        protected override void _StartAction(RPGCharacterController controller, EmptyContext context)
        {
            controller.Reload();
        }
    }
}