using System;
using UnityEngine;

namespace Engine.Scripts.Magic {

	class MagicDestroyTimer : MonoBehaviour {

		[SerializeField] public float destroyTime = 10f;

		private float currentTime = 0f;

		void Start() {
			currentTime = Time.deltaTime;
		}

		void Update() {
			if(Time.deltaTime-currentTime>=destroyTime)
				Destroy(this.gameObject);
		}

	}

}
