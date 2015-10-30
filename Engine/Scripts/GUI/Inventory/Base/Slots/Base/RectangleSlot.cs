using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace Engine.EGUI.Inventory {
	
	[Serializable]
	public class RectangleSlot {
	
		[SerializeField] public Texture2D      background;
		[SerializeField] public List<ItemSlot> Items = new List<ItemSlot>();
		[SerializeField] public SlotSet        position;
	
	}

}

