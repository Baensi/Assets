using UnityEngine;
using Engine.EGUI.PopupMenu;
using Engine.EGUI.ToolTip;
using Engine.Objects;

namespace Engine.EGUI.Inventory.PopupMenu {

	public class InventoryDropItemListener : MenuItemClickListener {

		private ToolTipBase toolTip;

			public InventoryDropItemListener(ToolTipBase toolTip) {
				this.toolTip=toolTip;
			}

		public void onClick(MenuItem menuItem) {
			InventoryPopupMenu menu = (InventoryPopupMenu)menuItem.getMenu();
			Item item = menu.getSelectedItem();

				DropItem(item);

			menu.hide();
			toolTip.hide();
			menu.setSelectedItem(null);
		}

		public void DropItem(Item selectedItem) {

            if(selectedItem==null)
                return;

			UInventory inventory = SingletonNames.getInventory().GetComponent<UInventory>();
			DObjectList.getInstance().CreateNewInstance(selectedItem); // создаём объект предмета в мире

			if (selectedItem.getCount() > 1) {

				selectedItem.decCount(1); // уменьшаем число экземпляров у предмета

			} else {

				inventory.removeItem(selectedItem, false, true); // удаляем именно этот предмет из инвентаря

			}

		}

	}

}
