using UnityEngine;
using Engine.Objects;
using Engine.Objects.Types;
using Engine.EGUI.PopupMenu;
using Engine.EGUI.ToolTip;

namespace Engine.EGUI.Inventory.PopupMenu {

	public class InventoryUseItemListener : MenuItemClickListener {

		private ToolTipBase toolTip;

			public InventoryUseItemListener(ToolTipBase toolTip) {
				this.toolTip=toolTip;
			}

		public void onClick(MenuItem menuItem) {

			if(menuItem==null)
				return;

			InventoryPopupMenu menu = (InventoryPopupMenu)menuItem.getMenu();
			Item item = menu.getSelectedItem();

				UseItem(item);

			menu.hide();
			toolTip.hide();
			menu.setSelectedItem(null);
		}

		public void UseItem(Item selectedItem) {

            if(selectedItem==null)
                return;

			UInventory     inventory     = SingletonNames.getInventory().GetComponent<UInventory>();
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
