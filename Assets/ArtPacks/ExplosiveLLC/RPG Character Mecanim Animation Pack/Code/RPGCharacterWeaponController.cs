using System.Collections;
using UnityEngine;

namespace RPGCharacterAnims
{
    public class RPGCharacterWeaponController : MonoBehaviour
    {
		// Components.
        private RPGCharacterController rpgCharacterController;
        private Animator animator;
        private CoroutineQueue coroQueue;

        // Weapon Parameters.
        [HideInInspector] bool isWeaponSwitching = false;
        [HideInInspector] bool dualSwitch;

		[Header("Weapon Models")]
		public GameObject twoHandAxe;
        public GameObject twoHandSword;
        public GameObject twoHandSpear;
        public GameObject twoHandBow;
        public GameObject twoHandCrossbow;
        public GameObject staff;
        public GameObject swordL;
        public GameObject swordR;
        public GameObject maceL;
        public GameObject maceR;
        public GameObject daggerL;
        public GameObject daggerR;
        public GameObject itemL;
        public GameObject itemR;
        public GameObject shield;
        public GameObject pistolL;
        public GameObject pistolR;
        public GameObject rifle;
        public GameObject spear;

        private void Awake()
        {
            coroQueue = new CoroutineQueue(1, StartCoroutine);
            rpgCharacterController = GetComponent<RPGCharacterController>();
            rpgCharacterController.SetHandler("SwitchWeapon", new Actions.SwitchWeapon());
            rpgCharacterController.SetHandler("Relax", new Actions.Relax());

            // Find the Animator component.
            animator = GetComponentInChildren<Animator>();

			// Character starts in Unarmed so hide all weapons.
            StartCoroutine(_HideAllWeapons(false, false));
        }

        private void Start()
        {
            // Listen for the animator's weapon switch event.
            RPGCharacterAnimatorEvents animatorEvents = animator.gameObject.GetComponent<RPGCharacterAnimatorEvents>();
            animatorEvents.OnWeaponSwitch.AddListener(WeaponSwitch);

            // Hide all weapons when the swim action begins.
            Actions.IActionHandler swimHandler = rpgCharacterController.GetHandler("Swim");
            swimHandler.AddStartListener(HideAllWeapons);
        }

        /// <summary>
        /// Add a callback to the coroutine queue to be executed in sequence.
        /// </summary>
        /// <param name="callback">The action to call.</param>
        public void AddCallback(System.Action callback)
        {
            coroQueue.RunCallback(callback);
        }

        /// <summary>
        /// Queue a command to unsheath a weapon.
        /// </summary>
        /// <param name="weaponNumber">Weapon to unsheath.</param>
        /// <param name="dual">Whether to unsheath the same weapon in the other hand.</param>
        public void UnsheathWeapon(int weaponNumber, bool dual)
        {
            if (dual) { coroQueue.RunCallback(() => { dualSwitch = true; }); }
            coroQueue.Run(_UnSheathWeapon(weaponNumber));
            if (dual) { coroQueue.RunCallback(() => { dualSwitch = false; }); }
        }

