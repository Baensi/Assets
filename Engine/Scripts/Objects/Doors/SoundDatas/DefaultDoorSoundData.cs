using UnityEngine;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;

namespace Engine.Objects.Doors {

	public class DefaultDoorSoundData : IDoorSoundData {
		
		private static IDoorSoundData instance;

		[SerializeField] public AudioClip openDoor;
		[SerializeField] public AudioClip closeDoor;
		[SerializeField] public AudioClip lockDoor;

		[SerializeField] public static AudioClip test;

		public AudioClip openDoorSound(){
			return openDoor;
		}

		public AudioClip closeDoorSound(){
			return closeDoor;
		}

		public AudioClip lockDoorSound(){
			return lockDoor;
		}

			public DefaultDoorSoundData(){

				openDoor  = Resources.Load("Sounds/Doors/Default/door_open")  as AudioClip;
				closeDoor = Resources.Load("Sounds/Doors/Default/door_close") as AudioClip;
				lockDoor  = Resources.Load("Sounds/Doors/Default/door_lock")  as AudioClip;
			
			}

		public static IDoorSoundData getInstance(){
			if(instance == null)
				instance = new DefaultDoorSoundData();
			return instance;
		}
			
	}
		
}