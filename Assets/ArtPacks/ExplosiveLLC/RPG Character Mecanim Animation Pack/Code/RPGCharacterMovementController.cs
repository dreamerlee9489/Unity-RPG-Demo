using System.Collections;
using UnityEngine;

namespace RPGCharacterAnims
{
    public enum RPGCharacterState
    {
        Idle = 0,
        Move = 1,
        Jump = 2,
        DoubleJump = 3,
        Fall = 4,
        Swim = 5,
        Block = 6,
        ClimbLadder = 7,
        Roll = 8,
        Knockback = 9,
        Knockdown = 10,
        DiveRoll = 11,
		Crawl = 12
    }

    public class RPGCharacterMovementController : SuperStateMachine
    {
        // Components.
        private SuperCharacterController superCharacterController;
        private RPGCharacterController rpgCharacterController;
        private Rigidbody rb;
        private Animator animator;
        private CapsuleCollider capCollider;

		// Variables.

		/// <summary>
		/// Returns whether the character can face.
		/// </summary>
		public bool acquiringGround { get { return superCharacterController.currentGround.IsGrounded(false, 0.01f); } }

		/// <summary>
		/// Returns whether the character can face.
		/// </summary>
		public bool maintainingGround { get { return superCharacterController.currentGround.IsGrounded(true, 0.5f); } }

        [HideInInspector] public Vector3 lookDirection { get; private set; }

		[Header("Knockback")]
        /// <summary>
        /// Multiplies the amount of knockback force a character recieves when they get hit.
        /// </summary>
        public float knockbackMultiplier = 1f;

		[Header("Movement Multiplier")]
        /// <summary>
        /// Multiplies the speed of animation velocity.
        /// </summary>
        public float movementAnimationMultiplier = 1f;

        /// <summary>
        /// If the character entered the ladder on the bottom.
        /// </summary>
        private bool ladderStartBottom;

        /// <summary>
        /// Vector3 movement velocity.
        /// </summary>
        [HideInInspector] public Vector3 currentVelocity;

        [Header("Movement")]
        /// <summary>
        /// Movement speed while walking and strafing.
        /// </summary>
        public float walkSpeed = .5f;

        /// <summary>
        /// Walking acceleration.
        /// </summary>
        public float walkAccel = 15f;

        /// <summary>
        /// Movement speed while running. (the default movement)
        /// </summary>
        public float runSpeed = 1f;

        /// <summary>
        /// Running acceleration.
        /// </summary>
        public float runAccel = 30f;

        /// <summary>
        /// Movement speed while sprinting.
        /// </summary>
        public float sprintSpeed = 2.5f;

        /// <summary>
        /// Sprinting acceleration.
        /// </summary>
        public float sprintAccel = 15;

		/// <summary>
		/// Movement speed while crawling.
		/// </summary>
		public float crawlSpeed = 0.15f;

		/// <summary>
		/// Crawling acceleration.
		/// </summary>
		public float crawlAccel = 15;

		/// <summary>
		/// Movement speed while injured.
		/// </summary>
		public float injuredSpeed = .675f;

        /// <summary>
        /// Acceleration while injured.
        /// </summary>
        public float injuredAccel = 20f;

        /// <summary>
        /// Ground friction, slows the character to a stop.
        /// </summary>
        public float groundFriction = 120f;

        /// <summary>
        /// Speed of rotation when turning the character to face movement direction or target.
        /// </summary>
        public float rotationSpeed = 100f;

        /// <summary>
        /// Determine is the character is sprinting or not.
        /// </summary>
        private bool isSprinting;

        /// <summary>
        /// Internal flag for when the character can jump.
        /// </summary>
        [HideInInspector] public bool canJump;

        /// <summary>
        /// Internal flag for if the player is holding the jump input. If this is released while
        /// the character is still ascending, the vertical speed is damped.
        /// </summary>
        [HideInInspector] public bool holdingJump;

