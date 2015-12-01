using UnityEngine;

namespace Engine.AI {

	public class PathWalker : MonoBehaviour {

		private NavMeshAgent agent;

		void Start() {
			agent = gameObject.GetComponent<NavMeshAgent>();

		}

	}

}
