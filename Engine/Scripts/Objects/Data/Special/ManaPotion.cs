using System;
using UnityEngine;
using Engine.Objects;
using Engine.Objects.Types;
using Engine.Player;
using Engine.EGUI.Inventory;

namespace Engine.Objects.Special {

	public class ManaPotion : DynamicObject, IUsedType {

		public ManaPotion() : base() {
			item = DObjectList.getInstance().getItem("ManaPotion");
		}

		void Start() {
			base.OnStart();
		}

		public bool onUse() {

			GamePlayer.states.Mana+=50;

			base.Destroy(true);
			return true;
		}

		
	}
	
}

