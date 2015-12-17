using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Engine.AI;
using Engine.AI.Behavior;
using EngineEditor.EUtils;

namespace EngineEditor.AI {

	public class AIWindow : EditorWindow {

		public const string id = "AIWindow";

		private static float cursorSize = 0.5f;

		private NavMeshAgent agent;
		private PathWalker   walker = null;

		private NavMeshPath path = new NavMeshPath();

        private bool showTrace        = true;
		private bool showEndPointMove = false;

		private AIPatrol patrol = null;
		private bool patrolEditMode = false;

		private float timeStamp = 0;

		private SceneView oldSceneView;

		public void setPatrol(AIPatrol patrol) {
			this.patrol = patrol;
        }

		public void setAgent(NavMeshAgent agent) {
			this.agent = agent;

			if (agent != null)
				walker = agent.GetComponent<PathWalker>();
			else
				walker = null;
        }

		void OnEnable() {
			Data.EditorFactory.getInstance().RegWindow(id, this);
		}

		void OnGUI() {

			if (walker != null) {

				GUILayout.Label("Ходок:", EditorStyles.boldLabel);
				showTrace = EditorGUILayout.Toggle("Показывать траекторию", showTrace);
				showEndPointMove = EditorGUILayout.Toggle("Перемещать конечную точку вручную", showEndPointMove);
				EditorGUILayout.Separator();

			}

			if (patrol != null) {

				GUILayout.Label("Патрулирование:", EditorStyles.boldLabel);
				patrolEditMode = EditorGUILayout.Toggle("Редактировать все точки",patrolEditMode);
				EditorGUILayout.Separator();
				
            }

			if (oldSceneView != null)
				oldSceneView.Repaint();

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
				Data.EditorFactory.getInstance().UnReg(id);
            } catch { }
		}

		public void OnSceneGUI(SceneView sceneView) {
			oldSceneView = sceneView;

            if (walker != null) {
				CheckMouse(sceneView);

				if (showTrace)
					DrawPath();

				DrawEndPoint();
				DrawSeeAngles();

			}

			if (patrol != null) {
				DrawPathPatrol(patrol);
			}

		}

		public void DrawPathPatrol(AIPatrol patrol) {
			foreach (AIPath path in patrol.getPaths()) {

				List<AIPoint> points = path.getPoints();

				if (points != null && points.Count > 0) {

					Vector3 startPosition = points[0].getData();

					foreach (AIPoint pos in points) {

						Handles.color = path.color;
						Handles.DrawLine(startPosition, pos.getData());

						Handles.color = new Color(1f, 0.98f, 0f);

						if (patrolEditMode) {
							pos.setData(Handles.DoPositionHandle(pos.getData(), Quaternion.Euler(0,0,0)));
						} else {
							DragHandleResult dhResult;
							float size = Vector3.Distance(SceneView.currentDrawingSceneView.camera.transform.position, pos.getData()) * 0.07f;
							Vector3 newPosition = UHandles.DragHandle(pos.getData(), size, Handles.CubeCap, Color.red, out dhResult);

							switch (dhResult) {
								case DragHandleResult.LeftMouseButtonDrag:
									pos.setData(newPosition);
									Handles.color = new Color(1f, 0, 0);
									Handles.Label(newPosition, newPosition.ToString());
									break;
							}
						}

						startPosition = pos.getData();

					}

				}

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

			switch (walker.State) {
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
					walker.setPoint(Handles.DoPositionHandle(walker.getPoint(), walker.transform.rotation));
				
				return;
			}
			
			Vector3    mousePosition = new Vector3(Event.current.mousePosition.x, sceneView.camera.pixelHeight - Event.current.mousePosition.y, 0);
			RaycastHit hitInfo = new RaycastHit();

			if (Physics.Raycast(sceneView.camera.ScreenPointToRay(mousePosition), out hitInfo))
				walker.setPoint(hitInfo.point);

		}

		private void DrawEndPoint() {

			Vector3 point = walker.getPoint();

            Handles.color = new Color(0f, 1f, 0f);
			Handles.DotCap(0, point, Quaternion.Euler(0, 0, 0), 0.2f);

			Quaternion startRot = Quaternion.Euler(90f,0,0);

			Handles.color = new Color(1f, 0f, 0f);
			Handles.DrawLine(point, point + (startRot * new Vector3(-cursorSize, 0, 0)));
			Handles.DrawLine(point, point + (startRot * new Vector3(cursorSize, 0, 0)));
			Handles.DrawLine(point, point + (startRot * new Vector3(0, cursorSize, 0)));
			Handles.DrawLine(point, point + (startRot * new Vector3(0, -cursorSize, 0)));

			if (Time.time - timeStamp >= 0.5f) {
				timeStamp = Time.time;
				NavMesh.CalculatePath(agent.transform.position, point, NavMesh.AllAreas, path);
			}

		}

		private void DrawPath() {

			NavMesh.CalculatePath(agent.transform.position, walker.getPoint(), NavMesh.AllAreas, path);

			Vector3 startPosition = agent.transform.position;

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
