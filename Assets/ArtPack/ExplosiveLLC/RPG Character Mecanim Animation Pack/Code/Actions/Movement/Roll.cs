// Roll Forward - 1
// Roll Right - 2
// Roll Backward - 3
// Roll Left - 4

namespace RPGCharacterAnims.Actions
{
    public class Roll : MovementActionHandler<int>
    {
        public Roll(RPGCharacterMovementController movement) : base(movement)
        {
        }

        public override bool CanStartAction(RPGCharacterController controller)
        {
			return controller.canAction && !controller.IsActive("Relax");
        }

        protected override void _StartAction(RPGCharacterController controller, int context)
        {
            controller.Roll(context);
            movement.currentState = RPGCharacterState.Roll;
		}

        public override bool IsActive()
        {
            return movement.currentState != null && (RPGCharacterState)movement.currentState == RPGCharacterState.Roll;
        }
    }
}