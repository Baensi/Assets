using Engine.Player;

namespace Engine.Calculators {

	public static class AttackCalculator {

		/// <summary>
		/// Урон, который посылает атакующая сторона
		/// </summary>
		/// <param name="attackerStates">статы атакующей стороны</param>
		/// <param name="attackerSpecifications">характеристики атакующей стороны</param>
		/// <param name="defaultAttack">значение атаки-по умолчанию предмета</param>
		/// <returns>Возвращает наносимый урон</returns>
		public static PlayerStates createDamage(PlayerStates         attackerStates,
												PlayerSpecifications attackerSpecifications,
												PlayerStates defaultAttack) {


			return defaultAttack;
		}

		/// <summary>
		/// Возвращает полученный и "усвоенный" урон
		/// </summary>
		/// <param name="defenderStates">Статы защищающейся стороны</param>
		/// <param name="defenderSpecifications">навыки защищающейся стороны</param>
		/// <param name="damage">урон-по умолчанию, который сторона получает</param>
		/// <returns>Возвращает полученный урон</returns>
		public static PlayerStates doProtection(PlayerStates         defenderStates,
												PlayerSpecifications defenderSpecifications,
												PlayerStates damage) {

			return damage;
		}

	}

}
