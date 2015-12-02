using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Engine.AI;

namespace EngineEditor.AI {

	[CustomEditor(typeof(PathWalker))]
	public class PathWalkerEditor : Editor {

		private PathWalkerWindow window;
        private NavMeshAgent agent;

		void OnEnable() {
			var walker = target as PathWalker;

            agent = walker.GetComponent<NavMeshAgent>();
        }

		public override void OnInspectorGUI() {

			base.OnInspectorGUI();

			if (GUILayout.Button("Траектория")) {
				if (window != null)
					window.Close();
				window = (PathWalkerWindow)EditorWindow.GetWindow(typeof(PathWalkerWindow));
				window.setAgent(agent);
                window.titleContent.text = "AI";
			}
		}

		

	}

}
