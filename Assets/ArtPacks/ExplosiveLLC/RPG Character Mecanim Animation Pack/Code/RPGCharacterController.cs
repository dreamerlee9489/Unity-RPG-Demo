using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPGCharacterAnims.Actions;

namespace RPGCharacterAnims
{
    /// <summary>
    /// RPGCharacterController is the main entry point for triggering animations and holds all the
    /// state related to a character. It is the core component of this package–no other controller
    /// will run without it.
    /// </summary>
    public class RPGCharacterController : MonoBehaviour
    {
        /// <summary>
        /// Event called when actions are locked by an animation.
        /// </summary>
        public event System.Action OnLockActions = delegate { };

        /// <summary>
        /// Event called when actions are unlocked at the end of an animation.
        /// </summary>
        public event System.Action OnUnlockActions = delegate { };

        /// <summary>
        /// Event called when movement is locked by an animation.
        /// </summary>
        public event System.Action OnLockMovement = delegate { };

        /// <summary>
        /// Event called when movement is unlocked at the end of an animation.
        /// </summary>
        public event System.Action OnUnlockMovement = delegate { };

        /// <summary>
        /// Unity Animator component.
        /// </summary>
        [HideInInspector] public Animator animator;

        /// <summary>
        /// Animation speed control. Doesn't affect lock timing.
        /// </summary>
        public float animationSpeed = 1;

        /// <summary>
        /// Whether to use PerfectLookAt headlook.
        /// </summary>
        public bool headLook = false;
        private bool isHeadlook = false;
        private PerfectLookAt headLookController;

		/// <summary>
		/// Whether to play idle alert animations.
		/// </summary>
		public bool idleAlert = true;

		/// <summary>
		/// IKHands component.
		/// </summary>
		[HideInInspector] public IKHands ikHands;

        /// <summary>
        /// Nearby ladder collider.
        /// </summary>
        [HideInInspector] public Collider ladder;

        /// <summary>
        /// Nearby cliff collider.
        /// </summary>
        [HideInInspector] public Collider cliff;

		/// <summary>
		/// Target for Aiming/Strafing.
		/// </summary>
		public Transform target;

		/// <summary>
		/// Returns whether the character can take actions.
		/// </summary>
		public bool canAction { get { return _canAction && !isDead; } }
        private bool _canAction;

        /// <summary>
        /// Returns whether the character can face.
        /// </summary>
        public bool canFace { get { return _canFace && !isDead && !isSwimming; } }
        private bool _canFace = true;

        /// <summary>
        /// Returns whether the character can move.
        /// </summary>
        public bool canMove { get { return _canMove && !isDead; } }
        private bool _canMove;

        /// <summary>
        /// Returns whether the character can strafe.
        /// </summary>
        public bool canStrafe { get { return _canStrafe && !isDead && !isSwimming; } }
        private bool _canStrafe = true;

        /// <summary>
        /// Returns whether the AcquiringGround action is active, signifying that the character is
        /// landing on the ground. AcquiringGround is added by RPGCharacterMovementController.
        /// </summary>
		public bool acquiringGround {
			get {
				if (HandlerExists("AcquiringGround")) { return IsActive("AcquiringGround"); }
				return false;
			}
		}

		/// <summary>
		/// Returns whether the Aim action is active.
		/// </summary>
		public bool isAiming {
			get {
				if (HandlerExists("Aim")) { return IsActive("Aim"); }
				return false;
			}
		}

		/// <summary>
		/// Returns whether the Attack action is active.
		/// </summary>
		public bool isAttacking {
			get {
				if (HandlerExists("Attack")) { return IsActive("Attack"); }
				return false;
			}
		}

		/// <summary>
		/// Returns whether the Block action is active.
		/// </summary>
		public bool isBlocking {
			get {
				if (HandlerExists("Block")) { return IsActive("Block"); }
				return false;
			}
		}

		/// <summary>
		/// Returns whether the Cast action is active.
		/// </summary>
		public bool isCasting {
			get {
				if (HandlerExists("Cast")) { return IsActive("Cast"); }
				return false;
			}
		}

		/// <summary>
		/// Returns whether the ClimbLadder action is active. ClimbLadder is added by
		/// RPGCharacterMovementController.
		/// </summary>
		public bool isClimbing {
			get {
				if (HandlerExists("ClimbLadder")) { return IsActive("ClimbLadder"); }
				return false;
			}
		}

		/// <summary>
		/// Returns whether the Crouch action is active.
		/// </summary>
		public bool isCrouching {
			get {
				if (HandlerExists("Crouch")) { return IsActive("Crouch"); }
				return false;
			}
		}

		/// <summary>
		/// Returns whether the Crouch action is active.
		/// </summary>
		public bool isCrawling {
			get {
				if (HandlerExists("Crawl")) { return IsActive("Crawl"); }
				return false;
			}
		}

		/// <summary>
		/// Returns whether the Death action is active.
		/// </summary>
		public bool isDead {
			get {
				if (HandlerExists("Death")) { return IsActive("Death"); }
				return false;
			}
		}

		/// <summary>
		/// Returns whether the Facing action is active.
		/// </summary>
		public bool isFacing {
			get {
				if (HandlerExists("Face")) { return IsActive("Face"); }
				return false;
			}
		}

		/// <summary>
		/// Returns whether the Fall action is active. Fall is added by
		/// RPGCharacterMovementController.
		/// </summary>
		public bool isFalling {
			get {
				if (HandlerExists("Fall")) { return IsActive("Fall"); }
				return false;
			}
		}

		/// <summary>
		/// Returns whether the HipShoot action is active.
		/// </summary>
		public bool isHipShooting {
			get {
				if (HandlerExists("HipShoot")) { return IsActive("HipShoot"); }
				return false;
			}
		}

		/// <summary>
		/// Returns whether the Idle action is active. Idle is added by
		/// RPGCharacterMovementController.
		/// </summary>
		public bool isIdle {
			get {
				if (HandlerExists("Idle")) { return IsActive("Idle"); }
				return false;
			}
		}

		/// <summary>
		/// Returns whether the Injure action is active.
		/// </summary>
		public bool isInjured {
			get {
				if (HandlerExists("Injure")) { return IsActive("Injure"); }
				return false;
			}
		}

		/// <summary>
		/// Returns whether the Move action is active. Idle is added by
		/// RPGCharacterMovementController.
		/// </summary>
		public bool isMoving {
			get {
				if (HandlerExists("Move")) { return IsActive("Move"); }
				return false;
			}
		}

		/// <summary>
		/// Returns whether the Navigation action is active. Navigation is added by
		/// RPGCharacterNavigationController.
		/// </summary>
		public bool isNavigating {
			get {
				if (HandlerExists("Navigation")) { return IsActive("Navigation"); }
				return false;
			}
		}

