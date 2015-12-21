using System;
using UnityEngine;

namespace Engine.AI.Behavior {

	public class AnimationBehavior : IAnimationBehavior {

		public const string IDLE_COUNT       = "IdleCount";
		public const string ATTACK_COUNT     = "AttackCount";
		public const string GET_DAMAGE_COUNT = "GetDamageCount";
		public const string DIE_COUNT        = "DieCount";

		public const string ATTACK     = "Attack";
		public const string IDLE       = "Idle";
		public const string WALK       = "Walk";
		public const string RUN        = "Run";
		public const string SNEAK      = "Sneak";
		public const string GET_DAMAGE = "GetDamage";
		public const string DIE        = "Die";

		private Animator animator;

			public AnimationBehavior(Animator animator) {
				this.animator = animator;
			}

		/// <summary>
		/// Устанавливает анимацию атаки
		/// </summary>
		public void setAttack() {
			setVariantValue(animator.GetInteger(ATTACK_COUNT), ATTACK);
        }

		/// <summary>
		/// Устаналивает анимацию получения урона
		/// </summary>
		public void getDamage() {
			setVariantValue(animator.GetInteger(GET_DAMAGE_COUNT), GET_DAMAGE);
        }

		/// <summary>
		/// Устанавливает анимацию смерти
		/// </summary>
		public void setDie() {
			setVariantValue(animator.GetInteger(DIE_COUNT), DIE);
        }

		/// <summary>
		/// Устанавливает анимацию ожидания
		/// </summary>
		public void setIdle() {
			setVariantValue(animator.GetInteger(IDLE_COUNT), IDLE);
		}

		/// <summary>
		/// Произвольно устанавливает одно значение анимации из набора вариаций
		/// </summary>
		/// <param name="variation">Максимальное число вариаций анимации</param>
		/// <param name="valueName">Имя анимации</param>
		private void setVariantValue(int variation, string valueName) {

			if (variation <= 1) {

				animator.SetInteger(valueName, 1);

			} else {

				int index = UnityEngine.Random.Range(0, variation);
				animator.SetInteger(valueName, index);
			}

		}

		/// <summary>
		/// Устанавливает анимацию бега
		/// </summary>
		public void setRun() {

			animator.SetInteger(IDLE,  0);

			animator.SetInteger(SNEAK, 0);
			animator.SetInteger(WALK,  0);
			animator.SetInteger(RUN, 1);

		}

		/// <summary>
		/// Устанавливает анимацию "красться"
		/// </summary>
		public void setSneak() {

			animator.SetInteger(IDLE, 0);

			animator.SetInteger(RUN,   0);
			animator.SetInteger(SNEAK, 1);
		}

		/// <summary>
		/// Устанавливает анимацию хотьбы
		/// </summary>
		public void setWalk() {

			animator.SetInteger(IDLE, 0);

			animator.SetInteger(RUN,  0);
			animator.SetInteger(WALK, 1);

		}

	}

}
