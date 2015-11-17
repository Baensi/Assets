using UnityEngine;
using System.Collections;

namespace Engine.Objects.Types {

	/// <summary>
	/// Предмет можно использовать
	/// </summary>
	public interface IUsedType {

		bool onUse(); // предмет использовали, возвращает true, если предмет был удалён

	}

	/// <summary>
	/// Расширяем интерфейс кодами для i18n словаря
	/// </summary>
	public static class DictionaryUsedTypeInjector {

		public static string K_USED_TYPE_NAME(this IUsedType type) {
			return "IUsedType_name";
		}

		public static string K_USED_TYPE_CAPTION(this IUsedType type) {
			return "IUsedType_caption";
		}

	}

}