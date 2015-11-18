using System;
using UnityEngine;
using UnityEditor;
using Engine.Objects;

namespace EngineEditor.Objects {

	[CustomEditor(typeof(ObjectDestroyed))]
	public class ObjectDestroyedEditor : Editor {

		public SerializedProperty maxHealth,
								  currentHealth,
								  destroyModel,
								  destructPart,
								  piecesDeathTime;

		private static string maxHealth_caption     = "Максимальное \"здоровье\" объекта";
		private static string currentHealth_caption = "Состояние \"здоровья\" объекта";
		private static string destroyModel_caption  = "Префаб сломанного объекта";
		private static string destructPart_caption  = "Партикл-эффект поломки или разрушения";

		void OnEnable() {
			maxHealth     = serializedObject.FindProperty("maxHealth");
			currentHealth = serializedObject.FindProperty("currentHealth");
			destroyModel  = serializedObject.FindProperty("destroyModel");
			destructPart  = serializedObject.FindProperty("destructPart");
		}

		public override void OnInspectorGUI() {
			EditorGUILayout.PropertyField(maxHealth, new GUIContent(maxHealth_caption));
			EditorGUILayout.Slider(currentHealth, 0f, maxHealth.floatValue, new GUIContent(currentHealth_caption));
			EditorGUILayout.Separator();
			EditorGUILayout.PropertyField(destroyModel, new GUIContent(destroyModel_caption));
			EditorGUILayout.PropertyField(destructPart, new GUIContent(destructPart_caption));
		}

	}

}