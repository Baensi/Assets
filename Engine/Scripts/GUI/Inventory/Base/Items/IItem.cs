using UnityEngine;
using System.Collections;
using Engine.Objects;

namespace Engine.EGUI.Inventory {

	public interface IItem {

		ItemSize getSize();
		void setSize(ItemSize size);

		int getMaxCount();

		bool isFullCount();

		int incCount();
		int decCount();
		int incCount(int value);
		int decCount(int value);

		int  getCount();
		void setCount(int count);

		Texture getIcon();
		ItemDescription getDescription();

		GameObject toGameObject();

	}

}
