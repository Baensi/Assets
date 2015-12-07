using System;
using System.Collections.Generic;
using UnityEngine;

namespace Engine.AI.Behavior {

	/// <summary>
	/// Поведение в хождении
	/// </summary>
	public class PathBehavior : MonoBehaviour, IPathBehavior {

		/// <summary> Набор патрулей </summary>
		private AIPatrol patrol;

			void Start() { }

		/// <summary>
		/// Возвращает все возможные патрули
		/// </summary>
		/// <returns></returns>
		public AIPatrol getPatrol() {
			return patrol;
		}

		public void setPatrol(AIPatrol patrol) {
			this.patrol = patrol;
		}

	}

}
