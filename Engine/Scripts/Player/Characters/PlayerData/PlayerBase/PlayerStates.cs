using System;
using UnityEngine;
using Engine.EGUI.Bars;

namespace Engine.Player {

	[Serializable]
	public class PlayerStates {

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


			public PlayerStates() {

			}


		public float MaxHealth {

			get { return maxHealth; }
			set {
				maxHealth = value;
				UBar healthBar = null;
				healthBar.max = maxHealth;
			}

		}

		public float Health {

			get { return health; }
			set {
				health = value;
				UBar healthBar = null;
				healthBar.value = health;
			}

		}

		public float MaxMana {

			get { return maxMana; }
			set {
				maxMana = value;
				UBar manaBar = null;
				manaBar.max = maxMana;
			}

		}

		public float Mana {

			get { return mana; }
			set {
				mana = value;
				UBar manaBar = null;
				manaBar.value = mana;
			}

		}

		public float MaxEnergy {

			get { return maxEnergy; }
			set {
				maxEnergy = value;
				UBar energyBar = null;
				energyBar.max = maxEnergy;
			}

		}

		public float Energy {

			get { return energy; }
			set {
				energy = value;
				UBar energyBar = null;
				energyBar.value = energy;
			}

		}

		/// <summary>
		/// Перегружаем оператор >
		/// </summary>
		/// <param name="s1"></param>
		/// <param name="s2"></param>
		/// <returns>Если хотябы один из параметров левого оператора больше</returns>
		public static bool operator >(PlayerStates s1, PlayerStates s2) {
			return
				s1.maxHealth                  > s2.maxHealth				  ||
				s1.health                     > s2.health					  ||
				s1.maxEnergy                  > s2.maxEnergy				  ||
				s1.energy                     > s2.energy				      ||
				s1.maxMana                    > s2.maxMana				      ||
				s1.mana                       > s2.mana					      ||
				s1.damageMelee                > s2.damageMelee			      ||
				s1.damageRanged               > s2.damageRanged			      ||
				s1.damageMagic                > s2.damageMagic				  ||
				s1.criticalDamageMelee        > s2.criticalDamageMelee		  ||
				s1.criticalDamageRanged       > s2.criticalDamageRanged	      ||
				s1.criticalDamageMagic        > s2.criticalDamageMagic		  ||
				s1.chanceCriticalDamageMelee  > s2.chanceCriticalDamageMelee  ||
				s1.chanceCriticalDamageRanged > s2.chanceCriticalDamageRanged ||
				s1.chanceCriticalDamageMagic  > s2.chanceCriticalDamageMagic  ||
				s1.protectionMelee            > s2.protectionMelee			  ||
				s1.protectionRanged           > s2.protectionRanged		      ||
				s1.protectionMagic            > s2.protectionMagic;
		}

		/// <summary>
		/// Перегружаем оператор <
		/// </summary>
		/// <param name="s1"></param>
		/// <param name="s2"></param>
		/// <returns>Если хотябы один из параметров правого оператора больше</returns>
		public static bool operator <(PlayerStates s1, PlayerStates s2) {
			return s2 > s1;
		}

		/// <summary>
		/// Перегружаем оператор сложения
		/// </summary>
		/// <param name="s1"></param>
		/// <param name="s2"></param>
		/// <returns></returns>
		public static PlayerStates operator +(PlayerStates s1, PlayerStates s2) {
			return new PlayerStates() {
				MaxHealth                  = s1.maxHealth                  + s2.maxHealth,
				Health                     = s1.health                     + s2.health,
				MaxEnergy                  = s1.maxEnergy                  + s2.maxEnergy,
				Energy                     = s1.energy                     + s2.energy,
				MaxMana                    = s1.maxMana                    + s2.maxMana,
				Mana                       = s1.mana                       + s2.mana,
				damageMelee                = s1.damageMelee                + s2.damageMelee,
				damageRanged               = s1.damageRanged               + s2.damageRanged,
				damageMagic                = s1.damageMagic                + s2.damageMagic,
				criticalDamageMelee        = s1.criticalDamageMelee        + s2.criticalDamageMelee,
				criticalDamageRanged       = s1.criticalDamageRanged       + s2.criticalDamageRanged,
				criticalDamageMagic        = s1.criticalDamageMagic        + s2.criticalDamageMagic,
				chanceCriticalDamageMelee  = s1.chanceCriticalDamageMelee  + s2.chanceCriticalDamageMelee,
				chanceCriticalDamageRanged = s1.chanceCriticalDamageRanged + s2.chanceCriticalDamageRanged,
				chanceCriticalDamageMagic  = s1.chanceCriticalDamageMagic  + s2.chanceCriticalDamageMagic,
				protectionMelee            = s1.protectionMelee            + s2.protectionMelee,
				protectionRanged           = s1.protectionRanged           + s2.protectionRanged,
				protectionMagic            = s1.protectionMagic            + s2.protectionMagic           
			};
		}

		/// <summary>
		/// Перегружаем оператор разности
		/// </summary>
		/// <param name="s1"></param>
		/// <param name="s2"></param>
		/// <returns></returns>
		public static PlayerStates operator -(PlayerStates s1, PlayerStates s2) {
			return new PlayerStates() {
				MaxHealth                  = s1.maxHealth                  - s2.maxHealth,
				Health                     = s1.health                     - s2.health,
				MaxEnergy                  = s1.maxEnergy                  - s2.maxEnergy,
				Energy                     = s1.energy                     - s2.energy,
				MaxMana                    = s1.maxMana                    - s2.maxMana,
				Mana                       = s1.mana                       - s2.mana,
				damageMelee                = s1.damageMelee                - s2.damageMelee,
				damageRanged               = s1.damageRanged               - s2.damageRanged,
				damageMagic                = s1.damageMagic                - s2.damageMagic,
				criticalDamageMelee        = s1.criticalDamageMelee        - s2.criticalDamageMelee,
				criticalDamageRanged       = s1.criticalDamageRanged       - s2.criticalDamageRanged,
				criticalDamageMagic        = s1.criticalDamageMagic        - s2.criticalDamageMagic,
				chanceCriticalDamageMelee  = s1.chanceCriticalDamageMelee  - s2.chanceCriticalDamageMelee,
				chanceCriticalDamageRanged = s1.chanceCriticalDamageRanged - s2.chanceCriticalDamageRanged,
				chanceCriticalDamageMagic  = s1.chanceCriticalDamageMagic  - s2.chanceCriticalDamageMagic,
				protectionMelee            = s1.protectionMelee            - s2.protectionMelee,
				protectionRanged           = s1.protectionRanged           - s2.protectionRanged,
				protectionMagic            = s1.protectionMagic            - s2.protectionMagic
			};
		}
		
	}

}