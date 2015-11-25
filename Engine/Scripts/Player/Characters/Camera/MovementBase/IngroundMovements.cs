using System;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using UnityStandardAssets.Utility;
using Engine.Objects.Weapon;
using Engine.Player.Attack;
using Engine.Player.Animations;

namespace Engine.Player.Movement.Movements {

	public class IngroundMovements : MonoBehaviour, IMovement {

		private float energyDec = 0.25f; // снижение энергии при беге
		private float energyInc = 0.08f; // восстановление энергии
		private float energyTimeStamp = 0f;

		private float playerStandupHeight = 2.0f;
		private float playerSitdownHeight = 0.0f;

		private bool isStay;
		private bool isSitdown = false;
		private bool isWalking;
		private bool isJumping;
		private bool jump = false;

		private Vector3 moveDir = Vector3.zero;

		private float playerWalkSpeed = 3.00f;
		private float playerRunSpeed  = 7.0f;
		private float playerJumpSpeed = 7.0f;

		private float m_RunstepLenghtenWalk = 0.7f;
		private float m_RunstepLenghtenRun = 0.1f;

		private float bobRangeWalk = 0.1f;
		private float bobRangeRun = 0.15f;

		private float stickToGroundForce = 10.0f;
		private float gravityMultiplier  = 2.0f;
		
		private CollisionFlags collisionFlags;
		private Vector3        originalCameraPosition;
		private bool           previouslyGrounded;
		private float          stepCycle;
		private float          stepInterval = 3f;
		private float          nextStep;
		
		private Actions             actions;
		private MovementAudioData   audioData;
		private MouseLook           mouseLook;
		private Camera              mainCameraObject;
		private FOVKick             fovKick;
		private CurveControlledBob  headBob;
		private LerpControlledBob   jumpBob;
		private CharacterController characterController;
		private AttackController    attackController;
		private AudioSource         audioSource;

		private ObjectsSelector objectsSelector;

		private Vector2 input;

		private GameObject playerObject;

		private Vector3        moveDirAddition = Vector3.zero;

			public void setUp(Actions             actions,
							  MouseLook           mouseLook,
							  FOVKick             fovKick,
							  CurveControlledBob  headBob,
							  LerpControlledBob   jumpBob,
							  AttackController    attackController) {
				
				this.actions=actions;
				this.mouseLook=mouseLook;
				this.fovKick=fovKick;
				this.headBob=headBob;
				this.jumpBob=jumpBob;
				this.attackController=attackController;

				this.mainCameraObject    = SingletonNames.getMainCamera();
				this.playerObject        = SingletonNames.getPlayer();
				this.audioSource         = playerObject.gameObject.AddComponent<AudioSource>();
				this.audioData           = playerObject.GetComponent<MovementAudioData>();
				this.characterController = playerObject.GetComponent<CharacterController>();

				objectsSelector = mainCameraObject.GetComponent<ObjectsSelector>();

				originalCameraPosition = mainCameraObject.transform.localPosition;

				fovKick.Setup(mainCameraObject);
				headBob.Setup(mainCameraObject, stepInterval);
				mouseLook.Init(playerObject.transform, mainCameraObject.transform);

				stepCycle = 0f;
				nextStep = stepCycle / 2f;
				isJumping = false;
				
			}
		
		public void update(){

				RotateView();

				if (CrossPlatformInputManager.GetButtonDown(SingletonNames.Input.ATTACK1)) {

						// если в руках что то есть
					if (objectsSelector.isPickedObject()) {

						objectsSelector.OnPickDrop(200f); // выкидываем предмет

					} else {

						attackController.setAttacker(WeaponTypes.Sword);
						attackController.startAttack();

					}
				}

				if (CrossPlatformInputManager.GetButtonDown(SingletonNames.Input.ATTACK_MAGIC)) {
					attackController.setAttacker(WeaponTypes.Magic);
					attackController.startAttack();
				}


				if (!jump)
					jump = CrossPlatformInputManager.GetButtonDown(SingletonNames.Input.JUMP);

				Sitdown();

				if (!previouslyGrounded && characterController.isGrounded) {
					StartCoroutine(jumpBob.DoBobCycle());
					PlayLandingSound();
					moveDir.y = 0f;
					isJumping = false;
				}

				if (!characterController.isGrounded && !isJumping && previouslyGrounded)
					moveDir.y = 0f;

				previouslyGrounded = characterController.isGrounded;
			
		}
		
		public void fixUpdate(){

			float speed;
			GetInput(out speed);
			// always move along the camera forward as it is the direction that it being aimed at
			Vector3 desiredMove = playerObject.transform.forward * input.y + playerObject.transform.right * input.x;

			// get a normal for the surface that is being touched to move along it
			RaycastHit hitInfo;
			Physics.SphereCast(playerObject.transform.position, characterController.radius, Vector3.down, out hitInfo,
							   characterController.height / 2f);
			desiredMove = Vector3.ProjectOnPlane(desiredMove, hitInfo.normal).normalized;

			moveDir.x = desiredMove.x * speed;
			moveDir.z = desiredMove.z * speed;


			if (characterController.isGrounded) {
				moveDir.y = -stickToGroundForce;

				if (jump) {
					moveDir.y = playerJumpSpeed;
					PlayJumpSound();
					jump = false;
					isJumping = true;
				}
			} else {
				moveDir += Physics.gravity * gravityMultiplier * Time.fixedDeltaTime;
			}

			if (moveDirAddition != Vector3.zero) {
				moveDir += moveDirAddition;
				moveDirAddition = Vector3.zero;
			}


			collisionFlags = characterController.Move(moveDir * Time.fixedDeltaTime);

			ProgressStepCycle(speed);
			UpdateCameraPosition(speed);
			
		}

