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

			GUILayout.BeginHorizontal();
			GUILayout.Label(item.resource.icon);
				GUILayout.BeginVertical();
				
					GUILayout.Label("id: "+item.description.name);
					GUILayout.Label("Название: "+item.resource.files.itemName);
					GUILayout.Label("Размер ячейки: "+item.size.getWidth().ToString()+"x"+item.size.getHeight().ToString());
					GUILayout.Label("Размер группы: " + item.getMaxCount().ToString());
					GUILayout.Label("Число звуков: " + item.resource.files.soundsNames.Count.ToString());
				
					if (GUILayout.Button("Добавить "+item.description.dName))
						listener.OnItemSelect(item);
				
				GUILayout.EndVertical();
            GUILayout.EndHorizontal();
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
