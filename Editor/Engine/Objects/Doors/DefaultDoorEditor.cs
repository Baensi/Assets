﻿using System;
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
