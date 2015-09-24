using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Engine.Objects.Types;

namespace Engine.Objects.Types.Readed {

	public class ReadedSoundData {

		private static ReadedSoundData instance;
		
		private List<AudioClip> openReaded;
		private int             openReadedPrevIndex  = 0;
		
		private List<AudioClip> closeReaded;
		private int             closeReadedPrevIndex = 0;
		
		private List<AudioClip> changePage;
		private int             changePagePrevIndex  = 0;
	
		public static ReadedSoundData getInstance(){
			if(instance==null)
				instance = new ReadedSoundData();
			return instance;
		}
		
		public ReadedSoundData(){
			
			
		}
		
		public AudioClip getOpenReadedSound(){
			int index  = 0;
				while ((index = UnityEngine.Random.Range(1, openReaded.Count)) == openReadedPrevIndex) { }
			openReadedPrevIndex = index;
			return openReaded[openReadedPrevIndex];
		}
		
		public AudioClip getCloseReadedSound(){
			int index  = 0;
				while ((index = UnityEngine.Random.Range(1, closeReaded.Count)) == closeReadedPrevIndex) { }
			closeReadedPrevIndex = index;
			return closeReaded[closeReadedPrevIndex];
		}
		
		public AudioClip getChangePageSound(){
			int index  = 0;
				while ((index = UnityEngine.Random.Range(1, changePage.Count)) == changePagePrevIndex) { }
			changePagePrevIndex = index;
			return changePage[changePagePrevIndex];
		}
		
	}
	
}

