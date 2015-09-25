using System;
using UnityEngine;

namespace Engine.Player.Data {

	[Serializable]
	public struct PlayerStates {

		public float maxHealth;
		public float health;
        
		public float maxEnergy;
		public float energy;
        
		public float maxMana;
		public float mana;
        
		public float damageMelee;
		public float damageRanged;
		public float damageMagic;
		
			public float criticalDamageMelee;
			public float criticalDamageRanged;
			public float criticalDamageMagic;
		
			public float chanceCriticalDamageMelee;
			public float chanceCriticalDamageRanged;
			public float chanceCriticalDamageMagic;
		
		public float protectionMelee;
		public float protectionRanged;
		public float protectionMagic;
		
	}

}