namespace RPGCharacterAnims.Actions
{
    public class EmoteCombat : InstantActionHandler<string>
    {
        public override bool CanStartAction(RPGCharacterController controller)
        {
            return controller.canAction && !controller.isRelaxed;
        }

        protected override void _StartAction(RPGCharacterController controller, string context)
        {
            switch (context) {
                case "Pickup":
                    controller.Pickup();
                    break;
                case "Activate":
                    controller.Activate();
                    break;
                case "Boost":
                    controller.Boost();
                    break;
            }
        }
    }
}