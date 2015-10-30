using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Engine.EGUI.Inventory {

	public class DropItemData {

		public RectangleSlot slot;
		public ItemSlot      item;
		public int           changeValue;

		public DropItemData(RectangleSlot slot, ItemSlot item, int changeValue) {
			this.slot=slot;
			this.item=item;
			this.changeValue=changeValue;
		}

		public void Clear() {

			slot = null;
			item = null;

		}

	}

}
