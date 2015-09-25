using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Engine.EGUI.Inventory;

namespace Engine.EGUI.InventoryA {
	
	[CustomEditor(typeof(UInventory))]
	public class UInventoryEditor : Editor {

		private static Color lineColor  = new Color(0.7f, 0.7f, 0.8f);
		private static Color lineShadow = new Color(0.4f, 0.4f, 0.4f);

		InventarWindow window;

		private UInventory inventory;

		void OnEnable() {
			inventory = target as UInventory;

			window = (InventarWindow)EditorWindow.GetWindow(typeof(InventarWindow));
			window.titleContent = new GUIContent("Инвентарь");
			window.setInventory(inventory);
		}

		public override void OnInspectorGUI() {

			base.OnInspectorGUI();

			Update();
			
		}

		void OnGUI() {
			Update();
		}

		private void Update() {

		}

		public void drawLine() {
			EditorGUI.DrawRect(GUILayoutUtility.GetRect(120, 2), lineColor);
			EditorGUI.DrawRect(GUILayoutUtility.GetRect(120, 2), lineShadow);
		}

	}
	
}