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

	public class Files {

		public string itemName;

		public string       gameObjectPath;
		public string       iconPath;
		public List<string> soundsNames = new List<string>();
		public List<string> soundsPaths = new List<string>();

	}

	public class ItemResource {

		public Files     files = new Files();
		public Texture   icon;
		public SortedDictionary<string,AudioClip> sounds;

		public void ReCreate() {

			icon = Resources.Load<Texture>(files.iconPath);

			if (sounds != null) {
				sounds.Clear();
				sounds = new SortedDictionary<string, AudioClip>();
            }

			for(int i=0;i<files.soundsPaths.Count-1;i++)
				sounds.Add(files.soundsNames[i], Resources.Load<AudioClip>(files.soundsPaths[i]));

		}

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
