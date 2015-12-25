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

		//private AIWindow window;

		void OnEnable() {

			//window = (AIWindow)EditorFactory.getInstance().FindWindow(AIWindow.id);

			//if (window == null) {
			//	window = (AIWindow)EditorWindow.GetWindow(typeof(AIWindow));
			//	window.titleContent.text = "AI";
			//}

		}

		void OnDestroy() { }

		public override void OnInspectorGUI() {
			base.OnInspectorGUI();
		}
		

	}

}
