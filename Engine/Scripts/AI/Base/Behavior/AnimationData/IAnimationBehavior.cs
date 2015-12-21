using System;
using UnityEngine;

namespace Engine.AI.Behavior {

	public interface IAnimationBehavior {

		void setIdle(); // стоит

		void setWalk(); // идёт

		void setSneak(); // крадётся

		void setRun(); // бежит

		void setAttack(); // наносит урон

		void getDamage(); // получает урон

		void setDie(); // умирает

	}

}
