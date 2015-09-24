using UnityEngine;
using System.Collections;
using Engine.Skills;

namespace Engine.Objects.Types {

	/// <summary>
	/// Предмет можно приготовить
	/// </summary>
	public interface ICookedType {

		void onEnterCookingZone(CookingZone cookingZone); // предмет вошёл в зону готовки
		void onExitCookingZone(CookingZone cookingZone);  // предмет вышел из зоны готовки
		void onDestroyCook(CookingZone cookingZone);      // предмет был исключён из зоны готовки

		void onCook();  // предмет готовится
		void endCook(); // предмет готов
		

	}

}