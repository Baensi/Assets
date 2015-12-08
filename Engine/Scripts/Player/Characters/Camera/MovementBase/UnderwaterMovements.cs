using System;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using UnityStandardAssets.Utility;
using Random = UnityEngine.Random;

using Engine.Player.Animations;
using Engine.Player.Attack;


namespace Engine.Player.Movement.Movements {

	public class UnderwaterMovements : MonoBehaviour, IMovement {

		private Vector3 moveDir = Vector3.zero;

		private bool isJumping;
		private float playerJumpSpeed = 1.50f;
		private float playerWalkSpeed = 2.00f;

		private float m_StickToGroundForce = 5.0f;
		private float m_GravityMultiplier = 0.1f;

		private CollisionFlags collisionFlags;
		private Vector3 originalCameraPosition;
		private bool previouslyGrounded;

		private Actions actions;
		//private MovementAudioData audioData;
		private MouseLook mouseLook;
		private Camera mainCameraObject;
		private CurveControlledBob headBob;
		private LerpControlledBob jumpBob;
		private CharacterController characterController;
		private AttackController attackController;
		//private AudioSource audioSource;

		private Vector2 input;

		private GameObject playerObject;

		public void setUp(Actions actions,
						  MouseLook           mouseLook,
						  FOVKick             fovKick,
						  CurveControlledBob  headBob,
						  LerpControlledBob   jumpBob,
						  AttackController    attackController) {
				
				this.actions=actions;
				this.mouseLook=mouseLook;
				this.headBob=headBob;
				this.jumpBob=jumpBob;

				this.attackController=attackController;

				this.mainCameraObject    = SingletonNames.getMainCamera();
				this.playerObject        = SingletonNames.getPlayer();
				//this.audioSource         = playerObject.gameObject.AddComponent<AudioSource>();
				//this.audioData           = playerObject.GetComponent<MovementAudioData>();
				this.characterController = playerObject.GetComponent<CharacterController>();

				originalCameraPosition = mainCameraObject.transform.localPosition;

			}

		public void addImpulse(Vector3 velocity) {
			moveDir = velocity;
		}

		public Vector3 getImpulse() {
			return moveDir;
		}

		public void update(){

			RotateView();

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
				moveDir.y = -m_StickToGroundForce;
			} else {
				moveDir += Physics.gravity * m_GravityMultiplier * Time.fixedDeltaTime;
			}

			if (isJumping)
				moveDir.y = playerJumpSpeed;

			collisionFlags = characterController.Move(moveDir * Time.fixedDeltaTime);

			UpdateCameraPosition(speed);
			
		}


		private void UpdateCameraPosition(float speed) {

			Vector3 newCameraPosition;

			if (characterController.velocity.magnitude > 0 && characterController.isGrounded) {

				mainCameraObject.transform.localPosition = headBob.DoHeadBob(characterController.velocity.magnitude + speed);
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

			isJumping = CrossPlatformInputManager.GetButton(SingletonNames.Input.JUMP);

			if (Input.GetMouseButton(0))
				attackController.startAttack();

			speed = playerWalkSpeed;

			input = new Vector2(horizontal, vertical);

			if (input.sqrMagnitude > 1)
				input.Normalize();

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