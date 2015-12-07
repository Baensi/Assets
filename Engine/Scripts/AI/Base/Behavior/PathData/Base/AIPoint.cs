using System;
using UnityEngine;

namespace Engine.AI.Behavior {

	public class AIPoint {

		private Vector3 data;

			public AIPoint() {
				data = new Vector3(0,0,0);
            }

			public AIPoint(Vector3 data) {
				this.data = data;
			}

		public Vector3 getData() {
			return data;
		}

		public void setData(Vector3 data) {
			this.data = data;
		}

	}

}
