﻿using UnityEditor;
using UnityEngine;
using Engine.EGUI.PopupMenu;

namespace EngineEditor.GUI.PopupMenu {
	
	[CustomEditor(typeof(PopupMenuBase),true)]
	public class PopupMenuBaseEditor : Editor {

		void OnEnable() {
			//menu = target as PopupMenuBase;
		}

		public override void OnInspectorGUI() {
			base.OnInspectorGUI();


		}

	}

}
