using UnityEngine;
using System.Collections;

namespace Engine.Objects.Doors {

	public interface IGameObjectAnimation {

		bool isComplete(GameObject gameObject, Vector3 vector, int direction, float speed);

		void update(GameObject gameObject, Vector3 vector, int direction, float speed);

	}

}