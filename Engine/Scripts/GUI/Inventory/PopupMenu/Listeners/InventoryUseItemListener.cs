using UnityEngine;
using Engine.Objects;
using Engine.Objects.Types;

namespace Engine.EGUI.Inventory.PopupMenu {

	public class InventoryUseItemListener {

		private UInventory inventory;

			public InventoryUseItemListener() {
				inventory = SingletonNames.getInventory().GetComponent<UInventory>();
			}

		public void UseItem(Item selectedItem) {

			Debug.LogWarning("using...");

			DynamicObject dynamicObject = selectedItem.toGameObject().GetComponent<DynamicObject>();
			IUsedType usedType = dynamicObject as IUsedType;
			usedType.onUse();

			if (selectedItem.getCount() > 1) {

				selectedItem.decCount(1);

			} else {

				inventory.removeItem(selectedItem, false, true);

			}

		}

	}
}
