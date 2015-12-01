using UnityEngine;
using Engine.EGUI.PopupMenu;
using Engine.EGUI.Inventory.PopupMenu;
using Engine.EGUI.ToolTip;

namespace Engine.EGUI.Inventory {

	/// <summary>
	/// Класс контекстного меню инвентаря
	/// Содержит пункты действий над предметами внутри инвентаря
	/// </summary>
	public class InventoryPopupMenu : PopupMenuBase {
		
		private Item selectedItem; // выбранный в инвентаре предмет, по которому строится popupMenu

		[SerializeField] public MenuItem useItem; // пункт "использовать"
		[SerializeField] public MenuItem dropItem; // пункт "выкинуть"
		[SerializeField] public MenuItem dropAllItems; // пункт "выкинуть всё"

#if UNITY_EDITOR

		void OnValidate() {

			if (items!=null && items.Count>0)
				foreach (MenuItem item in items) // даём всем элементам в меню ссылки на себя
					item.setPopupMenuBase(this);
		}

#endif

			void Start(){
				base.MenuStart();

				foreach (MenuItem item in items) // даём всем элементам в меню ссылки на себя
					item.setPopupMenuBase(this);
				
			}

		/// <summary>
		/// Установка слушателей пуктов меню
		/// </summary>
		/// <param name="toolTip"></param>
		public void SetupListeners(ToolTipBase toolTip) {
			useItem.setClickListener(new InventoryUseItemListener(toolTip));
			dropItem.setClickListener(new InventoryDropItemListener(toolTip));
			dropAllItems.setClickListener(new InventoryDropAllItemsListener(toolTip));
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
