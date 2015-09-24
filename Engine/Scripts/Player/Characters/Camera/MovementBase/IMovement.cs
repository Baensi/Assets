using System;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using UnityStandardAssets.Utility;
using Random = UnityEngine.Random;

using Engine.Player.Animations;
using Engine.Player.Attack;

namespace Engine.Player.Movement.Movements {

	public interface IMovement {
		
		void update();
		void fixUpdate();

		void addImpulse(Vector3 velocity);
		Vector3 getImpulse();

		void setUp(Actions actions,
				   MouseLook mouseLook,
				   FOVKick fovKick,
				   CurveControlledBob headBob,
				   LerpControlledBob jumpBob,
				   AttackController attackController);

	}
	
}