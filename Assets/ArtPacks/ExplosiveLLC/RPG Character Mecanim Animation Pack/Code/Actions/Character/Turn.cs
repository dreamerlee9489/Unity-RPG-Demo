// Turn Left - 1
// Turn Right - 2

namespace RPGCharacterAnims.Actions
{
    public class Turn : InstantActionHandler<int>
    {
        public override bool CanStartAction(RPGCharacterController controller)
        {
			return controller.canMove;
        }

        protected override void _StartAction(RPGCharacterController controller, int context)
        {
            controller.Turn(context);
        }
    }
}