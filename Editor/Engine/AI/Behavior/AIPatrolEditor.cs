using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Engine.AI.Behavior;

namespace EngineEditor.AI {

	public class AIPatrolEditor {

		private List<AIPath> trashcan = new List<AIPath>();

		private static AIPatrolEditor instance;

		public static AIPatrolEditor getInstance() {
			if (instance == null)
				instance = new AIPatrolEditor();
			return instance;
        }

		public void OnInspectorGUI(AIPatrol patrol) {

			foreach (AIPath path in patrol.getPaths()) {
				if (!path.markDeleted)
					AIPathEditor.getInstance().OnInspectorGUI(patrol, path);
				else
					trashcan.Add(path);
            }

			EditorGUILayout.Separator();

			EditorGUILayout.BeginHorizontal();

				if (GUILayout.Button("Добавить путь"))
					patrol.getPaths().Add(new AIPath(new List<AIPoint>()));

			EditorGUILayout.EndHorizontal();
			

			if (trashcan.Count > 0) {
				foreach (AIPath path in trashcan)
					patrol.getPaths().Remove(path);
				trashcan.Clear();
			}

		}

	}

}
