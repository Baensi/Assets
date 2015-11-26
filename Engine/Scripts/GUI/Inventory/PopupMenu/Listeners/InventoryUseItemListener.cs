using UnityEngine;
using Engine.Objects;
using Engine.Objects.Types;
using Engine.EGUI.PopupMenu;

namespace Engine.EGUI.Inventory.PopupMenu {

	public class InventoryUseItemListener : MenuItemClickListener {

		private UInventory inventory;

			public InventoryUseItemListener() {
				inventory = SingletonNames.getInventory().GetComponent<UInventory>();
			}

		public void onClick(MenuItem menuItem) {
			InventoryPopupMenu menu = (InventoryPopupMenu)menuItem.getMenu();
			Item item = menu.getSelectedItem();

				UseItem(item);

			menu.hide();
			menu.setSelectedItem(null);
		}

		public void UseItem(Item selectedItem) {

            if(selectedItem==null)
                return;

			IDynamicObject dynamicObject = selectedItem.toGameObject().GetComponent<DynamicObject>(); // получаем класс динамического объекта
			IUsedType      usedType      = dynamicObject as IUsedType; // достаём из класса "Тип Используемого предмета"

			usedType.onUse(); // вызываем метод использования предмета

			if (selectedItem.getCount() > 1) {

				selectedItem.decCount(1); // уменьшаем число экземпляров у предмета

			} else {

				inventory.removeItem(selectedItem, false, true); // удаляем именно этот предмет из инвентаря

			}

		}

	}
}
