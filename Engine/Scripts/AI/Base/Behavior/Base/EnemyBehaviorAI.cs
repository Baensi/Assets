using System;
using UnityEngine;
using Engine.Player;

namespace Engine.AI.Behavior {

	/// <summary>
	/// Модель поведения враждебного герою AI
	/// </summary>
	[RequireComponent(typeof(PathBehavior))]
	public class EnemyBehaviorAI : MonoBehaviour, IModelBehaviorAI, IAIState {

		private PathBehavior pathBehavior;
		private IAnimationBehavior animationBehavior;
		private IAudioBehavior audioBehavior;

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

			public void OnStart(Animator animator) {
				pathBehavior = GetComponent<PathBehavior>();
				audioBehavior = new AudioBehavior();
				animationBehavior = new AnimationBehavior(animator);
			}

			public void OnUpdate() {

			}

		public IAnimationBehavior getAnimationBehavior() {
            return animationBehavior;
        }

		public IAudioBehavior getAudioBehavior() {
			return audioBehavior;
		}

	}

}
