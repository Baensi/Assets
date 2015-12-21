using System;
using System.Collections.Generic;
using UnityEngine;
using Engine.Player;
using Engine.Calculators;

namespace Engine.AI.Behavior {

	/// <summary>
	/// ИИ боевой крысы
	/// </summary>
	public class RatBehaviorAI : MeleeAI {

		//private float timeStamp;

		void Start() {

			states         = new PlayerStates();
			damage         = new PlayerStates();
			specifications = new PlayerSpecifications();

				specifications.strength     = 3;
				specifications.stamina      = 4;
				specifications.intelligence = 0;
				specifications.agility      = 4;

				damage.health = 50f; // урон в 2 ед.

				states.damageMelee               = 2f;
				states.chanceCriticalDamageMelee = 0.05f;
				states.criticalDamageMelee       = 0.2f;
				states.health                    = 30f;
				states.maxHealth                 = 30f;

			OnStart();
		} 

		void Update() {

			//if (Time.time - timeStamp >= 0.01f) {

				OnUpdate();

				//timeStamp = Time.time;

			//}

		}

	}

}
