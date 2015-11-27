using System;
using UnityEngine;
using Engine.Objects;
using Engine.Objects.Types;

namespace Engine.EGUI.Inventory {
	
	/// <summary>
	/// Сервис для настройки контекстного меню инвентаря
	/// </summary>
	public class PopupMenuService {

		private static PopupMenuService instance;

		public static PopupMenuService getInatance() {
			if (instance == null)
				instance = new PopupMenuService();
			return instance;
        }

		/// <summary>
		/// Устанавливает необходимые пункты меню для указанного предмета
		/// </summary>
		/// <param name="menu">Контекстное меню, в котором проводится установка</param>
		/// <param name="item">Предмет, относительно которого устанавливаются настройки</param>
		public void SetupPopupMenu(InventoryPopupMenu menu, Item item) {
            menu.setSelectedItem(item); // устанавливаем контекстному меню ссылку на выбранный предмет

			menu.useItem.setEnabled(item.toGameObject().GetComponent<DynamicObject>() as IUsedType !=null);
			menu.dropAllItems.setEnabled(item.getMaxCount() > 1 && item.getCount() > 1);
		}

	}

}
