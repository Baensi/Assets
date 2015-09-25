using System;
using UnityEngine;
using System.Collections;
using Engine.Player.Data;

namespace Engine.Player {

	public class UPlayerInstance : MonoBehaviour {

		[SerializeField] public PlayerLevel          level;
		[SerializeField] public PlayerStates         states;
		[SerializeField] public PlayerSpecifications specifications;
		[SerializeField] public PlayerSkills         skills;

		void OnValidate() {

			if (level.experienceNextLevel<50)
				level.experienceNextLevel=50;

			if (states.maxHealth<100)
				states.maxHealth = 100.0f;

			if (states.health<100)
				states.health = 100.0f;

			if (states.maxEnergy<100)
				states.maxEnergy = 100.0f;

			if (states.energy<100)
				states.energy = 100.0f;

			if (states.maxMana<50)
				states.maxMana = 50.0f;

			if (states.mana<50)
				states.mana = 50.0f;

				if (states.damageMelee<1)
					states.damageMelee = 1.0f;

				if (states.damageRanged<1)
					states.damageRanged = 1.0f;

				if (states.damageMagic<1)
					states.damageMagic = 1.0f;


			if (specifications.strength<5)
				specifications.strength = 5;

			if (specifications.stamina<5)
				specifications.stamina = 5;

			if (specifications.intelligence<5)
				specifications.intelligence = 5;

			if (specifications.agility<5)
				specifications.agility = 5;

		}

			// Инициализация свойств персонажа
		void Start() {

		}

		public PlayerStates getStates(){
			return states;
		}
		
		public PlayerLevel getLevel(){
			return level;
		}
		
		public PlayerSpecifications getSpecifications(){
			return specifications;
		}
		
		public PlayerSkills getSkills(){
			return skills;
		}

	}

}
