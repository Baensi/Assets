using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Engine.AI.Behavior;
using EngineEditor.Data;

namespace EngineEditor.AI {

	public class AIPatrolEditor {

		private List<AIPath> trashcan = new List<AIPath>();

		private static AIPatrolEditor instance;

		public static AIPatrolEditor getInstance() {
			if (instance == null)
				instance = new AIPatrolEditor();
			return instance;
        }

		public bool OnInspectorGUI(AIPatrol patrol) {
			
			if (patrol == null)
				return true;

			List<AIPath> paths = patrol.getPaths();

			if (paths == null) {
				paths = new List<AIPath>();
                patrol.setPaths(paths);
			}

			if (DrawPaths(ref paths, patrol)) {

				GUILayout.Label("Всего " + paths.Count.ToString() + " путей", EditorStyles.boldLabel);

				EditorGUILayout.Separator();

				//EditorGUILayout.BeginHorizontal();

				if (GUILayout.Button("Добавить путь"))
					patrol.getPaths().Add(new AIPath(new List<AIPoint>()));

				//EditorGUILayout.EndHorizontal();

			}

			return true;
        }

		private bool DrawPaths(ref List<AIPath> paths, AIPatrol patrol) {
			if (paths.Count > 0)
				foreach (AIPath path in paths) {
					if (!path.markDeleted)
						if (!AIPathEditor.getInstance().OnInspectorGUI(patrol, path))
							return false;
						else
							trashcan.Add(path);
				}

			if (trashcan.Count > 0) {
				foreach (AIPath path in trashcan)
					paths.Remove(path);
				trashcan.Clear();
			}

			return true;
		}

	}

}
