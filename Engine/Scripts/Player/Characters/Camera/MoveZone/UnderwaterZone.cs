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

		[SerializeField] public Color fogColor = new Color(0f,0.4f,0.7f,0.6f);
		[SerializeField] public float fogDistance = 0.6f;

		public float getPlayerTopYPoint() {
			return characterController.transform.position.y + characterController.height - 1.0f;
		}

		public float getPlayerBodyTopY() {
			return characterController.transform.position.y - 2f;
		}

		public float getWaterTopYPoint() {
			return this.transform.position.y;
		}

		void Start() {
			
			playerController    = SingletonNames.getPlayer().GetComponent<PlayerMovementController>();
			scriptBlur          = SingletonNames.getMainCamera().GetComponent<VignetteAndChromaticAberration>();
			characterController = playerController.GetComponent<CharacterController>();
			
			gameObject.layer = SingletonNames.Layers.IGNORE_RAYCAST; // коллидер подводной зоны не должен мешать рейкасту

			scriptBlur.enabled = false;
			RenderSettings.fog = false;

			

		}

		/// <summary>
		/// Персонаж входит в воду
		/// </summary>
		/// <param name="other"></param>
		void OnTriggerEnter(Collider other) {

			if (getPlayerTopYPoint() > getWaterTopYPoint())
				setInwater();
			else
				setUnderwater();

		}

		/// <summary>
		/// Покидаем зону воды
		/// </summary>
		/// <param name="other"></param>
		void OnTriggerExit(Collider other) {

			Vector3 impulse = playerController.getCurrentMovement().getImpulse();
				setInground();
			playerController.getCurrentMovement().addImpulse(impulse);
			
		}

		void OnTriggerStay(Collider other) {
			
			Vector3 impulse;

			if (getPlayerTopYPoint() > getWaterTopYPoint()) {

				impulse = playerController.getCurrentMovement().getImpulse();
					setInwater();
				playerController.getCurrentMovement().addImpulse(impulse);

				if (getPlayerBodyTopY()+1f > getWaterTopYPoint()) {

					playerController.transform.position = new Vector3(playerController.transform.position.x,
																	  this.transform.position.y,
																	  playerController.transform.position.z);

				}

			} else {

				impulse = playerController.getCurrentMovement().getImpulse();
					setUnderwater();
				playerController.getCurrentMovement().addImpulse(impulse);

			}

		}

		void Update() {

			RenderSettings.fogColor = fogColor;
			RenderSettings.fogDensity = fogDistance;

		}

		void OnGUI() {

			
		}

		private void setInground() {
			RenderSettings.fog = false;
			scriptBlur.enabled = false;

			playerController.setMovementType(EMovementType.inground);
		}

		private void setInwater() {
			RenderSettings.fog = false;
			scriptBlur.enabled = false;

			playerController.setMovementType(EMovementType.inwater);

			InwaterMovements movement = playerController.getCurrentMovement() as InwaterMovements;
			movement.setUpWaterObject(this);

		}

		private void setUnderwater() {
			RenderSettings.fog = true;
			scriptBlur.enabled = true;
			
			playerController.setMovementType(EMovementType.underwater);
		}

	}

}
