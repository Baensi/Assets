using System;
using UnityEngine;
using Engine.Objects;
using Engine.Objects.Types;
using Engine.Player;
using Engine.EGUI.Inventory;

namespace Engine.Objects.Special {

	public class ManaPotion : DynamicObject, IPickedType {

		public ManaPotion() : base() {
			item = DObjectList.getInstance().getItem("ManaPotion");
		}

		void Start() {
			base.OnStart();
		}

		public bool onPick() {
			if (InventoryHelper.AddInInventory(item)) {
				base.Destroy(true);
				return true;
			} return false;
		}

		
	}
	
}

