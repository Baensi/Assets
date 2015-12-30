using System;
using UnityEngine;

namespace Engine.AI.Behavior {

	public class AIPoint : ScriptableObject {

#if UNITY_EDITOR
		public bool editMode = false;
#endif

		[SerializeField] public Vector3 data;
		[SerializeField] public float   range = 0f;

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
