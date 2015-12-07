using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Engine.AI;
using EngineEditor.Data;

namespace EngineEditor.AI {

	[CustomEditor(typeof(RangedAI),true)]
	public class PathWalkerEditor : Editor {

		private AIWindow window;
        private NavMeshAgent agent;

		void OnEnable() {
			var walker = target as PathWalker;

            agent = walker.GetComponent<NavMeshAgent>();
        }

		void OnDestroy() {
			window.setAgent(null);
        }

		public override void OnInspectorGUI() {

			base.OnInspectorGUI();

			if (GUILayout.Button("Траектория")) {

				window = (AIWindow)EditorFactory.getInstance().FindWindow(AIWindow.id);

					if (window == null) {
						window = (AIWindow)EditorWindow.GetWindow(typeof(AIWindow));
						window.titleContent.text = "AI";
					}

				window.setAgent(agent);
                
			}
		}
		

	}

}
