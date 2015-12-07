using System;
using System.Collections.Generic;
using UnityEngine;

namespace Engine.AI.Behavior {

	public class AIPatrol {

		private List<AIPath> paths;

		public AIPatrol(List<AIPath> paths) {
			this.paths = paths;
        }

		public List<AIPath> getPaths() {
			return paths;
		}

	}

}
