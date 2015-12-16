using UnityEngine;
using Engine.AI.Behavior;

namespace Engine.AI {

	/// <summary>
	/// Объект-"Ходок", перемещается по карте
	/// </summary>
	[RequireComponent(typeof(Animator))]
	[RequireComponent(typeof(NavMeshAgent))]
	public class PathWalker : EnemyBehaviorAI {

		/// <summary> дистанция на которую NPC может покинуть текущую точку </summary>
		[SerializeField] public float maxOutDistance = 45f;

		/// <summary> Дальность атаки </summary>
		[SerializeField] public float minAttackDistance = 2f;

		/// <summary> дистанция до конечной точки которой можно принебречь (считать что AI достиг цели) </summary>
		[SerializeField] public float minMovDistance = 0.2f;

		/// <summary> Время которое игрока не должны видеть чтобы уйти от погони </summary>
		[SerializeField] public float enemyMemory = 10f;

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

		private Transform player;
		private float timeStamp;


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

		public void OnStartWalker() {

			agent = gameObject.GetComponent<NavMeshAgent>();
			animator = GetComponent<Animator>();
			base.OnStartEnemyBehaviorAI(animator);

			player = SingletonNames.getPlayer().transform;
			State = AgressionStateAI.Normal;
			
        }


		public void OnUpdateWalker() {
			base.OnUpdateEnemyBehaviorAI();

			float distanceToPoint = Vector3.Distance(transform.position, point);

			if (distanceToPoint <= minMovDistance || (distanceToPoint <= minAttackDistance && target!=null)) {

				switch (State) {
					case AgressionStateAI.Normal:
						getAnimationBehavior().setIdle();
						break;
					case AgressionStateAI.Enemy:
						getAnimationBehavior().setAttack();
						break;
				}

				Debug.LogWarning("STAND");

			} else {

				agent.SetDestination(point);

				switch (State) {
					case AgressionStateAI.Enemy:
						getAnimationBehavior().setRun();
						break;
					case AgressionStateAI.Normal:
						getAnimationBehavior().setWalk();
						break;
					case AgressionStateAI.Warning:
						getAnimationBehavior().setSneak();
						break;
				}

            }

			if (Vector3.Distance(player.position, transform.position) <= seeDistance) {

				if(LookViewService.getInstance().isSee(transform.position, player.gameObject, seeDistance)) {
					State = AgressionStateAI.Enemy;
					target = player;
					timeStamp = Time.time;
                }

			} else {

				if (Time.time - timeStamp >= enemyMemory) {

					State = AgressionStateAI.Normal;
					target = null;

				}

			}

			if (target != null)
				transform.LookAt(target);
		}

	}

}
