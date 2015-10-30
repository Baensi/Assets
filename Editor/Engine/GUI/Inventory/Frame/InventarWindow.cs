using System;
using UnityEditor;
using UnityEngine;

namespace Engine.EGUI.Inventory {

	public class InventarWindow : EditorWindow {

		private static Color linesColor  = new Color(0f,1f,0f);
		private static Color screenColor = new Color(1f, 0f, 0f);

		private float width;
		private float height;

		private UInventory inventory = null;

		public void setInventory(UInventory inventory) {
			this.inventory=inventory;

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
				inventory.onResizeWindow(width, height); // изменяем размер окна
			}

			inventory.OnEditorUpdate();

			DrawScreen();
			DrawSlots();

				//Handles.color = new Color(1f, 0, 0);
				//Handles.Label(new Vector3(1, 1), new GUIContent(Event.current.mousePosition.ToString()));

			Handles.EndGUI();

			this.Repaint();
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
				Handles.DrawLine(new Vector3(rec.position.OffsetX+inventory.offsetX, rec.position.OffsetY + inventory.offsetY),
								 new Vector3(rec.position.OffsetX+rec.position.SlotWidth+inventory.offsetX, rec.position.OffsetY+ inventory.offsetY));

				Handles.DrawLine(new Vector3(rec.position.OffsetX+inventory.offsetX, rec.position.OffsetY+ inventory.offsetY),
								 new Vector3(rec.position.OffsetX+inventory.offsetX, rec.position.OffsetY+rec.position.SlotHeight+ inventory.offsetY));

				Handles.DrawLine(new Vector3(rec.position.OffsetX+rec.position.SlotWidth+inventory.offsetX, rec.position.OffsetY+ inventory.offsetY),
								 new Vector3(rec.position.OffsetX+rec.position.SlotWidth+inventory.offsetX, rec.position.OffsetY+rec.position.SlotHeight+ inventory.offsetY));

				Handles.DrawLine(new Vector3(rec.position.OffsetX+inventory.offsetX, rec.position.OffsetY+rec.position.SlotHeight+ inventory.offsetY),
								 new Vector3(rec.position.OffsetX+rec.position.SlotWidth+inventory.offsetX, rec.position.OffsetY+rec.position.SlotHeight+ inventory.offsetY));

				Handles.Label(new Vector3(rec.position.OffsetX+rec.position.SlotWidth+inventory.offsetX, rec.position.OffsetY+rec.position.SlotHeight+ inventory.offsetY), new GUIContent("слот"+(index++).ToString()));

			}

		}

		void OnFocus() {
			//SceneView.onSceneGUIDelegate -= this.OnSceneGUI;
			//SceneView.onSceneGUIDelegate += this.OnSceneGUI;
		}

		void OnDestroy() {
			//SceneView.onSceneGUIDelegate -= this.OnSceneGUI;
		}

		public void OnSceneGUI(SceneView sceneView) {


		}

	}

}