		/// <summary>
		/// Returns whether the character is near a cliff. Set by RPGCharacterMovementController.
		/// </summary>
		[HideInInspector] public bool isNearCliff = false;

        /// <summary>
        /// Returns whether the character is near a ladder. Set by RPGCharacterMovementController.
        /// </summary>
        [HideInInspector] public bool isNearLadder = false;

        /// <summary>
        /// Returns whether the Relax action is active. Relax is added by RPGCharacterWeapon
        /// controller. If this action does not exist, returns whether rightWeapon and leftWeapon
        /// are -1.
        /// </summary>
        public bool isRelaxed {
            get {
                if (HandlerExists("Relax")) { return IsActive("Relax"); }
                return rightWeapon == -1 && leftWeapon == -1;
            }
        }

        /// <summary>
        /// Returns whether the Roll action is active. Roll is added by
        /// RPGCharacterMovementController.
        /// </summary>
		public bool isRolling {
			get {
				if (HandlerExists("Roll")) { return IsActive("Roll"); }
				return false;
			}
		}

		/// <summary>
		/// Returns whether the Roll action is active. Roll is added by
		/// RPGCharacterMovementController.
		/// </summary>
		public bool isKnockback {
			get {
				if (HandlerExists("Knockback")) { return IsActive("Knockback"); }
				return false;
			}
		}

		/// <summary>
		/// Returns whether the Roll action is active. Roll is added by
		/// RPGCharacterMovementController.
		/// </summary>
		public bool isKnockdown {
			get {
				if (HandlerExists("Knockdown")) { return IsActive("Knockdown"); }
				return false;
			}
		}

		/// <summary>
		/// Returns whether the character is sitting or sleeping. This flag is set by the Emote
		/// action.
		/// </summary>
		[HideInInspector] public bool isSitting = false;

        /// <summary>
        /// Always returns true because all characters are special. Just kidding, this returns
        /// whether the character is performing a special attack. This flag is set by the Attack
        /// action.
        /// </summary>
        [HideInInspector] public bool isSpecial = false;

        /// <summary>
        /// Returns whether the Sprint action is active.
        /// </summary>
		public bool isSprinting {
			get {
				if (HandlerExists("Sprint")) { return IsActive("Sprint"); }
				return false;
			}
		}

		/// <summary>
		/// Returns whether the Strafe action is active.
		/// </summary>
		public bool isStrafing {
			get {
				if (HandlerExists("Strafe")) { return IsActive("Strafe"); }
				return false;
			}
		}

		/// <summary>
		/// Returns whether the Swim action is active. Swim is added by
		/// RPGCharacterMovementController.
		/// </summary>
		public bool isSwimming {
			get {
				if (HandlerExists("Swim")) { return IsActive("Swim"); }
				return false;
			}
		}

		/// <summary>
		/// Returns whether the character is talking. This flag is set by the Emote action.
		/// </summary>
		[HideInInspector] public bool isTalking = false;

        /// <summary>
        /// Returns whether the MaintainingGround action is active, signifying that the character
        /// is on the ground. MaintainingGround is added by RPGCharacterMovementController. If the
        /// action does not exist, this defaults to true.
        /// </summary>
        public bool maintainingGround {
            get {
                if (HandlerExists("MaintainingGround")) { return IsActive("MaintainingGround"); }
                return false;
            }
        }

        /// <summary>
        /// Vector3 for move input. Use SetMoveInput to change this.
        /// </summary>
        public Vector3 moveInput { get { return _moveInput; } }
        private Vector3 _moveInput;

        /// <summary>
        /// Vector3 for aim input. Use SetAimInput to change this.
        /// </summary>
        public Vector3 aimInput { get { return _aimInput; } }
        private Vector3 _aimInput;

        /// <summary>
        /// Vector3 for facing. Use SetFaceInput to change this.
        /// </summary>
        public Vector3 faceInput { get { return _faceInput; } }
        private Vector3 _faceInput;

        /// <summary>
        /// Vector3 for jump input. Use SetJumpInput to change this.
        /// </summary>
        public Vector3 jumpInput { get { return _jumpInput; } }
        private Vector3 _jumpInput;

        /// <summary>
        /// Camera relative input in the XZ plane. This is calculated when SetMoveInput is called.
        /// </summary>
        public Vector3 cameraRelativeInput { get { return _cameraRelativeInput; } }
        private Vector3 _cameraRelativeInput;

        private float _bowPull;

        /// <summary>
        /// Integer weapon number for the right hand. See the Weapon enum in AnimationData.cs for a
        /// full list.
        /// </summary>
        [HideInInspector] public int rightWeapon = 0;

        /// <summary>
        /// Integer weapon number for the left hand. See the Weapon enum in AnimationData.cs for a
        /// full list.
        /// </summary>
        [HideInInspector] public int leftWeapon = 0;

		/// <summary>
		/// Returns whether a weapon is held in the right hand. This is false if the character is
		/// unarmed or relaxed.
		public bool hasRightWeapon { get { return AnimationData.IsRightWeapon(rightWeapon); } }

        /// <summary>
        /// Returns whether a weapon is held in the left hand. This is false if the character is
        /// unarmed or relaxed.
        /// </summary>
        public bool hasLeftWeapon { get { return AnimationData.IsLeftWeapon(leftWeapon); } }

        /// <summary>
        /// Returns whether a weapon is held in both hands (hasRightWeapon && hasLeftWeapon).
        /// </summary>
        public bool hasDualWeapons { get { return hasLeftWeapon && hasRightWeapon; } }

        /// <summary>
        /// Returns whether the character is holding a two-handed weapon. Two-handed weapons are
        /// "held" in the right hand.
        /// </summary>
        public bool hasTwoHandedWeapon { get { return AnimationData.Is2HandedWeapon(rightWeapon); } }

		/// <summary>
		/// Returns whether the character is holding a weapon that can be aimed.
		/// </summary>
		public bool hasAimedWeapon { get { return rightWeapon == 4 || rightWeapon == 5 || rightWeapon == 18; } }

		/// <summary>
		/// Returns whether the character is holding a shield. Shields are held in the left hand.
		/// </summary>
		public bool hasShield { get { return leftWeapon == 7; } }

		/// <summary>
		/// Returns whether the character is in Unarmed or Relax state.
		/// </summary>
		public bool hasNoWeapon { get { return rightWeapon < 1 && leftWeapon < 1; } }

		private Dictionary<string, IActionHandler> actionHandlers = new Dictionary<string, IActionHandler>();

        #region Initialization

