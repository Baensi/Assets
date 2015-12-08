using System;
using UnityEngine;

namespace Engine.AI.Behavior {

	public interface IAnimationBehavior {

		void setIdle();

		void setWalk();

		void setSneak();

		void setRun();

		void setAttack();

	}

}
