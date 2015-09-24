using UnityEngine;
using System.Collections;

namespace Engine.Objects.Doors {

	public interface ILeverSoundData {

		AudioClip leverState1Sound();
		AudioClip leverState2Sound();

	}

}