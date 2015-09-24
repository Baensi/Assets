using System;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using UnityStandardAssets.Utility;
using Random = UnityEngine.Random;

using Engine.Player.Animations;

namespace Engine.Player.Movement.Movements {

	public enum EMovementType : int {
		
		inground   = 0, // наземное перемещение
		underwater = 1  // подводное перемещение
		
	};
	
}