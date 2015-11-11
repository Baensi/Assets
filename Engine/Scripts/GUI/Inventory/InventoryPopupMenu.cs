using UnityEngine;
using System.Collections;
using Engine.EGUI.PopupMenu;

namespace Engine.EGUI.Inventory {

	public class InventoryPopupMenu : PopupMenuBase {

		private Item selectedItem;

		[SerializeField] public MenuItem useItem;
		[SerializeField] public MenuItem dropItem;
		[SerializeField] public MenuItem dropAllItem;

		void Start(){
			base.MenuStart();

		}

		public void setSelectedItem(Item selectedItem) {
			this.selectedItem = selectedItem;
		}

		public void redraw() {
			base.MenuOnGUI();

			GUI.Box(new Rect(eventData.cursorPosition.x, eventData.cursorPosition.y, 240, 30), selectedItem.item.getDescription().dName + " - " + selectedItem.item.getDescription().dCaption);

		}

		void OnGUI(){

			

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

		public override void onSelect(MenuItem item){
			


		}

	}

}