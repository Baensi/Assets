using System;
using UnityEngine;
using UnityEditor;
using Engine.Objects;

namespace EngineEditor.Objects {

	public class LightLODWindow : EditorWindow {

		private static Color colorRange   = new Color(1f, 1f, 0f, 0.5f);
		private static Color colorDisable = new Color(1f, 0f, 0f, 0.5f);

		private bool oreol       = true;
		private LightLOD lightLOD;

		private SceneView sceneView;

		public void setLight(LightLOD lightLOD) {
			this.lightLOD = lightLOD;
		}

		void OnEnable() {
			SceneView.onSceneGUIDelegate += this.OnSceneGUI;
		}

		void OnDestroy() {
			SceneView.onSceneGUIDelegate -= this.OnSceneGUI;
		}

		private void drawSphere(Color color, Vector3 position, Quaternion rotation, float range) {

			Handles.color = color;
			Handles.CircleCap(0, position, rotation, range);

			Quaternion rot1 = Quaternion.Euler(90, 0, 0) * rotation;
			Quaternion rot2 = Quaternion.Euler(0, 90, 0) * rotation;
			Quaternion rot3 = Quaternion.Euler(0, 0, 90) * rotation;

			Handles.CircleCap(0, position, rot1, range);
			Handles.CircleCap(0, position, rot2, range);
			Handles.CircleCap(0, position, rot3, range);

		}

		void OnGUI() {

			if (lightLOD == null)
				return;

			oreol = EditorGUILayout.Toggle(new GUIContent("Показывать границу"), oreol);
			lightLOD.toLight().enabled = EditorGUILayout.Toggle(new GUIContent("Включить свет"), lightLOD.toLight().enabled);

			if (sceneView!=null)
				sceneView.Repaint();
		}

		public void OnSceneGUI(SceneView sceneView) {

			this.sceneView = sceneView;

			if (lightLOD == null)
				return;

			if (!oreol || !lightLOD.enabled)
				return;

			drawSphere(colorRange,lightLOD.transform.position, lightLOD.transform.rotation, lightLOD.maxRange);
			
			if (lightLOD.useSmoothIntensity)
				drawSphere(colorDisable,lightLOD.transform.position, lightLOD.transform.rotation, lightLOD.disableRange);

		}

	}

}