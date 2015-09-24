using UnityEngine;
using System.Collections;

namespace Engine.Objects.Doors {

	public enum LeverSoundDataCollection : int {

		defaultLeverSounds = 0x00

	};

	public static class LeverSoundDataCollectionConverter {

		public static ILeverSoundData getData(LeverSoundDataCollection type) {
			switch (type) {
				case LeverSoundDataCollection.defaultLeverSounds: return DefaultLeverSoundData.getInstance();
				default: return DefaultLeverSoundData.getInstance();
			}
		}

	}

}