using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Engine.AI.Behavior;
using EngineEditor.Data;
using EngineEditor.Baensi;
using UnityEditor.SceneManagement;

namespace EngineEditor.AI {
	
	[CustomEditor(typeof(PathBehavior),true)]
	public class PathBehaviorEditor : Editor {

		private PathBehavior pathBehavior;

		void OnEnable() {
			pathBehavior = target as PathBehavior;
		}

		void OnDestroy() {
			pathBehavior = null;
        }

		public override void OnInspectorGUI() {
			base.OnInspectorGUI();

			GUI.color = new Color(0.7f, 0.75f, 1f);
			EditorGUILayout.HelpBox("К скрипту прикреплён файл конфигураций, \nне забывайте сохранять изменения :)", MessageType.Info, true);
			GUI.color = Color.white;

			AIPatrol patrol = pathBehavior.getPatrol();
			AIPoints points = pathBehavior.getStayPoints();

			if (patrol == null)
				pathBehavior.setPatrol(patrol = ScriptableObject.CreateInstance<AIPatrol>());

			if (points == null)
				pathBehavior.setStayPoints(points = ScriptableObject.CreateInstance<AIPoints>());

			Tables.DrawTreeTable<AIPath,AIPoint>("Пути патрулирования", patrol.getPaths(), AIPathTableListener.getInstance(), true);

			GUILayout.BeginHorizontal();
			EditorGUILayout.Space();
			if (GUILayout.Button("Отделить патруль от группы",GUILayout.Width(180)))
				if (EditorUtility.DisplayDialog("Пересоздание", "Вы действительно хотите пересоздать патруль?\nЭто приведёт к тому, что данный объект получит УНИКАЛЬНЫЙ маршрут, независимый от группы", "Да", "Нет"))
					DetachPatrol();
			GUILayout.EndHorizontal();

			EditorGUILayout.Separator();

			Tables.DrawTable<AIPoint>("Области, в которых AI \"гуляет\"", points.getPoints(), AIPointTableListener.getInstance());

			GUILayout.BeginHorizontal();
			EditorGUILayout.Space();
			if (GUILayout.Button("Отделить точки от группы", GUILayout.Width(180)))
				if (EditorUtility.DisplayDialog("Пересоздание", "Вы действительно хотите пересоздать точки?\nЭто приведёт к тому, что данный объект получит УНИКАЛЬНЫЙ набор точек, независимый от группы", "Да", "Нет"))
					DetachPoints();
			GUILayout.EndHorizontal();

			EditorUtility.SetDirty(target);

			if (GUILayout.Button("Сохранить всё", GUILayout.Width(120))) {

				GameObject newObject = new GameObject();
				newObject.transform.name = "save...";
				GameObject.DestroyImmediate(newObject);

				EditorSceneManager.SaveScene(EditorSceneManager.GetActiveScene());
			}

		}

		private void DetachPatrol() {
			List<AIPath> newPaths = new List<AIPath>();

			foreach (AIPath path in pathBehavior.getPatrol().getPaths()) {

				AIPath newPath = new AIPath();
				newPath.Create(new List<AIPoint>());
				newPath.color = path.color;

				foreach (AIPoint point in path.getPoints()) {

					AIPoint newPoint = ScriptableObject.CreateInstance<AIPoint>();
					newPoint.Create(point.getData());
					newPath.getPoints().Add(newPoint);

				}

				newPaths.Add(newPath);

			}

			AIPatrol newPatrol = ScriptableObject.CreateInstance<AIPatrol>();
			newPatrol.Create(newPaths);
			pathBehavior.setPatrol(newPatrol);
		}

		private void DetachPoints() {

			List<AIPoint> newPointList = new List<AIPoint>();

				foreach (AIPoint point in pathBehavior.getStayPoints().getPoints()) {

					AIPoint newPoint = ScriptableObject.CreateInstance<AIPoint>();
					newPoint.Create(point.getData(), point.getRange());
					newPointList.Add(newPoint);

				}

			AIPoints newPoints = ScriptableObject.CreateInstance<AIPoints>();
			newPoints.Create(newPointList);
            pathBehavior.setStayPoints(newPoints);

		}

		void OnSceneGUI() {

			if (pathBehavior != null) {

				if (pathBehavior.getStayPoints() != null && pathBehavior.getStayPoints().getPoints() != null)
					Tables.DoHandlers<AIPoint>(pathBehavior.getStayPoints().getPoints(), AIPointTableListener.getInstance());

				if (pathBehavior.getPatrol() != null && pathBehavior.getPatrol().getPaths() != null)
					Tables.DoHandlers<AIPath>(pathBehavior.getPatrol().getPaths(), AIPathTableListener.getInstance());

			}

		}

	}
	
}
