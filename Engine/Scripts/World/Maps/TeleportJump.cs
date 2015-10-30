using UnityEngine;
using System.Collections;

namespace Engine.Maps {

	[RequireComponent(typeof(BoxCollider))]
	public class TeleportJump : MonoBehaviour {
	
		[SerializeField] public Vector3 playerNewPosition;
		[SerializeField] public Vector3 playerNewRotation;

		private GameObject player;

		void Start() {
			player = SingletonNames.getPlayer();
		}

		void OnTriggerEnter(Collider other) {

			if (other.gameObject != player)
				return;

			player.transform.position = playerNewPosition;
			player.transform.rotation = Quaternion.Euler(playerNewRotation);
			
		}

	}

}