        /// <summary>
        /// Internal flag for if the character can perform a double jump.
        /// </summary>
        [HideInInspector] public bool canDoubleJump = false;
        private bool doublejumped = false;

        [Header("Jumping")]
        /// <summary>
        /// Jumping speed while ascending.
        /// </summary>
        public float jumpSpeed = 12f;

        /// <summary>
        /// Gravity while ascending.
        /// </summary>
        public float jumpGravity = 24f;

        /// <summary>
        /// Double jump speed.
        /// </summary>
        public float doubleJumpSpeed = 8f;

        /// <summary>
        /// Horizontal speed while in the air.
        /// </summary>
        public float inAirSpeed = 8f;

        /// <summary>
        /// Horizontal acceleration while in the air.
        /// </summary>
        public float inAirAccel = 16f;

		/// <summary>
		/// Gravity while descending. Default is higher than ascending gravity (like a Mario jump).
		/// </summary>
		public float fallGravity = 32f;

		/// <summary>
		/// Allows control while character is falling.
		/// </summary>
		public bool fallingControl = false;

        [Header("Swimming")]
        /// <summary>
        /// Horizontal swim speed.
        /// </summary>
        public float swimSpeed = 4f;

        /// <summary>
        /// Swimming acceleration.
        /// </summary>
        public float swimAccel = 4f;

        /// <summary>
        /// Vertical swim speed.
        /// </summary>
        public float strokeSpeed = 6f;

        /// <summary>
        /// Friction in water which slows the character to a stop.
        /// </summary>
        public float waterFriction = 5f;

        private void Awake()
        {
            rpgCharacterController = GetComponent<RPGCharacterController>();
            rpgCharacterController.SetHandler("AcquiringGround", new Actions.SimpleActionHandler(() => { }, () => { }));
            rpgCharacterController.SetHandler("MaintainingGround", new Actions.SimpleActionHandler(() => { }, () => { }));
            rpgCharacterController.SetHandler("ClimbLadder", new Actions.ClimbLadder(this));
            rpgCharacterController.SetHandler("DiveRoll", new Actions.DiveRoll(this));
            rpgCharacterController.SetHandler("DoubleJump", new Actions.DoubleJump(this));
            rpgCharacterController.SetHandler("Fall", new Actions.Fall(this));
            rpgCharacterController.SetHandler("GetHit", new Actions.GetHit(this));
            rpgCharacterController.SetHandler("Idle", new Actions.Idle(this));
            rpgCharacterController.SetHandler("Jump", new Actions.Jump(this));
            rpgCharacterController.SetHandler("Knockback", new Actions.Knockback(this));
            rpgCharacterController.SetHandler("Knockdown", new Actions.Knockdown(this));
            rpgCharacterController.SetHandler("Move", new Actions.Move(this));
			rpgCharacterController.SetHandler("Roll", new Actions.Roll(this));
			rpgCharacterController.SetHandler("Swim", new Actions.Swim(this));
			rpgCharacterController.SetHandler("Crawl", new Actions.Crawl(this));
		}

        private void Start()
        {
            // Get other RPG Character components.
            superCharacterController = GetComponent<SuperCharacterController>();

            // Check if Animator exists, otherwise pause script.
            animator = GetComponentInChildren<Animator>();
			if (animator == null) {
				Debug.LogError("ERROR: THERE IS NO ANIMATOR COMPONENT ON CHILD OF CHARACTER.");
				Debug.Break();
			}
			// Setup Collider and Rigidbody for collisions.
			capCollider = GetComponent<CapsuleCollider>();
            rb = GetComponent<Rigidbody>();

            // Set restraints on startup if using Rigidbody.
            if (rb != null) { rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ; }
            rpgCharacterController.OnLockMovement += LockMovement;
            rpgCharacterController.OnUnlockMovement += UnlockMovement;
            RPGCharacterAnimatorEvents animatorEvents = rpgCharacterController.GetAnimatorTarget().GetComponent<RPGCharacterAnimatorEvents>();
            animatorEvents.OnMove.AddListener(AnimatorMove);
        }

