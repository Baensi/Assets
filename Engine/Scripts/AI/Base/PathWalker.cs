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


		public override void OnMoveIteration() {

			switch (State) {
				case AgressionStateAI.Enemy:

						if(target!=null)
							agent.SetDestination(target.position);

					getAnimationBehavior().setRun();
					break;
				case AgressionStateAI.Normal:

						agent.SetDestination(point);

					getAnimationBehavior().setWalk();
					break;
				case AgressionStateAI.Warning:

						agent.SetDestination(point);

					getAnimationBehavior().setSneak();
					break;
			}

		}

		public override void OnIdleIteration() {

			if(State == AgressionStateAI.Normal)
				getAnimationBehavior().setIdle();

		}

		public override void OnAttackIteration() {

			if (State == AgressionStateAI.Enemy)
				getAnimationBehavior().setAttack();

        }

		public override bool isMinIdleDistance(Vector3 point1, Vector3 point2, float minMovDistance) {
			return Vector3.Distance(point1,point2) <= minMovDistance;
        }

		public override bool isMinAttackDistance(Transform point1, Transform point2, float minAttackDistance, Transform target) {
			if (point1 == null || point2 == null)
				return false;
			return Vector3.Distance(point1.position,point2.position) <= minAttackDistance && target != null;
        }

		public override bool isSeeDistanceGameObject(Transform targetObject, Transform seeObject) {
			return Vector3.Distance(targetObject.position, seeObject.position) <= seeDistance;
		}

		public override bool isSeeGameObject(Transform targetObject, Transform seeObject, float seeAngle, float seeDistance) {
			return LookViewService.getInstance().isSee(seeObject.position, targetObject.gameObject, seeAngle, seeDistance);
        }

		public void OnUpdateWalker() {
			base.OnUpdateEnemyBehaviorAI();

			bool moveFlag = true;

			if (isMinIdleDistance(transform.position, point, minMovDistance)) {
				OnIdleIteration();
				moveFlag = false;
			}

			if(isMinAttackDistance(transform, target, minAttackDistance, target)) {
				OnAttackIteration();
				moveFlag = false;
			}

			if(moveFlag)
				OnMoveIteration();

			if (isSeeDistanceGameObject(player,transform)) {

				if(isSeeGameObject(player,transform,seeAngle,seeDistance)) {

					State = AgressionStateAI.Enemy;
					target = player;
					timeStamp = Time.time;

                }

				if (target != null)
					transform.LookAt(target);

			} else {

				if (Time.time - timeStamp >= enemyMemory) { // смотрим, как долго персонаж пропадает из вида
														    // если время больше "памяти" AI, забываем цель
				
					State = AgressionStateAI.Normal;
					target = null;

				}

			}

		}

	}

}
