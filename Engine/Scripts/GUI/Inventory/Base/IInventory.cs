using System.Collections;
using System.Collections.Generic;

namespace Engine.EGUI.Inventory {

	public interface IInventory {

		float getX();
		float getY();

		List<RectangleSlot> getSlots();
		void                redraw();

			int  addItem(Item item);
			bool removeItem(Item item, bool equals, bool full = true, int count = 1);

		void show();
		void hide();
		bool isVisible();

	}

}