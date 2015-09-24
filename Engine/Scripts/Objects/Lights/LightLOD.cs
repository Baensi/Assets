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
		private Light      light;
		private Transform  player;
		
		void Start(){
			this.player = SingletonNames.getPlayer().transform;
			this.light  = gameObject.GetComponent<Light>();
			
			defaultIntensity = light.intensity;
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
		
		private float calcRange(){
			return Mathf.Sqrt(Mathf.Pow(player.position.x-transform.position.x,2)
							 +Mathf.Pow(player.position.y-transform.position.y,2)
							 +Mathf.Pow(player.position.z-transform.position.z,2));
		}
		
		void Update(){
			
			float range = calcRange();
			
			if(range>=disableRange){

				if(gameObject.activeSelf)
					gameObject.SetActive(false);
				
			} else {

				if(!gameObject.activeSelf)
					gameObject.SetActive(true);
				
				if(useSmoothIntensity) {
				
					light.intensity = defaultIntensity * range / disableRange;
				
				}
				
			}
			
		}
		
	}
	
}