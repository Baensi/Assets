using System;
using UnityEngine;
using UnityStandardAssets.Effects;

namespace Engine.Objects {
	
	[RequireComponent(typeof(Light))]
	public class LightLOD : MonoBehaviour {
		
		[SerializeField] public float maxRange           = 10f;
		[SerializeField] public float disableRange       = 15f;
		[SerializeField] public bool  useSmoothIntensity = true;
		
		private float      defaultIntensity = 1f;
		private Light      currentLight;
		private Transform  player;
		
		void Start(){
			this.player = SingletonNames.getPlayer().transform;
			this.currentLight  = gameObject.GetComponent<Light>();

			defaultIntensity = currentLight.intensity;
		}

		//public Light toObject() {
		//	return light;
		//}

		void OnValidate(){
			
				// запрещаям часть скриптов
			if(useSmoothIntensity && 
			  (gameObject.GetComponent<FireLight>()!=null || gameObject.GetComponent<FireLightPro>()!=null || gameObject.GetComponent<FireLightCustom>()!=null))
				useSmoothIntensity=false;
			
		}
		
		void Update(){

			float range = Vector3.Distance(player.position, transform.position);
			
			if(range>=disableRange){

				if(gameObject.activeSelf)
					gameObject.SetActive(false);
				
			} else {

				if(!gameObject.activeSelf)
					gameObject.SetActive(true);
				
				if(useSmoothIntensity) {

					currentLight.intensity = defaultIntensity * range / disableRange;
				
				}
				
			}
			
		}
		
	}
	
}