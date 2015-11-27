using System;
using UnityEngine;
using Engine.Objects;
using Engine.Objects.Types;
using Engine.Player;
using Engine.EGUI.Inventory;

namespace Engine.Objects.Special {

	public class HealthPotion : DynamicObject, IPickedType {

		void Start() {
			base.OnStart();
            item = DObjectList.getInstance().getItem("HealthPotion");
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

