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

		private NavMeshPath path = new NavMeshPath();

        private bool showTrace        = true;
		private bool showEndPointMove = false;

		private float timeStamp = 0;

		public void setAgent(NavMeshAgent agent) {
			this.agent = agent;
			walker = agent.GetComponent<PathWalker>();
        }

		void OnEnable() {
			Data.EditorFactory.getInstance().RegWindow(id, this);
		}

		void OnGUI() {
			showTrace = EditorGUILayout.Toggle("Показывать траекторию", showTrace);
			showEndPointMove = EditorGUILayout.Toggle("Перемещать конечную точку вручную", showEndPointMove);
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
				DrawSeeAngles();

			}

        }

		private void DrawSeeAngles() {

			Vector3 position    = walker.transform.position;
			Vector3 positionEnd = walker.transform.position + walker.transform.forward*(walker.seeDistance*1.05f);

			Handles.color = new Color(1f,0f,0f,0.5f);
            Handles.DrawLine(position, positionEnd);
			Handles.ConeCap(0, positionEnd, walker.transform.rotation, 0.2f);

			Vector3 rotation = walker.transform.rotation.eulerAngles;
            Quaternion angleR = Quaternion.Euler(0, rotation.y- walker.seeAngle * 0.5f - 90f, 0);
			Vector3 positionR = angleR * new Vector3(walker.seeDistance,0f,0f)+ walker.transform.forward;

			switch (walker.state) {
				case AgressionStateAI.Normal:
					Handles.color = new Color(0.05f, 0.9f, 0f, 0.2f);
				break;
				case AgressionStateAI.Warning:
					Handles.color = new Color(1f, 0.9f, 0f, 0.3f);
					break;
				case AgressionStateAI.Enemy:
					Handles.color = new Color(1f, 0f, 0f, 0.4f);
					break;
			}

			Handles.DrawSolidArc(position, walker.transform.up, positionR, walker.seeAngle, walker.seeDistance);

		}

		private void CheckMouse(SceneView sceneView) {

			if (!Event.current.control) {

				if (showEndPointMove)
					walker.setPoint(Handles.DoPositionHandle(walker.point, walker.transform.rotation));
				
				return;
			}
			
			Vector3    mousePosition = new Vector3(Event.current.mousePosition.x, sceneView.camera.pixelHeight - Event.current.mousePosition.y, 0);
			RaycastHit hitInfo = new RaycastHit();

			if (Physics.Raycast(sceneView.camera.ScreenPointToRay(mousePosition), out hitInfo))
				walker.setPoint(hitInfo.point);

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

			if (Time.time - timeStamp >= 0.5f) {
				timeStamp = Time.time;
				NavMesh.CalculatePath(agent.transform.position, walker.point, NavMesh.AllAreas, path);
			}

		}

		private void DrawPath() {
			Vector3 startPosition = agent.transform.position;
			Vector3 endPosition   = walker.point;

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
