using System;
using UnityEngine;
using System.Collections;
using Engine.EGUI.Inventory;
using Engine.Objects;
using Engine.Player;

namespace Engine.Objects {

	/// <summary>
	/// ��������� ������������� ��������
	/// </summary>
	public interface IDynamicObject {

		TextDisplayed getDisplayed();
		GameObject    toObject();

		/// <summary>
		/// ������������� ��������
		/// </summary>
		/// <returns>���������� ��� ��������</returns>
		int    getId();

		/// <summary>
		/// �������� ��������
		/// </summary>
		/// <returns>���������� id �����</returns>
		string getName();

		/// <summary>
		/// �������� ��������
		/// </summary>
		/// <returns>���������� id ��������</returns>
		string getCaption();

		/// <summary>
		/// ��������� ��������
		/// </summary>
		/// <returns>���������� ��������� ��������</returns>
		float  getCostValue();

		/// <summary>
		/// �����, ������� �������� �������
		/// </summary>
		/// <returns>���������� �����, ���� ����� �� �������� - ����� null</returns>
		PlayerStates getStates();

		void Destroy(bool presently);
		bool isDestroy();

		void setSelection(bool selection);
		bool isSelected();
	}

}

