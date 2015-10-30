using System;
using UnityEngine;
using UnityStandardAssets.Effects;

namespace Engine.Objects {
	
	[RequireComponent(typeof(Light))]
	public class LightLOD : MonoBehaviour {
		
		[SerializeField] public float maxRange           = 10f;
		[SerializeField] public float disableRange       = 15f;
		[SerializeField] public bool  useSmoothIntensity = true;

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

		public Light toLight() {
			return gameObject.GetComponent<Light>();
		}

		void OnValidate(){

			if (maxRange > disableRange)
				maxRange = disableRange;

			if (disableRange < maxRange)
				disableRange = maxRange;

				// запрещаям часть скриптов
			if(useSmoothIntensity && 
			  (gameObject.GetComponent<FireLight>()!=null || gameObject.GetComponent<FireLightPro>()!=null || gameObject.GetComponent<FireLightCustom>()!=null))
				useSmoothIntensity=false;

		}

#endif

		void OnGUI() {

			GUI.Label(new Rect(30f, 96, 120, 30), "range="+range.ToString());

		}

		void Update(){

			range = Vector3.Distance(player.position, transform.position);
			
			if(range>=disableRange){

				if (currentLight.enabled)
					currentLight.enabled = false;
				
			} else {

				if (!currentLight.enabled)
					currentLight.enabled = true;

				if (useSmoothIntensity && range > maxRange) {

					currentLight.intensity = defaultIntensity / deltaRange * (deltaRange - range + maxRange);

					return;
				}

				currentLight.intensity = defaultIntensity;
				
			}
			
		}
		
	}
	
}