        private void Awake()
        {
            // Setup Animator, add AnimationEvents script.
            animator = GetComponentInChildren<Animator>();

            if (animator == null) {
                Debug.LogError("ERROR: THERE IS NO ANIMATOR COMPONENT ON CHILD OF CHARACTER.");
                Debug.Break();
            }

            animator.gameObject.AddComponent<RPGCharacterAnimatorEvents>();
            animator.updateMode = AnimatorUpdateMode.AnimatePhysics;
            animator.cullingMode = AnimatorCullingMode.CullUpdateTransforms;
            animator.SetInteger("Weapon", 0);
            animator.SetInteger("WeaponSwitch", 0);

            // Find HeadLookController if applied.
            headLookController = GetComponent<PerfectLookAt>();

			// Setup IKhands if used.
            ikHands = GetComponentInChildren<IKHands>();

            SetHandler("Aim", new Actions.SimpleActionHandler(() => { }, StopAim));
            SetHandler("Attack", new Actions.Attack());
            SetHandler("Block", new Actions.SimpleActionHandler(StartBlock, EndBlock));
            SetHandler("Cast", new Actions.Cast());
            SetHandler("Crouch", new Actions.SimpleActionHandler(StartCrouch, EndCrouch));
            SetHandler("Death", new Actions.SimpleActionHandler(Death, Revive));
            SetHandler("Dodge", new Actions.Dodge());
            SetHandler("Emote", new Actions.Emote());
            SetHandler("EmoteCombat", new Actions.EmoteCombat());
            SetHandler("Face", new Actions.SimpleActionHandler(StartFace, EndFace));
            SetHandler("HipShoot", new Actions.SimpleActionHandler(() => { }, () => { }));
            SetHandler("Injure", new Actions.SimpleActionHandler(StartInjured, EndInjured));
            SetHandler("Null", new Actions.Null());
            SetHandler("Reload", new Actions.Reload());
            SetHandler("Shoot", new Actions.Shoot());
            SetHandler("SlowTime", new Actions.SlowTime());
            SetHandler("Sprint", new Actions.SimpleActionHandler(StartSprint, EndSprint));
            SetHandler("Strafe", new Actions.SimpleActionHandler(StartStrafe, EndStrafe));
            SetHandler("Turn", new Actions.Turn());

            OnLockActions += LockHeadlook;
            OnUnlockActions += UnlockHeadlook;

            // Unlock actions and movement.
            Unlock(true, true);

			// Set Aim Input.
			SetAimInput(target.transform.position);
		}

		#endregion

		#region Actions

		/// <summary>
		/// Set an action handler.
		/// </summary>
		/// <param name="action">Name of the action.</param>
		/// <param name="handler">The handler associated with this action.</param>
		public void SetHandler(string action, IActionHandler handler)
        {
            actionHandlers[action] = handler;
        }

        /// <summary>
        /// Get an action handler by name. If it doesn't exist, return the Null handler.
        /// </summary>
        /// <param name="action">Name of the action.</param>
        public IActionHandler GetHandler(string action)
        {
            if (HandlerExists(action)) { return actionHandlers[action]; }
            Debug.LogError("RPGCharacterController: No handler for action \"" + action + "\"");
            return actionHandlers["Null"];
        }

        /// <summary>
        /// Check if a handler exists.
        /// </summary>
        /// <param name="action">Name of the action.</param>
        /// <returns>Whether or not that action exists on this controller.</returns>
        public bool HandlerExists(string action)
        {
            return actionHandlers.ContainsKey(action);
        }

        /// <summary>
        /// Check if an action is active.
        /// </summary>
        /// <param name="action">Name of the action.</param>
        /// <returns>Whether the action is active. If the action does not exist, returns false.</returns>
        public bool IsActive(string action)
        {
            return GetHandler(action).IsActive();
        }

        /// <summary>
        /// Check if an action can be started.
        /// </summary>
        /// <param name="action">Name of the action.</param>
        /// <returns>Whether the action can be started. If the action does not exist, returns false.</returns>
        public bool CanStartAction(string action)
        {
            return GetHandler(action).CanStartAction(this);
        }

        /// <summary>
        /// Check if an action can be ended.
        /// </summary>
        /// <param name="action">Name of the action.</param>
        /// <returns>Whether the action can be ended. If the action does not exist, returns false.</returns>
        public bool CanEndAction(string action)
        {
            return GetHandler(action).CanEndAction(this);
        }

        /// <summary>
        /// Start the action with the specified context. If the action does not exist, there is no effect.
        /// </summary>
        /// <param name="action">Name of the action.</param>
        /// <param name="context">Contextual object used by this action. Leave blank if none is required.</param>
        public void StartAction(string action, object context = null)
        {
            GetHandler(action).StartAction(this, context);
        }

        /// <summary>
        /// End the action. If the action does not exist, there is no effect.
        /// </summary>
        /// <param name="action">Name of the action.</param>
        public void EndAction(string action)
        {
            GetHandler(action).EndAction(this);
        }

        #endregion

        #region Updates

        private void LateUpdate()
        {
            // Update Animator animation speed.
            animator.SetFloat("AnimationSpeed", animationSpeed);

            // Headlook.
            if (headLookController != null) {
                if (canAction && isHeadlook == true && !isAiming) {  headLookController.m_Weight += 0.03f; }
				else { headLookController.m_Weight -= 0.03f; }
                headLookController.m_Weight = Mathf.Clamp(headLookController.m_Weight, 0f, 1f);
            }
			// Aiming.
            if (isAiming) { Aim(isAiming, aimInput, _bowPull); }
        }

        #endregion

        #region Input Axes

        /// <summary>
        /// Set move input. This method expects the x-axis to be left-right input and the
        /// y-axis to be up-down input.
        ///
        /// The z-axis is ignored, but the type is a Vector3 in case you wish to use the z-axis.
        ///
        /// This method computes CameraRelativeInput using the x and y axis of the move input
        /// and the main camera, producing a normalized Vector3 in the XZ plane.
        /// </summary>
        /// <param name="_moveInput">Vector3 move input</param>
        public void SetMoveInput(Vector3 _moveInput)
        {
            this._moveInput = _moveInput;

            // Forward vector relative to the camera along the x-z plane.
            Vector3 forward = Camera.main.transform.TransformDirection(Vector3.forward);
            forward.y = 0;
            forward = forward.normalized;

            // Right vector relative to the camera always orthogonal to the forward vector.
            Vector3 right = new Vector3(forward.z, 0, -forward.x);
            Vector3 relativeVelocity = _moveInput.x * right + _moveInput.y * forward;

            // Reduce input for diagonal movement.
            if (relativeVelocity.magnitude > 1) { relativeVelocity.Normalize(); }

            _cameraRelativeInput = relativeVelocity;
        }

        /// <summary>
        /// Set facing input. This is a position in world space of the object that the character
        /// is facing towards.
        /// </summary>
        /// <param name="_faceInput">Vector3 face input.</param>
        public void SetFaceInput(Vector3 _faceInput)
        {
            this._faceInput = _faceInput;
        }

