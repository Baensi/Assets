using UnityEngine;
using Engine.AI;
using Engine.AI.Behavior;
using System;
using Engine.Player.Animations;

namespace Engine.Player.Behavior {

	[RequireComponent(typeof(Actions))]
	public class PlayerBehavior : MonoBehaviour, AIC {

		private Actions actions;

			void Start() {
				actions = GetComponent<Actions>();
			}

		public IAnimationBehavior getAnimationBehavior() {
			return actions;
		}

		public IAudioBehavior getAudioBehavior() {
			return null;
		}

		public void getDamage(PlayerStates value) {
			GamePlayer.states+=value;
		}

		public PlayerStates getDamageStates() {
			return null;
		}

		public AIFraction getFraction() {
			return AIFraction.Friend;
		}

		public PlayerSpecifications getSpecifications() {
			return GamePlayer.specifications;
		}

		public PlayerStates getStates() {
			return GamePlayer.states;
		}

		public GameObject toObject() {
			return gameObject;
		}

	}

}
