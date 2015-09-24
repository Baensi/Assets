using System;
using UnityEngine;

namespace Engine.Objects {
	
	public class FireLightPro : MonoBehaviour {
		
		[SerializeField] [Range(1.0f, 5.0f)] public float minIntensity = 3.0f;
		[SerializeField] [Range(1.0f, 5.0f)] public float maxIntensity = 5.0f;
		
		[SerializeField] [Range(0.001f, 3.0f)]  public float speedIntensityNoise = 0.001f;
		
		[SerializeField] [Range(0.001f, 5.0f)] public float speedMoveNoise = 0.5f;
		
		[SerializeField] [Range(-10.0f, 10.0f)] public float minDistanceX = 0.39f;
		[SerializeField] [Range(-10.0f, 10.0f)] public float minDistanceY = 0.0f;
		[SerializeField] [Range(-10.0f, 10.0f)] public float minDistanceZ = 0.24f;
		
		[SerializeField] [Range(-10.0f, 10.0f)] public float maxDistanceX = 0.39f;
		[SerializeField] [Range(-10.0f, 10.0f)] public float maxDistanceY = 0.0f;
		[SerializeField] [Range(-10.0f, 10.0f)] public float maxDistanceZ = 0.24f;
		
		[SerializeField] public bool fixX = false;
		[SerializeField] public bool fixY = false;
		[SerializeField] public bool fixZ = false;
		
		[SerializeField] public float defaultCenterX = 0.0f;
		[SerializeField] public float defaultCenterY = 0.0f;
		[SerializeField] public float defaultCenterZ = 0.0f;
		
		private float rndValue = 0.0f;
		
		private float rndX = 0.0f;
		private float rndY = 0.0f;
		private float rndZ = 0.0f;
		
		private float timeStamp = 0.0f;
		
		private bool isBurning = true;
		private Light lightObject;
		
		private void Start() {
			rndValue = UnityEngine.Random.value * 100.0f;
			lightObject = GetComponent<Light>();
			
			generate();
		}
		
		private void generate() {
			rndX = UnityEngine.Random.value*200-100;
			rndY = UnityEngine.Random.value*200-100;
			rndZ = UnityEngine.Random.value*200-100;
		}
		
		private void Update() {
			
			if (!isBurning) return;
			
			float time = Time.time;
			
			lightObject.intensity = minIntensity + (maxIntensity - minIntensity) * Mathf.PerlinNoise(rndValue + time, rndValue + 1 + time * speedIntensityNoise);
			
			float x = fixX ? defaultCenterX : minDistanceX + Mathf.PerlinNoise(time * speedMoveNoise, speedMoveNoise*rndX*10) * maxDistanceX + defaultCenterX;
			float y = fixY ? defaultCenterY : minDistanceY + Mathf.PerlinNoise(time * speedMoveNoise, speedMoveNoise*rndY*10) * maxDistanceY + defaultCenterY;
			float z = fixZ ? defaultCenterZ : minDistanceZ + Mathf.PerlinNoise(time * speedMoveNoise, speedMoveNoise*rndZ*10) * maxDistanceZ + defaultCenterZ;
			
			transform.localPosition = new Vector3(x, y, z);
			
		}
		
		public void Extinguish() {
			isBurning = false;
			lightObject.enabled = false;
		}
		
	}
	
}