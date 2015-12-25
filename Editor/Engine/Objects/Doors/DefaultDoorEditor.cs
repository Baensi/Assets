using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Engine.Objects.Doors;

namespace EngineEditor.Objects.Doors {

	[CustomEditor(typeof(DefaultDoor), true)]
	public class DefaultDoorEditor : Editor {

		private bool        previewMode = false;
		private DefaultDoor door;

		void OnEnable() {
			door = target as DefaultDoor;door = target as DefaultDoor;
		}

		public override void OnInspectorGUI() {
			base.OnInspectorGUI();

			previewMode = EditorGUILayout.Toggle(new GUIContent("Предпросмотр"), previewMode);

			if(!previewMode) return;

			door.OnUpdate();

		}

	}

}
