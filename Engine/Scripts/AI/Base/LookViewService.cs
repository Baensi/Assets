using UnityEngine;
using System.Collections.Generic;
using Engine.AI.Behavior;

namespace Engine.AI {

	/// <summary>
	/// Класс-сервис выполняющий расчёты по обнаружению AI объектов
	/// </summary>
	public class LookViewService {

		private static LookViewService instance;

		public static LookViewService getInstance() {
			if (instance == null)
				instance = new LookViewService();
			return instance;
        }

			public LookViewService() {
				
			}


		/// <summary>
		/// Ищет AI объекты в указанной области зрения
		/// </summary>
		/// <param name="see">AI который смотрит</param>
		/// <param name="seeRay">Луч зрения</param>
		/// <param name="maxDistance">Дистанция зрения</param>
		/// <param name="maxAngle">Угол зрения</param>
		/// <returns> Возвращает всех видимых AI </returns>
		public List<IStateAI> getSeeAIObjects(EnemyBehaviorAI see, Ray seeRay, float maxDistance, float maxAngle) {
			RaycastHit[] hits = Physics.SphereCastAll(seeRay,maxDistance); // накрываем область сферическим кастом, получаем все объекты

			if(hits==null)
				return null;

			List<IStateAI> result = new List<IStateAI>();

				foreach(RaycastHit hit in hits) {
				
					EnemyBehaviorAI ai = hit.transform.gameObject.GetComponent<EnemyBehaviorAI>();

					if (ai != null && ai != see) { // нас интерисуют только объекты AI

						if (isSee(see.transform.position, ai.toObject(), maxAngle, maxDistance)) // смотрим на объект
							result.Add(ai as IStateAI); // добавляем объект в список видимых

					}

				}

			return result;
		}

		/// <summary>
		/// Проверяет, виден ли объект gameObject из точки point
		/// </summary>
		/// <param name="point">точка откуда направлен взгляд AI</param>
		/// <param name="gameObject">Объект который AI пытается увидеть</param>
		/// <param name="maxAngle">Угол зрения</param>
		/// <param name="maxDistance">Дальность зрения</param>
		/// <returns>Возвращает логический результат - ВИДЕН ЛИ объект gameObject? из указанной точки point или нет</returns>
		public bool isSee(Vector3 point, GameObject gameObject, float maxAngle, float maxDistance) {

			Vector3 heading = gameObject.transform.position - point;
			Vector3 direction = heading / heading.magnitude;

			Ray ray = new Ray(point, direction);
			RaycastHit[] hitInfo = Physics.RaycastAll(ray, maxDistance);

			if (hitInfo!=null)
				foreach(RaycastHit hit in hitInfo)
					if(hit.transform.gameObject == gameObject)
						return true;

			return false;
		}

	}

}
