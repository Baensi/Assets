using System;
using UnityEngine;
using Engine.Player;

namespace Engine.Calculators {
	
	public static class PlayerLevelCalculator {
		
		public static int getExperienceNextLevel(int level){
			return (int)(50 * Mathf.Pow(1.65f, level));
		}
		
		public static int getSpecificationsPoint(int newLevel){
			return (newLevel % 2);
		}
		
		public static int getSkillsPoint(int newLevel){
			return 2;
		}
		
	}
	
}