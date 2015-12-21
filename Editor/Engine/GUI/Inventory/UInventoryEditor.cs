using UnityEngine;
using UnityEditor;
using Engine.EGUI.Inventory;
using EngineEditor.Data;

namespace EngineEditor.EGUI.Inventory {
	
	[CustomEditor(typeof(UInventory))]
	public class UInventoryEditor : Editor, IItemSelectedListener {

		private static Color lineColor  = new Color(0.7f, 0.7f, 0.8f);
		private static Color lineShadow = new Color(0.4f, 0.4f, 0.4f);

		InventoryWindow window;

		private UInventory inventory;

		void OnEnable() {
			inventory = target as UInventory;
		}

		public override void OnInspectorGUI() {

			base.OnInspectorGUI();

			if (GUILayout.Button("Проверка инвентаря")) {

				if (window != null)
					window.Close();

				
				window = (InventoryWindow)EditorWindow.GetWindow(typeof(InventoryWindow));
				EditorFactory.getInstance().RegWindow(InventoryWindow.id, window);
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