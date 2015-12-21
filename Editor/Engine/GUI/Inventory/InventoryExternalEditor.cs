using UnityEditor;
using UnityEngine;
using Engine.EGUI.Inventory;
using EngineEditor.Data;

namespace EngineEditor.EGUI.Inventory {

	[CustomEditor(typeof(InventoryExternal),true)]
	public class InventoryExternalEditor : Editor {

		private InventoryExternal inventory;

		private int selectItem = 0;

		private InventoryExternalWindow window;

			void OnEnable() {
				inventory = target as InventoryExternal;
			}

		public override void OnInspectorGUI() {

			GUILayout.Label("Сумка", EditorStyles.boldLabel);
			EditorGUILayout.Separator();

			inventory.titleTextId = EditorGUILayout.TextField("id заголовка сумки", inventory.titleTextId);

            GUILayout.Label("Ячейки:");
			EditorGUILayout.BeginHorizontal();

				inventory.cellXCount = EditorGUILayout.IntField("X",inventory.cellXCount);
				if (inventory.cellXCount < 1)
					inventory.cellXCount = 1;

                inventory.cellYCount = EditorGUILayout.IntField("Y",inventory.cellYCount);
				if (inventory.cellYCount < 1)
					inventory.cellYCount = 1;

			EditorGUILayout.EndHorizontal();

			selectItem = GUILayout.SelectionGrid(selectItem, inventory.initListItems.ToArray(), inventory.initListItems.Count, EditorStyles.miniButton);

			if(GUILayout.Button("Редактировать содержимое")) {

				if (window != null)
					window.Close();

				window = (InventoryExternalWindow)EditorWindow.GetWindow(typeof(InventoryExternalWindow));
				EditorFactory.getInstance().RegWindow(InventoryExternalWindow.id, window);
				window.titleContent = new GUIContent("Сумка");
				window.setInventory(inventory);

			}

        }

	}

}
