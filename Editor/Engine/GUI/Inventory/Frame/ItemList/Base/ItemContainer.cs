using UnityEngine;
using Engine.EGUI.Inventory;

namespace EngineEditor.EGUI.Inventory {

	public class ItemContainer {

		private Item                  item;
		private IItemSelectedListener listener;

		public ItemContainer(Item item, IItemSelectedListener listener) {
			this.item     = item;
			this.listener = listener;
		}

		public Item getItem() {
			return item;
		}

		public void draw() {

			if(item==null || item.resource==null)
				return;

			GUILayout.BeginHorizontal();

				GUILayout.Label(item.resource.icon);

				GUILayout.BeginVertical();

					GUILayout.Label("id: " + item.description.name);
					GUILayout.Label("Название: " + item.resource.files.itemName);
					GUILayout.Label("Размер ячейки: " + item.size.getWidth().ToString() + "x" + item.size.getHeight().ToString());
					GUILayout.Label("Размер группы: " + item.getMaxCount().ToString());
					GUILayout.Label("Число звуков: " + item.resource.files.soundsNames.Count.ToString());

					if (GUILayout.Button("Добавить " + item.description.dName))
						listener.OnItemSelect(item);

				GUILayout.EndVertical();

			GUILayout.EndHorizontal();
		}

	}

}
