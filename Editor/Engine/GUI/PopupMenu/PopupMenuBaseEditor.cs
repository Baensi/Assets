using UnityEditor;
using UnityEngine;
using Engine.EGUI.PopupMenu;

namespace EngineEditor.EGUI.PopupMenu {
	
	[CustomEditor(typeof(PopupMenuBase),true)]
	public class PopupMenuBaseEditor : Editor {
		
		public override void OnInspectorGUI() {
			base.OnInspectorGUI();
			
		}

	}

}
