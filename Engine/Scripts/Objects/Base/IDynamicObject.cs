using System;
using UnityEngine;
using System.Collections;
using Engine.EGUI.Inventory;

namespace Engine.Objects {

	public interface IDynamicObject {

		TextDisplayed getDisplayed();
		IItem         getItem();
		GameObject    toObject();

		int    getId();
		string getName();
		string getCaption();
		float  getCostValue();

		void Destroy(bool presently);
		bool isDestroy();

		void setSelection(bool selection);
		bool isSelected();
	}

}

