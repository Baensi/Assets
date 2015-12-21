using UnityEngine;

namespace Engine.EGUI.Inventory {

	public interface IExternalData {

		int  addItem(Item item);

		bool removeItem(Item item);

	}

}
