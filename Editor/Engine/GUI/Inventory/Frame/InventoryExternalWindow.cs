using UnityEditor;
using UnityEngine;
using Engine.EGUI.Inventory;
using EngineEditor.Data;
using Engine.Objects;
using System;

namespace EngineEditor.EGUI.Inventory {

	public class InventoryExternalWindow : EditorWindow, IItemSelectedListener {

		private float drawTimeStamp;
		public const string id = "InventoryExternalWindow";

		private InventoryExternal inventory;

		public void setInventory(InventoryExternal inventory) {
			this.inventory = inventory;

			this.position = new Rect(this.position.x,
									 this.position.y,
									 inventory.getWidth(),
									 inventory.getHeight()+200);

			inventory.getSlot().Items.Clear();

			for (int i = 0; i < inventory.initListItems.Count; i++)
				inventory.addItem(DObjectList.getInstance().getItem(inventory.initListItems[i]));

        }

		void OnDestroy() {
			EditorFactory.getInstance().UnReg(id);
		}

		void OnGUI() {
			GUILayout.BeginHorizontal();
				
				if (GUILayout.Button("Добавить")) {
					var window = (ItemListWindow)EditorWindow.GetWindow(typeof(ItemListWindow));
					window.setListener(this);
				}

				if (GUILayout.Button("Очистить всё")) {
					inventory.getSlot().Items.Clear();
					inventory.initListItems.Clear();
				}
				
			GUILayout.EndHorizontal();

            float x = (this.position.width - inventory.getWidth()) / 2;
			float y = (this.position.height - inventory.getHeight()) / 2;


			Handles.BeginGUI();

				inventory.OnUpdateEditor(x, y);

			Handles.EndGUI();

			this.Repaint();
		}

		void Update() {

			if(Time.time- drawTimeStamp >= 0.2) {
				OnGUI();
				drawTimeStamp = Time.time;
            }

		}

		public void OnItemSelect(Item selected) {
			inventory.addItem(selected.Clone());
			inventory.initListItems.Add(selected.resource.files.itemName);

			Repaint();
			//OnGUI();
		}
	}

}
