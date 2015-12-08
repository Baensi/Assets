using UnityEditor;
using UnityEngine;
using System.Collections.Generic;
using Engine.EGUI.Inventory;
using Engine.Objects;

namespace EngineEditor.EGUI.Inventory {
	
	public class ItemListWindow : EditorWindow {
		
		private List<ItemContainer>  items = new List<ItemContainer>();
		private Vector2 scroll;

		public void setListener(IItemSelectedListener listener) {

			foreach (Item item in DObjectList.getInstance().getItemList())
				items.Add(new ItemContainer(item, listener));
			
		}

		void OnGUI() {

			scroll = GUILayout.BeginScrollView(scroll, GUILayout.Width(position.width), GUILayout.Height(position.height));

				foreach (ItemContainer item in items)
					item.draw();

			GUILayout.EndScrollView();
		}

		void OnDestroy() {
			items.Clear();
			items = null;
		}

	}

}
