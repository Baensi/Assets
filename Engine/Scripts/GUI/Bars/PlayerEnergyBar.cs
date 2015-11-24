using System;
using UnityEngine;

namespace Engine.EGUI.Bars {

	[ExecuteInEditMode]
	public class PlayerEnergyBar : BarBase {

		[SerializeField] public float maxEnergy = 100f;
		[SerializeField] public float energy = 100f;

#if UNITY_EDITOR
		void OnValidate() {
			GamePlayer.states.maxEnergy = maxEnergy;

			if (energy <= maxEnergy && energy >= 0)
				GamePlayer.states.energy = energy;

			if (GamePlayer.states.energy > GamePlayer.states.maxEnergy)
				GamePlayer.states.energy = GamePlayer.states.maxEnergy;
		}
#endif

			void Start() {
				GamePlayer.states.maxEnergy = maxEnergy;
				GamePlayer.states.energy = energy;
			}

		void OnGUI() {
			OnDraw();
		}

		public override Vector2 getBarPosition() {
			return new Vector2(20f, Screen.height - getHeight() - 10f);
		}

		void Update() {
			updateValues(GamePlayer.states.energy, GamePlayer.states.maxEnergy);
		}

	}

}
