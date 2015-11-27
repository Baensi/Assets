using System;
using UnityEditor;
using UnityEngine;
using Engine.EGUI.Inventory;

namespace EngineEditor.EGUI.Inventory {

	public class InventarWindow : EditorWindow {

		private static Color linesColor  = new Color(0f,1f,0f);
		private static Color screenColor = new Color(1f, 0f, 0f);

		private float width;
		private float height;

		private IItemSelectedListener listener = null;
		private UInventory inventory = null;

		public void setData(UInventory inventory, IItemSelectedListener listener) {
			this.inventory=inventory;
			this.listener = listener;

            width  = this.position.width;
			height = this.position.height;
		}

		void OnGUI() {
			Handles.BeginGUI();

			if (inventory==null)
				return;

			if (this.position.width!=width || this.position.height!=height) {
				width = this.position.width;
				height = this.position.height+200;
			}

			inventory.OnEditorUpdate(width,height);

			DrawScreen();
			DrawSlots();

			Handles.EndGUI();

			this.Repaint();

			GUILayout.BeginHorizontal();
				if (GUILayout.Button("Добавить...")) {
					var window = (ItemListWindow)EditorWindow.GetWindow(typeof(ItemListWindow));
					window.setListener(listener);
				}
			GUILayout.EndHorizontal();
        }

		private void DrawScreen() {
			Handles.color = screenColor;
				Handles.DrawLine(new Vector3(1, 0), new Vector3(1, Screen.height-23));
				Handles.DrawLine(new Vector3(1, 0), new Vector3(Screen.width-1, 0));
				Handles.DrawLine(new Vector3(1, Screen.height-23), new Vector3(Screen.width-1, Screen.height-23));
				Handles.DrawLine(new Vector3(Screen.width-1, 0), new Vector3(Screen.width-1, Screen.height-23));
			Handles.Label(new Vector3(Screen.width-60, Screen.height-45), new GUIContent("Экран"));
		}

		private void DrawSlots() {
			int index = 0;

			foreach (RectangleSlot rec in inventory.slots) {

				Handles.color = linesColor;
				Handles.DrawLine(new Vector3(rec.position.OffsetX+inventory.getX(), rec.position.OffsetY + inventory.getY()),
								 new Vector3(rec.position.OffsetX+rec.position.SlotWidth+inventory.getX(), rec.position.OffsetY+ inventory.getY()));

				Handles.DrawLine(new Vector3(rec.position.OffsetX+inventory.getX(), rec.position.OffsetY+ inventory.getY()),
								 new Vector3(rec.position.OffsetX+inventory.getX(), rec.position.OffsetY+rec.position.SlotHeight+ inventory.getY()));

				Handles.DrawLine(new Vector3(rec.position.OffsetX+rec.position.SlotWidth+inventory.getX(), rec.position.OffsetY+ inventory.getY()),
								 new Vector3(rec.position.OffsetX+rec.position.SlotWidth+inventory.getX(), rec.position.OffsetY+rec.position.SlotHeight+ inventory.getY()));

				Handles.DrawLine(new Vector3(rec.position.OffsetX+inventory.getX(), rec.position.OffsetY+rec.position.SlotHeight+ inventory.getY()),
								 new Vector3(rec.position.OffsetX+rec.position.SlotWidth+inventory.getX(), rec.position.OffsetY+rec.position.SlotHeight+ inventory.getY()));

				Handles.Label(new Vector3(rec.position.OffsetX+rec.position.SlotWidth+inventory.getX(), rec.position.OffsetY+rec.position.SlotHeight+ inventory.getY()), new GUIContent("слот"+(index++).ToString()));

			}

		}

		void OnDestroy() {

			foreach(RectangleSlot slot in inventory.getSlots())
				slot.Items.Clear();

		}

	}

}
