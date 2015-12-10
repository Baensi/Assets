using System;
using System.Collections.Generic;
using UnityEngine;
using Engine.Player;

namespace Engine.AI.Behavior {

	/// <summary>
	/// ИИ боевой крысы
	/// </summary>
	public class RatBehaviorAI : MeleeAI {

		private PlayerSpecifications specifications = PlayerSpecifications.Create(3, 4, 0, 4);
		private PlayerStates states = new PlayerStates() {
			damageMelee = 2f,
			criticalDamageMelee = 0.05f,
			health = 30,
			maxHealth=30,
		};

		public override PlayerSpecifications getSpecifications() {
			return specifications;
		}

		public override PlayerStates getStates() {
			return states;
		}

			void Start() {
				base.OnStart();


			} 

		void Update() {
			base.OnUpdate();


		}

	}

}
