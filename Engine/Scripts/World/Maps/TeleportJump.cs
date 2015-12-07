using UnityEngine;
using System.Collections;
using Engine.Player.Movement;

namespace Engine.Maps {

	[RequireComponent(typeof(BoxCollider))]
	public class TeleportJump : MonoBehaviour {
	
		[SerializeField] public Vector3 playerNewPosition;

		[SerializeField] public bool    useRotation;
		[SerializeField] public Vector3 playerNewRotation;

		private GameObject player;

		void Start() {
			player = SingletonNames.getPlayer();
		}

		void OnTriggerEnter(Collider other) {

			if (other.gameObject != player)
				return;

			player.transform.position = playerNewPosition;

			if (!useRotation)
				return;

			player.transform.rotation = Quaternion.Euler(playerNewRotation);
			player.transform.localRotation = Quaternion.Euler(playerNewRotation);
			player.GetComponent<PlayerMovementController>().mouseLook.Init(player.transform,SingletonNames.getMainCamera().transform);
			
		}

	}

}