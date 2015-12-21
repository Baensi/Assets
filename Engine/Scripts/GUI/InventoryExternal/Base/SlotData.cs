using System;
using System.Collections.Generic;
using UnityEngine;

namespace Engine.EGUI.Inventory {

	[Serializable]
	public class SlotData {

		[SerializeField] public List<ItemSlot> Items;
		[SerializeField] public SlotSet        position;

	}

}
