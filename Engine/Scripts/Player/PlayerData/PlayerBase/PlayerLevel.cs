using System;
using UnityEngine;

namespace Engine.Player.Data {
	
	[Serializable]
	public struct PlayerLevel {
		
		public int level;
		
			public int experience          ;
			public int experienceNextLevel;
		
		public int specificationsPoint;
		public int skillsPoint;
		
	}
	                     
}