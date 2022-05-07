namespace RPGCharacterAnims.Actions
{
    public class Emote : BaseActionHandler<string>
    {
        public override bool CanStartAction(RPGCharacterController controller)
        {
            return controller.canAction && controller.isRelaxed && !controller.isSitting && !controller.isTalking && !active;
        }

        public override bool CanEndAction(RPGCharacterController controller)
        {
            return active;
        }

        protected override void _StartAction(RPGCharacterController controller, string context)
        {
            switch (context) {

                // Sit, Sleep and Talk all stay "on", until turned off.
                case "Sit":
                    controller.isSitting = true;
                    controller.Sit();
                    break;
                case "Sleep":
                    controller.isSitting = true;
                    controller.Sleep();
                    break;
                case "Talk":
                    controller.isTalking = true;
                    controller.StartConversation();
                    break;

                // Drink, Bow, Yes, and No run once and exit immediately.
                case "Drink":
                    controller.Drink();
                    EndAction(controller);
                    break;
                case "Bow":
                    controller.Bow();
                    EndAction(controller);
                    break;
                case "Yes":
                    controller.Yes();
                    EndAction(controller);
                    break;
                case "No":
                    controller.No();
                    EndAction(controller);
                    break;
            }
        }

        protected override void _EndAction(RPGCharacterController controller)
        {
            if (controller.isSitting) {
                controller.isSitting = false;
                controller.Stand();
            }
            if (controller.isTalking) {
                controller.isTalking = false;
                controller.EndConversation();
            }
        }
    }
}