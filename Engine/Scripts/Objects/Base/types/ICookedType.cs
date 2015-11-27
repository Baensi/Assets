using UnityEngine;
using System.Collections;
using Engine.Skills;

namespace Engine.Objects.Types {

	/// <summary>
	/// Интерфейс модификатор. Предмет можно приготовить
	/// </summary>
	public interface ICookedType {

		void onEnterCookingZone(CookingZone cookingZone); // предмет вошёл в зону готовки
		void onExitCookingZone(CookingZone cookingZone);  // предмет вышел из зоны готовки
		void onDestroyCook(CookingZone cookingZone);      // предмет был исключён из зоны готовки

		void onCook();  // предмет готовится
		void endCook(); // предмет готов

	}

	/// <summary>
	/// Расширяем интерфейс кодами для i18n словаря
	/// </summary>
	public static class DictionaryCookedTypeInjector {

		public static string K_COOKED_TYPE_NAME(this ICookedType type) {
			return "ICookedType_name";
		}

		public static string K_COOKED_TYPE_CAPTION(this ICookedType type) {
			return "ICookedType_caption";
		}

	}

}