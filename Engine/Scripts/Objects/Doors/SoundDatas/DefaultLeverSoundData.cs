using UnityEngine;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;

namespace Engine.Objects.Doors {

	public class DefaultLeverSoundData : ILeverSoundData {

		private static ILeverSoundData instance;

		public AudioClip leverState1;
		public AudioClip leverState2;

		public AudioClip leverState1Sound(){
			return leverState1;
		}

		public AudioClip leverState2Sound(){
			return leverState2;
		}

			public DefaultLeverSoundData() {

				leverState1 = Resources.Load("Sounds/Levers/Default/lever_state1") as AudioClip;
				leverState2 = Resources.Load("Sounds/Levers/Default/lever_state2") as AudioClip;
			
			}

		public static ILeverSoundData getInstance() {
			if (instance == null)
				instance = new DefaultLeverSoundData();
			return instance;
		}

	}

}