        /// <summary>
        /// Set aim input. This is a position in world space of the object that the character
        /// is aiming at, so that you can easily lock on to a moving target.
        /// </summary>
        /// <param name="_aimInput">Vector3 aim input.</param>
        public void SetAimInput(Vector3 _aimInput)
        {
            this._aimInput = _aimInput;
        }

        /// <summary>
        /// Set jump input. Use this with Vector3.up and Vector3.down (y-axis).
        ///
        /// The X and Z axes are  ignored, but the type is a Vector3 in case you wish to
        /// use the X and Z axes for other actions.
        /// </summary>
        /// <param name="_jumpInput">Vector3 jump input.</param>
        public void SetJumpInput(Vector3 _jumpInput)
        {
            this._jumpInput = _jumpInput;
        }

        /// <summary>
        /// Set bow pull. This is the amount between 0 and 1 that the character
        /// is drawing back a bowstring. This is only used when the character is wielding a
        /// 2-handed bow.
        /// </summary>
        /// <param name="_bowPull">Float between 0-1.</param>
        public void SetBowPull(float _bowPull)
        {
            this._bowPull = _bowPull;
        }

        #endregion

        #region Toggles

        /// <summary>
        /// Toggles headlook on and off.
        /// </summary>
        public void ToggleHeadlook()
        {
            isHeadlook = !isHeadlook;
        }

        #endregion

        #region Aiming / Shooting

        /// <summary>
        /// Triggers the shoot animation. Use the "Shoot" action for a friendly interface.
        /// </summary>
        /// <param name="action">Which animation to play: 1- normal shoot, 2- hip shooting with rifle.</param>
        public void Shoot(int action)
        {
            animator.SetInteger("Action", action);
            SetAnimatorTrigger(AnimatorTrigger.AttackTrigger);
        }

        /// <summary>
        /// Triggers the reload animation.
        ///
        /// Use the "Reload" action for a friendly interface.
        /// </summary>
        public void Reload()
        {
            SetAnimatorTrigger(AnimatorTrigger.ReloadTrigger);
			SetIKPause(2f);
		}

        /// <summary>
        /// Updates the animator for directional aiming used by 2-Handed Bow and Rifle.
        ///
        /// Use the "Aim" action for a friendly interface.
        /// </summary>
        /// <param name="aiming">Whether or not aiming is enabled.</param>
        /// <param name="target">The position in world space of the target.</param>
        /// <param name="bowPull">The magnitude from 0 (none) to 1 (all the way) that the bowstring should be pulled.</param>
        public void Aim(bool aiming, Vector3 target, float bowPull)
        {
            animator.SetBool("Aiming", aiming);
            if (aiming) {
                Vector3 aimTarget = target - transform.position;
                Vector3 horizontalTarget = Vector3.ProjectOnPlane(aimTarget, Vector3.up);
                Quaternion aimRotation = Quaternion.LookRotation(horizontalTarget, Vector3.up);

                float verticalAngle = Vector3.Angle(horizontalTarget, aimTarget);
                if (aimTarget.y - horizontalTarget.y < 0) { verticalAngle *= -1f; }
                verticalAngle = verticalAngle / 90f;

                float horizontalAngle = Vector3.Angle(transform.forward, horizontalTarget);
                float angleDirection = (((aimRotation.eulerAngles.y - transform.rotation.eulerAngles.y) + 360f) % 360f) > 180f ? -1 : 1;
                horizontalAngle = (horizontalAngle / 180f) * angleDirection;

                animator.SetFloat("AimHorizontal", horizontalAngle);
                animator.SetFloat("AimVertical", verticalAngle);
                animator.SetFloat("BowPull", _bowPull);
            }
        }

        /// <summary>
        /// Resets aiming values.
        ///
        /// Use the "Aim" action for a friendly interface.
        /// </summary>
        public void StopAim()
        {
            Aim(false, Vector3.zero, 0f);
        }

        #endregion

        #region Movement

        /// <summary>
        /// Sets "Sprinting" in Animator.
        /// </summary>
        public void StartSprint()
        {
            animator.SetBool("Sprint", true);
            _canStrafe = false;
        }

        /// <summary>
        /// Unset "Sprinting" in Animator.
        /// </summary>
        public void EndSprint()
        {
            animator.SetBool("Sprint", false);
            _canStrafe = true;
        }

        /// <summary>
        /// Turn the character 90 degrees.
        ///
        /// Use the "Turn" action for a friendly interface.
        /// </summary>
        /// <param name="direction">1- Left, 2- Right.</param>
        public void Turn(int direction)
        {
            animator.SetInteger("Action", direction);
            SetAnimatorTrigger(AnimatorTrigger.TurnTrigger);
            Lock(true, true, true, 0, 0.55f);
        }

        /// <summary>
        /// Dive Roll.
        ///
        /// Use the "DiveRoll" action for a friendly interface.
        /// </summary>
        /// <param name="rollNumber">1- Forward.</param>
        public void DiveRoll(int rollNumber)
        {
            animator.SetInteger("Action", rollNumber);
            SetAnimatorTrigger(AnimatorTrigger.DiveRollTrigger);
            Lock(true, true, true, 0, 1f);
			SetIKPause(1.05f);
        }

        /// <summary>
        /// Roll in the specified direction.
        ///
        /// Use the "Roll" action for a friendly interface.
        /// </summary>
        /// <param name="rollNumber">1- Forward, 2- Right, 3- Backward, 4- Left.</param>
        public void Roll(int rollNumber)
        {
            animator.SetInteger("Action", rollNumber);
            SetAnimatorTrigger(AnimatorTrigger.RollTrigger);
            Lock(true, true, true, 0, 0.5f);
			SetIKPause(0.75f);
		}

        /// <summary>
        /// Knockback in the specified direction.
        ///
        /// Use the "Knockback" action for a friendly interface. Forwards only for Unarmed state.
        /// </summary>
        /// <param name="direction">1- Backwards, 2- Backward version2.</param>
        public void Knockback(int direction)
        {
            animator.SetInteger("Action", direction);
            SetAnimatorTrigger(AnimatorTrigger.KnockbackTrigger);
            Lock(true, true, true, 0, 1f);
			if (direction == 1) { SetIKPause(1.125f); }
			else if (direction == 2) { SetIKPause(1f); }
        }

        /// <summary>
        /// Knockdown in the specified direction. Currently only backwards.
        ///
        /// Use the "Knockdown" action for a friendly interface.
        /// </summary>
        /// <param name="direction">1- Backwards.</param>
        public void Knockdown(int direction)
        {
            animator.SetInteger("Action", direction);
            SetAnimatorTrigger(AnimatorTrigger.KnockdownTrigger);
            Lock(true, true, true, 0, 5.25f);
			SetIKPause(5.25f);
		}

