using System;

namespace Engine.I18N {

	public static class DictionaryPlayer {

		public static class Level {
			public static string K_LEVEL                = "player_level_level";
			public static string K_EXPERIENCE           = "player_level_experience";
			public static string K_SPECIFICATIONSPOINTS = "player_level_specificationspoints";
			public static string K_SKILLSPOINTS         = "player_level_skillspoints";
		}
		
		public static class States {
			public static string K_HEALTH = "player_states_health";
			public static string K_ENERGY = "player_states_energy";
			public static string K_MENA   = "player_states_mana";
			
			public static string K_DAMAGE_MELEE  = "player_states_damage_melee";
			public static string K_DAMAGE_RANGED = "player_states_damage_ranged";
			public static string K_DAMAGE_MAGIC  = "player_states_damage_magic";
			
			public static string K_CRITICAL_DAMAGE_MELEE  = "player_states_critical_damage_melee";
			public static string K_CRITICAL_DAMAGE_RANGED = "player_states_critical_damage_ranged";
			public static string K_CRITICAL_DAMAGE_MAGIC  = "player_states_critical_damage_magic";
			
			public static string K_CHANCE_CRITICAL_DAMAGE_MELEE  = "player_states_chance_critical_damage_melee";
			public static string K_CHANCE_CRITICAL_DAMAGE_RENGED = "player_states_chance_critical_damage_ranged";
			public static string K_CHANCE_CRITICAL_DAMAGE_MAGIC  = "player_states_chance_critical_damage_magic";
			
			public static string K_PROTECTION_MELEE  = "player_states_protection_melee";
			public static string K_PROTECTION_RANGED = "player_states_protection_ranged";
			public static string K_PROTECTION_MAGIC  = "player_states_protection_magic";

		}
		
		public static class Specifications {
			public static string K_STRENGTH     = "player_specifications_strength";
			public static string K_STAMINA      = "player_specifications_stamina";
			public static string K_INTELLIGENCE = "player_specifications_intelligence";
			public static string K_AGILITY      = "player_specifications_agility";
		}
		
		public static class Skills {
			public static string K_FENCING     = "player_skills_fencing";
			public static string K_SHOOTING    = "player_skills_shooting";
			public static string K_MAGIC       = "player_skills_magic";
			public static string K_INTUITION   = "player_skills_intuition";
			public static string K_ALCHEMY     = "player_skills_alchemy";
			public static string K_ENGINEERING = "player_skills_engineering";
		}
		
	}
}