        /// <summary>
        /// Async method to unsheath a weapon.
        /// </summary>
        /// <param name="weaponNumber">Weapon to unsheath.</param>
        /// <returns>IEnumerator for use with StartCoroutine.</returns>
        private IEnumerator _UnSheathWeapon(int weaponNumber)
        {
            Debug.Log("UnsheathWeapon:" + weaponNumber);
            isWeaponSwitching = true;

            // Use Dual switch only if no 1 handed weapon.
            if (dualSwitch) {
				if (rpgCharacterController.hasNoWeapon) {
					yield return _DualUnSheath(weaponNumber);
					yield break;
				}
            }

            // Switching to Unarmed from Relax.
            if (weaponNumber == 0) {
				Debug.Log("Switching to Unarmed from Relax.");
                DoWeaponSwitch(-1, -1, -1, 0, false);
                yield return new WaitForSeconds(0.1f);
                SetAnimator(0, -2, 0, 0, 0);
            }

            // Switching to 2handed weapon.
            else if (AnimationData.Is2HandedWeapon(weaponNumber)) {
				Debug.Log("Switching to 2Handed Weapon: " + weaponNumber);

				// Switching from 2handed weapon.
				if (AnimationData.Is2HandedWeapon(animator.GetInteger("Weapon"))) {
                    DoWeaponSwitch(0, weaponNumber, weaponNumber, -1, false);

					// Wait for WeaponSwitch() to happen then update Animator.
					yield return new WaitForSeconds(0.75f);
                    SetAnimator(weaponNumber, -2, animator.GetInteger("Weapon"), -1, -1);
                }
				else {
                    DoWeaponSwitch(animator.GetInteger("Weapon"), weaponNumber, weaponNumber, -1, false);

					// Wait for WeaponSwitch() to happen then update Animator.
					yield return new WaitForSeconds(0.75f);
                    SetAnimator(weaponNumber, -2, weaponNumber, -1, -1);
                }
            }

            // Switching to 1handed weapons.
            else {

                // If switching from Unarmed or Relax.
                if (AnimationData.IsNoWeapon(animator.GetInteger("Weapon"))) {
                    animator.SetInteger("WeaponSwitch", 7);
                    if (AnimationData.IsLeftWeapon(weaponNumber)) {  animator.SetInteger("LeftWeapon", weaponNumber); }
					else if (AnimationData.IsRightWeapon(weaponNumber)) { animator.SetInteger("RightWeapon", weaponNumber); }
                }

                // Left hand weapons.
                if (weaponNumber == 7
					|| weaponNumber == 8
					|| weaponNumber == 10
					|| weaponNumber == 12
					|| weaponNumber == 14
					|| weaponNumber == 16) {
                    DoWeaponSwitch(7, weaponNumber, animator.GetInteger("Weapon"), 1, false);

					// Wait for WeaponSwitch() to happen then update Animator.
					yield return new WaitForSeconds(0.5f);
                    SetAnimator(7, 7, weaponNumber, -1, 1);
                }

                // Right hand weapons.
                else if (weaponNumber == 9
					|| weaponNumber == 11
					|| weaponNumber == 13
					|| weaponNumber == 15
					|| weaponNumber == 17
					|| weaponNumber == 19) {
                    DoWeaponSwitch(7, weaponNumber, animator.GetInteger("Weapon"), 2, false);

					// Wait for WeaponSwitch() to happen then update Animator.
					yield return new WaitForSeconds(0.5f);
                    SetAnimator(7, 7, -1, weaponNumber, 2);
                }
            }
        }

        /// <summary>
        /// Queue a command to sheath the current weapon and switch to a new one.
        /// </summary>
        /// <param name="fromWeapon">Which weapon to sheath.</param>
        /// <param name="toWeapon">Target weapon if immediately unsheathing new weapon.</param>
        /// <param name="dual">Whether to sheath both weapons at once.</param>
        public void SheathWeapon(int fromWeapon, int toWeapon, bool dual)
        {
            if (dual) { coroQueue.RunCallback(() => { dualSwitch = true; }); }
            coroQueue.Run(_SheathWeapon(fromWeapon, toWeapon));
            if (dual) { coroQueue.RunCallback(() => { dualSwitch = false; }); }
        }

