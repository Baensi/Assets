using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace Engine.Objects.Doors {

	[CustomEditor(typeof(DefaultDoor), true)]
	public class DefaultDoorEditor : Editor {

		private bool        previewMode = true;
		private DefaultDoor door;

		void OnEnable() {
			
		}

		void OnDestroy() {

		}

		public override void OnInspectorGUI() {

			base.OnInspectorGUI();

			door = target as DefaultDoor;

			//if (GUI.changed) {
			//	door.OnStart();
			//	door.OnUpdate();
			//}


			#if UNITY_EDITOR
				previewMode = EditorGUILayout.Toggle(new GUIContent("Предпросмотр"), previewMode);

				if(!previewMode) return;

				door.OnUpdate();

			#endif

		}


	}

}
