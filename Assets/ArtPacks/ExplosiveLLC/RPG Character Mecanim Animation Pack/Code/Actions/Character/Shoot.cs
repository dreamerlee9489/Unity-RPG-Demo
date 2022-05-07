namespace RPGCharacterAnims.Actions
{
    public class Shoot : InstantActionHandler<EmptyContext>
    {
        public override bool CanStartAction(RPGCharacterController controller)
        {
            return controller.isAiming;
        }

        protected override void _StartAction(RPGCharacterController controller, EmptyContext context)
        {
            int attackNumber = 1;
            if (controller.rightWeapon == (int)Weapon.Rifle && controller.isHipShooting) { attackNumber = 2; }
            controller.Shoot(attackNumber);
        }
    }
}