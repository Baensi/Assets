using System;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Engine;

namespace Engine.AI.Behavior {

	/// <summary>
	/// Поведение в хождении
	/// </summary>
	public class PathBehavior : MonoBehaviour, IPathBehavior {

		/// <summary> Набор патрулей </summary>
		[SerializeField] public AIPatrol patrol;
		[SerializeField] public AIPoints stayPoints;

			void Start() {
				
			}

#if UNITY_EDITOR

		void OnDrawGizmos() {

			if (stayPoints != null)
				foreach (AIPoint point in stayPoints.getPoints()) {
					Gizmos.color = new Color(1f,1f,0,0.5f);
					Gizmos.DrawCube(point.getData(), new Vector3(1f, 1f, 1f));

					Gizmos.color = new Color(0,1f,0,0.5f);
					Gizmos.DrawLine(transform.position, point.getData());
				}

		}

#endif

		public AIPoints getStayPoints() {
			return stayPoints;
        }

		public void setStayPoints(AIPoints stayPoints) {
			this.stayPoints = stayPoints;
        }

		/// <summary>
		/// Возвращает все возможные патрули
		/// </summary>
		/// <returns></returns>
		public AIPatrol getPatrol() {
			return patrol;
		}

		public void setPatrol(AIPatrol patrol) {
			this.patrol = patrol;
		}

	}

}
