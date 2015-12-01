
using System;

namespace Engine.EGUI.PopupMenu {
	
	/// <summary>
	/// Слушатель выделения пункта меню
	/// </summary>
	public interface MenuItemSelectListener {

		/// <summary>
		/// Указывает какуо пункт меню был выделен
		/// </summary>
		/// <param name="item"></param>
		void onSelect(MenuItem item);

	}
	
}