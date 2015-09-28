using System;

namespace Engine.Player {

	/// <summary>
	/// Требования к персонажу
	/// </summary>
	public struct ClothRequire {

		public int          minLevel;
		public PlayerStates minStates;

		/// <summary>
		/// Возвращает true, если текущие требования не подходят персонажу
		/// </summary>
		/// <param name="require">Требования</param>
		/// <returns></returns>
		public static bool operator !(ClothRequire require) {

			if (require.minLevel > GamePlayer.level.level)
				return true;

			return require.minStates > GamePlayer.states; // если в левом операторе, хотябы один из параметров больше
		}

	}

}
