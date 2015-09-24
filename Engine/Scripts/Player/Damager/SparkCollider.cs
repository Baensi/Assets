using UnityEngine;
using System.Collections;

using Engine.Player.Movement;

namespace Engine.Player.Weapon.Sparks {
	
	public class SparkCollider : MonoBehaviour {

		public ParticleSystem sparks;

		void Start(){
			//sparks.Stop();
		}

		void OnTriggerEnter(Collider other) {
			
			//SwordObject sword = other.gameObject.GetComponent<SwordObject>();
			
			//if(sword==null) return;
			
			//sparks.transform.position = sword.transform.position;
			//sparks.Play();
			
		}


	}

}