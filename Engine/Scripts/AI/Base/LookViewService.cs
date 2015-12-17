using UnityEngine;

namespace Engine.AI {

	/// <summary>
	/// Сервис выполняющий расчёты по
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
		/// Проверяет, виден ли объект gameObject из точки point
		/// </summary>
		/// <param name="point">точка откуда направлен взгляд AI</param>
		/// <param name="gameObject">Объект который AI пытается увидеть</param>
		/// <param name="maxAngle">Угол зрения</param>
		/// <param name="maxDistance">Дальность зрения</param>
		/// <returns>Возвращает логический результат - ВИДЕН ЛИ объект gameObject? из указанной точки point или нет</returns>
		public bool isSee(Vector3 point, GameObject gameObject, float maxAngle, float maxDistance) {
			
			Vector3 heading   = gameObject.transform.position - point;
			Vector3 direction = heading / heading.magnitude;

			Ray ray = new Ray(point, direction);
			RaycastHit hitInfo = new RaycastHit();

				if(Physics.Raycast(ray, out hitInfo, maxDistance))
					return hitInfo.transform.gameObject == gameObject;

			return false;
		}


	}

}
