using System;
using System.Collections.Generic;
using UnityEngine;

namespace Engine.AI.Behavior {

	/// <summary>
	/// Модель поведения AI
	/// </summary>
	public class SoldierBehaviorAI : IModelBehaviorAI {

		private IAudioBehavior audioBehavior;

			public SoldierBehaviorAI() {
				audioBehavior = new AudioBehavior();
            }

		public IAudioBehavior getAudioBehavior() {
			return audioBehavior;
		}



	}



}
