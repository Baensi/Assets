using System;
using System.Collections.Generic;
using UnityEngine;

namespace Engine.Objects.Doors {

	public class PositionGameObjectAnimator : IGameObjectAnimation {

		private static IGameObjectAnimation instance;

		public static IGameObjectAnimation getInstance() {
			if (instance == null)
				instance = new PositionGameObjectAnimator();
			return instance;
		}

		public bool isComplete(GameObject gameObject, Vector3 vector, int direction, float speed) {

			if(gameObject.transform.parent!=null)
				vector += gameObject.transform.parent.position;

			return Mathf.Abs(gameObject.transform.position.x - vector.x) <= speed &&
				   Mathf.Abs(gameObject.transform.position.y - vector.y) <= speed &&
				   Mathf.Abs(gameObject.transform.position.z - vector.z) <= speed;

		}

		private Vector3 iterationTo(Vector3 v1, Vector3 v2, float speed) {

			return new Vector3( v1.x == v2.x ? v1.x : v1.x + (v1.x > v2.x ? -speed : speed),
								v1.y == v2.y ? v1.y : v1.y + (v1.y > v2.y ? -speed : speed),
								v1.z == v2.z ? v1.z : v1.z + (v1.z > v2.z ? -speed : speed));

		}

		public void update(GameObject gameObject, Vector3 vector, int direction, float speed) {

			Vector3 result = vector;
			Vector3 position = gameObject.transform.position;

			if(gameObject.transform.parent!=null)
				result += gameObject.transform.parent.position;

			if (isComplete(gameObject, vector, direction, speed))
				gameObject.transform.position = result;
			else
				gameObject.transform.position = iterationTo(position, result, speed);

		}

    }

}
