using System;
using System.Collections.Generic;
using UnityEngine;

namespace Engine.AI.Behavior {

	public class AIPoints : ScriptableObject {

		[SerializeField] public List<AIPoint> points;

			void OnEnable() {
				if (points == null)
					points = new List<AIPoint>();
			}

		public List<AIPoint> getPoints() {
			if (points == null)
				points = new List<AIPoint>();
			return points;
		}

		public void setPoints(List<AIPoint> points) {
			this.points = points;
		}

	}

}
