using UnityEngine;
using System.Collections.Generic;
using UnityStandardAssets.CrossPlatformInput;
using Engine.I18N;
using Engine.Objects;

namespace Engine.EGUI.Inventory {
	
	public class SlotDrawService {

		private static Rect textCoords = new Rect(1, 1, 1, 1);

		private ItemDrawService itemDrawService;
		
		private List<RectangleSlot> slots;
		private Rect backgroundRect;
		
		public SlotDrawService(List<RectangleSlot> slots, GUIStyle iconStyle, GUIStyle iconShadow){
			this.slots=slots;
			itemDrawService = new ItemDrawService(iconStyle,iconShadow);
		}

		public ItemDrawService getItemDrawService() {
			return itemDrawService;
		}
		
		public void DrawSlots(float offsetX, float offsetY){

			if (slots == null)
				return;

			foreach(RectangleSlot slot in slots){

				if (slot.background != null) {

					backgroundRect = new Rect(slot.position.OffsetX + CellSettings.cellPaddingX,
											  slot.position.OffsetY + CellSettings.cellPaddingY,
											  slot.position.SlotWidth,
											  slot.position.SlotHeight);


					Rect rect = new Rect(backgroundRect.x + offsetX, backgroundRect.y + offsetY, backgroundRect.width, backgroundRect.height);
					GUI.color = Color.white;
					GUI.DrawTexture(rect, slot.background);

					if (slot.Items != null && slot.Items.Count > 0)
						foreach (ItemSlot item in slot.Items)
							itemDrawService.DrawItem(item, slot.position.OffsetX + offsetX, slot.position.OffsetY + offsetY);

				}
				
			}
			
			
		}

		public void DrawCellsItem(RectangleSlot slot, float offsetX, float offsetY, ItemSlot item, Texture2D image) {
			Rect cellRect = new Rect();
			cellRect.x = offsetX + (item.getPosition().X-1) * CellSettings.cellWidth  + CellSettings.cellPaddingX + slot.position.OffsetX;
			cellRect.y = offsetY + (item.getPosition().Y-1) * CellSettings.cellHeight + CellSettings.cellPaddingY + slot.position.OffsetY;
			cellRect.width  = CellSettings.cellWidth * item.item.getSize().getWidth();
			cellRect.height = CellSettings.cellHeight * item.item.getSize().getHeight();

			GUI.color = Color.white;
			GUI.DrawTextureWithTexCoords(cellRect, image, textCoords);
		}

		public void DrawCells(RectangleSlot slot, float offsetX, float offsetY, ItemPosition position, ItemSize size, Texture2D image) {
			Rect cellRect = new Rect();
			cellRect.x = offsetX+(position.X-1) * CellSettings.cellWidth  + CellSettings.cellPaddingX + slot.position.OffsetX;
			cellRect.y = offsetY+(position.Y-1) * CellSettings.cellHeight + CellSettings.cellPaddingY + slot.position.OffsetY;
			cellRect.width  = CellSettings.cellWidth * size.getWidth();
			cellRect.height = CellSettings.cellHeight * size.getHeight();

			GUI.color = Color.white;
			GUI.DrawTextureWithTexCoords(cellRect, image, textCoords);
		}
		
		
	}
	
}