using System;
using UnityEngine;
using Engine.EGUI.Bars;

namespace Engine.Player {

	/// <summary>
	/// Класс статов
	/// </summary>
	public class PlayerStates {

		public float maxHealth = 0.0f;
		public float health = 0.0f;
        
		public float maxEnergy = 0.0f;
		public float energy = 0.0f;
        
		public float maxMana = 0.0f;
		public float mana = 0.0f;
        
		public float damageMelee = 0.0f;
		public float damageRanged = 0.0f;
		public float damageMagic = 0.0f;
		
			public float criticalDamageMelee = 0.0f;
			public float criticalDamageRanged = 0.0f;
			public float criticalDamageMagic = 0.0f;
		
			public float chanceCriticalDamageMelee = 0.0f;
			public float chanceCriticalDamageRanged = 0.0f;
			public float chanceCriticalDamageMagic = 0.0f;
		
		public float protectionMelee = 0.0f;
		public float protectionRanged = 0.0f;
		public float protectionMagic = 0.0f;
		
			public PlayerStates() {

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
		/// <returns>Возвращает сумму статов</returns>
		public static PlayerStates operator +(PlayerStates s1, PlayerStates s2) {
			PlayerStates result = new PlayerStates() {
				maxHealth                  = s1.maxHealth                  + s2.maxHealth,
				health                     = s1.health                     + s2.health,
				maxEnergy                  = s1.maxEnergy                  + s2.maxEnergy,
				energy                     = s1.energy                     + s2.energy,
				maxMana                    = s1.maxMana                    + s2.maxMana,
				mana                       = s1.mana                       + s2.mana,
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

			if (result.health > result.maxHealth)
				result.health = result.maxHealth;

			if (result.mana > result.maxMana)
				result.mana = result.maxMana;

			if (result.energy > result.maxEnergy)
				result.energy = result.maxEnergy;

			return result;
		}

		/// <summary>
		/// Меняет знак статов
		/// </summary>
		/// <param name="s1"></param>
		/// <returns>Возвращает статы с противоположным знаком</returns>
		public static PlayerStates operator -(PlayerStates s1) {
			PlayerStates result = new PlayerStates() {
				maxHealth                  = -s1.maxHealth,
				health                     = -s1.health,
				maxEnergy                  = -s1.maxEnergy,
				energy                     = -s1.energy,
				maxMana                    = -s1.maxMana,
				mana                       = -s1.mana,
				damageMelee                = -s1.damageMelee,
				damageRanged               = -s1.damageRanged,
				damageMagic                = -s1.damageMagic,
				criticalDamageMelee        = -s1.criticalDamageMelee,
				criticalDamageRanged       = -s1.criticalDamageRanged,
				criticalDamageMagic        = -s1.criticalDamageMagic,
				chanceCriticalDamageMelee  = -s1.chanceCriticalDamageMelee,
				chanceCriticalDamageRanged = -s1.chanceCriticalDamageRanged,
				chanceCriticalDamageMagic  = -s1.chanceCriticalDamageMagic,
				protectionMelee            = -s1.protectionMelee,
				protectionRanged           = -s1.protectionRanged,
				protectionMagic            = -s1.protectionMagic            
			};

			if (result.health > result.maxHealth)
				result.health = result.maxHealth;

			if (result.mana > result.maxMana)
				result.mana = result.maxMana;

			if (result.energy > result.maxEnergy)
				result.energy = result.maxEnergy;

			return result;
		}

		/// <summary>
		/// Перегружаем оператор разности
		/// </summary>
		/// <param name="s1"></param>
		/// <param name="s2"></param>
		/// <returns>Возвращает разность статов</returns>
		public static PlayerStates operator -(PlayerStates s1, PlayerStates s2) {
			PlayerStates result = new PlayerStates() {
				maxHealth                  = s1.maxHealth                  - s2.maxHealth,
				health                     = s1.health                     - s2.health,
				maxEnergy                  = s1.maxEnergy                  - s2.maxEnergy,
				energy                     = s1.energy                     - s2.energy,
				maxMana                    = s1.maxMana                    - s2.maxMana,
				mana                       = s1.mana                       - s2.mana,
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

			if (result.health > result.maxHealth)
				result.health = result.maxHealth;

			if (result.mana > result.maxMana)
				result.mana = result.maxMana;

			if (result.energy > result.maxEnergy)
				result.energy = result.maxEnergy;

			return result;
		}
		
	}

}