using System;
using UnityEngine;

namespace Engine.Player {

	[Serializable]
	public struct PlayerSpecifications {
		
		public byte strength;
		public byte stamina;
		public byte intelligence;
		public byte agility;

		public static PlayerSpecifications NULL;

		public static PlayerSpecifications Create(byte strength,
												  byte stamina,
												  byte intelligence,
												  byte agility) {

			PlayerSpecifications result = new PlayerSpecifications();
				result.strength = strength;
				result.stamina = stamina;
				result.intelligence = intelligence;
				result.agility = agility;
			return result;

		}

	}
	                     
}