using System;
using UnityEngine;
using UnityEditor;
using Engine.Objects;

namespace EngineEditor.Objects {

	[CustomEditor(typeof(LightLOD))]
	public class LightLODEditor : Editor {

		private LightLODWindow window;

		public void showWindow() {

			if (window != null)
				window.Close();

			window = (LightLODWindow)EditorWindow.GetWindow(typeof(LightLODWindow));
			window.setLight(target as LightLOD);
			window.titleContent = new GUIContent("LightLOD");

		}

		void OnEnable() {

		}

		public override void OnInspectorGUI() {
			base.OnInspectorGUI();

			if (GUILayout.Button("Открыть редактор"))
				showWindow();
		}
		
	}
	
}