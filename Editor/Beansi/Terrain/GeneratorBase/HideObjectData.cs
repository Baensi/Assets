using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace EngineEditor.Terrain {

	/// <summary>
	/// Класс скрыватель объекта от лучей добра
	/// </summary>
	public class HideObjectData {

		private static int SHOW_RAYCAST = 0;
		private static int HIDE_RAYCAST = 2;

		//private int        defaultLayer;
		private GameObject gameObject;

		public HideObjectData(GameObject gameObject) {
			this.gameObject = gameObject;

			//defaultLayer = gameObject.layer; 
			gameObject.layer = HIDE_RAYCAST; // прятаем объект на слой недоступный для рейкаста
		}

		/// <summary>
		/// Восстанавливает слой для объекта
		/// </summary>
		public void Restore() {
			gameObject.layer = SHOW_RAYCAST;
		}

		public GameObject toGameObject() {
			return gameObject;
		}

	}

}