        /// <summary>
        /// Dodge the specified direction.
        ///
        /// Use the "Dodge" action for a friendly interface.
        /// </summary>
        /// <param name="direction">1- Left, 2- Right.</param>
        public void Dodge(int direction)
        {
            animator.SetInteger("Action", direction);
            SetAnimatorTrigger(AnimatorTrigger.DodgeTrigger);
            Lock(true, true, true, 0, 0.55f);
        }

        /// <summary>
        /// Triggers ladder climbing animations.
        ///
        /// Use the "ClimbLadder" action for a friendly interface.
        /// </summary>
        /// <param name="action">1- Climb Up, 2- Climb Down, 3- Dismount Top, 4- Dismount Bottom, 5- Mount Top, 6- Mount Bottom.</param>
        public void ClimbLadder(int action)
        {
            float duration = 0f;

            switch (action) {
                case 1:
                case 2:
                case 6:
                    duration = 1.167f;
                    break;
                case 3:
                case 5:
                    duration = 2.667f;
                    break;
                case 4:
                    duration = 1.0f;
                    break;
                default:
                    return;
            }

            // Lock actions when getting on the ladder.
            if (action == 5 || action == 6) { Lock(false, true, false, 0f, 0f); }

            // Trigger animation.
            animator.SetInteger("Action", action);
            SetAnimatorTrigger(AnimatorTrigger.ClimbLadderTrigger);

			// If we are getting off the ladder, we should unlock actions too.
			if (action == 3 || action == 4) { StartCoroutine(_Lock(true, true, true, 0.1f, duration)); }
			// Manually start the coroutine to lock movement here so that it doesn't clobber
			// the one we started above to lock actions.
			else { StartCoroutine(_Lock(true, false, true, 0.1f, duration)); }
        }

		/// <summary>
		/// Dodge the specified direction.
		///
		/// Use the "Crawl" action for a friendly interface.
		/// </summary>
		public void Crawl()
		{
			EndAction("Strafe");
			EndAction("Aim");
			Lock(false, true, false, 0f, 1f);
			SetIKOff();
			animator.SetInteger("Action", 1);
			SetAnimatorTrigger(AnimatorTrigger.CrawlTrigger);
		}

		/// <summary>
		/// End Crawling.
		/// </summary>
		public void EndCrawl()
		{
			animator.SetInteger("Action", 2);
			SetAnimatorTrigger(AnimatorTrigger.CrawlTrigger);
			if (ikHands) {
				ikHands.BlendIK(true, 1, 0.5f, animator.GetInteger("Weapon"));
			}
		}

		#endregion

		#region Combat

		/// <summary>
		/// Ends the relaxed state. This is useful for actions which put the character in
		/// combat, like getting hit.
		/// </summary>
		public void GetAngry()
        {
            if (isRelaxed) { EndAction("Relax"); }
        }

        /// <summary>
        /// Trigger an attack animation.
        ///
        /// Use the "Attack" action for a friendly interface.
        /// </summary>
        /// <param name="attackNumber">Animation number to play. See AnimationData.RandomAttackNumber for details.</param>
        /// <param name="attackSide">Side of the attack: 0- None, 1- Left, 2- Right, 3- Dual.</param>
        /// <param name="leftWeapon">Left side weapon. See Weapon enum in AnimationData.cs.</param>
        /// <param name="rightWeapon">Right-hand weapon. See Weapon enum in AnimationData.cs.</param>
        /// <param name="duration">Duration in seconds that animation is locked.</param>
        public void Attack(int attackNumber, int attackSide, int leftWeapon, int rightWeapon, float duration)
        {
            animator.SetInteger("AttackSide", attackSide);
            Lock(true, true, true, 0, duration);

			// If shooting, use regular or hipshooting attack.
			if (rightWeapon == 18) {
				if (attackSide == 0) {
					if (isHipShooting) { attackNumber = 2; }
					else { attackNumber = 1; }
				}
			}

			// Trigger the animation.
			animator.SetInteger("Action", attackNumber);
            if (attackSide == 3) {  SetAnimatorTrigger(AnimatorTrigger.AttackDualTrigger); }
			else { SetAnimatorTrigger(AnimatorTrigger.AttackTrigger); }
		}

        /// <summary>
        /// Trigger the running attack animation.
        ///
        /// Use the "Attack" action for a friendly interface.
        /// </summary>
        /// <param name="attackSide">Side of the attack: 0- None, 1- Left, 2- Right, 3- Dual.</param>
        /// <param name="leftWeapon">Whether to attack on the left side.</param>
        /// <param name="rightWeapon">Whether to attack on the right side.</param>
        /// <param name="dualWeapon">Whether to attack on both sides.</param>
        /// <param name="twoHandedWeapon">If wielding a two-handed weapon.</param>
        public void RunningAttack(int attackSide, bool leftWeapon, bool rightWeapon, bool dualWeapon, bool twoHandedWeapon)
        {
			if (attackSide == 1 && leftWeapon) {
				animator.SetInteger("Action", 1);
				SetAnimatorTrigger(AnimatorTrigger.AttackTrigger);
			}
			else if (attackSide == 2 && rightWeapon) {
				animator.SetInteger("Action", 4);
				SetAnimatorTrigger(AnimatorTrigger.AttackTrigger);
			}
			else if (attackSide == 3 && dualWeapon) {
				animator.SetInteger("Action", 1);
				SetAnimatorTrigger(AnimatorTrigger.AttackDualTrigger);
			}
			else if (twoHandedWeapon) {
				animator.SetInteger("Action", 1);
				SetAnimatorTrigger(AnimatorTrigger.AttackTrigger);
			}
			else if (hasNoWeapon) {
				animator.SetInteger("Action", 1);
				animator.SetInteger("AttackSide", attackSide);
				SetAnimatorTrigger(AnimatorTrigger.AttackTrigger);
			}
        }

        /// <summary>
        /// Trigger the air attack animation.
        ///
        /// Use the "Attack" action for a friendly interface.
        /// </summary>
        public void AirAttack()
        {
            animator.SetInteger("Action", 1);
            SetAnimatorTrigger(AnimatorTrigger.AttackTrigger);
        }

        /// <summary>
        /// Trigger a kick animation.
        ///
        /// Use the "Attack" action for a friendly interface.
        /// </summary>
        /// <param name="kickSide">1- Left, 2- Right.</param>
        public void AttackKick(int kickSide)
        {
            animator.SetInteger("Action", kickSide);
            SetAnimatorTrigger(AnimatorTrigger.AttackKickTrigger);
            Lock(true, true, true, 0, 0.9f);
        }

