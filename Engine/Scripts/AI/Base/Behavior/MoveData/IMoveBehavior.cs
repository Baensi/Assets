using UnityEngine;

namespace Engine.AI.Behavior {

	public interface IMoveBehavior {

		void OnFindNewIdlePoint();

		/// <summary> Итерация прощупывания окружения </summary>
		void OnSeeIteration();

		/// <summary> Итерация движения </summary>
		void OnMoveIteration();

		/// <summary> Итерация "простоя" </summary>
		void OnIdleIteration();

		/// <summary> Итерация атаки </summary>
		void OnAttackIteration();

		/// <summary>
		/// Проверяет, находится ли объект достаточно близко к point2
		/// </summary>
		/// <param name="point1">точка нахождение объекта</param>
		/// <param name="point2">конечная точка, к которой объект идёт</param>
		/// <param name="minMovDistance"> минимальное расстояние, считаемое как область допустимой погрешности </param>
		/// <returns></returns>
		bool isMinIdleDistance(Vector3 point1, Vector3 point2, float minMovDistance);

		/// <summary>
		/// Проверяет, находится ли объект на расстоянии достаточном для совершении атаки
		/// </summary>
		/// <param name="point1">Точка нахождение объекта</param>
		/// <param name="point2">Конечная точка атаки</param>
		/// <param name="minAttackDistance">дистанция атаки</param>
		/// <param name="target">атакуемый объект</param>
		/// <returns></returns>
		bool isMinAttackDistance(Transform point1, Transform point2, float minAttackDistance, Transform target);

		/// <summary>
		/// Проверяет, находится ли объект targetObject на расстоянии области видимости объекта seeObject
		/// </summary>
		/// <param name="targetObject">объект на который пытается смотреть seeObject</param>
		/// <param name="seeObject">объект который пытается смотреть на targetObject</param>
		/// <returns></returns>
		bool isSeeDistanceGameObject(Transform targetObject, Transform seeObject);

		/// <summary>
		/// Проверяет, видел ли объект targetObject объектом seeObject
		/// </summary>
		/// <param name="targetObject">объект на который пытается смотреть seeObject</param>
		/// <param name="seeObject">объект который пытается смотреть на targetObject</param>
		/// <param name="seeAngle">Угол обзора смотрящего обхекта</param>
		/// <param name="seeDistance">Дистанция обзора смотрящего объекта</param>
		/// <returns></returns>
		bool isSeeGameObject(Transform targetObject, Transform seeObject, float seeAngle, float seeDistance);

	}

}
