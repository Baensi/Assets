using System;
using Engine.Player;

namespace Engine.AI {

	public interface IAIState {

		PlayerSpecifications getSpecifications();

		PlayerStates         getStates();

	}

}
