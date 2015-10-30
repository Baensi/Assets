using System;
using UnityEngine;
using System.Collections;

namespace Engine.Player.Torch.Burn {

	public class BurnCoal : IBurn {
        
		private const float speedStep = 0.10f;
		private const float speedTime = 2.00f;

		private float currentEnergy = 100.0f;
		private float oldTime       = 0.0f;

			public BurnCoal(){

			}

		public float getEnergy() {
			
			return currentEnergy;
		}

		public EBurnType getBurnType() {
			
			return EBurnType.BurnCoal;
		}

		public void updateBurn(){

			float time = Time.time;

			if (currentEnergy <= 0.0f) return;

			if (time-oldTime >= speedTime){
				currentEnergy -= speedStep;
				oldTime = time;
			}

		}

	}

}

