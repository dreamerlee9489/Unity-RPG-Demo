using UnityEngine;

namespace RPGCharacterAnims.Actions
{
    public class ClimbLadder : MovementActionHandler<EmptyContext>
    {
        public ClimbLadder(RPGCharacterMovementController movement) : base(movement)
        {
        }

        public override bool CanStartAction(RPGCharacterController controller)
        {
            return !IsActive() && controller.isNearLadder;
        }

        protected override void _StartAction(RPGCharacterController controller, EmptyContext context)
        {
            Collider ladder = controller.ladder;
            SuperCharacterController superCharacterController = movement.GetComponent<SuperCharacterController>();

            float threshold = 1f;
            Vector3 ladderTop = new Vector3(ladder.transform.position.x, ladder.bounds.max.y, ladder.transform.position.z);
            Vector3 ladderBottom = new Vector3(ladder.transform.position.x, ladder.bounds.min.y, ladder.transform.position.z);
            float distanceFromTop = (controller.transform.position - ladderTop).magnitude;
            float distanceFromBottom = (controller.transform.position - ladderBottom).magnitude;

            // If the top of the ladder is below the character's head, climb onto the top of the ladder.
            if (distanceFromTop < distanceFromBottom && distanceFromTop < threshold) {
                movement.ClimbLadder(false);
                controller.ClimbLadder(5);
                movement.currentState = RPGCharacterState.ClimbLadder;
            }
			else if (distanceFromBottom < distanceFromTop && distanceFromBottom < threshold) {
                movement.ClimbLadder(true);
                controller.ClimbLadder(6);
                movement.currentState = RPGCharacterState.ClimbLadder;
            }
        }

        public override bool IsActive()
        {
            return movement.currentState != null && (RPGCharacterState)movement.currentState == RPGCharacterState.ClimbLadder;
        }
    }
}