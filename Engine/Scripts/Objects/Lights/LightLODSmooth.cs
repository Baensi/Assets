using System;
using UnityEngine;
using UnityStandardAssets.Effects;

namespace Engine.Objects {
	
	[RequireComponent(typeof(Light))]
	public class LightSmoothLOD : MonoBehaviour {
		
		[SerializeField] public float maxRange           = 10f;
		[SerializeField] public float disableRange       = 15f;

		private float      deltaRange;
		private float      defaultIntensity = 1f;
		private Light      currentLight;
		private Transform  player;

		private float range;

		void Start(){
			this.player = SingletonNames.getPlayer().transform;
			this.currentLight  = gameObject.GetComponent<Light>();

			defaultIntensity = currentLight.intensity;
			deltaRange       = disableRange - maxRange;
		}

#if UNITY_EDITOR

		[SerializeField] public bool drawGizmos = true;

		void OnDrawGizmosSelected() {
			if (!drawGizmos)
				return;

			Color colorRange = new Color(1f, 1f, 0f, 0.3f);
			Color colorDisable = new Color(1f, 0f, 0f, 0.2f);

			Gizmos.color = colorRange;
			Gizmos.DrawSphere(transform.position, maxRange);

			Gizmos.color = colorDisable;
			Gizmos.DrawSphere(transform.position, disableRange);

		}

		void OnValidate(){

			if (maxRange < 0)
				maxRange = 0;

			if (disableRange < 0)
				disableRange = 0;

			if (maxRange > disableRange)
				maxRange = disableRange;

			if (disableRange < maxRange)
				disableRange = maxRange;
			
		}

#endif

		void Update(){

			range = Vector3.Distance(player.position, transform.position);
			
			if(range>=disableRange){

				if (currentLight.enabled)
					currentLight.enabled = false;
				
			} else {

				if (!currentLight.enabled)
					currentLight.enabled = true;

				if (range > maxRange) {

					currentLight.intensity = defaultIntensity / deltaRange * (deltaRange - range + maxRange);

					return;
				}

				currentLight.intensity = defaultIntensity;
				
			}
			
		}
		
	}
	
}