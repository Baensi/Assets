using UnityEngine;
using System.Collections.Generic;
using Engine.Sounds;

namespace Engine.Player.Movement {

	public class MovementAudioData : MonoBehaviour {

		private int audioSectorSize; // размер сектора (число звуков на каждую поверхность)

		private List<AudioClip> footstepSounds = new List<AudioClip>();  // шаги персонажа по разным поверхностям

		private AudioClip jumpSound;     // звук прыжка
		private AudioClip landSound;     // звук падения
		private AudioClip sitdownSound;  // звук приседания
		private AudioClip standupSound;  // звук подъёма

		private int prevIndex = 0;
		private MovementTypeZone currentZone;

		void Start() {

			DSoundList factory = DSoundList.getInstance();

			jumpSound    = factory.getSound("player_jump");
			landSound    = factory.getSound("player_land");
			sitdownSound = factory.getSound("player_sitdown");
			standupSound = factory.getSound("player_standup");

			audioSectorSize = int.Parse(factory.getParameter("sector_size"));
			int zonesCount = int.Parse(factory.getParameter("zones_count")) * audioSectorSize;

			for (int i = 0; i < zonesCount; i++) {
				int zone  = i / audioSectorSize;
				int index = i % audioSectorSize;
				footstepSounds.Add(factory.getSound("zone_" + zone.ToString() + "_index_" + index.ToString()));
			}

		}

		public MovementAudioData () {
			currentZone = MovementTypeZone.ground;
		} 

		public void setZone(MovementTypeZone zone){
			this.currentZone = zone;
		}

		public MovementTypeZone getZone(){
			return currentZone;
		}

		/// <summary>
		/// Содержит звуки зон в следующем порядке:
		/// ground, dirt, tile, metal, snow, wade, step
		/// </summary>
		/// <returns>Звук шага для текущей зоны</returns>
		public AudioClip getFootStepSound(AudioClip prevous){

            int sector = (int)currentZone*audioSectorSize;
			int index  = 0;

			if(audioSectorSize!=1)
				while((index=Random.Range(1,audioSectorSize))==prevIndex){}
			else
				return footstepSounds[sector];

			prevIndex = index;

			return footstepSounds[index+sector];
		}

		public AudioClip getJumpSound(){
			return jumpSound;
		}

		public AudioClip getLandSound(){
			return landSound;
		}

		public AudioClip getSitdownSound(){
			return sitdownSound;
		}

		public AudioClip getStandupSound(){
			return standupSound;
		}


	}

}