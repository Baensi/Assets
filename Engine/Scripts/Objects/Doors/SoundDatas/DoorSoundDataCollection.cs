using UnityEngine;
using System.Collections;

namespace Engine.Objects.Doors {

	public enum DoorSoundDataCollection : int {

		defaultDoorSounds = 0x00

	};

	public static class DoorSoundDataCollectionConverter {

		public static IDoorSoundData getData(DoorSoundDataCollection type){
			switch(type){
				case DoorSoundDataCollection.defaultDoorSounds: return DefaultDoorSoundData.getInstance();
				default : return DefaultDoorSoundData.getInstance();
			}
		}

	}

}