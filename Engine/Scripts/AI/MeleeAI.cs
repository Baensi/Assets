using System;
using UnityEngine;
using Engine.AI.Behavior;

namespace Engine.AI {

	/// <summary>
	/// Класс - "враг ближнего боя"
	/// </summary>
	public class MeleeAI : PathWalker {

		[SerializeField] public float minAttackDistance = 2f;

			void Start() {
				base.OnStart();
			}

		void Update() {
			base.OnUpdate();

			switch (State) {
				case AgressionStateAI.Normal:



					break;
				case AgressionStateAI.Enemy:

					// Если AI достаточно близко - начинаем атаку
					if (Vector3.Distance(target.position, transform.position) <= minAttackDistance)
						getAnimationBehavior().setAttack();

					break;
				case AgressionStateAI.Warning:



					break;
			}

		}

	}
}
