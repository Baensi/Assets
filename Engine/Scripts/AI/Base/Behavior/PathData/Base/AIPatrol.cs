using System;
using System.Collections.Generic;
using UnityEngine;

namespace Engine.AI.Behavior {

	public class AIPatrol : ScriptableObject {

		[SerializeField] public List<AIPath> paths;

			void OnEnable() {
				if (paths == null)
					paths = new List<AIPath>();
			}

		public List<AIPath> getPaths() {
			return paths;
		}

		public void setPaths(List<AIPath> paths) {
			this.paths = paths;
		}

	}

}
