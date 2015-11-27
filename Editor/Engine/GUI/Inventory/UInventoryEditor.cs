using UnityEngine;
using UnityEditor;
using Engine.EGUI.Inventory;

namespace EngineEditor.EGUI.Inventory {
	
	[CustomEditor(typeof(UInventory))]
	public class UInventoryEditor : Editor, IItemSelectedListener {

		private static Color lineColor  = new Color(0.7f, 0.7f, 0.8f);
		private static Color lineShadow = new Color(0.4f, 0.4f, 0.4f);

		InventarWindow window;

		private UInventory inventory;

		void OnEnable() {
			inventory = target as UInventory;
		}

		public override void OnInspectorGUI() {

			base.OnInspectorGUI();

			if (GUILayout.Button("Проверка инвентаря")) {

				if (window != null)
					window.Close();

				window = (InventarWindow)EditorWindow.GetWindow(typeof(InventarWindow));
				window.titleContent = new GUIContent("Инвентарь");
				window.setData(inventory,this);
			}
			
		}

		public void OnItemSelect(Item item) {
			inventory.addItem(item.Clone());
		}

		public void drawLine() {
			EditorGUI.DrawRect(GUILayoutUtility.GetRect(120, 2), lineColor);
			EditorGUI.DrawRect(GUILayoutUtility.GetRect(120, 2), lineShadow);
		}

	}
	
}