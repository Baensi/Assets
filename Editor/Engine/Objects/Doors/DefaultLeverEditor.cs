using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace Engine.Objects.Doors {

	[CustomEditor(typeof(DoorPushLever), true)]
	public class DefaultLeverEditor : Editor {

		private bool          previewMode = true;
		private DoorPushLever lever;

		void OnEnable() {
			
		}

		void OnDestroy() {

		}

		public override void OnInspectorGUI() {

			base.OnInspectorGUI();

			lever = target as DoorPushLever;

			#if UNITY_EDITOR
				previewMode = EditorGUILayout.Toggle(new GUIContent("Предпросмотр"), previewMode);

				if(!previewMode) return;

				lever.OnUpdate();

				if (lever.door!=null)
					lever.door.OnUpdate();

			#endif

		}


	}

}
