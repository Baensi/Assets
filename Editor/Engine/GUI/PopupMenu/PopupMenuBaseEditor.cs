using UnityEditor;
using UnityEngine;
using Engine.EGUI.PopupMenu;

namespace EngineEditor.GUI.PopupMenu {
	
	[CustomEditor(typeof(PopupMenuBase),true)]
	public class PopupMenuBaseEditor : Editor {
		
		public override void OnInspectorGUI() {
			base.OnInspectorGUI();
			
		}

	}

}
