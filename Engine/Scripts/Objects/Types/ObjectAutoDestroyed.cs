using System;
using UnityEngine;

namespace Engine.Objects.Types {

	/// <summary>
	/// Автоматически уничтожает объект через определённый промежуток времени (время жизни)
	/// </summary>
	public class ObjectAutoDestroyed : MonoBehaviour {

		[SerializeField] public float lifeTime = 1f;
		private float timeStamp;

		void OnStart() {
			timeStamp = Time.time;
		}
		
		void Update() {

			if (Time.time-timeStamp<lifeTime)
				return;

			Destroy(this.gameObject);

		}


	}

}
