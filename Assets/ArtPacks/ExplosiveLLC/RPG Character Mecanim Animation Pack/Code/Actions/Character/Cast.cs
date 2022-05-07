namespace RPGCharacterAnims.Actions
{
    public class CastContext
    {
        public string type;
        public int side;
        public int number;

        public CastContext(string type, int side, int number = -1)
        {
            this.type = type;
            this.side = side;
            this.number = number;
        }

        public CastContext(string type, string side, int number = -1)
        {
            this.type = type;
            this.number = number;
            switch (side.ToLower()) {
                case "none":
                    this.side = (int)AttackSide.None;
                    break;
                case "left":
                    this.side = (int)AttackSide.Left;
                    break;
                case "right":
                    this.side = (int)AttackSide.Right;
                    break;
                case "dual":
                    this.side = (int)AttackSide.Dual;
                    break;
            }
        }
    }

    public class Cast : BaseActionHandler<CastContext>
    {
        public override bool CanStartAction(RPGCharacterController controller)
        {
            return !controller.isRelaxed && !active && controller.maintainingGround && controller.canAction;
        }

        public override bool CanEndAction(RPGCharacterController controller)
        {
            return controller.isCasting;
        }

        protected override void _StartAction(RPGCharacterController controller, CastContext context)
        {
            int number = context.number;
            if (number == -1) { number = AnimationData.RandomCastNumber(context.type); }
            controller.StartCast(number, context.side, context.type);
        }

        protected override void _EndAction(RPGCharacterController controller)
        {
            controller.EndCast();
        }
    }
}