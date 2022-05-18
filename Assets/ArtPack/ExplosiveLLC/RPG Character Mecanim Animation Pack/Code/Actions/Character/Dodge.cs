// Dodge Left - 1
// Dodge Right - 2

namespace RPGCharacterAnims.Actions
{
    public class Dodge : InstantActionHandler<int>
    {
        public override bool CanStartAction(RPGCharacterController controller)
        {
			return controller.canAction && !controller.IsActive("Relax");
		}

        protected override void _StartAction(RPGCharacterController controller, int context)
        {
            controller.GetAngry();
            controller.Dodge(context);
        }
    }
}