        #region Updates

        /*
		Update is normally run once on every frame update. We won't be using it in this case, since the SuperCharacterController
        component sends a callback Update called SuperUpdate. SuperUpdate is recieved by the SuperStateMachine, and then fires
        further callbacks depending on the state.

        If SuperCharacterController is disabled then we still want the SuperStateMachine to run, so we call SuperUpdate manually.
        */

        void Update()
        {
            if (!superCharacterController.enabled) { gameObject.SendMessage("SuperUpdate", SendMessageOptions.DontRequireReceiver); }
        }

        // Put any code in here you want to run BEFORE the state's update function. This is run regardless of what state you're in.
        protected override void EarlyGlobalSuperUpdate()
        {
            bool acquiringGround = superCharacterController.currentGround.IsGrounded(false, 0.01f);
            bool maintainingGround = superCharacterController.currentGround.IsGrounded(true, 0.5f);

            if (acquiringGround) { rpgCharacterController.StartAction("AcquiringGround"); }
			else { rpgCharacterController.EndAction("AcquiringGround"); }

            if (maintainingGround) {  rpgCharacterController.StartAction("MaintainingGround"); }
			else { rpgCharacterController.EndAction("MaintainingGround"); }
        }

        // Put any code in here you want to run AFTER the state's update function.  This is run regardless of what state you're in.
        protected override void LateGlobalSuperUpdate()
        {
            // If the movement controller itself is disabled, this shouldn't run.
            if (!enabled) { return; }

            // Move the player by our velocity every frame.
            transform.position += currentVelocity * superCharacterController.deltaTime;

            // If alive and is moving, set animator.
            if (!rpgCharacterController.isDead && rpgCharacterController.canMove) {
                if (currentVelocity.magnitude > 0f) {
                    animator.SetFloat("Velocity X", 0);
                    animator.SetFloat("Velocity Z", transform.InverseTransformDirection(currentVelocity).z * movementAnimationMultiplier);
                    animator.SetBool("Moving", true);
                }
				else {
                    animator.SetFloat("Velocity X", 0f);
                    animator.SetFloat("Velocity Z", 0f);
                    animator.SetBool("Moving", false);
                }
            }
			// Aiming.
			if (rpgCharacterController.isAiming || rpgCharacterController.isStrafing) { RotateTowardsTarget(rpgCharacterController.aimInput); }

			// Facing.
			else if (rpgCharacterController.isFacing) { RotateTowardsDirection(rpgCharacterController.faceInput); }
			else if (rpgCharacterController.canMove) { RotateTowardsMovementDir(); }

            if (currentState == null && rpgCharacterController.CanStartAction("Idle")) { rpgCharacterController.StartAction("Idle"); }

			// Update animator with local movement values.
			animator.SetFloat("Velocity X", transform.InverseTransformDirection(currentVelocity).x * movementAnimationMultiplier);
			animator.SetFloat("Velocity Z", transform.InverseTransformDirection(currentVelocity).z * movementAnimationMultiplier);
		}

        #endregion

        /// <summary>
        /// Rotates the character to be head up compared to gravity.
        /// </summary>
        /// <param name="up">Up direction. (i.e. Vector3.up)</param>
        public void RotateGravity(Vector3 up)
        {
            lookDirection = Quaternion.FromToRotation(transform.up, up) * lookDirection;
        }

        #region States
        // Below are the state functions. Each one is called based on the name of the state, so when currentState = Idle,
        // we call Idle_EnterState. If currentState = Jump, we call Jump_SuperUpdate().

        private void Idle_EnterState()
        {
            superCharacterController.EnableSlopeLimit();
            superCharacterController.EnableClamping();
            canJump = true;
            doublejumped = false;
            canDoubleJump = false;
        }

