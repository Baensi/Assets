using UnityEditor;
using UnityEngine;
using Engine.EGUI.Inventory;
using EngineEditor.Data;
using Engine.I18N;

namespace EngineEditor.EGUI.Inventory {
	
	[CustomEditor(typeof(InventoryExternal))]
	public class InventoryExternalEditor : Editor {

		private InventoryExternal       inventory;
		private InventoryExternalWindow window;

			void OnEnable() {
				inventory = target as InventoryExternal;
			}

		public override void OnInspectorGUI() {

			base.OnInspectorGUI();

			if(GUILayout.Button("Редактировать содержимое")) {

				if (window != null)
					window.Close();

				window = (InventoryExternalWindow)EditorWindow.GetWindow(typeof(InventoryExternalWindow));
				window.titleContent = new GUIContent("Сумка");
				window.setInventory(inventory);

			}

        }

	}
	
}
