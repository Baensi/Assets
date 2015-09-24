using UnityEngine;
using System.Collections;

namespace Engine.Objects.Doors {

	public interface IDoorSoundData {

		AudioClip openDoorSound();
		AudioClip closeDoorSound();
		AudioClip lockDoorSound();

	}

}