        // Run every frame character is in the idle state.
        private void Idle_SuperUpdate()
        {
			// Check if the character starts falling.
			if (rpgCharacterController.CanStartAction("Fall")) {
				rpgCharacterController.StartAction("Fall");
				return;
			}

			// Apply friction to slow to a halt.
			currentVelocity = Vector3.MoveTowards(currentVelocity, Vector3.zero, groundFriction * superCharacterController.deltaTime);

			if (rpgCharacterController.CanStartAction("Move")) { rpgCharacterController.StartAction("Move"); }
        }

        // Run every frame character is moving.
        private void Move_SuperUpdate()
        {
			// Check if the character starts falling.
			if (rpgCharacterController.CanStartAction("Fall")) {
                rpgCharacterController.StartAction("Fall");
                return;
            }
            // Set speed determined by movement type.
            if (rpgCharacterController.canMove) {
                float moveSpeed = runSpeed;
                float moveAccel = runAccel;

				if (rpgCharacterController.isInjured) {
                    moveSpeed = injuredSpeed;
                    moveAccel = injuredAccel;
                }
				else if (rpgCharacterController.isStrafing) {
                    moveSpeed = walkSpeed;
                    moveAccel = walkAccel;
                }
				else if (rpgCharacterController.isSprinting) {
                    moveSpeed = sprintSpeed;
                    moveAccel = sprintAccel;
                }

                currentVelocity = Vector3.MoveTowards(currentVelocity,
					rpgCharacterController.cameraRelativeInput * moveSpeed,
					moveAccel * superCharacterController.deltaTime);
			}
			// If there is no movement detected, go into Idle.
            if (rpgCharacterController.CanStartAction("Idle")) {  rpgCharacterController.StartAction("Idle"); }
        }

        private void Jump_EnterState()
        {
            superCharacterController.DisableClamping();
            superCharacterController.DisableSlopeLimit();

            if ((RPGCharacterState)lastState == RPGCharacterState.Swim) {
				currentVelocity = new Vector3(currentVelocity.x, strokeSpeed, currentVelocity.z); }
			else { currentVelocity = new Vector3(currentVelocity.x, jumpSpeed, currentVelocity.z); }
            animator.SetInteger("Jumping", 1);
            rpgCharacterController.SetAnimatorTrigger(AnimatorTrigger.JumpTrigger);
            canJump = false;
        }

        private void Jump_SuperUpdate()
        {
            holdingJump = rpgCharacterController.jumpInput.y != 0f;

            // Cap jump speed if we stop holding the jump button.
            if (!holdingJump && currentVelocity.y > (jumpSpeed / 4f)) {
                currentVelocity = Vector3.MoveTowards(currentVelocity, new Vector3(currentVelocity.x,
					(jumpSpeed / 4f), currentVelocity.z),
					fallGravity * superCharacterController.deltaTime);
            }

            Vector3 planarMoveDirection = Math3d.ProjectVectorOnPlane(superCharacterController.up, currentVelocity);
            Vector3 verticalMoveDirection = currentVelocity - planarMoveDirection;

            // Falling.
            if (currentVelocity.y < 0) {
                currentVelocity = planarMoveDirection;
                currentState = RPGCharacterState.Fall;
                return;
            }

			planarMoveDirection = Vector3.MoveTowards(planarMoveDirection,
				rpgCharacterController.cameraRelativeInput * inAirSpeed,
				inAirAccel * superCharacterController.deltaTime);

            verticalMoveDirection -= superCharacterController.up * jumpGravity * superCharacterController.deltaTime;
            currentVelocity = planarMoveDirection + verticalMoveDirection;
        }

        private void DoubleJump_EnterState()
        {
            currentVelocity = new Vector3(currentVelocity.x, doubleJumpSpeed, currentVelocity.z);
            canDoubleJump = false;
            doublejumped = true;
            animator.SetInteger("Jumping", 3);
            rpgCharacterController.SetAnimatorTrigger(AnimatorTrigger.JumpTrigger);
        }

        private void DoubleJump_SuperUpdate()
        {
            Jump_SuperUpdate();
        }

