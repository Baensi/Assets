using System;
using UnityEngine;

namespace Engine.EGUI.Bars {

	public class PlayerManaBar : BarBase {

		[SerializeField] public float maxMana = 100f;
		[SerializeField] public float mana    = 100f;

#if UNITY_EDITOR
		void OnValidate() {
			GamePlayer.states.maxMana = maxMana;

			if (mana <= maxMana && mana >= 0)
				GamePlayer.states.mana = mana;

			if (GamePlayer.states.mana > GamePlayer.states.maxMana)
				GamePlayer.states.mana = GamePlayer.states.maxMana;
		}
#endif

			void Start() {
				GamePlayer.states.maxMana = maxMana;
				GamePlayer.states.mana = mana;
			}

		void OnGUI() {
			OnDraw();
		}

		public override Vector2 getBarPosition() {
			return new Vector2(35f, Screen.height - getHeight() - 5f);
		}

		void Update() {
			updateValues(GamePlayer.states.mana, GamePlayer.states.maxMana);
		}

	}
	
}