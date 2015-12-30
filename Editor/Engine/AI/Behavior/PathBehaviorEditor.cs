using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Engine.AI.Behavior;
using EngineEditor.Data;
using EngineEditor.Beansi;
using UnityEditor.SceneManagement;

namespace EngineEditor.AI {
	
	[CustomEditor(typeof(PathBehavior),true)]
	public class PathBehaviorEditor : Editor {

		private PathBehavior pathBehavior;

		void OnEnable() {
			pathBehavior = target as PathBehavior;
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

			Tables.DrawTable<AIPath>("Пути патрулирования", patrol.getPaths(), AIPathTableListener.getInstance(), true);

			EditorGUILayout.Separator();

			Tables.DrawTable<AIPoint>("Области, в которых AI \"гуляет\"", points.getPoints(), AIPointTableListener.getInstance());

			EditorUtility.SetDirty(target);

			if (GUILayout.Button("Сохранить всё", GUILayout.Width(120))) {

				GameObject newObject = new GameObject();
				newObject.transform.name = "save...";
				GameObject.DestroyImmediate(newObject);

				EditorSceneManager.SaveScene(EditorSceneManager.GetActiveScene());
			}

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