        private void Fall_EnterState()
        {
            if (!doublejumped) { canDoubleJump = true; }
            superCharacterController.DisableClamping();
            superCharacterController.DisableSlopeLimit();
            canJump = false;
            animator.SetInteger("Jumping", 2);
            rpgCharacterController.SetAnimatorTrigger(AnimatorTrigger.JumpTrigger);
        }

        private void Fall_SuperUpdate()
        {
            if (rpgCharacterController.CanStartAction("Idle")) {
                currentVelocity = Math3d.ProjectVectorOnPlane(superCharacterController.up, currentVelocity);
                rpgCharacterController.StartAction("Idle");
                return;
            }

			// If FallingControl is enabled.
			if (fallingControl) {
				Vector3 planarMoveDirection = Math3d.ProjectVectorOnPlane(superCharacterController.up, currentVelocity);
				Vector3 verticalMoveDirection = currentVelocity - planarMoveDirection;

				planarMoveDirection = Vector3.MoveTowards(planarMoveDirection,
					rpgCharacterController.cameraRelativeInput * inAirSpeed,
					inAirAccel * superCharacterController.deltaTime);

				verticalMoveDirection -= superCharacterController.up * fallGravity * superCharacterController.deltaTime;
				currentVelocity = planarMoveDirection + verticalMoveDirection;
			}
			else { currentVelocity -= superCharacterController.up * fallGravity * superCharacterController.deltaTime; }
		}

		private void Fall_ExitState()
		{
			animator.SetInteger("Jumping", 0);
			rpgCharacterController.SetAnimatorTrigger(AnimatorTrigger.JumpTrigger);
		}

		private void Crawl_EnterState()
		{
			superCharacterController.DisableClamping();
			superCharacterController.DisableSlopeLimit();
		}

		private void Crawl_ExitState()
		{
			rpgCharacterController.OnUnlockMovement += InstantSwitchOnceAfterMoveUnlock;
			rpgCharacterController.Lock(true, true, true, 0f, 1f);
			rpgCharacterController.EndCrawl();
		}

		private void Crawl_SuperUpdate()
		{
			Vector3 cameraRelativeInput = rpgCharacterController.cameraRelativeInput;

			// Set speed.
			if (rpgCharacterController.canMove) {
				float moveSpeed = crawlSpeed;
				float moveAccel = crawlAccel;

				currentVelocity = Vector3.MoveTowards(currentVelocity, cameraRelativeInput * moveSpeed, moveAccel
					* superCharacterController.deltaTime);
			}

			RotateTowardsMovementDir();
		}

		private void Swim_EnterState()
        {
			superCharacterController.DisableClamping();
			superCharacterController.DisableSlopeLimit();
			rpgCharacterController.EndAction("Strafe");
			rpgCharacterController.EndAction("Aim");
			rpgCharacterController.Lock(false, true, false, 0f, 0f);
			rpgCharacterController.SetAnimatorTrigger(AnimatorTrigger.SwimTrigger);
			animator.SetBool("Swimming", true);

			// Scale collider to match position of character.
			superCharacterController.radius = 1.5f;
            if (capCollider) { capCollider.radius = 1.5f; }
        }

        private void Swim_ExitState()
        {
            if (capCollider) { capCollider.radius = 0.6f; }
            superCharacterController.radius = 0.6f;
            rpgCharacterController.Unlock(false, true);
        }

