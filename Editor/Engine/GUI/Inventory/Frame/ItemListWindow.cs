using UnityEditor;
using UnityEngine;
using System.Collections.Generic;
using Engine.EGUI.Inventory;
using Engine.Objects;

namespace EngineEditor.EGUI.Inventory {

	public interface ItemSelectedListener {

		void OnItemSelect(Item selected);

	}

	public class ItemContainer {

		private Item item;
		private ItemSelectedListener listener;

			public ItemContainer(Item item, ItemSelectedListener listener) {
				this.item = item;
				this.listener = listener;
			}
		
		public Item getItem() {
			return item;
		}

		public void draw() {

			GUILayout.Label(item.resource.icon);
			if (GUILayout.Button(item.description.dName + " ["+item.description.name+"]"))
				listener.OnItemSelect(item);
		}

	}

	public class ItemListWindow : EditorWindow {

		private ItemSelectedListener listener;
		private List<ItemContainer> items = new List<ItemContainer>();
		private Vector2 scroll;

		public void setListener(ItemSelectedListener listener) {
			this.listener = listener;

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
