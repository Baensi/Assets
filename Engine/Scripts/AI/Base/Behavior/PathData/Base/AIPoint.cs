using System;
using UnityEngine;

namespace Engine.AI.Behavior {

	public class AIPoint {

		private Vector3 data;
		private float   range = 0f;

			public AIPoint() {
				data = new Vector3(0,0,0);
            }

			public AIPoint(Vector3 data) {
				this.data = data;
			}

		public void setRange(float range) {
			this.range = range;
		}

		public float getRange() {
			return range;
		}

		public Vector3 getData() {
			return data;
		}

		public void setData(Vector3 data) {
			this.data = data;
		}

	}

}