        private void Swim_SuperUpdate()
        {
            Vector3 cameraRelativeInput = rpgCharacterController.cameraRelativeInput;
            Vector3 jumpInput = rpgCharacterController.jumpInput;

            // If moving faster than we should be in the water, slow down a lot.
            if (currentVelocity.magnitude > Mathf.Max(swimSpeed, strokeSpeed)) {
                currentVelocity = Vector3.MoveTowards(currentVelocity, Vector3.zero,
					waterFriction * waterFriction * superCharacterController.deltaTime);
            }
            // Horizontal swim movement.
            if (cameraRelativeInput.magnitude > 0) {
                currentVelocity = Vector3.MoveTowards(currentVelocity, cameraRelativeInput * swimSpeed,
					swimAccel * superCharacterController.deltaTime);
            }
			else {
                // Apply friction to slow character to a halt on horizontal axes.
                currentVelocity = Vector3.MoveTowards(currentVelocity, new Vector3(0, currentVelocity.y, 0),
					waterFriction * superCharacterController.deltaTime);
            }
            // Apply friction to slow character to a halt on vertical axis.
            currentVelocity = Vector3.MoveTowards(currentVelocity, new Vector3(currentVelocity.x, 0, currentVelocity.z),
				waterFriction * superCharacterController.deltaTime);
            if (jumpInput.y == 0f) { holdingJump = false; }

			// Swim up.
            if (!holdingJump && jumpInput.y > 0) {
				currentVelocity += superCharacterController.up * strokeSpeed;
				animator.SetInteger("Action", 1);
				rpgCharacterController.SetAnimatorTrigger(AnimatorTrigger.JumpTrigger);
				holdingJump = true;
			}
			// Swim down.
			else if (!holdingJump && jumpInput.y < 0) {
				currentVelocity -= superCharacterController.up * strokeSpeed;
				animator.SetInteger("Action", 2);
				rpgCharacterController.SetAnimatorTrigger(AnimatorTrigger.JumpTrigger);
				holdingJump = true;
			}
        }

        private void ClimbLadder_EnterState()
        {
            Collider ladder = rpgCharacterController.ladder;
            Vector3 ladderTop = new Vector3(ladder.transform.position.x, ladder.bounds.max.y, ladder.transform.position.z);
            Vector3 ladderBottom = new Vector3(ladder.transform.position.x, ladder.bounds.min.y, ladder.transform.position.z);
            Vector3 startPoint = ladderStartBottom ? ladderBottom : ladderTop;
            Vector3 newVector = Vector3.Cross(ladder.transform.forward, ladder.transform.right);
            Vector3 newSpot;

            if (ladderStartBottom) { newSpot = startPoint + (newVector.normalized * 0.71f); }
			else { newSpot = startPoint + (newVector.normalized * -0.87f); }

            superCharacterController.DisableClamping();
            superCharacterController.DisableSlopeLimit();
            superCharacterController.enabled = false;

            LockMovement();

            if (rb != null) { rb.isKinematic = false; }
            if (capCollider != null) { capCollider.center = new Vector3(0, 0.75f, 0); }

            transform.position = newSpot;
            transform.rotation = Quaternion.Euler(transform.rotation.x, ladder.transform.rotation.eulerAngles.y, transform.rotation.z);
        }

        private void ClimbLadder_ExitState()
        {
            if (capCollider != null) { capCollider.center = new Vector3(0, 1.25f, 0); }
            if (rb != null) { rb.isKinematic = true; }

            UnlockMovement();

            superCharacterController.enabled = true;
            superCharacterController.EnableClamping();
            superCharacterController.EnableSlopeLimit();
        }

        private void ClimbLadder_SuperUpdate()
        {
            Vector3 moveInput = rpgCharacterController.moveInput;

            // If no input, don't do anything.
            if (moveInput == Vector3.zero) { return; }

            // If we can't move (i.e. because we're animating) ignore input.
            if (!rpgCharacterController.canMove) { return; }

            float ladderThreshold = 1.2f;

            if (moveInput.y > 0f) {
                Collider ladder = rpgCharacterController.ladder;
                Vector3 ladderTop = new Vector3(transform.position.x, ladder.bounds.max.y - ladderThreshold, transform.position.z);

                // Climb Off Top or Climb Up.
                if (superCharacterController.PointBelowHead(ladderTop)) {
                    rpgCharacterController.ClimbLadder(3);
                    rpgCharacterController.OnUnlockMovement += IdleOnceAfterMoveUnlock;
                }
				else { rpgCharacterController.ClimbLadder(1); }
            }
			else if (moveInput.y < 0f) {
                Collider ladder = rpgCharacterController.ladder;
                Vector3 ladderBottom = new Vector3(transform.position.x, ladder.bounds.min.y + ladderThreshold, transform.position.z);

                // Climb Off Bottom or Climb Down.
                if (superCharacterController.PointAboveFeet(ladderBottom)) {
                    rpgCharacterController.ClimbLadder(4);
                    rpgCharacterController.OnUnlockMovement += IdleOnceAfterMoveUnlock;
                }
				else { rpgCharacterController.ClimbLadder(2); }
            }
        }

