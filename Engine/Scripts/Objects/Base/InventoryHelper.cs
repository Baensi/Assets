using UnityEngine;
using Engine.EGUI.Inventory;

namespace Engine.Objects {

	public static class InventoryHelper {

		public static bool AddInInventory(Item item) {

			UInventory inventory = SingletonNames.getInventory().GetComponent<UInventory>();

			if (inventory!=null) {

				if(inventory.addItem(item.Clone())!=0)
                    Debug.LogWarning("Ошибка!");

				return true;

			} else {

                Debug.LogError("Ссылка на инвентарь не верна! ["+SingletonNames.Constants.GUI.INVENTORY+"]");

				return false;

			}

		}

	}

}
