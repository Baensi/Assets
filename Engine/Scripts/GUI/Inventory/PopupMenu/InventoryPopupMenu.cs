using UnityEngine;
using Engine.EGUI.PopupMenu;
using Engine.EGUI.Inventory.PopupMenu;

namespace Engine.EGUI.Inventory {

	public class InventoryPopupMenu : PopupMenuBase {
		
		private Item selectedItem;

		[SerializeField] public MenuItem useItem; // пункт "использовать"
		[SerializeField] public MenuItem dropItem; // пункт "выкинуть"
		[SerializeField] public MenuItem dropAllItems; // пункт "выкинуть всё"

			void Start(){
				base.MenuStart();

					// устанавливаем слушателей
				useItem.setClickListener(new InventoryUseItemListener());
				dropItem.setClickListener(new InventoryDropItemListener());
				dropAllItems.setClickListener(new InventoryDropAllItemsListener());

				foreach (MenuItem item in items) // даём всем элементам в меню ссылки на себя
					item.setPopupMenuBase(this);	

			}

		public Item getSelectedItem() {
			return selectedItem;
		}

		public void setSelectedItem(Item selectedItem) {
			this.selectedItem = selectedItem;
		}

		/// <summary>
		/// Отрисовка контекстного меню
		/// </summary>
		public void redraw() {
			base.MenuOnGUI();
		}

	}

}