        private void DiveRoll_EnterState()
        {
            rpgCharacterController.OnUnlockMovement += IdleOnceAfterMoveUnlock;
        }

		private void DiveRoll_SuperUpdate()
		{
			if (rpgCharacterController.CanStartAction("Idle")) {
				currentVelocity = Math3d.ProjectVectorOnPlane(superCharacterController.up, currentVelocity);
				rpgCharacterController.StartAction("Idle");
				return;
			}
			currentVelocity -= superCharacterController.up * (fallGravity / 2) * superCharacterController.deltaTime;
		}

		private void Roll_EnterState()
        {
            rpgCharacterController.OnUnlockMovement += IdleOnceAfterMoveUnlock;
        }

        private void Knockback_EnterState()
        {
            rpgCharacterController.OnUnlockMovement += IdleOnceAfterMoveUnlock;
        }

        private void Knockdown_EnterState()
        {
            rpgCharacterController.OnUnlockMovement += IdleOnceAfterMoveUnlock;
        }

		#endregion

        /// <summary>
        /// Set the direction that the ladder is being mounted from.
        /// </summary>
        /// <param name="bottom">Where to start climbing the ladder: true- bottom, false- top.</param>
        public void ClimbLadder(bool ladderStartBottom)
        {
            this.ladderStartBottom = ladderStartBottom;
        }

        private void RotateTowardsMovementDir()
        {
            Vector3 movementVector = new Vector3(currentVelocity.x, 0, currentVelocity.z);
            if (movementVector.magnitude > 0.01f) {
                transform.rotation = Quaternion.Slerp(transform.rotation,
					Quaternion.LookRotation(movementVector),
					Time.deltaTime * rotationSpeed);
            }
        }

        private void RotateTowardsTarget(Vector3 targetPosition)
        {
			Debug.Log("RotateTowardsTarget: " + targetPosition);
			Vector3 lookTarget = new Vector3(targetPosition.x - transform.position.x, 0, targetPosition.z - transform.position.z);
			if (lookTarget != Vector3.zero) {
				Quaternion targetRotation = Quaternion.LookRotation(lookTarget);
				transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
			}
        }

		private void RotateTowardsDirection(Vector3 direction)
		{
			Debug.Log("RotateTowardsDirection: " + direction);
			Vector3 lookDirection = new Vector3(direction.x, 0, -direction.y);
			Quaternion lookRotation = Quaternion.LookRotation(lookDirection, Vector3.up);
			transform.rotation = Quaternion.Lerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);
		}

		/// <summary>
		/// Exert a knockback force on the character. Used by the GetHit, Knockdown, and Knockback
		/// actions.
		/// </summary>
		/// <param name="knockDirection">Vector3 direction knock the character.</param>
		/// <param name="knockBackAmount">Amount to knock back.</param>
		/// <param name="variableAmount">Random variance in knockback.</param>
		public void KnockbackForce(Vector3 knockDirection, float knockBackAmount, float variableAmount)
        {
            StartCoroutine(_KnockbackForce(knockDirection, knockBackAmount, variableAmount));
        }

