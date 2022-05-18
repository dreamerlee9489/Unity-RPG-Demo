namespace RPGCharacterAnims.Actions
{
    public class DoubleJump : MovementActionHandler<EmptyContext>
    {
        public DoubleJump(RPGCharacterMovementController movement) : base(movement)
        {
        }

        public override bool CanStartAction(RPGCharacterController controller)
        {
            return controller.isFalling && movement.canDoubleJump && !controller.isBlocking;
        }

        protected override void _StartAction(RPGCharacterController controller, EmptyContext context)
        {
            movement.currentState = RPGCharacterState.DoubleJump;
        }

        public override bool IsActive()
        {
            return movement.currentState != null && (RPGCharacterState)movement.currentState == RPGCharacterState.DoubleJump;
        }
    }
}