        /// <summary>
        /// Start a special attack.
        ///
        /// Use the "Attack" action for a friendly interface.
        /// </summary>
        /// <param name="special">Number of the attack.</param>
        public void StartSpecial(int special)
        {
            animator.SetInteger("Action", special);
            SetAnimatorTrigger(AnimatorTrigger.SpecialAttackTrigger);
            Lock(true, true, true, 0, 0.5f);
        }

        /// <summary>
        /// End a special attack.
        ///
        /// Use the "Attack" action for a friendly interface.
        /// </summary>
        public void EndSpecial()
        {
            SetAnimatorTrigger(AnimatorTrigger.SpecialEndTrigger);
            Lock(true, true, true, 0, 0.6f);
            Unlock(true, true);
			SetIKPause(0.6f);
        }

        /// <summary>
        /// Cast a spell.
        ///
        /// Use the "Cast" action for a friendly interface.
        /// </summary>
        /// <param name="attackNumber">Which attack aniimatino to play.</param>
        /// <param name="attackSide">0- None, 1- Left, 2- Right, 3- Dual.</param>
        /// <param name="type">Type of spell to cast: Cast | AOE | Summon | Buff.</param>
        public void StartCast(int attackNumber, int attackSide, string type)
        {
            animator.SetInteger("LeftRight", attackSide);
            animator.SetInteger("Action", attackNumber);

            if (type == "Cast") { SetAnimatorTrigger(AnimatorTrigger.AttackCastTrigger); }
			else { SetAnimatorTrigger(AnimatorTrigger.CastTrigger); }
            Lock(true, true, false, 0, 0.8f);
        }

        /// <summary>
        /// End spellcasting.
        ///
        /// Use the "Cast" action for a friendly interface.
        /// </summary>
        public void EndCast()
        {
            SetAnimatorTrigger(AnimatorTrigger.CastEndTrigger);
            Lock(true, true, true, 0, 0.1f);
        }

        /// <summary>
        /// Block attacks.
        ///
        /// Use the "Block" action for a friendly interface.
        /// </summary>
        public void StartBlock()
        {
            animator.SetBool("Blocking", true);
            SetAnimatorTrigger(AnimatorTrigger.BlockTrigger);
            Lock(true, true, false, 0f, 0f);
			if (hasAimedWeapon) { SetIKOff(); }
        }

        /// <summary>
        /// Stop blocking attacks.
        ///
        /// Use the "Block" action for a friendly interface.
        /// </summary>
        public void EndBlock()
        {
            animator.SetBool("Blocking", false);
            Unlock(true, true);
			if (hasAimedWeapon) { SetIKOn(); }
		}

        /// <summary>
        /// Run left and right while still facing a target.
        ///
        /// Use the "Face" action for a friendly interface.
        /// </summary>
        public void StartFace()
        {
        }

        /// <summary>
        /// Stop facing.
        ///
        /// Use the "Face" action for a friendly interface.
        /// </summary>
        public void EndFace()
        {
        }

        /// <summary>
        /// Strafe left and right while still facing a target.
        ///
        /// Use the "Strafe" action for a friendly interface.
        /// </summary>
        public void StartStrafe()
        {
        }

        /// <summary>
        /// Stop strafing.
        ///
        /// Use the "Strafe" action for a friendly interface.
        /// </summary>
        public void EndStrafe()
        {
        }

        /// <summary>
        /// Get hit with an attack.
        ///
        /// Use the "GetHit" action for a friendly interface.
        /// </summary>
        public void GetHit(int hitNumber)
        {
            animator.SetInteger("Action", hitNumber);
            SetAnimatorTrigger(AnimatorTrigger.GetHitTrigger);
            Lock(true, true, true, 0.1f, 0.4f);
			SetIKPause(0.6f);
		}

        /// <summary>
        /// Fall over unconscious.
        ///
        /// Use the "Death" action for a friendly interface.
        /// </summary>
        public void Death()
        {
            EndAction("Block");
            SetAnimatorTrigger(AnimatorTrigger.DeathTrigger);
            Lock(true, true, false, 0.1f, 0f);
			SetIKOff();
        }

        /// <summary>
        /// Regain consciousness.
        ///
        /// Use the "Death" action for a friendly interface.
        /// </summary>
        public void Revive()
        {
            SetAnimatorTrigger(AnimatorTrigger.ReviveTrigger);
            GetAngry();
            Lock(true, true, true, 0f, 1f);
			SetIKPause(1f);
        }

        #endregion

        #region Emotes

        /// <summary>
        /// Sit down.
        ///
        /// Use the "Emote" action for a friendly interface.
        /// </summary>
        public void Sit()
        {
            animator.SetInteger("Action", 0);
            SetAnimatorTrigger(AnimatorTrigger.ActionTrigger);
            Lock(true, true, false, 0f, 0f);
        }

        /// <summary>
        /// Lie down and sleep.
        ///
        /// Use the "Emote" action for a friendly interface.
        /// </summary>
        public void Sleep()
        {
            animator.SetInteger("Action", 1);
            SetAnimatorTrigger(AnimatorTrigger.ActionTrigger);
        }

        /// <summary>
        /// Stand when sitting or sleeping.
        ///
        /// Use the "Emote" action for a friendly interface.
        /// </summary>
        public void Stand()
        {
            // Sitting.
            if (animator.GetInteger("Action") == 0) {
                animator.SetInteger("Action", 9);
                Lock(true, true, true, 0f, 1f);
            }
            //Laying down.
            else if (animator.GetInteger("Action") == 1) {
                animator.SetInteger("Action", 10);
                Lock(true, true, true, 0f, 2f);
            }
            SetAnimatorTrigger(AnimatorTrigger.ActionTrigger);
        }

        /// <summary>
        /// Pickup an item.
        ///
        /// Use the "EmoteCombat" action for a friendly interface.
        /// </summary>
        public void Pickup()
        {
			animator.SetInteger("Action", 2);
            SetAnimatorTrigger(AnimatorTrigger.ActionTrigger);
            Lock(true, true, true, 0, 1.4f);
			SetIKPause(1.2f);
        }

		/// <summary>
		/// Activate a button or switch.
		///
		/// Use the "EmoteCombat" action for a friendly interface.
		/// </summary>
		public void Activate()
		{
			animator.SetInteger("Action", 3);
			SetAnimatorTrigger(AnimatorTrigger.ActionTrigger);
			Lock(true, true, true, 0, 1.2f);
			if (rightWeapon == 3) { SetIKPause(1.4f); }
			else { SetIKPause(1f); }
		}

        /// <summary>
        /// Take a swig.
        ///
        /// Use the "Emote" action for a friendly interface.
        /// </summary>
        public void Drink()
        {
            animator.SetInteger("Action", 4);
            SetAnimatorTrigger(AnimatorTrigger.ActionTrigger);
            Lock(true, true, true, 0, 1f);
        }

