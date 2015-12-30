using System;
using UnityEngine;
using UnityStandardAssets.Effects;

namespace Engine.Objects {
	
	[RequireComponent(typeof(Light))]
	public class LightLOD : MonoBehaviour {
		
		[SerializeField] public float disableRange = 15f;
		
		private Light      currentLight;
		private Transform  player;

		void Start(){
			this.player = SingletonNames.getPlayer().transform;
			this.currentLight  = gameObject.GetComponent<Light>();
		}

#if UNITY_EDITOR

		[SerializeField] public bool drawGizmos = true;

		void OnDrawGizmosSelected() {
			if (!drawGizmos)
				return;

			Color colorDisable = new Color(1f, 0f, 0f, 0.2f);

			Gizmos.color = colorDisable;
			Gizmos.DrawSphere(transform.position, disableRange);

		}

		void OnValidate() {
			if (disableRange < 0)
				disableRange = 0;
		}

#endif

		void Update(){

			if(Vector3.Distance(player.position, transform.position) >= disableRange){
				if (currentLight.enabled)
					currentLight.enabled = false;
			} else {
				if (!currentLight.enabled)
					currentLight.enabled = true;
			}
			
		}
		
	}
	
}