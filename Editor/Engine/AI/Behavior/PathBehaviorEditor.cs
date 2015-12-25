using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Engine.AI.Behavior;
using EngineEditor.Data;
using EngineEditor.Beansi;

namespace EngineEditor.AI {
	
	[CustomEditor(typeof(PathBehavior),true)]
	public class PathBehaviorEditor : Editor {

		private bool editMode = false;

		private PathBehavior pathBehavior;
		//private AIWindow     window;

		void OnEnable() {
			pathBehavior = target as PathBehavior;

			//window = (AIWindow)EditorFactory.getInstance().FindWindow(AIWindow.id);

			//if (window == null) {
			//	window = (AIWindow)EditorWindow.GetWindow(typeof(AIWindow));
			//	window.titleContent.text = "AI";
			//}


			//if (pathBehavior.getStayPoints() == null)
			//	pathBehavior.setStayPoints(new List<AIPoint>());

			//pathBehavior.getStayPoints().Add(new AIPoint(new Vector3(0, 0, 0)));

		}

		public override void OnInspectorGUI() {
			base.OnInspectorGUI();

			editMode = EditorGUILayout.Toggle("Включить редактирование", editMode);

			AIPatrol patrol = pathBehavior.getPatrol();
			
			if (patrol == null)
				pathBehavior.setPatrol(new AIPatrol(new List<AIPath>()));
			
			AIPatrolEditor.getInstance().OnInspectorGUI(patrol);
			AIStayPointsEditor.getInstance().OnInspectorGUI(pathBehavior);

		}

	}
	
}
