using System;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Engine.AI.Behavior {

	/// <summary>
	/// Поведение в хождении
	/// </summary>
	public class PathBehavior : MonoBehaviour, IPathBehavior {

		/// <summary> Набор патрулей </summary>
		[SerializeField] private AIPatrol      patrol = new AIPatrol(new List<AIPath>());
		[SerializeField] private List<AIPoint> stayPoints = new List<AIPoint>();

			void Start() {
				
			}

#if UNITY_EDITOR

		void OnDrawGizmos() {

			if (stayPoints != null)
				foreach (AIPoint point in stayPoints) {
					Gizmos.color = new Color(1f,1f,0,0.5f);
					Gizmos.DrawCube(point.getData(), new Vector3(1f, 1f, 1f));

					Gizmos.color = new Color(0,1f,0,0.5f);
					Gizmos.DrawLine(transform.position, point.getData());
				}

			if (patrol != null) {

				foreach (AIPath path in patrol.getPaths()) {

					List<AIPoint> points = path.getPoints();

					Vector3 startPosition = points[0].getData();

					foreach (AIPoint pos in points) {

						Gizmos.color = path.color;
						Gizmos.DrawLine(startPosition, pos.getData());

						Gizmos.color = new Color(1f, 0.98f, 0f);
						pos.setData(Handles.DoPositionHandle(pos.getData(), Quaternion.Euler(0, 0, 0)));
						startPosition = pos.getData();

					}
				}
			}

		}

#endif


		public List<AIPoint> getStayPoints() {
			return stayPoints;
        }

		public void setStayPoints(List<AIPoint> stayPoints) {
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