        /// <summary>
        /// Async method to sheath the current weapon and switch to a new one.
        /// </summary>
        /// <param name="weaponNumber">Which weapon to sheath.</param>
        /// <param name="weaponTo">Target weapon if immediately unsheathing a new weapon.</param>
        /// <returns>IEnumerator for use with StartCoroutine.</returns>
        public IEnumerator _SheathWeapon(int weaponNumber, int weaponTo)
        {
            Debug.Log("Sheath Weapon:" + weaponNumber + "   Weapon To:" + weaponTo);

            // Reset for animation events.
            isWeaponSwitching = true;

            // Use Dual switch.
            if (dualSwitch) {
                yield return _DualSheath(weaponNumber, weaponTo);
                yield break;
            }

            // Set LeftRight hand for 1handed switching.
            if (AnimationData.IsLeftWeapon(weaponNumber)) { animator.SetInteger("LeftRight", 1); }
			else if (AnimationData.IsRightWeapon(weaponNumber)) { animator.SetInteger("LeftRight", 2); }

            // Switching to Unarmed or Relaxed.
            if (weaponTo < 1) {

                // Have at least 1 weapon.
                if (rpgCharacterController.rightWeapon != 0 || rpgCharacterController.leftWeapon != 0) {

                    // Sheath 1handed weapon.
                    if (AnimationData.Is1HandedWeapon(weaponNumber)) {

                        // If sheathing both weapons, go to Armed first.
                        if (rpgCharacterController.rightWeapon != 0 && rpgCharacterController.leftWeapon != 0) {
							DoWeaponSwitch(7, weaponNumber, 7, -1, true); }
						else { DoWeaponSwitch(weaponTo, weaponNumber, 7, -1, true); }

						// Wait for WeaponSwitch() to happen then update Animator.
						yield return new WaitForSeconds(0.55f);

						// Set Left Weapon.
                        if (AnimationData.IsLeftWeapon(weaponNumber)) {
							animator.SetInteger("LeftWeapon", 0);
							SetAnimator(weaponTo, -2, 0, -1, -1);
						}
						// Set Right Weapon.
						else if (AnimationData.IsRightWeapon(weaponNumber)) {
							animator.SetInteger("RightWeapon", 0);
							SetAnimator(weaponTo, -2, -1, 0, -1);
						}
                    }
                    // Sheath 2handed weapon.
                    else {
                        DoWeaponSwitch(weaponTo, weaponNumber, animator.GetInteger("Weapon"), -1, true);
						yield return new WaitForSeconds(0.5f);
						SetAnimator(weaponTo, -2, 0, 0, -1);
					}
                }
                // Unarmed, switching to Relax.
                else if (rpgCharacterController.rightWeapon == 0 && rpgCharacterController.leftWeapon == 0) {
                    DoWeaponSwitch(weaponTo, weaponNumber, animator.GetInteger("Weapon"), 0, true);
					yield return new WaitForSeconds(0.5f);
					SetAnimator(weaponTo, -2, 0, 0, -1);
				}
            }
            // Switching to 2Handed Weapon.
            else if (AnimationData.Is2HandedWeapon(weaponTo)) {
				Debug.Log("Switching to 2Handed Weapon.");

				// Switching from 1Handed Weapon(s).
				if (animator.GetInteger("Weapon") == 7) {
					Debug.Log("Switching from 1Handed Weapon(s).");

					// Dual weilding, switch to Armed if first switch.
					if (rpgCharacterController.leftWeapon != 0 && rpgCharacterController.rightWeapon != 0) {
                        DoWeaponSwitch(7, weaponNumber, 7, -1, true);
                        if (AnimationData.IsLeftWeapon(weaponNumber)) { SetAnimator(7, -2, 0, -1, -1); }
						else if (AnimationData.IsRightWeapon(weaponNumber)) { SetAnimator(7, -2, -1, 0, -1); }
                    }
					else {
						Debug.Log("Switch from 1Handed Weapon.");
                        DoWeaponSwitch(0, weaponNumber, 7, -1, true);

						// Wait for WeaponSwitch() to happen then update Animator.
						yield return new WaitForSeconds(0.55f);
						SetAnimator(0, -2, 0, 0, -1);
					}
                }
                // Switching from 2handed Weapon.
                else {
					Debug.Log("Switching from 2handed Weapon.");
					DoWeaponSwitch(0, weaponNumber, animator.GetInteger("Weapon"), -1, true);

					// Wait for WeaponSwitch() to happen then update Animator.
                    yield return new WaitForSeconds(0.55f);
                    SetAnimator(weaponNumber, weaponNumber, weaponNumber, 0, -1);
                }
            }
            // Switching to 1handed weapons.
            else {
                // Switching from 2handed weapons, go to Unarmed before next switch.
                if (AnimationData.Is2HandedWeapon(animator.GetInteger("Weapon"))) {
                    DoWeaponSwitch(0, weaponNumber, animator.GetInteger("Weapon"), 0, true);
                    yield return new WaitForSeconds(0.5f);
                    SetAnimator(0, -2, 0, 0, 0);
                }
                // Switching from 1handed weapon(s), go to Armed before next switch.
                else if (AnimationData.Is1HandedWeapon(animator.GetInteger("Weapon"))) {
                    DoWeaponSwitch(7, weaponNumber, 7, -1, true);
                    yield return new WaitForSeconds(0.5f);
                    if (AnimationData.IsLeftWeapon(weaponNumber)) { SetAnimator(7, 7, 0, -1, 0); }
					else { SetAnimator(7, 7, -1, 0, 0); }
                }
            }
        }

