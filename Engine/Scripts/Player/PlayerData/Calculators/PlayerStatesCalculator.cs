using System;
using UnityEngine;
using Engine.Player.Data;

namespace Engine.Player.Calculators {
	
	public static class PlayerStatesCalculator {
		
		public static PlayerStates sum(PlayerStates state1, PlayerStates state2){ // result = state1+state2
			PlayerStates result = new PlayerStates();
			
			result.maxHealth                 = state1.maxHealth                  +state2.maxHealth;
			result.health                    = state1.health                     +state2.health;

			result.maxEnergy                 = state1.maxEnergy                  +state2.maxEnergy;
			result.energy                    = state1.energy                     +state2.energy;

			result.maxMana                   = state1.maxMana                    +state2.maxMana;
			result.mana                      = state1.mana                       +state2.mana;

			result.damageMelee               = state1.damageMelee                +state2.damageMelee;
			result.damageRanged              = state1.damageRanged               +state2.damageRanged;
			result.damageMagic               = state1.damageMagic                +state2.damageMagic;

			result.criticalDamageMelee       = state1.criticalDamageMelee        +state2.criticalDamageMelee;
			result.criticalDamageRanged      = state1.criticalDamageRanged       +state2.criticalDamageRanged;
			result.criticalDamageMagic       = state1.criticalDamageMagic        +state2.criticalDamageMagic;

			result.chanceCriticalDamageMelee = state1.chanceCriticalDamageMelee  +state2.chanceCriticalDamageMelee; 
			result.chanceCriticalDamageRanged= state1.chanceCriticalDamageRanged +state2.chanceCriticalDamageRanged;
			result.chanceCriticalDamageMagic = state1.chanceCriticalDamageMagic  +state2.chanceCriticalDamageMagic; 

			result.protectionMelee           = state1.protectionMelee            +state2.protectionMelee; 
			result.protectionRanged          = state1.protectionRanged           +state2.protectionRanged;
			result.protectionMagic           = state1.protectionMagic            +state2.protectionMagic;
			
			return result;
		}
		
		public static PlayerStates sub(PlayerStates state1, PlayerStates state2){ // result = state1-state2
			PlayerStates result = new PlayerStates();
			
			result.maxHealth                 = state1.maxHealth                  -state2.maxHealth;
			result.health                    = state1.health                     -state2.health;
                                                                                 
			result.maxEnergy                 = state1.maxEnergy                  -state2.maxEnergy;
			result.energy                    = state1.energy                     -state2.energy;
                                                                                 
			result.maxMana                   = state1.maxMana                    -state2.maxMana; 
			result.mana                      = state1.mana                       -state2.mana;  
                                                                                 
			result.damageMelee               = state1.damageMelee                -state2.damageMelee; 
			result.damageRanged              = state1.damageRanged               -state2.damageRanged;
			result.damageMagic               = state1.damageMagic                -state2.damageMagic; 
                                                                                 
			result.criticalDamageMelee       = state1.criticalDamageMelee        -state2.criticalDamageMelee; 
			result.criticalDamageRanged      = state1.criticalDamageRanged       -state2.criticalDamageRanged; 
			result.criticalDamageMagic       = state1.criticalDamageMagic        -state2.criticalDamageMagic; 
                                                                                 
			result.chanceCriticalDamageMelee = state1.chanceCriticalDamageMelee  -state2.chanceCriticalDamageMelee;
			result.chanceCriticalDamageRanged= state1.chanceCriticalDamageRanged -state2.chanceCriticalDamageRanged;
			result.chanceCriticalDamageMagic = state1.chanceCriticalDamageMagic  -state2.chanceCriticalDamageMagic; 
                                                                                 
			result.protectionMelee           = state1.protectionMelee            -state2.protectionMelee; 
			result.protectionRanged          = state1.protectionRanged           -state2.protectionRanged;
			result.protectionMagic           = state1.protectionMagic            -state2.protectionMagic;
			
			return result;
		}
		
	}
	
}
