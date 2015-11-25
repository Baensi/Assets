using UnityEngine;
using System.Collections;
using Engine.EGUI.PopupMenu;
using Engine.EGUI.ToolTip;
using Engine.EGUI.Inventory.PopupMenu;

namespace Engine.EGUI.Inventory {

	public class InventoryPopupMenu : PopupMenuBase {
		
		private Item selectedItem;

		[SerializeField] public MenuItem useItem;
		private InventoryUseItemListener inventoryUseItemListener;

		[SerializeField] public MenuItem dropItem;
		private InventoryDropItemListener inventoryDropItemListener;

		[SerializeField] public MenuItem dropAllItem;
		private InventoryDropAllItemsListener inventoryDropAllItemsListener;


		void Start(){
			base.MenuStart();

			inventoryUseItemListener = new InventoryUseItemListener();
			inventoryDropItemListener = new InventoryDropItemListener();
			inventoryDropAllItemsListener = new InventoryDropAllItemsListener();
        }

		public void setSelectedItem(Item selectedItem) {
			this.selectedItem = selectedItem;
		}

		public void redraw() {
			base.MenuOnGUI();
		}

		public override void onClick(MenuItem item) {

			Debug.LogWarning(item.getCode().ToString()+" - clicked!");

			if (item == useItem) {
				Debug.LogWarning("enter use");
				inventoryUseItemListener.UseItem(selectedItem);
				selectedItem = null;
				hide();
				return;
			}
			
			if (item == dropItem) {
				Debug.LogWarning("enter drop");
				inventoryDropItemListener.DropItem(selectedItem);
				selectedItem = null;
				hide();
				return;
			}
			
			if (item == dropAllItem) {
				Debug.LogWarning("enter drop all");
				inventoryDropAllItemsListener.DropAllItem(selectedItem);
				selectedItem = null;
				hide();
                return;
			}
			
		}

		public override void onSelect(MenuItem item){ }

	}

}