using System;
using UnityEngine;

namespace Engine.Objects.Types {

	/// <summary>
	/// Автоматически уничтожает объект через определённый промежуток времени (время жизни)
	/// </summary>
	public class ObjectAutoDestroyed : MonoBehaviour {

		[SerializeField] public float lifeTime = 1f;

		/// <summary>
		/// Когда объект загружен
		/// </summary>
		void OnStart() {
			Destroy(gameObject, lifeTime); // откладываем самоуничтожение на время жизни
		}

	}

}
