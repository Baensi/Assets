﻿using System;
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
		public Texture2D icon;
		public SortedDictionary<string,AudioClip> sounds;

		public ItemResource(Texture2D icon, List<SoundPack> sounds) {
			this.icon=icon;

			this.sounds = new SortedDictionary<string, AudioClip>();

			if (sounds!=null)
				foreach (SoundPack sound in sounds) {
					this.sounds.Add(sound.name, sound.sound);
				}

		}

	}

}
