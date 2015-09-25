using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace Engine.EGUI.Inventory {
	
	[Serializable]
	public class RectangleSlot {

		[SerializeField] public Texture2D   background;
		[SerializeField] public List<IItem> Items;
		[SerializeField] public SlotSet     position;

		private Rect backgroundRect;

		public void redraw(float x, float y){

			if (background==null) return;

			backgroundRect = new Rect(position.OffsetX + CellSettings.cellPaddingX,
									  position.OffsetY + CellSettings.cellPaddingY,
									  position.SlotWidth,
									  position.SlotHeight);

			Rect rect = new Rect(backgroundRect.x+x, backgroundRect.y+y, backgroundRect.width, backgroundRect.height);
			GUI.DrawTexture(rect, background);

				if(Items.Count>0)
					foreach(IItem item in Items)
						item.redraw(position.OffsetX+x,
					           		position.OffsetY+y);

		}

	}

}

