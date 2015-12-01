using System;

namespace Engine.EGUI.PopupMenu {
	
	/// <summary>
	/// ��������� ����� �� ������ ����
	/// </summary>
	public interface MenuItemClickListener {
		
		/// <summary>
		/// ��������� �� ������ ������ ���� ��� �������� ����
		/// </summary>
		/// <param name="item"></param>
		void onClick(MenuItem item);
		
	}
	
}