        /// <summary>
        /// Async method to unsheath both weapons at once. This is called by _UnSheath when dualSwitch is
        /// set to true. To do this all at once, just use the Unsheath method.
        /// </summary>
        /// <param name="weaponNumber">Weapon number to unsheath.</param>
        /// <returns>IEnumerator for use with StartCoroutine.</returns>
        public IEnumerator _DualUnSheath(int weaponNumber)
        {
            Debug.Log("_DualUnSheath:" + weaponNumber);

            // Switching to 1handed weapons.
            if (AnimationData.Is1HandedWeapon(weaponNumber)) {

                // Only if both hands are empty.
                if (rpgCharacterController.leftWeapon <1 && rpgCharacterController.rightWeapon < 1) {

                    DoWeaponSwitch(7, weaponNumber, animator.GetInteger("Weapon"), 3, false);

                    // Set alternate weapon for Left.
                    if (AnimationData.IsRightWeapon(weaponNumber)) {
                        rpgCharacterController.rightWeapon = weaponNumber;
                        animator.SetInteger("RightWeapon", weaponNumber);
                        rpgCharacterController.leftWeapon = weaponNumber - 1;
                        animator.SetInteger("LeftWeapon", weaponNumber - 1);
                    }

                    //Set alternate weapon for Right.
                    else if (AnimationData.IsLeftWeapon(weaponNumber)) {
                        rpgCharacterController.leftWeapon = weaponNumber;
                        animator.SetInteger("LeftWeapon", weaponNumber);
                        rpgCharacterController.rightWeapon = weaponNumber + 1;
                        animator.SetInteger("RightWeapon", weaponNumber + 1);
                    }

                    yield return new WaitForSeconds(0.5f);
                    SetAnimator(7, -2, -1, -1, 3);
                }

                // Only 1 1handed weapon.
                else {
                    DoWeaponSwitch(7, weaponNumber, 7, 3, false);
                    yield return new WaitForSeconds(0.5f);
                    SetAnimator(7, -2, 0, 0, 1);
                }
            }
        }

        /// <summary>
        /// Async method sheath both weapons at once. This is called by _Sheath when dualSwitch is
        /// set to true. To do this all at once, just use the Sheath method.
        /// </summary>
        /// <param name="weaponNumber">Weapon number to unsheath.</param>
        /// <returns>IEnumerator for use with StartCoroutine.</returns>
        public IEnumerator _DualSheath(int weaponNumber, int weaponTo)
        {
            Debug.Log("_DualSheath:" + weaponNumber + "   Weapon To:" + weaponTo);

            // If switching to Relax from Unarmed.
            if (weaponNumber == 0 && weaponTo == -1) {
                DoWeaponSwitch(-1, -1, 0, -1, true);
                yield return new WaitForSeconds(0.5f);
                SetAnimator(-1, -1, 0, 0, 0);
            }
            // Sheath 2handed weapon.
            else if (AnimationData.Is2HandedWeapon(weaponNumber)) {

                // Switching to Relax.
                if (weaponTo == -1) { DoWeaponSwitch(weaponTo, weaponNumber, weaponNumber, 1, true); }
				else { DoWeaponSwitch(0, weaponNumber, weaponNumber, 1, true); }
                yield return new WaitForSeconds(0.5f);
                SetAnimator(weaponTo, -1, 0, 0, 0);
            }
            // Sheath 1handed weapon(s).
            else if (AnimationData.Is1HandedWeapon(weaponNumber)) {

                // If has 2 1handed weapons.
                if (rpgCharacterController.leftWeapon != 0 && rpgCharacterController.rightWeapon != 0) {

                    // If swtiching to 2handed weapon, goto Unarmed.
                    if (AnimationData.Is2HandedWeapon(weaponTo)) {
                        DoWeaponSwitch(0, weaponNumber, 7, 3, true);
                        yield return new WaitForSeconds(0.5f);
                        StartCoroutine(_HideAllWeapons(false, false));
                        SetAnimator(0, -2, 0, 0, 0);
                    }
                    // Switching to other 1handed weapons.
                    else if (AnimationData.Is1HandedWeapon(weaponTo)) {
                        DoWeaponSwitch(7, weaponNumber, 7, 3, true);
                        yield return new WaitForSeconds(0.5f);
                        StartCoroutine(_HideAllWeapons(false, false));
                        SetAnimator(7, -2, 0, 0, 0);
                    }
                    // Switching to Unarmed/Relax.
                    else if (AnimationData.IsNoWeapon(weaponTo)) {
                        DoWeaponSwitch(weaponTo, weaponNumber, 7, 3, true);
                        yield return new WaitForSeconds(0.5f);
                        StartCoroutine(_HideAllWeapons(false, false));
                        SetAnimator(weaponTo, -2, 0, 0, 0);
                    }
                }
                //Has 1 1handed weapon.
                else {
                    DoWeaponSwitch(7, weaponNumber, 7, 3, true);
                    yield return new WaitForSeconds(0.5f);
                    SetAnimator(weaponTo, -2, 0, 0, 0);
                }
            }
            yield return null;
        }

