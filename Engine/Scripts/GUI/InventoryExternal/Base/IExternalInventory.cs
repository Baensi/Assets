using UnityEngine;
using Engine.EGUI.Inventory;

namespace Engine.EGUI {

	public interface IExternalInventory : IExternalData {

		string getTitleText();

		string getCaptionText();

		void hide();

		bool isVisible();

		void show(IExternalData inventory, float x, float y);

		float getWidth();

		float getHeight();


	}

}
