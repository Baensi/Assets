using System;
using UnityEngine;
using Engine.Player;

namespace Engine.AI.Behavior {

	/// <summary>
	/// Модель поведения враждебного герою AI
	/// </summary>
	[RequireComponent(typeof(PathBehavior))]
	public abstract class EnemyBehaviorAI : MonoBehaviour, IModelBehaviorAI, IAIState, IMoveBehavior {

		private PathBehavior       pathBehavior;
		private IAnimationBehavior animationBehavior;
		private IAudioBehavior     audioBehavior;

		/// <summary>
		/// Возвращает характеристики AI
		/// </summary>
		/// <returns></returns>
		public virtual PlayerSpecifications getSpecifications() { return PlayerSpecifications.NULL; }
		/// <summary>
		/// Возвращает статы AI
		/// </summary>
		/// <returns></returns>
		public virtual PlayerStates         getStates() { return null; }

			public void OnStartEnemyBehaviorAI(Animator animator) {
				pathBehavior = GetComponent<PathBehavior>();
				audioBehavior = new AudioBehavior();
				animationBehavior = new AnimationBehavior(animator);
			}

			public void OnUpdateEnemyBehaviorAI() {



			}

		public IAnimationBehavior getAnimationBehavior() {
            return animationBehavior;
        }

		public IAudioBehavior getAudioBehavior() {
			return audioBehavior;
		}

		public abstract void OnMoveIteration();
		public abstract void OnIdleIteration();
		public abstract void OnAttackIteration();
		public abstract bool isMinIdleDistance(Vector3 point1, Vector3 point2, float minMovDistance);
		public abstract bool isMinAttackDistance(Transform point1, Transform point2, float minAttackDistance, Transform target);
		public abstract bool isSeeDistanceGameObject(Transform targetObject, Transform seeObject);
		public abstract bool isSeeGameObject(Transform targetObject, Transform seeObject, float seeAngle, float seeDistance);

	}

}
