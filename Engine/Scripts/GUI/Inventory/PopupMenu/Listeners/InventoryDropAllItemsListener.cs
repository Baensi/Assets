using UnityEngine;
using Engine.EGUI.PopupMenu;
using Engine.EGUI.ToolTip;
using Engine.Objects;

namespace Engine.EGUI.Inventory.PopupMenu {

	public class InventoryDropAllItemsListener : MenuItemClickListener {

		private ToolTipBase toolTip;

			public InventoryDropAllItemsListener(ToolTipBase toolTip) {
				this.toolTip=toolTip;
			}

		public void onClick(MenuItem menuItem) {
			InventoryPopupMenu menu = (InventoryPopupMenu)menuItem.getMenu();
            Item item = menu.getSelectedItem();

				DropAllItem(item);

			menu.hide();
			toolTip.hide();
			menu.setSelectedItem(null);
		}

		public void DropAllItem(Item selectedItem) {

            if(selectedItem==null)
                return;

			UInventory inventory = SingletonNames.getInventory().GetComponent<UInventory>();

			for (int i = 1; i <= selectedItem.getCount(); i++)
				DObjectList.getInstance().CreateNewInstance(selectedItem); // создаём объект предмета в мире
			
			inventory.removeItem(selectedItem, false, true); // удаляем именно этот предмет из инвентаря

		}

	}

}
