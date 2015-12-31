using System;
using UnityEngine;
using Engine.Player;

namespace Engine.AI.Behavior {

	/// <summary>
	/// Модель поведения враждебного герою AI
	/// </summary>
	[RequireComponent(typeof(PathBehavior))]
	public abstract class EnemyBehaviorAI : MonoBehaviour, AIC {

		[SerializeField] public AIFraction fraction;

		private PathBehavior       pathBehavior;
		private IAnimationBehavior animationBehavior;
		private IAudioBehavior     audioBehavior;

		/// <summary>
		/// Возвращает характеристики AI
		/// </summary>
		/// <returns></returns>
		public abstract PlayerSpecifications getSpecifications();
		/// <summary>
		/// Возвращает статы AI
		/// </summary>
		/// <returns></returns>
		public abstract PlayerStates         getStates();
		public abstract PlayerStates         getDamageStates();
		/// <summary>
		/// Увеличивает текущие статы AI на набор value
		/// </summary>
		/// <param name="value">набор статов, изменяющих значение</param>
		public abstract void getDamage(PlayerStates value);

		public AIFraction getFraction() {
			return fraction;
		}

			public void OnStartEnemyBehaviorAI(Animator animator) {
				pathBehavior = gameObject.GetComponent<PathBehavior>();
				audioBehavior = new AudioBehavior();
				animationBehavior = new AnimationBehavior(animator);
			}

			public void OnStartEnemyBehaviorAI(Animator animator, PathBehavior pathBehavior) {
				this.pathBehavior = pathBehavior;
				audioBehavior = new AudioBehavior();
				animationBehavior = new AnimationBehavior(animator);
			}

		public PathBehavior getPathBehavior() {
			return pathBehavior;
		}

		public IAnimationBehavior getAnimationBehavior() {
            return animationBehavior;
        }

		public IAudioBehavior getAudioBehavior() {
			return audioBehavior;
		}

		public GameObject toObject() {
			return gameObject;
		}

	}

}