        /// <summary>
        /// Switch to the weapon number instantly.
        /// </summary>
        /// <param name="weaponNumber">Weapon to switch to.</param>
        public void InstantWeaponSwitch(int weaponNumber)
        {
            coroQueue.Run(_InstantWeaponSwitch(weaponNumber));
        }

        /// <summary>
        /// Async method to instant weapon switch.
        /// </summary>
        /// <param name="weaponNumber">Weapon number to switch to.</param>
        /// <returns>IEnumerator for use with StartCoroutine.</returns>
        /// /// <summary>
        public IEnumerator _InstantWeaponSwitch(int weaponNumber)
        {
            Debug.Log("_InstantWeaponSwitch:" + weaponNumber);
            rpgCharacterController.SetAnimatorTrigger(AnimatorTrigger.InstantSwitchTrigger);
			rpgCharacterController.SetIKOff();
            StartCoroutine(_HideAllWeapons(false, false));

            // 1Handed.
            if (AnimationData.Is1HandedWeapon(weaponNumber)) {

                // Dual weapons.
                if (dualSwitch) {
					Debug.Log("InstantSwitch DualWeapons.");
					animator.SetInteger("Weapon", 7);
                    StartCoroutine(_WeaponVisibility(weaponNumber, true, true));
                    animator.SetInteger("LeftRight", 3);
                }
				// Single weapon.
				else {
					Debug.Log("InstantSwitch to 1HandedWeapon.");
					animator.SetInteger("Weapon", 7);
                    animator.SetInteger("WeaponSwitch", 7);

                    // Right weapon.
                    if (AnimationData.IsRightWeapon(weaponNumber)) {
						Debug.Log("Right Weapon.");
                        animator.SetInteger("RightWeapon", weaponNumber);
                        rpgCharacterController.rightWeapon = weaponNumber;
                        StartCoroutine(_WeaponVisibility(weaponNumber, true, false));
                        if (rpgCharacterController.hasLeftWeapon) { animator.SetInteger("LeftRight", 3); }
						else { animator.SetInteger("LeftRight", 2); }
                    }
					// Left weapon.
					else if (AnimationData.IsLeftWeapon(weaponNumber)) {
						Debug.Log("Left Weapon.");
                        animator.SetInteger("LeftWeapon", weaponNumber);
                        rpgCharacterController.leftWeapon = weaponNumber;
                        StartCoroutine(_WeaponVisibility(weaponNumber, true, false));
                        if (rpgCharacterController.hasRightWeapon) { animator.SetInteger("LeftRight", 3); }
						else { animator.SetInteger("LeftRight", 1); }
                    }
                }
            }
            // 2Handed.
            else if (AnimationData.Is2HandedWeapon(weaponNumber)) {
				Debug.Log("InstantSwitch to 2HandedWeapon.");
                animator.SetInteger("Weapon", weaponNumber);
                rpgCharacterController.rightWeapon = 0;
                rpgCharacterController.leftWeapon = 0;
                animator.SetInteger("LeftWeapon", 0);
                animator.SetInteger("RightWeapon", 0);
                StartCoroutine(_HideAllWeapons(false, false));
                StartCoroutine(_WeaponVisibility(weaponNumber, true, false));
				if (AnimationData.IsIKWeapon(weaponNumber)) { rpgCharacterController.SetIKOn(); }
            }
            // Switching to Unarmed or Relax.
            else {
                animator.SetInteger("Weapon", weaponNumber);
                rpgCharacterController.rightWeapon = 0;
                rpgCharacterController.leftWeapon = 0;
                animator.SetInteger("LeftWeapon", 0);
                animator.SetInteger("RightWeapon", 0);
                animator.SetInteger("LeftRight", 0);
                StartCoroutine(_HideAllWeapons(false, false));
            }
            yield return null;
        }

