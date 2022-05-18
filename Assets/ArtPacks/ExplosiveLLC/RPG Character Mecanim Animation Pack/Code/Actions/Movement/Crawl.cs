namespace RPGCharacterAnims.Actions
{
    public class Crawl : MovementActionHandler<EmptyContext>
    {
        public Crawl(RPGCharacterMovementController movement) : base(movement)
        {
        }

        public override bool CanStartAction(RPGCharacterController controller)
        {
            return !IsActive();
        }

        protected override void _StartAction(RPGCharacterController controller, EmptyContext context)
        {
			controller.Crawl();
			movement.currentState = RPGCharacterState.Crawl;
		}

		public override bool IsActive()
        {
            return movement.currentState != null && (RPGCharacterState)movement.currentState == RPGCharacterState.Crawl;
        }
    }
}