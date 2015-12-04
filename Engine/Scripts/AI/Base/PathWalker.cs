using UnityEngine;

namespace Engine.AI {

	/// <summary>
	/// Объект-"Ходок", перемещается по карте
	/// </summary>
	[RequireComponent(typeof(NavMeshAgent))]
	public class PathWalker : MonoBehaviour {

		/// <summary>Объект, за которым наблюдает ходок</summary>
		[SerializeField] public Transform target = null;

		/// <summary>Дальность видимости ходока</summary>
		[SerializeField] public float     seeDistance;

		/// <summary>Угол обзора ходока</summary>
		[SerializeField] public float     seeAngle;

		/// <summary>Точка, к которой стремится ходок</summary>
		[SerializeField] public Vector3   point;

		/// <summary>Статус ходока</summary>
		[SerializeField] public AgressionStateAI state = AgressionStateAI.Normal;

		private NavMeshAgent agent;

		/// <summary>
		/// Устанавливает точку, к которой ходоку надо идти
		/// </summary>
		/// <param name="point"></param>
		public void setPoint(Vector3 point) {
			this.point = point;
		}

		/// <summary>
		/// Возвращает расстояние до цели, на которую смотрит ходок, если ходок не смотрит на цель, возвращает дистанцию до точки, к которой он идёт
		/// </summary>
		/// <returns></returns>
		public float getDistance() {
			return target == null ? Vector3.Distance(transform.position, point) : Vector3.Distance(transform.position, target.position);
		}

			public void WalkerStart() {
				agent = gameObject.GetComponent<NavMeshAgent>();
			}


		public void WalkerUpdate() {
			agent.SetDestination(point);

			if (target == null)
				return;

			transform.LookAt(target);
		}

	}

}
