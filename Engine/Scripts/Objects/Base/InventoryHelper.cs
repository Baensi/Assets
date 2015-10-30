using System;
using System.Collections.Generic;
using Engine.EGUI.Inventory;

namespace Engine.Objects {

	public static class InventoryHelper {

		public static bool AddInInventory(Item item) {

			UInventory inventory = SingletonNames.getGUI().GetComponent<UInventory>();

			if (inventory!=null) {

				inventory.addItem(item);

				return true;

			} else {

				return false;

			}

		}

	}

}
