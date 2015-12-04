using System;
using UnityEngine;

namespace Engine.AI.Behavior {

	public interface IAudioBehavior {

		AudioClip getStateMessage(AgressionStateAI state);


	}

}
