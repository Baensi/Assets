using System;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using Engine.Objects.Types;
using Engine.Objects;
using Engine.Skills;
using Engine.Player;
using Engine.EGUI.Inventory;

namespace Engine.Objects.Food {

	public class CookedFish : DynamicObject, IPickedType, IUsedType, IChangedStatesType {

		private PlayerStates states = new PlayerStates() { // изменяемые статы
			health = 5.0f,
		};

			void Start() {
				base.OnStart();

				item = DObjectList.getInstance().getItem("CookedFish");
			}

		void OnGUI() {

		}

		public void onUse() {
			GamePlayer.states += getStates();
		}

		public PlayerStates getStates() {
			return states;
		}

		public bool onPick() {
			if (InventoryHelper.AddInInventory(item)) {
				base.Destroy(true);
				return true;
			} return false;
		}

		void Update() {
			base.OnUpdate();
		}
		
	}
	
}