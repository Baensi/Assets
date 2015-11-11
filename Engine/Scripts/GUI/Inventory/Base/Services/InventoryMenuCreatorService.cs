using System;
using UnityEngine;

namespace Engine.EGUI.Inventory {
	
	/// <summary>
	/// Сервис для настройки контекстного меню инвентаря
	/// </summary>
	public class InventoryMenuCreatorService {

		private static InventoryMenuCreatorService instance;

		public static InventoryMenuCreatorService getInatance() {
			if (instance == null)
				instance = new InventoryMenuCreatorService();
			return instance;
        }

		/// <summary>
		/// Устанавливает необходимые пункты меню для указанного предмета
		/// </summary>
		/// <param name="menu">Контекстное меню, в котором проводится установка</param>
		/// <param name="item">Предмет, относительно которого устанавливаются настройки</param>
		public void SetupPopupMenu(InventoryPopupMenu menu, Item item) {

			menu.dropAllItem.setEnabled(item.getMaxCount() > 1 && item.getCount() > 1);

		}

	}

}
