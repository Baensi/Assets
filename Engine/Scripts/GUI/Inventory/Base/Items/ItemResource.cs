using System;
using System.Collections.Generic;
using UnityEngine;

namespace Engine.EGUI.Inventory {

	public class SoundPack {

		public AudioClip sound;
		public string    name;

		public SoundPack(AudioClip sound, string name) {
			this.sound=sound;
			this.name=name;
		}

	}

	public class ItemResource {

		public Texture   icon;
		public SortedDictionary<string,AudioClip> sounds;

		public ItemResource(Texture icon, List<SoundPack> sounds) {
			this.icon=icon;

			this.sounds = new SortedDictionary<string, AudioClip>();

			if (sounds!=null)
				foreach (SoundPack sound in sounds) {
					this.sounds.Add(sound.name, sound.sound);
				}

		}

	}

}