		private void Sitdown() {

			if (CrossPlatformInputManager.GetButtonDown(SingletonNames.Input.SITDOWN))
				isSitdown = !isSitdown;

			if (isSitdown && characterController.height != playerSitdownHeight) {
				characterController.height = playerSitdownHeight;
				actions.Sitdown();
			}

			if (!isSitdown && characterController.height != playerStandupHeight) {
				characterController.height = playerStandupHeight;
				actions.Standup();
			}

		}

		private void PlayLandingSound() {
			audioSource.clip = audioData.getLandSound();
			audioSource.Play();
			actions.Stay();
			nextStep = stepCycle + .5f;
		}


		private void PlayJumpSound() {
			audioSource.clip = audioData.getJumpSound();
			audioSource.Play();
			actions.Jump();
		}


		private void ProgressStepCycle(float speed) {

			if (characterController.velocity.sqrMagnitude > 0 && (input.x != 0 || input.y != 0)) {
				stepCycle += (characterController.velocity.magnitude + (speed * (isWalking ? m_RunstepLenghtenWalk : m_RunstepLenghtenRun))) *
							 Time.fixedDeltaTime;
			}

			if (!(stepCycle > nextStep)) {
				return;
			}

			nextStep = stepCycle + stepInterval;

			PlayFootStepAudio();
		}


		private void PlayFootStepAudio() {

			if (!characterController.isGrounded)
				return;

			if (isWalking)
				actions.Walk();
			else
				actions.Run();

			audioSource.clip = audioData.getFootStepSound(audioSource.clip);
			audioSource.PlayOneShot(audioSource.clip);
		}

		public void addImpulse(Vector3 velocity) {
			moveDir = velocity;
		}

		public Vector3 getImpulse() {
			return moveDir;
		}

		private void UpdateCameraPosition(float speed) {

			Vector3 newCameraPosition;

			if (characterController.velocity.magnitude > 0 && characterController.isGrounded) {

				mainCameraObject.transform.localPosition = headBob.DoHeadBob(characterController.velocity.magnitude +
									  (speed * (isWalking ? m_RunstepLenghtenWalk : m_RunstepLenghtenRun)));

				newCameraPosition = mainCameraObject.transform.localPosition;
				newCameraPosition.y = mainCameraObject.transform.localPosition.y - jumpBob.Offset();
			} else {

				newCameraPosition = mainCameraObject.transform.localPosition;
				newCameraPosition.y = originalCameraPosition.y - jumpBob.Offset();

				actions.Stay();
			}

		}

		private void GetInput(out float speed) {
			float horizontal = CrossPlatformInputManager.GetAxis(SingletonNames.Input.HORIZONTAL);
			float vertical = CrossPlatformInputManager.GetAxis(SingletonNames.Input.VERTICAL);

			bool waswalking = isWalking;

			if (Input.GetMouseButton(0))
				attackController.startAttack();

#if !MOBILE_INPUT

			isWalking = !Input.GetKey(KeyCode.LeftShift) && GamePlayer.states.energy > energyDec;

			if (!isWalking) {

				GamePlayer.states.energy -= energyDec;
				energyTimeStamp = Time.time;

            } else {

				if(Time.time-energyTimeStamp>1.5f && GamePlayer.states.energy!=GamePlayer.states.maxEnergy) // восстанавливаем энергию после простоя в 1,5 сек.
                    if(GamePlayer.states.energy<GamePlayer.states.maxEnergy)
                     GamePlayer.states.energy += energyInc;
                    else
                        GamePlayer.states.energy = GamePlayer.states.maxEnergy;

			}

#endif

			speed = isWalking ? playerWalkSpeed : playerRunSpeed;
			speed *= isSitdown ? 0.5f : 1.0f;

			if (isWalking) {
				headBob.HorizontalBobRange = bobRangeWalk;
				headBob.VerticalBobRange   = bobRangeWalk;
			} else {
				headBob.HorizontalBobRange = bobRangeRun;
				headBob.VerticalBobRange   = bobRangeRun;
			}

			input = new Vector2(horizontal, vertical);

			if (input.sqrMagnitude > 1)
				input.Normalize();

			if (isWalking != waswalking && characterController.velocity.sqrMagnitude > 0) {
				StopAllCoroutines();
				StartCoroutine(!isWalking ? fovKick.FOVKickUp() : fovKick.FOVKickDown());
			}

		}


		private void RotateView() {
			mouseLook.LookRotation(playerObject.transform, mainCameraObject.transform);
		}


		private void OnControllerColliderHit(ControllerColliderHit hit) {
			Rigidbody body = hit.collider.attachedRigidbody;

			if (collisionFlags == CollisionFlags.Below) return;
			if (body == null || body.isKinematic) return;

			body.AddForceAtPosition(characterController.velocity * 0.1f, hit.point, ForceMode.Impulse);

		}
		
	}
	
}