using UnityEngine;
using System.Collections;

namespace Engine.Objects.Types {

	/// <summary>
	/// Предмет можно взять в инвентарь
	/// </summary>
	public interface IPickedType {

		void onPick(); // предмет взяли в инвентарь

	}

	/// <summary>
	/// Расширяем интерфейс кодами для i18n словаря
	/// </summary>
	public static class DictionaryPickedTypeInjector {

		public static string K_PICKED_TYPE_NAME(this IPickedType type) {
			return "IPickedType_name";
		}

		public static string K_PICKED_TYPE_CAPTION(this IPickedType type) {
			return "IPickedType_caption";
		}

	}

}