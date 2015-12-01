using System;

namespace Engine.EGUI.PopupMenu {
	
	/// <summary>
	/// —лушатель клика по пункту меню
	/// </summary>
	public interface MenuItemClickListener {
		
		/// <summary>
		/// ”казывает по какому пункту меню был совершЄн клик
		/// </summary>
		/// <param name="item"></param>
		void onClick(MenuItem item);
		
	}
	
}