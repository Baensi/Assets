using UnityEngine;

namespace Engine.AI.Behavior {

	public interface IMoveBehavior {

		void OnMoveIteration();

		void OnIdleIteration();

		void OnAttackIteration();

		bool isMinIdleDistance(Vector3 point1, Vector3 point2, float minMovDistance);

		bool isMinAttackDistance(Transform point1, Transform point2, float minAttackDistance, Transform target);

		bool isSeeDistanceGameObject(Transform targetObject, Transform seeObject);

		bool isSeeGameObject(Transform targetObject, Transform seeObject, float seeAngle, float seeDistance);

	}

}