        private void DoWeaponSwitch(int weaponSwitch, int weaponVisibility, int weaponNumber, int leftRight, bool sheath)
        {
			Debug.Log("DO WEAPON SWITCH---------------------------");
            Debug.Log("WeaponSwitch:"
				+ weaponSwitch + "   WeaponVisibility:"
				+ weaponVisibility + "   WeaponNumber:"
				+ weaponNumber + "   LeftRight:"
				+ leftRight + "   Sheath:"
				+ sheath);

			// Lock character movement for switch unless has moving sheath/unsheath anims.
			if (rpgCharacterController.isMoving) { rpgCharacterController.Lock(false, true, true, 0f, 1f); }
			else { rpgCharacterController.Lock(true, true, true, 0f, 1f); }

            // Set WeaponSwitch and Weapon.
            animator.SetInteger("WeaponSwitch", weaponSwitch);
            animator.SetInteger("Weapon", weaponNumber);

            // Set LeftRight if applicable.
            if (leftRight != -1) { animator.SetInteger("LeftRight", leftRight); }

			// Sheath.
			if (sheath) {
                rpgCharacterController.SetAnimatorTrigger(AnimatorTrigger.WeaponSheathTrigger);
                StartCoroutine(_WeaponVisibility(weaponVisibility, false, dualSwitch));
                // If using IKHands, trigger IK blend.
                if (rpgCharacterController.ikHands != null) { rpgCharacterController.ikHands.BlendIK(false, 0f, 0.2f, weaponVisibility); }

            }
			// Unsheath.
			else {
                rpgCharacterController.SetAnimatorTrigger(AnimatorTrigger.WeaponUnsheathTrigger);
                StartCoroutine(_WeaponVisibility(weaponVisibility, true, dualSwitch));

                // If using IKHands, trigger IK blend.
                if (rpgCharacterController.ikHands != null) { rpgCharacterController.ikHands.BlendIK(true, 0.75f, 1, weaponVisibility); }
            }
        }

        /// <summary>
        /// Sets the animator state. This method is very close to the metal, it's recommended that you use
        /// other entry points rather than calling this directly.
        /// </summary>
        /// <param name="weapon">Animator weapon number. Use AnimationData's AnimatorWeapon enum.</param>
        /// <param name="weaponSwitch">Weapon switch.</param>
        /// <param name="Lweapon">Left weapon number. Use AnimationData's Weapon enum.</param>
        /// <param name="Rweapon">Right weapon number. Use AnimationData's Weapon enum.</param>
        /// <param name="weaponSide">Weapon side: 0- None, 1- Left, 2- Right, 3- Dual.</param>
        public void SetAnimator(int weapon, int weaponSwitch, int Lweapon, int Rweapon, int weaponSide)
        {
			Debug.Log("SET ANIMATOR---------------------------");
			Debug.Log("Weapon:"
				+ weapon + "   Weaponswitch:"
				+ weaponSwitch + "   Lweapon:"
				+ Lweapon + "   Rweapon:"
				+ Rweapon + "   Weaponside:"
				+ weaponSide);

            // Set Weapon if applicable.
            if (weapon != -2) { animator.SetInteger("Weapon", weapon); }

			// Set WeaponSwitch if applicable.
            if (weaponSwitch != -2) { animator.SetInteger("WeaponSwitch", weaponSwitch); }

			// Set left weapon if applicable.
            if (Lweapon != -1) { animator.SetInteger("LeftWeapon", Lweapon); }

			// Set right weapon if applicable.
            if (Rweapon != -1) { animator.SetInteger("RightWeapon", Rweapon); }

			// Set weapon side if applicable.
            if (weaponSide != -1) { animator.SetInteger("LeftRight", weaponSide); }

			rpgCharacterController.AnimatorDebug();
		}

        /// <summary>
        /// Callback to use with Animator's WeaponSwitch event.
        /// </summary>
        public void WeaponSwitch()
        {
            if (isWeaponSwitching) { isWeaponSwitching = false; }
        }

        /// <summary>
        /// Helper method used by other weapon visibility methods to safely set a weapon object's visibility.
        /// This will work even if the object is not set in the component parameters.
        /// </summary>
        /// <param name="weaponObject">Weapon to update.</param>
        /// <param name="visibility">Visibility status.</param>
        public void SafeSetVisibility(GameObject weaponObject, bool visibility)
        {
            if (weaponObject != null) { weaponObject.SetActive(visibility); }
        }

        /// <summary>
        /// Hide all weapon objects and set the animator and the character controller to the unarmed state.
        /// </summary>
        public void HideAllWeapons()
        {
            StartCoroutine(_HideAllWeapons(false, true));
        }

