using UnityEngine;

namespace Engine.EGUI.Inventory {

	public class InventoryExternalDrawServices {

		private static InventoryExternalDrawServices instance;

		private Rect textCoords = new Rect(1,1,1,1);

		public static InventoryExternalDrawServices getInstance() {
			if(instance==null)
				instance = new InventoryExternalDrawServices();
			return instance;
		}

		public void DrawCellsItem(float offsetX, float offsetY, ItemSlot item, Texture2D image) {
			Rect cellRect = new Rect();

			cellRect.x = offsetX + (item.getPosition().X - 1) * CellSettings.cellWidth + CellSettings.cellPaddingX;
			cellRect.y = offsetY + (item.getPosition().Y - 1) * CellSettings.cellHeight + CellSettings.cellPaddingY;
			cellRect.width = CellSettings.cellWidth * item.item.getSize().getWidth();
			cellRect.height = CellSettings.cellHeight * item.item.getSize().getHeight();

			GUI.color = Color.white;
			GUI.DrawTextureWithTexCoords(cellRect, image, textCoords);
		}

	}

}
