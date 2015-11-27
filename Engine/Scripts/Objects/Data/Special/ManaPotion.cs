using System;
using UnityEngine;
using Engine.Objects;
using Engine.Objects.Types;
using Engine.Player;
using Engine.EGUI.Inventory;

namespace Engine.Objects.Special {

	public class ManaPotion : DynamicObject, IPickedType {

		void Start() {
			base.OnStart();
            item = DObjectList.getInstance().getItem("ManaPotion");
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

