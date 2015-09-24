using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace Engine.EGUI.Inventory {
	
	[Serializable]
	public class RectangleSlot : MonoBehaviour {

		[SerializeField] public Texture2D Background = null;
		[SerializeField] public List<IItem> Items = new List<IItem>();
		[SerializeField] public Rect backgroundRect = new Rect(0, 0, 0, 0);
		[SerializeField] public SlotSet position;

		public void redraw(float x, float y){

			if (Background==null) return;

			Rect rect = new Rect(backgroundRect.x+x, backgroundRect.y+y, backgroundRect.width, backgroundRect.height);
			GUI.DrawTexture(rect, Background);

				if(Items.Count>0)
					foreach(IItem item in Items)
						item.redraw(position.OffsetX+x,
					           		position.OffsetY+y);

		}

		void OnValidate() {

			backgroundRect = new Rect(position.OffsetX + CellSettings.cellPaddingX,
									  position.OffsetY + CellSettings.cellPaddingY,
									  position.SlotWidth,
									  position.SlotHeight);

		}

		public SlotSet Position {
			private get {return position;}

			set {
				position = value;
				backgroundRect = new Rect(position.OffsetX + CellSettings.cellPaddingX,
										  position.OffsetY + CellSettings.cellPaddingY,
										  position.SlotWidth,
										  position.SlotHeight);
			}

		}


	}

}

