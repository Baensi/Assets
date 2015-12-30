using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Engine;

namespace EngineEditor.Terrain {

	/// <summary>
	/// Класс скрыватель объекта от лучей добра
	/// </summary>
	public class HideObjectData {
		
		private GameObject gameObject;

		public HideObjectData(GameObject gameObject) {
			this.gameObject = gameObject;
			
			gameObject.layer = SingletonNames.Layers.IGNORE_RAYCAST; // прятаем объект на слой недоступный для рейкаста
		}

		/// <summary>
		/// Восстанавливает слой для объекта
		/// </summary>
		public void Restore() {

			gameObject.layer = SingletonNames.Layers.DEFAULT;

		}

		public GameObject toGameObject() {
			return gameObject;
		}

	}

}
