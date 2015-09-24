using UnityEngine;
using System.Collections;

namespace Engine.Player.Movement {

	public class MovementAudioData : MonoBehaviour {

		[SerializeField] [Range(2,4)] public int audioSectorSize = 4; // размер сектора (число звуков на каждую поверхность)

		[SerializeField] private AudioClip[] footstepSounds;  // шаги персонажа по разным поверхностям

		[SerializeField] private AudioClip jumpSound;     // звук прыжка
		[SerializeField] private AudioClip landSound;     // звук падения
		[SerializeField] private AudioClip sitdownSound;  // звук приседания
		[SerializeField] private AudioClip standupSound;  // звук подъёма

		private int prevIndex = 0;
		private MovementTypeZone currentZone;

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

				while((index=Random.Range(1,audioSectorSize))==prevIndex){}

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