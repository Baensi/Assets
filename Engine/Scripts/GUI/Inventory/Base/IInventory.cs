﻿using System.Collections;
using System.Collections.Generic;

namespace Engine.EGUI.Inventory {

	public interface IInventory {

		float getX();
		float getY();

		List<RectangleSlot> getSlots();
		void                redraw();

			bool addItem(IItem item);
			bool removeItem(IItem item);

		void show();
		void hide();
		bool isVisible();

	}

}