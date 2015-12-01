
using System;

namespace Engine.EGUI.PopupMenu {
	
	/// <summary>
	/// ��������� ��������� ������ ����
	/// </summary>
	public interface MenuItemSelectListener {

		/// <summary>
		/// ��������� ����� ����� ���� ��� �������
		/// </summary>
		/// <param name="item"></param>
		void onSelect(MenuItem item);

	}
	
}