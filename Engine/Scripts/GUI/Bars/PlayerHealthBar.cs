using System;
using UnityEngine;

namespace Engine.EGUI.Bars {

	[ExecuteInEditMode]
	public class PlayerHealthBar : BarBase {

		[SerializeField] public float maxHealth = 100f;
		[SerializeField] public float health    = 100f;

#if UNITY_EDITOR
		void OnValidate() {
			GamePlayer.states.maxHealth = maxHealth;

			if(health <= maxHealth && health >=0 )
				GamePlayer.states.health    = health;

			if (GamePlayer.states.health > GamePlayer.states.maxHealth)
				GamePlayer.states.health = GamePlayer.states.maxHealth;
			
        }
#endif

			void Start(){
				GamePlayer.states.maxHealth = maxHealth;
				GamePlayer.states.health    = health;
			}

		void OnGUI(){
			OnDraw();
		}

		public override Vector2 getBarPosition() {
			return new Vector2(5f, Screen.height-getHeight()-5f);
		}

		void Update(){
			updateValues(GamePlayer.states.health, GamePlayer.states.maxHealth);
		}
		
	}
	
}