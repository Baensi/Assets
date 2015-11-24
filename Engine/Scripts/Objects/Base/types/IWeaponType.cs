using Engine.Player;
using Engine.Objects.Weapon;

namespace Engine.Objects.Types {

	public interface IWeaponType {

		/// <summary>
		/// Возвращает затраты персонажа, которые выполнятся до атаки
		/// </summary>
		/// <returns></returns>
		PlayerStates getAttackRequireStates();

		/// <summary>
		/// Возвращает статы, которые добавятся цели атаки
		/// </summary>
		/// <returns></returns>
		PlayerStates getAttackEffectStates();

		/// <summary>
		/// Возвращает тип оружия
		/// </summary>
		/// <returns></returns>
		WeaponTypes getType();

	}

	/// <summary>
	/// Расширяем интерфейс кодами для i18n словаря
	/// </summary>
	public static class DictionaryWeaponTypeInjector {

		public static string K_WEAPON_TYPE_NAME(this IReadedType type) {
			return "IReadedType_name";
		}

		public static string K_WEAPON_TYPE_CAPTION(this IReadedType type) {
			return "IReadedType_caption";
		}

	}

}
