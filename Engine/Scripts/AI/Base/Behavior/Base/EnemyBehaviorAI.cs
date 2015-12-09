using System;
using UnityEngine;

namespace Engine.AI.Behavior {

	/// <summary>
	/// Модель поведения враждебного герою AI
	/// </summary>
	[RequireComponent(typeof(PathBehavior))]
	public class EnemyBehaviorAI : MonoBehaviour, IModelBehaviorAI {
		
		private PathBehavior       pathBehavior;
		private IAnimationBehavior animationBehavior;
		private IAudioBehavior     audioBehavior;

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
