using System;
using UnityEngine;
using System.Collections;
using Engine.Player.Data;

namespace Engine.Player {

	//[RequireComponent(typeof(PlayerLevel))]
	//[RequireComponent(typeof(PlayerStates))]
	//[RequireComponent(typeof(PlayerSpecifications))]
	//[RequireComponent(typeof(PlayerSkills))]
	public class UPlayerInstance : MonoBehaviour {

	[SerializeField] public PlayerLevel  level;
	[SerializeField] public PlayerStates states;
	[SerializeField] public PlayerSpecifications specifications;
	[SerializeField] public PlayerSkills skills;

			// Инициализация свойств персонажа
		void Start() {
			level          = GetComponent<PlayerLevel>();
			states         = GetComponent<PlayerStates>();
			specifications = GetComponent<PlayerSpecifications>();

			skills         = GetComponent<PlayerSkills>();
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
