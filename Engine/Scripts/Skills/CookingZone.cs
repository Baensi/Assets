using System;
using System.Collections.Generic;
using UnityEngine;
using Engine.Objects.Types;
using Engine.Objects;
using Engine.Player;

namespace Engine.Skills {

	[RequireComponent(typeof(BoxCollider))]
	public class CookingZone : MonoBehaviour {

		private bool cook = false; // семафор, показывает что список занят
		private List<ICookedType> foods; // очередь еды на готовку
		private List<ICookedType> removeList; // очередь еды на удаление из очереди на готовку

		void Start() {

			foods      = new List<ICookedType>(); // инициализируем коллекции
			removeList = new List<ICookedType>(); // инициализируем коллекции

		}

		/// <summary>
		/// Метод удаления еды из очереди готовки
		/// </summary>
		/// <param name="food"></param>
		public void removeFood(ICookedType food) {
			if (!foods.Contains(food)) return;

			if (!cook) { // если коллекция очереди не занята
				foods.Remove(food); // удаляем еду из очереди
				food.onDestroyCook(this); // посылаем команду еде, что она больше не готовится
			} else {
				removeList.Add(food); // добавляем еду в очередь на удаление
			}

		}

		/// <summary>
		/// Добавление еды в очередь на готовку
		/// </summary>
		/// <param name="food"></param>
		public void addFood(ICookedType food) {
			if (foods.Contains(food)) return;

			foods.Add(food);
		}

		void Update() {

			if (removeList.Count > 0) { // проверяем, есть ли задачи на удаление еды из очереди готовки
				foreach (ICookedType food in removeList) { 
					foods.Remove(food); // удаляем еду из споска готовки
					food.onDestroyCook(this); // посылаем команду еде, что она больше не готовится
				}
				removeList.Clear(); // очищаем очередь на удаление
			}

			cook = true;
			foreach(ICookedType food in foods)
				food.onCook(); // готовим всю еду в зоне
			cook = false;

		}

		/// <summary>
		/// В зону готовки вошёл какой то объект
		/// </summary>
		/// <param name="collider"></param>
		void OnTriggerEnter(Collider collider) {

			ICookedType food = collider.gameObject.GetComponent<ICookedType>();
			if (food == null) return; // если объект не является едой, котиорую можно приготовить, выходим

			addFood(food); // добавляем еду в очередь на готовку
			food.onEnterCookingZone(this); // говорим еде что она вошла в зону готовки

		}

		/// <summary>
		/// Из зоны готовки вышел какой то объект
		/// </summary>
		/// <param name="collider"></param>
		void OnTriggerExit(Collider collider) {

			ICookedType food = collider.gameObject.GetComponent<ICookedType>();
			if (food == null) return; // если объект не является едой, котиорую можно приготовить, выходим

			removeFood(food); // удаляем объект из очереди на готовку
			food.onExitCookingZone(this); // говорим еде что она покинула зону готовки

		}


	}

}
