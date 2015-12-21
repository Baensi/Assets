using System;
using Engine.Player;

namespace Engine.Calculators {
	
	public static class ObjectMassCalculator {
		
		
		public static float getMaxPickUpMass(PlayerSpecifications specifications){

			return specifications.strength * 10.0f + specifications.intelligence * 10.0f;
			
		}
		
	}
	
}