        /// <summary>
        /// Take a bow.
        ///
        /// Use the "Emote" action for a friendly interface.
        /// </summary>
        public void Bow()
        {
            int numberOfBows = Random.Range(1, 3);
            animator.SetInteger("Action", numberOfBows + 4);
            SetAnimatorTrigger(AnimatorTrigger.ActionTrigger);
            Lock(true, true, true, 0, 3f);
        }

        /// <summary>
        /// Shake head no.
        ///
        /// Use the "Emote" action for a friendly interface.
        /// </summary>
        public void No()
        {
            animator.SetInteger("Action", 7);
            SetAnimatorTrigger(AnimatorTrigger.ActionTrigger);
        }

        /// <summary>
        /// Nod head yes.
        ///
        /// Use the "Emote" action for a friendly interface.
        /// </summary>
        public void Yes()
        {
            animator.SetInteger("Action", 8);
            SetAnimatorTrigger(AnimatorTrigger.ActionTrigger);
        }

        /// <summary>
        /// Do a victorious leap.
        ///
        /// Use the "EmoteCombat" action for a friendly interface.
        /// </summary>
        public void Boost()
        {
			SetIKPause(1f);
			animator.SetInteger("Action", 9);
            SetAnimatorTrigger(AnimatorTrigger.ActionTrigger);
            Lock(true, true, true, 0, 1f);
        }

        /// <summary>
        /// Switch to the injured state.
        ///
        /// Use the "Injure" action for a friendly interface.
        /// </summary>
        public void StartInjured()
        {
            animator.SetBool("Injured", true);
        }

        /// <summary>
        /// Recover from the injured state.
        ///
        /// Use the "Injure" action for a friendly interface.
        /// </summary>
        public void EndInjured()
        {
            animator.SetBool("Injured", false);
        }

        /// <summary>
        /// Crouch to move stealthily.
        ///
        /// Use the "Crouch" action for a friendly interface.
        /// </summary>
        public void StartCrouch()
        {
            animator.SetBool("Crouch", true);
        }

        /// <summary>
        /// Stand from a crouching position
        ///
        /// Use the "Crouch" action for a friendly interface.
        /// </summary>
        public void EndCrouch()
        {
            animator.SetBool("Crouch", false);
        }

        /// <summary>
        /// Start a conversation.
        ///
        /// Use the "Emote" action for a friendly interface.
        /// </summary>
        public void StartConversation()
        {
            StartCoroutine(_PlayConversationClip());
            Lock(true, true, false, 0f, 0f);
        }

        /// <summary>
        /// Stop a conversation.
        ///
        /// Use the "Emote" action for a friendly interface.
        /// </summary>
        public void EndConversation()
        {
            animator.SetInteger("Talking", 0);
            StopCoroutine("_PlayConversationClip");
            Unlock(true, true);
        }

        /// <summary>
        /// Plays a random conversation animation.
        /// </summary>
        /// <returns>IEnumerator for use with StartCoroutine.</returns>
        private IEnumerator _PlayConversationClip()
        {
            if (!isTalking) { yield break; }
            animator.SetInteger("Talking", AnimationData.RandomConversationNumber());
            yield return new WaitForSeconds(2f);
            StartCoroutine(_PlayConversationClip());
        }

        /// <summary>
        /// Plays random idle animation. Currently only Alert1 animation.
        /// </summary>
        public void RandomIdle()
        {
            if (idleAlert && isIdle && !isRelaxed && !isAiming) {
                animator.SetInteger("Action", 1);
                SetAnimatorTrigger(AnimatorTrigger.IdleTrigger);
                Lock(true, true, true, 0, 1.25f);
				SetIKPause(2.125f);
            }
        }

        #endregion

        #region Misc

        /// <summary>
        /// Gets the object with the animator on it. Useful if that object is a child of this one.
        /// </summary>
        /// <returns>GameObject to which the animator is attached.</returns>
        public GameObject GetAnimatorTarget()
        {
            return animator.gameObject;
        }

		/// <summary>
		/// Returns the current animation length of the given animation layer.
		/// </summary>
		/// <param name="animationlayer">The animation layer being checked.</param>
		/// <returns>Float time of the currently played animation on animationlayer.</returns>
		private float CurrentAnimationLength(int animationlayer)
		{
			return animator.GetCurrentAnimatorClipInfo(animationlayer).Length;
		}

        /// <summary>
        /// Keep character from looking at the target.
        /// </summary>
        private void LockHeadlook()
        {
            isHeadlook = false;
        }

        /// <summary>
        /// Allow the character to look at the target.
        /// </summary>
        private void UnlockHeadlook()
        {
            if (headLook) { isHeadlook = true; }
        }

        /// <summary>
        /// Lock character movement and/or action, on a delay for a set time.
        /// </summary>
        /// <param name="lockMovement">If set to <c>true</c> lock movement.</param>
        /// <param name="lockAction">If set to <c>true</c> lock action.</param>
        /// <param name="timed">If set to <c>true</c> timed.</param>
        /// <param name="delayTime">Delay time.</param>
        /// <param name="lockTime">Lock time.</param>
        public void Lock(bool lockMovement, bool lockAction, bool timed, float delayTime, float lockTime)
        {
            StopCoroutine("_Lock");
            StartCoroutine(_Lock(lockMovement, lockAction, timed, delayTime, lockTime));
        }

        private IEnumerator _Lock(bool lockMovement, bool lockAction, bool timed, float delayTime, float lockTime)
        {
            if (delayTime > 0) { yield return new WaitForSeconds(delayTime); }
            if (lockMovement) {
                _canMove = false;
                OnLockMovement();
            }
            if (lockAction) {
                _canAction = false;
                OnLockActions();
            }
            if (timed) {
                if (lockTime > 0) { yield return new WaitForSeconds(lockTime); }
                Unlock(lockMovement, lockAction);
            }
        }

        /// <summary>
        /// Let character move and act again.
        /// </summary>
        /// <param name="movement">Unlock movement if true.</param>
        /// <param name="actions">Unlock actions if true.</param>
        public void Unlock(bool movement, bool actions)
        {
            if (movement) {
                _canMove = true;
                OnUnlockMovement();
            }
            if (actions) {
                _canAction = true;
                OnUnlockActions();
            }
        }

		/// <summary>
		/// Turns IK to 0 instantly.
		/// </summary>
		public void SetIKOff()
		{
			if (ikHands != null) {
				ikHands.leftHandPositionWeight = 0;
				ikHands.leftHandRotationWeight = 0;
			}
		}

