using System;
using System.Collections.Generic;
using UnityEngine;
namespace Engine.AI.Behavior {

	/// <summary>
	/// Набор точек патруля
	/// </summary>
	public class AIPath {

#if UNITY_EDITOR
		public Color color = new Color(1,0,0);
		public bool  markDeleted = false;
#endif

		private List<AIPoint> points;

		public AIPath(List<AIPoint> points) {
			this.points = points;
        }

		public List<AIPoint> getPoints() {
			return points;
		}

	}

}
