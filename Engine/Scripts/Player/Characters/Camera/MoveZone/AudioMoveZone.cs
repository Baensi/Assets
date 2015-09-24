using System;
using UnityEngine;

namespace Engine.Player.Movement {
	
	[RequireComponent(typeof (BoxCollider))]
	public class AudioMoveZone : MonoBehaviour {

		[SerializeField] public MovementTypeZone typeZone;
		
		void OnTriggerEnter(Collider other) {

			PlayerMovementController player = other.gameObject.GetComponent<PlayerMovementController>();

			if(player==null) return;

			MovementAudioData movementAudioData = other.gameObject.GetComponent<MovementAudioData>();
			
			if(!movementAudioData.getZone().Equals(typeZone))
				movementAudioData.setZone(typeZone);
		
		}
		
	}
	
}