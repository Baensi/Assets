using System;
using UnityEngine;
using System.Collections;
using Engine.EGUI.Inventory;
using Engine.Objects;
using Engine.Player;

namespace Engine.Objects {

	/// <summary>
	/// Интерфейс динамического предмета
	/// </summary>
	public interface IDynamicObject {

		TextDisplayed getDisplayed();
		GameObject    toObject();

		/// <summary>
		/// Идентификатор предмета
		/// </summary>
		/// <returns>Возвращает код предмета</returns>
		int    getId();

		/// <summary>
		/// Название предмета
		/// </summary>
		/// <returns>Возвращает id имени</returns>
		string getName();

		/// <summary>
		/// Описание предмета
		/// </summary>
		/// <returns>Возвращает id описания</returns>
		string getCaption();

		/// <summary>
		/// Стоимость предмета
		/// </summary>
		/// <returns>Возвращает стоимость предмета</returns>
		float  getCostValue();

		/// <summary>
		/// Статы, которые изменяет предмет
		/// </summary>
		/// <returns>Возвращает статы, если статы не меняются - вернёт null</returns>
		PlayerStates getStates();

		void Destroy(bool presently);
		bool isDestroy();

		void setSelection(bool selection);
		bool isSelected();
	}

}