		/// <summary>
		/// Turns IK to 1 instantly.
		/// </summary>
		public void SetIKOn()
		{
			if (ikHands != null) {
				ikHands.leftHandPositionWeight = 1;
				ikHands.leftHandRotationWeight = 1;
			}
		}

		/// <summary>
		/// Pauses IK while character uses Left Hand during an animation.
		/// </summary>
		public void SetIKPause(float pauseTime)
		{
			if (ikHands != null && ikHands.isUsed) { ikHands.SetIKPause(pauseTime); }
		}

		/// <summary>
		/// Set Animator Trigger.
		/// </summary>
		public void SetAnimatorTrigger(AnimatorTrigger trigger)
        {
			Debug.Log("SetAnimatorTrigger: " + trigger + " - " + ( int )trigger);
			animator.SetInteger("TriggerNumber", (int)trigger);
            animator.SetTrigger("Trigger");
        }

        /// <summary>
        /// Set Animator Trigger using legacy Animation Trigger names.
        /// </summary>
        public void LegacySetAnimationTrigger(string trigger)
        {
			Debug.Log("LegacyAnimationTrigger: " + ( AnimatorTrigger )System.Enum.Parse(typeof(AnimatorTrigger), trigger) + " - " + ( int )( AnimatorTrigger )System.Enum.Parse(typeof(AnimatorTrigger), trigger));
			AnimatorTrigger parsed_enum = (AnimatorTrigger)System.Enum.Parse(typeof(AnimatorTrigger), trigger);
            animator.SetInteger("TriggerNumber", (int)(AnimatorTrigger)System.Enum.Parse(typeof(AnimatorTrigger), trigger));
            animator.SetTrigger("Trigger");
        }

        /// <summary>
        /// Log out current animator settings.
        /// </summary>
        public void AnimatorDebug()
        {
            Debug.Log("ANIMATOR SETTINGS---------------------------");
			Debug.Log("Moving: " + animator.GetBool("Moving"));
			Debug.Log("Aiming: " + animator.GetBool("Aiming"));
			Debug.Log("Stunned: " + animator.GetBool("Stunned"));
			Debug.Log("Swimming: " + animator.GetBool("Swimming"));
			Debug.Log("Blocking: " + animator.GetBool("Blocking"));
			Debug.Log("Injured: " + animator.GetBool("Injured"));
			Debug.Log("Weapon: " + animator.GetInteger("Weapon"));
            Debug.Log("WeaponSwitch: " + animator.GetInteger("WeaponSwitch"));
            Debug.Log("LeftRight: " + animator.GetInteger("LeftRight"));
            Debug.Log("LeftWeapon: " + animator.GetInteger("LeftWeapon"));
            Debug.Log("RightWeapon: " + animator.GetInteger("RightWeapon"));
			Debug.Log("AttackSide: " + animator.GetInteger("AttackSide"));
			Debug.Log("Jumping: " + animator.GetInteger("Jumping"));
			Debug.Log("Action: " + animator.GetInteger("Action"));
			Debug.Log("SheathLocation: " + animator.GetInteger("SheathLocation"));
			Debug.Log("Talking: " + animator.GetInteger("Talking"));
			Debug.Log("Velocity X: " + animator.GetFloat("Velocity X"));
			Debug.Log("Velocity Z: " + animator.GetFloat("Velocity Z"));
			Debug.Log("AimHorizontal: " + animator.GetFloat("AimHorizontal"));
			Debug.Log("AimVertical: " + animator.GetFloat("AimVertical"));
			Debug.Log("BowPull: " + animator.GetFloat("BowPull"));
			Debug.Log("Charge: " + animator.GetFloat("Charge"));
		}

        /// <summary>
        /// Log out current controller settings.
        /// </summary>
        public void ControllerDebug()
        {
            Debug.Log("CONTROLLER SETTINGS---------------------------");
            Debug.Log("AnimationSpeed: " + animationSpeed);
            Debug.Log("headLook: " + headLook);
            Debug.Log("isHeadlook: " + isHeadlook);
            Debug.Log("ladder: " + ladder);
            Debug.Log("cliff: " + cliff);
            Debug.Log("canAction: " + canAction);
            Debug.Log("canFace: " + canFace);
            Debug.Log("canMove: " + canMove);
            Debug.Log("canStrafe: " + canStrafe);
            Debug.Log("acquiringGround: " + acquiringGround);
            Debug.Log("maintainingGround: " + maintainingGround);
            Debug.Log("isAiming: " + isAiming);
			Debug.Log("isAttacking: " + isAttacking);
			Debug.Log("isBlocking: " + isBlocking);
            Debug.Log("isCasting: " + isCasting);
            Debug.Log("isClimbing: " + isClimbing);
            Debug.Log("isCrouching: " + isCrouching);
			Debug.Log("isCrawling: " + isCrawling);
			Debug.Log("isDead: " + isDead);
            Debug.Log("isFacing: " + isFacing);
            Debug.Log("isFalling: " + isFalling);
            Debug.Log("isHipShooting: " + isHipShooting);
            Debug.Log("isIdle: " + isIdle);
            Debug.Log("isInjured: " + isInjured);
            Debug.Log("Aiming: " + animator.GetBool("Aiming"));
            Debug.Log("isMoving: " + isMoving);
            Debug.Log("isNavigating: " + isNavigating);
            Debug.Log("isNearCliff: " + isNearCliff);
            Debug.Log("isNearLadder: " + isNearLadder);
            Debug.Log("isRelaxed: " + isRelaxed);
            Debug.Log("isRolling: " + isRolling);
            Debug.Log("isKnockback: " + isKnockback);
            Debug.Log("isKnockdown: " + isKnockdown);
            Debug.Log("isSitting: " + isSitting);
            Debug.Log("isSpecial: " + isSpecial);
            Debug.Log("isSprinting: " + isSprinting);
            Debug.Log("isStrafing: " + isStrafing);
            Debug.Log("isSwimming: " + isSwimming);
            Debug.Log("isTalking: " + isTalking);
            Debug.Log("moveInput: " + moveInput);
            Debug.Log("aimInput: " + aimInput);
            Debug.Log("jumpInput: " + jumpInput);
            Debug.Log("cameraRelativeInput: " + cameraRelativeInput);
            Debug.Log("_bowPull: " + _bowPull);
            Debug.Log("rightWeapon: " + rightWeapon);
            Debug.Log("leftWeapon: " + leftWeapon);
            Debug.Log("hasRightWeapon: " + hasRightWeapon);
            Debug.Log("hasLeftWeapon: " + hasLeftWeapon);
            Debug.Log("hasDualWeapons: " + hasDualWeapons);
            Debug.Log("hasTwoHandedWeapon: " + hasTwoHandedWeapon);
            Debug.Log("hasShield: " + hasShield);
        }

        #endregion
    }
}