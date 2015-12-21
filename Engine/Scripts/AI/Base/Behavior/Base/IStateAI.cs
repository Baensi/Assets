using System;
using Engine.Player;
using Engine.AI.Behavior;
using UnityEngine;

namespace Engine.AI.Behavior {

	/// <summary>
	/// Общий интерфейс AI
	/// </summary>
	public interface IStateAI {

		GameObject toObject();

		AIFraction getFraction(); // фракция AI

		PlayerSpecifications getSpecifications(); // текущие характеристики AI

		/// <summary> Возвращает статы урона-по умолчанию this </summary>
		PlayerStates getDamageStates(); // статы, наносящиеся с уроном

		/// <summary> Возвращает статы this </summary><returns></returns>
		PlayerStates getStates(); // Текущие статы AI

		/// <summary> Урон, который получил this </summary>
		void getDamage(PlayerStates value); // наносит урон AI

	}

}
