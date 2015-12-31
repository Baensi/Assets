using UnityEngine;
using Engine.AI;
using Engine.AI.Behavior;
using System;
using Engine.Player.Animations;

namespace Engine.Player.Behavior {

	[RequireComponent(typeof(Actions))]
	[RequireComponent(typeof(Animator))]
	public class PlayerBehavior : EnemyBehaviorAI {

		private Animator animator;
		private Actions  actions;

			void Start() {
			
				animator = GetComponent<Animator>();
				actions  = GetComponent<Actions>();
			
				fraction = AIFraction.Friend;
				base.OnStartEnemyBehaviorAI(animator,null);

			}

		public override void getDamage(PlayerStates value) {
			GamePlayer.states+=value;
		}

		public override PlayerStates getDamageStates() {
			return null;
		}

		public override PlayerSpecifications getSpecifications() {
			return GamePlayer.specifications;
		}

		public override PlayerStates getStates() {
			return GamePlayer.states;
		}

	}

}
