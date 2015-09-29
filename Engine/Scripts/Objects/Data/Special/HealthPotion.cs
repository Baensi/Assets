using System;
using UnityEngine;
using Engine.Objects;
using Engine.Objects.Types;
using Engine.Player;
using Engine.EGUI.Inventory;

namespace Engine.Objects.Special {

	public class HealthPotion : DynamicObject, IUsedType {

		public HealthPotion() : base() {
			item = DObjectList.Items.HealthPotion;
		}

		void Start() {
			base.OnStart();
		}

		public bool onUse() {

			GamePlayer.states.Health+=50;

			base.Destroy(true);
			return true;
		}

		
	}
	
}

