using UnityEngine;
using System.Collections;
using Engine.Objects;

namespace Engine.EGUI.Inventory {

	public interface IItem {

		ItemSize     getSize();
		ItemPosition getPosition();

		void setSize(ItemSize size);
		void setPosition(ItemPosition position);

		int getMaxCount();

		void incCount();
		void decCount();
		int  getCount();
		void setCount(int count);

		Texture getIcon();

		bool isSelected();
		void setSelected(bool selected);

		GameObject toGameObject();

		void redraw(float posX, float posY);

	}

}
