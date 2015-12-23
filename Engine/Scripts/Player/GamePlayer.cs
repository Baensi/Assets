using System;
using System.Collections.Generic;
using Engine.Player;
using Engine.Objects.Types;
using Engine.Objects.Weapon;
using UnityEngine;

namespace Engine {

	public static class GamePlayer {

		public static PlayerLevel          level          = new PlayerLevel();
		public static PlayerStates         states         = new PlayerStates();
		public static PlayerSpecifications specifications = new PlayerSpecifications();
		public static PlayerSkills         skills         = new PlayerSkills();

		public static PlayerCloth          cloth          = new PlayerCloth();

	}

}
