using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Engine.AI;
using EngineEditor.Data;
using Engine.AI.Behavior;

namespace EngineEditor.AI {

	[CustomEditor(typeof(EnemyBehaviorAI),true)]
	public class PathWalkerEditor : Editor {

		private AIWindow window;
        private NavMeshAgent agent;

		void OnEnable() {
			var enemy = target as EnemyBehaviorAI;
            agent = enemy.GetComponent<NavMeshAgent>();

			window = (AIWindow)EditorFactory.getInstance().FindWindow(AIWindow.id);

			if (window == null) {
				window = (AIWindow)EditorWindow.GetWindow(typeof(AIWindow));
				window.titleContent.text = "AI";
			}

			window.setAgent(agent);

		}

		void OnDestroy() {
			window.setAgent(null);
        }

		public override void OnInspectorGUI() {
			base.OnInspectorGUI();
		}
		

	}

}
