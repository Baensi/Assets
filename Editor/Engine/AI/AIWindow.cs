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

		private NavMeshPath path = new NavMeshPath();

        private bool showTrace        = true;
		private bool showEndPointMove = false;

		private bool stayPointsEditMode = false;
        private bool patrolEditMode     = false;

		private float timeStamp = 0;

		private PathWalker selected;
        private SceneView oldSceneView;

		void OnEnable() {
			Data.EditorFactory.getInstance().RegWindow(id, this);
		}

		void OnGUI() {

			selected = Selection.activeGameObject == null ? null : Selection.activeGameObject.GetComponent<PathWalker>();

			GUILayout.Label("Ходок:", EditorStyles.boldLabel);
			showTrace = EditorGUILayout.Toggle("Показывать траекторию", showTrace);
			showEndPointMove = EditorGUILayout.Toggle("Перемещать конечную точку вручную", showEndPointMove);
			EditorGUILayout.Separator();

			if (selected != null) {

				PathBehavior path = selected.getPathBehavior();

				if (path != null) {
					if (path.getPatrol() != null) {

						GUILayout.Label("Патрулирование:", EditorStyles.boldLabel);
						patrolEditMode = EditorGUILayout.Toggle("Редактировать все точки", patrolEditMode);
						EditorGUILayout.Separator();

					}

					if (path.getStayPoints() != null) {

						GUILayout.Label("Точки \"прогулки\":", EditorStyles.boldLabel);
						stayPointsEditMode = EditorGUILayout.Toggle("Редактировать все точки", stayPointsEditMode);
						EditorGUILayout.Separator();

					}
				}

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


			foreach (PathWalker walker in GameObject.FindObjectsOfType<PathWalker>()) {

				if (walker != null) {

					CheckMouse(sceneView);

					if (showTrace)
						DrawPath(walker);

					DrawEndPoint(walker);
					DrawSeeAngles(walker);

					DrawAttackSphere(walker);

				}

			}

			if (selected != null) {

				PathBehavior path = selected.getPathBehavior();

				if (path != null) {

					if (path.getPatrol() != null)
						DrawPathPatrol(path.getPatrol());

				}

			}

		}

		public void DrawAttackSphere(PathWalker walker) {

			Handles.color = new Color(1f,0f,0f,0.1f);
			Handles.SphereCap(0, walker.transform.position, Quaternion.Euler(0,0,0), walker.minAttackDistance);

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

		private void DrawSeeAngles(PathWalker walker) {

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
					Handles.color = new Color(0.05f, 0.9f, 0f, 0.07f);
				break;
				case AgressionStateAI.Warning:
					Handles.color = new Color(1f, 0.9f, 0f, 0.07f);
					break;
				case AgressionStateAI.Enemy:
					Handles.color = new Color(1f, 0f, 0f, 0.07f);
					break;
			}

			Handles.DrawSolidArc(position, walker.transform.up, positionR, walker.seeAngle, walker.seeDistance);

		}

		private void CheckMouse(SceneView sceneView) {

			if(selected==null)
				return;

			if (!Event.current.control) {

				if (showEndPointMove)
					selected.setPoint(Handles.DoPositionHandle(selected.getPoint(), selected.transform.rotation));
				
				return;
			}
			
			Vector3    mousePosition = new Vector3(Event.current.mousePosition.x, sceneView.camera.pixelHeight - Event.current.mousePosition.y, 0);
			RaycastHit hitInfo = new RaycastHit();

			if (Physics.Raycast(sceneView.camera.ScreenPointToRay(mousePosition), out hitInfo))
				selected.setPoint(hitInfo.point);

		}

		private void DrawEndPoint(PathWalker walker) {

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
				NavMesh.CalculatePath(walker.transform.position, point, NavMesh.AllAreas, path);
			}

		}

		private void DrawPath(PathWalker walker) {

			NavMesh.CalculatePath(walker.transform.position, walker.getPoint(), NavMesh.AllAreas, path);

			Vector3 startPosition = walker.transform.position;

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
