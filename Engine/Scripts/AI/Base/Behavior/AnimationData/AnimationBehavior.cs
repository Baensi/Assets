using System;
using UnityEngine;

namespace Engine.AI.Behavior {

	public class AnimationBehavior : IAnimationBehavior {

		private Animator animator;

			public AnimationBehavior(Animator animator) {
				this.animator = animator;
			}

		public void setAttack() {
			
		}

		public void setIdle() {

		}

		public void setRun() {

		}

		public void setSneak() {

		}

		public void setWalk() {

		}

	}

}
