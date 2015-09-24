using System;
using UnityEngine;
using UnityEditor;

namespace Engine.Objects {
	
	[CustomEditor(typeof(LightLOD))]
	public class LightLODEditor : Editor {

		private static Color colorRange   = new Color(1f,1f,0f);
		private static Color colorDisable = new Color(1f,0f,0f);

		private static bool oreol = true;

		private LightLOD lightLOD;

		void OnEnable() {
			lightLOD = target as LightLOD;
		}
		
		public override void OnInspectorGUI() {
			base.OnInspectorGUI();

			oreol = EditorGUILayout.Toggle(new GUIContent("Показывать границу"), oreol);

			if (!oreol)
				return;

			Handles.color = colorRange;
			Handles.SphereCap(0, lightLOD.transform.position, lightLOD.transform.rotation, lightLOD.maxRange);

			if (!lightLOD.useSmoothIntensity)
				return;

			Handles.color = colorDisable;
			Handles.SphereCap(0, lightLOD.transform.position, lightLOD.transform.rotation, lightLOD.disableRange);

		}
		
	}
	
}