        /// <summary>
        /// Async method to all weapon objects and set the animator and the character controller to the unarmed state.
        /// </summary>
        /// <param name="timed">Whether to wait until a period of time to hide the weapon.</param>
        /// <param name="resetToUnarmed">Whether to reset the animator and the character controller to the unarmed state.</param>
        /// <returns>IEnumerator for use with StartCoroutine.</returns>
        public IEnumerator _HideAllWeapons(bool timed, bool resetToUnarmed)
        {
            if (timed) { while (!isWeaponSwitching) { yield return null; } }

            // Reset to Unarmed.
            if (resetToUnarmed) {
                animator.SetInteger("Weapon", 0);
                rpgCharacterController.rightWeapon = (int)Weapon.Unarmed;
                rpgCharacterController.leftWeapon = (int)Weapon.Unarmed;
                StartCoroutine(_WeaponVisibility(rpgCharacterController.leftWeapon, false, true));
                animator.SetInteger("RightWeapon", 0);
                animator.SetInteger("LeftWeapon", 0);
                animator.SetInteger("LeftRight", 0);
            }
            SafeSetVisibility(twoHandAxe, false);
            SafeSetVisibility(twoHandBow, false);
            SafeSetVisibility(twoHandCrossbow, false);
            SafeSetVisibility(twoHandSpear, false);
            SafeSetVisibility(twoHandSword, false);
            SafeSetVisibility(staff, false);
            SafeSetVisibility(swordL, false);
            SafeSetVisibility(swordR, false);
            SafeSetVisibility(maceL, false);
            SafeSetVisibility(maceR, false);
            SafeSetVisibility(daggerL, false);
            SafeSetVisibility(daggerR, false);
            SafeSetVisibility(itemL, false);
            SafeSetVisibility(itemR, false);
            SafeSetVisibility(shield, false);
            SafeSetVisibility(pistolL, false);
            SafeSetVisibility(pistolR, false);
            SafeSetVisibility(rifle, false);
            SafeSetVisibility(spear, false);
        }

        /// <summary>
        /// Set a single weapon's visibility.
        /// </summary>
        /// <param name="weaponNumber">Weapon object to set.</param>
        /// <param name="visible">Whether to set it visible or not.</param>
        /// <param name="dual">Whether to update the same weapon in the other hand as well.</param>
        public IEnumerator _WeaponVisibility(int weaponNumber, bool visible, bool dual)
        {
            Debug.Log("WeaponVisibility:" + weaponNumber + "   Visible:" + visible + "   Dual:" + dual);
            while (isWeaponSwitching) { yield return null; }
            if (weaponNumber == 1) { SafeSetVisibility(twoHandSword, visible); }
			else if (weaponNumber == 2) { SafeSetVisibility(twoHandSpear, visible); }
			else if (weaponNumber == 3) { SafeSetVisibility(twoHandAxe, visible); }
			else if (weaponNumber == 4) { SafeSetVisibility(twoHandBow, visible); }
			else if (weaponNumber == 5) { SafeSetVisibility(twoHandCrossbow, visible); }
			else if (weaponNumber == 6) { SafeSetVisibility(staff, visible); }
			else if (weaponNumber == 7) { SafeSetVisibility(shield, visible); }
			else if (weaponNumber == 8) {
                SafeSetVisibility(swordL, visible);
                if (dual) { SafeSetVisibility(swordR, visible); } }
			else if (weaponNumber == 9) {
                SafeSetVisibility(swordR, visible);
                if (dual) { SafeSetVisibility(swordL, visible); } }
			else if (weaponNumber == 10) {
                SafeSetVisibility(maceL, visible);
                if (dual) { SafeSetVisibility(maceR, visible); } }
			else if (weaponNumber == 11) {
                SafeSetVisibility(maceR, visible);
                if (dual) { SafeSetVisibility(maceL, visible); } }
			else if (weaponNumber == 12) {
                SafeSetVisibility(daggerL, visible);
                if (dual) { SafeSetVisibility(daggerR, visible); } }
			else if (weaponNumber == 13) {
                SafeSetVisibility(daggerR, visible);
                if (dual) { SafeSetVisibility(daggerL, visible); } }
			else if (weaponNumber == 14) {
                SafeSetVisibility(itemL, visible);
                if (dual) { SafeSetVisibility(itemR, visible); } }
			else if (weaponNumber == 15) {
                SafeSetVisibility(itemR, visible);
                if (dual) { SafeSetVisibility(itemL, visible); } }
			else if (weaponNumber == 16) {
                SafeSetVisibility(pistolL, visible);
                if (dual) { SafeSetVisibility(pistolR, visible); } }
			else if (weaponNumber == 17) {
                SafeSetVisibility(pistolR, visible);
                if (dual) { SafeSetVisibility(pistolL, visible); } }
			else if (weaponNumber == 18) { SafeSetVisibility(rifle, visible); }
			else if (weaponNumber == 19) { SafeSetVisibility(spear, visible); }
            yield return null;
        }

