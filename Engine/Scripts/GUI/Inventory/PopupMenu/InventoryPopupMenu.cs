using UnityEngine;
using Engine.EGUI.PopupMenu;
using Engine.EGUI.Inventory.PopupMenu;

namespace Engine.EGUI.Inventory {

	public class InventoryPopupMenu : PopupMenuBase {
		
		private Item selectedItem;

		[SerializeField] public MenuItem useItem; // пункт "использовать"
		[SerializeField] public MenuItem dropItem; // пункт "выкинуть"
		[SerializeField] public MenuItem dropAllItems; // пункт "выкинуть всё"

#if UNITY_EDITOR

		void OnValidate() {

			if(useItem!=null)
				useItem.setClickListener(new InventoryUseItemListener());

			if (dropItem != null)
				dropItem.setClickListener(new InventoryDropItemListener());

			if (dropAllItems != null)
				dropAllItems.setClickListener(new InventoryDropAllItemsListener());

			if (items!=null && items.Count>0)
				foreach (MenuItem item in items) // даём всем элементам в меню ссылки на себя
					item.setPopupMenuBase(this);
		}

#endif

			void Start(){
				base.MenuStart();

				foreach (MenuItem item in items) // даём всем элементам в меню ссылки на себя
					item.setPopupMenuBase(this);

					// устанавливаем слушателей
				useItem.setClickListener(new InventoryUseItemListener());
				dropItem.setClickListener(new InventoryDropItemListener());
				dropAllItems.setClickListener(new InventoryDropAllItemsListener());
				
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