        private IEnumerator _KnockbackForce(Vector3 knockDirection, float knockBackAmount, float variableAmount)
        {
            if (rb == null) { yield break; }

            float startTime = Time.time;
            float elapsed = 0f;

            rb.isKinematic = false;

            while (elapsed < .1f) {
                rb.AddForce(knockDirection
					* ((knockBackAmount + Random.Range(-variableAmount, variableAmount))
					* knockbackMultiplier * 10), ForceMode.Impulse);
                elapsed = Time.time - startTime;
                yield return null;
            }

            rb.isKinematic = true;
        }

        private void OnTriggerEnter(Collider collide)
        {
            // Entering a water volume.
            if (collide.gameObject.layer == 4) { rpgCharacterController.StartAction("Swim"); }

            // Near a ladder.
            else if (collide.transform.parent != null) {
                if (collide.transform.parent.name.Contains("Ladder")) {
                    rpgCharacterController.isNearLadder = true;
                    rpgCharacterController.ladder = collide;
                }
            }
            // Near a cliff.
            else if (collide.transform.name.Contains("Cliff")) {
                rpgCharacterController.isNearCliff = true;
                rpgCharacterController.cliff = collide;
            }
        }

        private void OnTriggerExit(Collider collide)
        {
            // Leaving a water volume.
            if (collide.gameObject.layer == 4) {
                animator.SetBool("Swimming", false);

                // Normally we don't set the state directly, but here we make an exception.
                // The controller can Jump, though the player cannot.
                currentState = RPGCharacterState.Jump;
            }
            // Leaving a ladder.
            else if (collide.transform.parent != null) {
                if (collide.transform.parent.name.Contains("Ladder")) {
                    rpgCharacterController.isNearLadder = false;
                    rpgCharacterController.ladder = null;
                }
            }
            // Leaving a cliff.
            else if (collide.transform.name.Contains("Cliff")) {
                rpgCharacterController.isNearCliff = false;
                rpgCharacterController.cliff = null;
            }
        }

        /// <summary>
        /// Event listener for when RPGCharacterController.OnLockMovement is called.
        /// </summary>
        public void LockMovement()
        {
            currentVelocity = new Vector3(0, 0, 0);
            animator.SetBool("Moving", false);
            animator.applyRootMotion = true;
        }

        /// <summary>
        /// Event listener for when RPGCharacterController.OnUnlockMovement is called.
        /// </summary>
        public void UnlockMovement()
        {
            animator.applyRootMotion = false;
        }

        /// <summary>
        /// Event listener for when RPGCharacterAnimatorEvents.OnMove is called.
        /// </summary>
        /// <param name="deltaPosition">Change in position.</param>
        /// <param name="rootRotation">Change in rotation.</param>
        public void AnimatorMove(Vector3 deltaPosition, Quaternion rootRotation)
        {
            transform.position += deltaPosition;
            transform.rotation = rootRotation;
        }

        /// <summary>
        /// Event listener to return to the Idle state once movement is unlocked, which executes
        /// once. Use with the RPGCharacterController.OnUnlockMovement event.
        ///
        /// e.g.: rpgCharacterController.OnUnlockMovement += IdleOnceAfterMoveUnlock;
        /// </summary>
        public void IdleOnceAfterMoveUnlock()
        {
            rpgCharacterController.StartAction("Idle");
            rpgCharacterController.OnUnlockMovement -= IdleOnceAfterMoveUnlock;
        }

        /// <summary>
        /// Event listener to instant switch once movement is unlocked, which executes only
        /// once. Use with the RPGCharacterController.OnUnlockMovement event. This is used by
        /// the Crawl->Crouch transition to get back into crouching.
        ///
        /// e.g.: rpgCharacterController.OnUnlockMovement += InstantSwitchOnceAfterMoveUnlock;
        /// </summary>
        public void InstantSwitchOnceAfterMoveUnlock()
        {
            rpgCharacterController.SetAnimatorTrigger(AnimatorTrigger.InstantSwitchTrigger);
            rpgCharacterController.OnUnlockMovement -= InstantSwitchOnceAfterMoveUnlock;
        }
    }
}