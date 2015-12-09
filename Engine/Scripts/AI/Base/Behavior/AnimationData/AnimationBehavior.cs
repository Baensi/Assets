using System;
using UnityEngine;

namespace Engine.AI.Behavior {

	public class AnimationBehavior : IAnimationBehavior {

		public const string IDLE_COUNT   = "IdleCount";
		public const string ATTACK_COUNT = "AttackCount";

		public const string ATTACK = "Attack";
		public const string IDLE   = "Idle";
		public const string WALK   = "Walk";
		public const string RUN    = "Run";
		public const string SNEAK  = "Sneak";

		private Animator animator;

			public AnimationBehavior(Animator animator) {
				this.animator = animator;
			}

		public void setAttack() {
			setVariantValue(animator.GetInteger(ATTACK_COUNT), ATTACK);
        }

		public void setIdle() {
			setVariantValue(animator.GetInteger(IDLE_COUNT), IDLE);
		}

		private void setVariantValue(int variation, string valueName) {

			if (variation <= 1) {

				animator.SetInteger(valueName, 1);

			} else {

				int index = UnityEngine.Random.Range(0, variation);
				animator.SetInteger(valueName, index);
			}

		}

		public void setRun() {
			animator.SetInteger(SNEAK, 0);
			animator.SetInteger(WALK,  0);
			animator.SetInteger(RUN,   1);
		}

		public void setSneak() {
			animator.SetInteger(RUN,   0);
			animator.SetInteger(SNEAK, 1);
		}

		public void setWalk() {
			animator.SetInteger(RUN,  0);
			animator.SetInteger(WALK, 1);
		}

	}

}
