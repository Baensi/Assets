using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Engine.Objects.Doors;

namespace EngineEditor.Objects.Doors {

	[CustomEditor(typeof(DoorPushLever), true)]
	public class DefaultLeverEditor : Editor {

		private bool          previewMode = false;
		private DoorPushLever lever;

		void OnEnable() {
			lever = target as DoorPushLever;
		}

		public override void OnInspectorGUI() {

			base.OnInspectorGUI();

			previewMode = EditorGUILayout.Toggle(new GUIContent("Предпросмотр"), previewMode);

			if(!previewMode) return;

			lever.OnUpdate();

			if (lever.door!=null)
				lever.door.OnUpdate();

		}


	}

}
