using UnityEditor;
using UnityEngine;
using Engine.AI;

namespace EngineEditor.AI {

	[CustomEditor(typeof(SeeAI))]
	public class SeeAIEditor : Editor {

		private SeeAI see;

		void OnEnable() {
			see = target as SeeAI;
		}

		public override void OnInspectorGUI() {
			base.OnInspectorGUI();

			PathWalkerWindow window = (PathWalkerWindow)Data.EditorFactory.getInstance().FindWindow(PathWalkerWindow.id);
			window.setSee(see);

		}

	}

}
