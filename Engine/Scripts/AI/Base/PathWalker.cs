using UnityEngine;
using System.Collections.Generic;
using Engine.AI.Behavior;
using Engine.Calculators;
using Engine.Player;

namespace Engine.AI {

	/// <summary>
	/// Объект-"Ходок", перемещается по карте
	/// </summary>
	[RequireComponent(typeof(Animator))]
	[RequireComponent(typeof(NavMeshAgent))]
	public class PathWalker : EnemyBehaviorAI {

		private bool destroyed = false;

		private const int IDLE  = 0x00;
		private const int SNEAK = 0x01;
		private const int WALK  = 0x02;
		private const int RUN   = 0x04;

		/// <summary> Задержка перед сканированием видимых объектов </summary>
		private const float SEE_UPDATE_TIME = 0.2f;

		/// <summary> Максимальное время которое AI может стоять на месте без дела </summary>
		[SerializeField] public float maxIdleDelay = 15f;

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

		private float seeDelayTimeStamp;
		private float memoryTimeStamp;
		private float idleTimeStamp;

		private NavMeshPath  tmpPath = new NavMeshPath();
		private NavMeshAgent agent;
		private Animator     animator;

		private bool idle;
		private int  walkState;

		protected PlayerSpecifications specifications;
		protected PlayerStates         damage;
		protected PlayerStates         states;

		/// <summary>
		/// Возвращает текущие характеристики ходока
		/// </summary>
		/// <returns></returns>
		public override PlayerSpecifications getSpecifications() {
			return specifications;
		}

		/// <summary>
		/// Возвращает текущие статы ходока
		/// </summary>
		/// <returns></returns>
		public override PlayerStates getStates() {
			return states;
		}

		/// <summary>
		/// Возвращает текущий урон-по умолчанию ходока
		/// </summary>
		/// <returns></returns>
		public override PlayerStates getDamageStates() {
			return damage;
		}

		/// <summary>
		/// Наносит урон this
		/// </summary>
		/// <param name="value">Значение наносимого урона</param>
		public override void getDamage(PlayerStates value) {
			states -= AttackCalculator.doProtection(getStates(),getSpecifications(),value); // пытаемся защититься нанесённому урону
		}

		/// <summary>
		/// Возвращает логическое значение "AI стоит на месте"
		/// </summary>
		/// <returns></returns>
		public bool isIdle() {
			return walkState == IDLE;
        }

		/// <summary>
		/// Возвращает логическое значение "AI крадётся"
		/// </summary>
		public bool isSneak() {
			return walkState == SNEAK;
		}

		/// <summary>
		/// Возвращает логическое значение "AI идёт"
		/// </summary>
		public bool isWalk() {
			return walkState == WALK;
		}

		/// <summary>
		/// Возвращает логическое значение "AI бежит"
		/// </summary>
		public bool isRun() {
			return walkState == RUN;
		}

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

			if(tmpPath==null)
				tmpPath = new NavMeshPath();

			if (!NavMesh.CalculatePath(transform.position, point, NavMesh.AllAreas, tmpPath)) // пытаемся построить путь
				return;

			int last = tmpPath.corners.Length - 1;

			if(last<0)
				return;

			this.point = tmpPath.corners[last]; // используем в качестве конечной точки - последнюю точку построенного пути (она может сильно отличаться от точки в аргументе функции!!)
			
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

			State = AgressionStateAI.Normal;
			
        }


		public override void OnSeeIteration() {

			if(target!=null)
				return;

			List<IStateAI> seeNPC = LookViewService.getInstance().getSeeAIObjects(this, new Ray(transform.position, transform.forward),seeDistance,seeAngle);

			foreach(IStateAI ai in seeNPC) {

				if (getFraction() != ai.getFraction()) { // ходок видит врага
					State  = AgressionStateAI.Enemy;
					target = ai.toObject().transform;
					memoryTimeStamp = Time.time; // запоминаем момент когда видим цель
					return;
				}

			}

		}

		public override void OnMoveIteration() {

			if(destroyed)
				return;

			switch (State) {
				case AgressionStateAI.Enemy:

						if(target!=null)
							agent.SetDestination(target.position);

					getAnimationBehavior().setRun();
					walkState = RUN;
					break;
				case AgressionStateAI.Normal:

						agent.SetDestination(point);

					getAnimationBehavior().setWalk();
					walkState = WALK;
					break;
				case AgressionStateAI.Warning:

						agent.SetDestination(point);

					getAnimationBehavior().setSneak();
					walkState = SNEAK;
					break;
			}

		}

		public override void OnIdleIteration() {

			if (State == AgressionStateAI.Normal) {

				if (walkState == IDLE && Time.time - idleTimeStamp >= maxIdleDelay) { // проверяем, если AI простаиваает уже достаточно долго



				} else {

					getAnimationBehavior().setIdle(); // проигрываем анимацию ожидания

					if (walkState != IDLE) // если персонаж до этого совершал другие действия
						idleTimeStamp = Time.time;

					walkState = IDLE;

				}

			} else {



			}

		}

		public override void OnAttackIteration() {

			if (target!=null && State == AgressionStateAI.Enemy) {
				getAnimationBehavior().setAttack();
				
				IStateAI trg = target.gameObject.GetComponent<EnemyBehaviorAI>() as IStateAI;

				if(trg!=null)
					trg.getDamage(AttackCalculator.createDamage(getStates(),getSpecifications(),getDamageStates()));

			}

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

		public void DoDie() {
			destroyed = true;
			Destroy(this.gameObject);
		}

		public void OnUpdateWalker() {

			if(destroyed)
				return;

			if (states.health <= 0) // ходок умер
				DoDie();

			base.OnUpdateEnemyBehaviorAI();

			bool moveFlag = true;

				// Пытаемся просканировать видимую местность не чаще раза в SEE_UPDATE_TIME сек.
			if (Time.time - seeDelayTimeStamp >= SEE_UPDATE_TIME) {
				OnSeeIteration();
				seeDelayTimeStamp = Time.time;
			}

				// Проверяем,
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

			if (target != null && isSeeDistanceGameObject(target,transform)) {

				memoryTimeStamp = Time.time;
				transform.LookAt(target);

			} else {

				if (Time.time - memoryTimeStamp >= enemyMemory) { // смотрим, как долго персонаж пропадает из вида
														          // если время больше "памяти" AI, забываем цель
				
					State = AgressionStateAI.Normal;
					target = null;

				}

			}

		}

	}

}
