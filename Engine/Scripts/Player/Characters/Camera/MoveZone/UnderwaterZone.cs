using System;
using UnityEngine;
using Engine.Player.Movement.Movements;
using UnityStandardAssets.CrossPlatformInput;
using UnityStandardAssets.ImageEffects;

namespace Engine.Player.Movement {

	[RequireComponent(typeof(BoxCollider))]
	public class UnderwaterZone : MonoBehaviour {

		private VignetteAndChromaticAberration scriptBlur;
		private PlayerMovementController       playerController;
		private CharacterController            characterController;
		private Rigidbody                      playerRigidBody;
		private BoxCollider                    water;

		private Vector3 impulse = Vector3.zero;

		[SerializeField] public Color fogColor = new Color(0f,0.4f,0.7f,0.6f);
		[SerializeField] public float fogDistance = 0.6f;

		private float getPlayerTopYPoint() {
			return characterController.transform.position.y + characterController.height - 1.0f;
		}

		private float getWaterTopYPoint() {
			return this.transform.position.y;
		}

		void Start() {

			water               = GetComponent<BoxCollider>();
			playerController    = SingletonNames.getPlayer().GetComponent<PlayerMovementController>();
			scriptBlur          = SingletonNames.getMainCamera().GetComponent<VignetteAndChromaticAberration>();
			characterController = playerController.GetComponent<CharacterController>();
			playerRigidBody     = playerController.GetComponent<Rigidbody>();

			scriptBlur.enabled = false;
			RenderSettings.fog = false;

			RenderSettings.fogColor   = fogColor;
			RenderSettings.fogDensity = fogDistance;

		}

		void OnTriggerEnter(Collider other) {

			

		}

		

		void OnTriggerExit(Collider other) {

			//if (playerController.getMovementType().Equals(EMovementType.inground)) return;
			//if (getPlayerTopYPoint() < getWaterTopYPoint()) return;

			//	setInground();

		}

		void OnTriggerStay(Collider other) {

			if (//other.GetComponent<PlayerMovementController>() == null || // если мы не в воде или эффекты уже наложены, выходим
				getPlayerTopYPoint() > getWaterTopYPoint()) {

					impulse = playerController.getCurrentMovement().getImpulse();
						setInground();

						if (CrossPlatformInputManager.GetButton("Jump"))
							impulse.y += 50.0f;

					playerController.getCurrentMovement().addImpulse(impulse);

			} else {


				impulse = playerController.getCurrentMovement().getImpulse();
					setUnderwater();
				playerController.getCurrentMovement().addImpulse(impulse);


			}

		}

		void Update() {



		}

		void OnGUI() {

			GUI.Label(new Rect(20f, 20f, 120f, 20f), "camera: " + getPlayerTopYPoint().ToString());
			GUI.Label(new Rect(20f, 40f, 120f, 20f), "water: " + getWaterTopYPoint().ToString());

		}

		private void setInground() {
			RenderSettings.fog = false;
			scriptBlur.enabled = false;

			playerController.setMovementType(EMovementType.inground);
		}

		private void setUnderwater() {
			RenderSettings.fog = true;
			scriptBlur.enabled = true;
			
			playerController.setMovementType(EMovementType.underwater);
		}

	}

}
