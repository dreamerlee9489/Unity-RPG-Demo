namespace RPGCharacterAnims.Actions
{
    public class DiveRoll : MovementActionHandler<int>
    {
        public DiveRoll(RPGCharacterMovementController movement) : base(movement)
        {
        }

        public override bool CanStartAction(RPGCharacterController controller)
        {
            return controller.canAction;
        }

        protected override void _StartAction(RPGCharacterController controller, int context)
        {
            controller.DiveRoll(context);
            movement.currentState = RPGCharacterState.DiveRoll;
		}

        public override bool IsActive()
        {
            return movement.currentState != null && (RPGCharacterState)movement.currentState == RPGCharacterState.DiveRoll;
        }
    }
}