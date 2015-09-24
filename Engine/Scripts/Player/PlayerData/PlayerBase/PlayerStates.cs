using System;
using UnityEngine;

namespace Engine.Player.Data {

	[Serializable]
	public class PlayerStates : MonoBehaviour {

		[SerializeField] public float maxHealth = 100.0f;
		[SerializeField] public float health    = 100.0f;
        
		[SerializeField] public float maxEnergy = 100.0f;
		[SerializeField] public float energy    = 100.0f;
        
		[SerializeField] public float maxMana   = 50.0f;
		[SerializeField] public float mana      = 50.0f;
        
		[SerializeField] public float damageMelee  = 1.0f;
		[SerializeField] public float damageRanged = 1.0f;
		[SerializeField] public float damageMagic  = 1.0f;
		
		[SerializeField] [Range(0f,100f)] public float criticalDamageMelee  = 0.0f;
		[SerializeField] [Range(0f,100f)] public float criticalDamageRanged = 0.0f;
		[SerializeField] [Range(0f,100f)] public float criticalDamageMagic  = 0.0f;
		
		[SerializeField] [Range(0f,100f)] public float chanceCriticalDamageMelee  = 0.0f;
		[SerializeField] [Range(0f,100f)] public float chanceCriticalDamageRanged = 0.0f;
		[SerializeField] [Range(0f,100f)] public float chanceCriticalDamageMagic  = 0.0f;
		
		[SerializeField] public float protectionMelee  = 0.0f;
		[SerializeField] public float protectionRanged = 0.0f;
		[SerializeField] public float protectionMagic  = 0.0f;
		
	}

}