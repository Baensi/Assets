using System;
using UnityEngine;

namespace Engine.AI.Behavior {

	/// <summary>
	/// Модель поведения враждебного герою AI
	/// </summary>
	public class EnemyBehaviorAI : MonoBehaviour, IModelBehaviorAI {

		[SerializeField] public Animator     animator;
		[SerializeField] public PathBehavior pathBehavior;
		
		private IAnimationBehavior animationBehavior;
		private IAudioBehavior     audioBehavior;

			public void OnStart() {
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
