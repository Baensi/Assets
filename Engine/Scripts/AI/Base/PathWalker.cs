using UnityEngine;

namespace Engine.AI {

	public class PathWalker : MonoBehaviour {

		[SerializeField] public float   minMove = 0.1f;
		[SerializeField] public Vector3 point;

		private NavMeshAgent agent;

		private float   timeStamp;
		private Vector3 oldPoint;

		public void setPoint(Vector3 point) {
			this.point = point;
		}

		void Start() {
			agent = gameObject.GetComponent<NavMeshAgent>();
		}

		void Update() {
			agent.SetDestination(point);
		}

	}

}
