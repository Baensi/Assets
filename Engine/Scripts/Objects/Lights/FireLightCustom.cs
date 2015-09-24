using System;
using UnityEngine;

namespace Engine.Objects {
	
	public class FireLightCustom : MonoBehaviour {
		
		private static float MAX_INTENSITY = 5.0f;
		
		[SerializeField] [Range(1.0f, 5.0f)] public float rndIntensity = 2.0f;
		[SerializeField] [Range(0.5f, 3.0f)] public float speedIntensityNoise = 1.0f;
		
		[SerializeField] [Range(1.0f, 15.0f)] public float speedMoveNoise = 2.0f;
		
		[SerializeField] [Range(1.0f, 10.0f)] public float distanceX = 1.0f;
		[SerializeField] [Range(1.0f, 10.0f)] public float distanceY = 1.0f;
		[SerializeField] [Range(1.0f, 10.0f)] public float distanceZ = 1.0f;
		
		[SerializeField] public bool fixX = false;
		[SerializeField] public bool fixY = false;
		[SerializeField] public bool fixZ = false;
		
		[SerializeField] public float defaultFixX = 0.0f;
		[SerializeField] public float defaultFixY = 0.0f;
		[SerializeField] public float defaultFixZ = 0.0f;
		
		private float rndValue = 0.0f;
		private bool isBurning = true;
		private Light lightObject;
		
		private void Start() {
			rndValue = UnityEngine.Random.value * 100.0f;
			lightObject = GetComponent<Light>();
		}
		
		private void Update() {
			
			if (!isBurning) return;
			
			float time = Time.time;
			
			lightObject.intensity = rndIntensity + (MAX_INTENSITY - rndIntensity) * Mathf.PerlinNoise(rndValue + time, rndValue + 1 + time * speedIntensityNoise);
			
			float x = fixX ? defaultFixX : Mathf.PerlinNoise(rndValue + 0 + time * speedMoveNoise, rndValue + 1 + time * speedMoveNoise) - distanceX;
			float y = fixY ? defaultFixY : Mathf.PerlinNoise(rndValue + 2 + time * speedMoveNoise, rndValue + 3 + time * speedMoveNoise) - distanceY;
			float z = fixZ ? defaultFixZ : Mathf.PerlinNoise(rndValue + 4 + time * speedMoveNoise, rndValue + 5 + time * speedMoveNoise) - distanceZ;
			
			transform.localPosition = Vector3.up + new Vector3(x, y, z);
			
		}
		
		public void Extinguish() {
			isBurning = false;
			lightObject.enabled = false;
		}
		
	}
	
}
