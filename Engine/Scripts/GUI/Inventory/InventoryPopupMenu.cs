using UnityEngine;
using System.Collections;
using Engine.EGUI.PopupMenu;
using Engine.EGUI.ToolTip;

namespace Engine.EGUI.Inventory {

	public class InventoryPopupMenu : PopupMenuBase {

		private ToolTipBase toolTip;
		private Item selectedItem;

		[SerializeField] public MenuItem useItem;
		[SerializeField] public MenuItem dropItem;
		[SerializeField] public MenuItem dropAllItem;

		void Start(){
			base.MenuStart();
			toolTip = new ToolTipBase();
        }

		public void setSelectedItem(Item selectedItem) {
			this.selectedItem = selectedItem;
		}

		public void redraw() {
			base.MenuOnGUI();
		}

		public override void onClick(MenuItem item) {
			
			if (item == useItem) {
				
				hide();
				return;
			}
			
			if (item == dropItem) {
				
				hide();
				return;
			}
			
			if (item == dropAllItem) {
				
				hide();
				return;
			}
			
		}

		public override void onSelect(MenuItem item){ }

	}

}