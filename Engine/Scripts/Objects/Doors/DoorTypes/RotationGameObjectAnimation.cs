using UnityEngine;
using System.Collections;

namespace Engine.Objects.Doors {

	public class RotationGameObjectAnimation : IGameObjectAnimation {

		private static IGameObjectAnimation instance;

		public static IGameObjectAnimation getInstance(){
			if(instance==null)
				instance = new RotationGameObjectAnimation();
			return instance;
		}

		private Vector3 normalization(Vector3 vector) {

			while (vector.x < 0)
				vector.x += 360;
			while (vector.y < 0)
				vector.y += 360;
			while (vector.z < 0)
				vector.z += 360;

			while (vector.x > 360)
				vector.x -= 360;
			while (vector.y > 360)
				vector.y -= 360;
			while (vector.z > 360)
				vector.z -= 360;

			return vector;

		}

		public bool isComplete(GameObject gameObject, Vector3 vector, int direction, float speed) {

			Vector3 rotation = gameObject.transform.rotation.eulerAngles;

			if (gameObject.transform.parent != null)
				rotation += gameObject.transform.parent.rotation.eulerAngles;

			vector   = normalization(vector);
			rotation = normalization(rotation);

				speed *= 1.5f;

			return Mathf.Abs(rotation.x - vector.x) <= speed &&
				   Mathf.Abs(rotation.y - vector.y) <= speed &&
				   Mathf.Abs(rotation.z - vector.z) <= speed;

		}

		public void update(GameObject gameObject, Vector3 vector, int direction, float speed) {

			Vector3 rotation = gameObject.transform.eulerAngles;
			Vector3 result = vector;

			if (gameObject.transform.parent != null)
				result += gameObject.transform.parent.eulerAngles;

			if (isComplete(gameObject, vector, direction, speed))
				gameObject.transform.rotation = Quaternion.Euler(result);
			else
				gameObject.transform.rotation = Quaternion.RotateTowards(Quaternion.Euler(rotation), Quaternion.Euler(result), speed * direction);

		}

	}

}