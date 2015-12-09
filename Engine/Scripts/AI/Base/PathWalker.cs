using UnityEngine;
using Engine.AI.Behavior;

namespace Engine.AI {

	/// <summary>
	/// Объект-"Ходок", перемещается по карте
	/// </summary>
	[RequireComponent(typeof(Animator))]
	[RequireComponent(typeof(NavMeshAgent))]
	public class PathWalker : EnemyBehaviorAI {

		[SerializeField] public float normalSpeed  = 1f;
		[SerializeField] public float warningSpeed = 2f;
		[SerializeField] public float enemySpeed   = 0.8f;

		[SerializeField] public float normalAngularSpeed  = 140f;
		[SerializeField] public float warningAngularSpeed = 300f;
		[SerializeField] public float enemyAngularSpeed   = 380f;

		/// <summary>Объект, за которым наблюдает ходок</summary>
		[SerializeField] public Transform target = null;

		/// <summary>Дальность видимости ходока</summary>
		[SerializeField] public float     seeDistance;

		/// <summary>Угол обзора ходока</summary>
		[SerializeField] public float     seeAngle;

		/// <summary>Точка, к которой стремится ходок</summary>
		[SerializeField] private Vector3   point;

		private AgressionStateAI state = AgressionStateAI.Normal;

		private NavMeshAgent agent;
		private Animator animator;

		/// <summary>Статус ходока</summary>
		public AgressionStateAI State {
			get { return state; }
			set {

				state = value;

				switch (state) {
					case AgressionStateAI.Normal:
						agent.speed = normalSpeed;
						agent.angularSpeed = normalAngularSpeed;
						break;
					case AgressionStateAI.Warning:
						agent.speed = warningSpeed;
						agent.angularSpeed = warningAngularSpeed;
						break;
					case AgressionStateAI.Enemy:
						agent.speed = enemySpeed;
						agent.angularSpeed = enemyAngularSpeed;
						break;

				}
			}
		}

		public void setMoveSpeed(float speed) {
			agent.speed = speed;
		}

		public void setAngularSpeed(float angularSpeed) {
			agent.angularSpeed = angularSpeed;
		}

		public NavMeshAgent getAgent() {
			return agent;
		}

		/// <summary>
		/// Устанавливает точку, к которой ходоку надо идти
		/// </summary>
		/// <param name="point"></param>
		public void setPoint(Vector3 point) {
			this.point = point;
		}

		public Vector3 getPoint() {
			return point;
		}

		/// <summary>
		/// Возвращает расстояние до цели, на которую смотрит ходок, если ходок не смотрит на цель, возвращает дистанцию до точки, к которой он идёт
		/// </summary>
		/// <returns></returns>
		public float getDistance() {
			return target == null ? Vector3.Distance(transform.position, point) : Vector3.Distance(transform.position, target.position);
		}

		public void OnStart() {
			agent = gameObject.GetComponent<NavMeshAgent>();
			animator = GetComponent<Animator>();
			base.OnStart(animator);
		}


		public new void OnUpdate() {
			base.OnUpdate();

			agent.SetDestination(point);

			if (target == null)
				return;

			transform.LookAt(target);
		}

	}

}
