using System;
using UnityEngine;
using Engine.AI.Behavior;

namespace Engine.AI {

	/// <summary>
	/// Класс - "враг ближнего боя"
	/// </summary>
	public class MeleeAI : PathWalker {

		public void OnStart() {
			base.OnStartWalker();
		}

		public void OnUpdate() {
			base.OnUpdateWalker();
		}

	}
}
