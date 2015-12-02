using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Engine.AI;

namespace EngineEditor.AI {

	public class PathWalkerWindow : EditorWindow {

		private static float cursorSize = 0.5f;

		private NavMeshAgent agent;
		private PathWalker walker;

		private NavMeshPath path = new NavMeshPath();
        private bool showTrace = true;

		private float timeStamp = 0;

		public void setAgent(NavMeshAgent agent) {
			this.agent = agent;
			walker = agent.GetComponent<PathWalker>();
        }

		void OnEnable() {

		}

		void OnGUI() {
			showTrace = EditorGUILayout.Toggle("Показывать траекторию", showTrace);
			//this.Repaint();
		}

		void OnFocus() {
			try { 
				SceneView.onSceneGUIDelegate -= this.OnSceneGUI;
				SceneView.onSceneGUIDelegate += this.OnSceneGUI;
			} catch { }
			}
		void OnDestroy() {
			try {
				SceneView.onSceneGUIDelegate -= this.OnSceneGUI;
			} catch { }
		}

		public void OnSceneGUI(SceneView sceneView) {

			CheckMouse(sceneView);

			if (showTrace)
				DrawPath();

			DrawEndPoint();

        }

		private void CheckMouse(SceneView sceneView) {

			if (!Event.current.control)
				return;

			Vector3 mousePosition = new Vector3(Event.current.mousePosition.x, sceneView.camera.pixelHeight - Event.current.mousePosition.y, 0);

			RaycastHit hitInfo = new RaycastHit();

			if (Physics.Raycast(sceneView.camera.ScreenPointToRay(mousePosition), out hitInfo)) {
				walker.setPoint(hitInfo.point);
				NavMesh.CalculatePath(agent.transform.position, walker.point, NavMesh.AllAreas, path);
			}

			DrawPath();

		}

		private void DrawEndPoint() {
			Handles.color = new Color(0f, 1f, 0f);
			Handles.DotCap(0, walker.point, Quaternion.Euler(0, 0, 0), 0.2f);

			Quaternion startRot = Quaternion.Euler(90f,0,0);

			Handles.color = new Color(1f, 0f, 0f);
			Handles.DrawLine(walker.point, walker.point + (startRot * new Vector3(-cursorSize, 0, 0)));
			Handles.DrawLine(walker.point, walker.point + (startRot * new Vector3(cursorSize, 0, 0)));
			Handles.DrawLine(walker.point, walker.point + (startRot * new Vector3(0, cursorSize, 0)));
			Handles.DrawLine(walker.point, walker.point + (startRot * new Vector3(0, -cursorSize, 0)));
		}

		private void DrawPath() {
			Vector3 startPosition = agent.transform.position;
			Vector3 endPosition   = walker.point;


			if (Time.time-timeStamp >= 0.5f) {
				timeStamp = Time.time;
                NavMesh.CalculatePath(startPosition, endPosition, NavMesh.AllAreas, path);
			}

            foreach (Vector3 pos in path.corners) {

				Handles.color = new Color(0f,1f,0f);
				Handles.DrawLine(startPosition, pos);
				Handles.color = new Color(1f, 0.98f, 0f);
				Handles.DotCap(0, pos, Quaternion.Euler(0, 0, 0), 0.2f);

				startPosition = pos;
			}

		}

	}


}
