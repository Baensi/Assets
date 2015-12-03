using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Engine.AI;

namespace EngineEditor.AI {

	public class PathWalkerWindow : EditorWindow {

		public const string id = "PathWalkerWindow";

		private static float cursorSize = 0.5f;

		private NavMeshAgent agent;

		private PathWalker walker = null;
		private SeeAI      see = null;

		private NavMeshPath path = new NavMeshPath();
        private bool showTrace = true;

		private float timeStamp = 0;

		public void setAgent(NavMeshAgent agent) {
			this.agent = agent;
			walker = agent.GetComponent<PathWalker>();
        }

		public void setSee(SeeAI see) {
			this.see = see;
		}

		void OnEnable() {
			Data.EditorFactory.getInstance().RegWindow(id, this);
		}

		void OnGUI() {
			showTrace = EditorGUILayout.Toggle("Показывать траекторию", showTrace);
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

			if (walker != null) {
				CheckMouse(sceneView);

				if (showTrace)
					DrawPath();

				DrawEndPoint();
			}

			if (see != null) {

				DrawSeeAngles();

			}

        }

		private void DrawSeeAngles() {

			Vector3 angleR = new Vector3(0f,see.seeAngle, 0f);
			Vector3 angleL = new Vector3(0f,-see.seeAngle, 0f);

			Vector3 position    = see.transform.position;
			Vector3 positionEnd = see.transform.position + see.transform.forward*3f;

			Handles.color = new Color(1f,0f,0f,0.5f);
            Handles.DrawLine(position, positionEnd);
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
