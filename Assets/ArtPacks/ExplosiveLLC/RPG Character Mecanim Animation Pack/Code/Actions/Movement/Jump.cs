namespace RPGCharacterAnims.Actions
{
    public class Jump : MovementActionHandler<EmptyContext>
    {
        public Jump(RPGCharacterMovementController movement) : base(movement)
        {
        }

        public override bool CanStartAction(RPGCharacterController controller)
        {
            return (movement.canJump || movement.canDoubleJump) &&
				!controller.isBlocking &&
				controller.maintainingGround &&
				controller.canAction;
        }

        protected override void _StartAction(RPGCharacterController controller, EmptyContext context)
        {
            controller.GetAngry();
            movement.currentState = RPGCharacterState.Jump;
        }

        public override bool IsActive()
        {
            return movement.currentState != null && (RPGCharacterState)movement.currentState == RPGCharacterState.Jump;
        }
    }
}