namespace RPGCharacterAnims.Actions
{
    public class Swim : MovementActionHandler<EmptyContext>
    {
        public Swim(RPGCharacterMovementController movement) : base(movement)
        {
        }

        public override bool CanStartAction(RPGCharacterController controller)
        {
            return !IsActive();
        }

        protected override void _StartAction(RPGCharacterController controller, EmptyContext context)
        {
            movement.currentState = RPGCharacterState.Swim;
			controller.SetIKOff();
		}

        public override bool IsActive()
        {
            return movement.currentState != null && (RPGCharacterState)movement.currentState == RPGCharacterState.Swim;
        }
    }
}