        /// <summary>
        /// Sync weapon object visibility to the current weapons in the RPGCharacterController.
        /// </summary>
        public void SyncWeaponVisibility()
        {
            coroQueue.Run(_SyncWeaponVisibility());
        }

        /// <summary>
        /// Async method to sync weapon object visiblity to the current weapons in RPGCharacterController.
        /// This will wait for weapon switching to finish. If your aim is to force this update, call WeaponSwitch
        /// first. This will stop the _HideAllWeapons and _WeaponVisibility coroutines.
        /// </summary>
        /// <returns>IEnumerator for use with.</returns>
        private IEnumerator _SyncWeaponVisibility()
        {
            while (isWeaponSwitching && !(rpgCharacterController.canAction && rpgCharacterController.canMove)) { yield return null; }

            StopCoroutine("_HideAllWeapons");
            StopCoroutine("_WeaponVisibility");

            SafeSetVisibility(twoHandAxe, false);
            SafeSetVisibility(twoHandBow, false);
            SafeSetVisibility(twoHandCrossbow, false);
            SafeSetVisibility(twoHandSpear, false);
            SafeSetVisibility(twoHandSword, false);
            SafeSetVisibility(staff, false);
            SafeSetVisibility(swordL, false);
            SafeSetVisibility(swordR, false);
            SafeSetVisibility(maceL, false);
            SafeSetVisibility(maceR, false);
            SafeSetVisibility(daggerL, false);
            SafeSetVisibility(daggerR, false);
            SafeSetVisibility(itemL, false);
            SafeSetVisibility(itemR, false);
            SafeSetVisibility(shield, false);
            SafeSetVisibility(pistolL, false);
            SafeSetVisibility(pistolR, false);
            SafeSetVisibility(rifle, false);
            SafeSetVisibility(spear, false);

            switch (rpgCharacterController.leftWeapon) {
                case (int)Weapon.Shield: SafeSetVisibility(shield, true); break;
                case (int)Weapon.LeftSword: SafeSetVisibility(swordL, true); break;
                case (int)Weapon.LeftMace: SafeSetVisibility(maceL, true); break;
                case (int)Weapon.LeftDagger: SafeSetVisibility(daggerL, true); break;
                case (int)Weapon.LeftItem: SafeSetVisibility(itemL, true); break;
                case (int)Weapon.LeftPistol: SafeSetVisibility(pistolL, true); break;
            }
            switch (rpgCharacterController.rightWeapon) {
                case (int)Weapon.TwoHandSword: SafeSetVisibility(twoHandSword, true); break;
                case (int)Weapon.TwoHandSpear: SafeSetVisibility(twoHandSpear, true); break;
                case (int)Weapon.TwoHandAxe: SafeSetVisibility(twoHandAxe, true); break;
                case (int)Weapon.TwoHandBow: SafeSetVisibility(twoHandBow, true); break;
                case (int)Weapon.TwoHandCrossbow: SafeSetVisibility(twoHandCrossbow, true); break;
                case (int)Weapon.TwoHandStaff: SafeSetVisibility(staff, true); break;
                case (int)Weapon.RightSword: SafeSetVisibility(swordR, true); break;
                case (int)Weapon.RightMace: SafeSetVisibility(maceR, true); break;
                case (int)Weapon.RightDagger: SafeSetVisibility(daggerR, true); break;
                case (int)Weapon.RightItem: SafeSetVisibility(itemR, true); break;
                case (int)Weapon.RightPistol: SafeSetVisibility(pistolR, true); break;
                case (int)Weapon.Rifle: SafeSetVisibility(rifle, true); break;
                case (int)Weapon.RightSpear: SafeSetVisibility(spear, true); break;
            }
        }
    }
}