using System;
using System.Collections.Generic;
using UnityEngine;
using Engine.Images;
using Engine.I18N;
using Engine.Objects;

namespace Engine.EGUI.Inventory {

	public class InventoryExternal : MonoBehaviour, IExternalData {

		private GUIStyle titleStyle;

		private const int MAX_COUNT_X = 4;
		private const int MAX_COUNT_Y = 6;

		private Texture2D background;
		private Texture2D correctCellImage;

		[SerializeField] public string titleTextId;
		private string titleText;

		[SerializeField] public int cellXCount = 4;
		[SerializeField] public int cellYCount = 2;

		private bool visible;

		private SlotData slot;

		private Rect     titleRect;
		private Rect     sourceBounds;
		private Rect     bounds;

		[SerializeField] public List<string> initListItems = new List<string>();

		private ItemSlot selectedItem;

		private ItemDrawService drawService;

		private UInventory    playerInventory;
        private IExternalData inventory;

			void Start() {

				foreach(string item in initListItems)
					addItem(DObjectList.getInstance().getItem(item));

				background = DImageList.getInstance().getImage("external_inventory_background");
				correctCellImage = DImageList.getInstance().getImage("inventory_correct_cell");

				playerInventory = SingletonNames.getInventory().GetComponent<UInventory>();
				drawService = new ItemDrawService(playerInventory.iconStyle);
				titleStyle = playerInventory.titleStyle;
				
				titleText = CLang.getInstance().get(titleTextId);

				initListItems.Clear();

			}

		public SlotData getSlot() {
			return slot;
		}

		public float getWidth() {
			return slot.position.SlotWidth;
		}

		public float getHeight() {
			return slot.position.SlotHeight;
		}

		public void show(IExternalData inventory, float x, float y) {
			this.inventory=inventory;
			slot.position.OffsetX=x;
			slot.position.OffsetY=y;

			slot.position.CellsXCount = cellXCount;
			slot.position.CellsYCount = cellYCount;

			float width = getWidth();
			float height = getHeight();

			bounds       = new Rect(x, y, width, height);
			titleRect    = new Rect(x, y - 12, getWidth(), 48);

			float maxWidth  = CellSettings.cellPaddingX + MAX_COUNT_X * CellSettings.cellWidth;
			float maxHeight = CellSettings.cellPaddingY + MAX_COUNT_Y * CellSettings.cellHeight;

			sourceBounds = new Rect(0,0, maxWidth/width, maxHeight/height); // расчитываем мколько нужно вырезать из исходной картинки чтобы показать видимые ячейки

			visible = true;
		}

		public void hide() {
			visible = false;
		}

		public bool isVisible() {
			return visible;
		}

		public int addItem(Item item) {

			int result = ExternalInventoryAlgorithm.getInstance().AddItem(slot, item);
			
			if(result!=item.getCount())
				ExternalInventoryAlgorithm.getInstance().SortData(slot);

			return result;
		}

		public bool removeItem(Item item) {
			ItemSlot itemSlot = ExternalInventoryAlgorithm.getInstance().FindItemSlot(slot, item);

			if(itemSlot==null)
				return false;

			bool result = ExternalInventoryAlgorithm.getInstance().RemoveItem(slot,itemSlot);

			if(result)
				ExternalInventoryAlgorithm.getInstance().SortData(slot);

			return result;
			
		}

		void OnGUI() {

			int selX = (int)((Event.current.mousePosition.x - slot.position.OffsetX - CellSettings.cellPaddingX) / CellSettings.cellWidth)+1;
			int selY = (int)((Event.current.mousePosition.y - slot.position.OffsetY - CellSettings.cellPaddingY) / CellSettings.cellHeight)+1;

			GUI.Label(titleRect, titleText, titleStyle); // отображаем метку сумки
			GUI.DrawTextureWithTexCoords(bounds, background, sourceBounds, true); // рисуем фрагмент инвентаря, нужного размера

			if (selX >= 0 && selX <= cellXCount && selY >= 0 && selY <= cellYCount)
				selectedItem = ExternalInventoryAlgorithm.getInstance().getItem(slot, selX, selY);
			else
				selectedItem = null;


			if (selectedItem != null) {
				InventoryExternalDrawServices.getInstance().DrawCellsItem(slot.position.OffsetX,
																		  slot.position.OffsetY,
																		  selectedItem,
																		  correctCellImage);

				if (Event.current.isMouse && Event.current.type == EventType.MouseDown && Event.current.button == 0) {

					if (inventory != null) { // пытаемся передать предмет в другой инвентарь

						int result = inventory.addItem(selectedItem.item);

						if (result == 0)
							slot.Items.Remove(selectedItem);
						else
							selectedItem.item.setCount(result);

					}

				}
			}


			foreach (ItemSlot item in slot.Items) // рисуем иконки предметов
				drawService.DrawItem(item, slot.position.OffsetX, slot.position.OffsetY);

		}

#if UNITY_EDITOR

		void OnValidate() {

			if (cellXCount < 1)
				cellXCount = 1;

			if (cellYCount < 1)
				cellYCount = 1;

		}

		public void OnUpdateEditor(float x, float y) {

			slot.position.OffsetX = x;
			slot.position.OffsetY = y;

			slot.position.CellsXCount = cellXCount;
			slot.position.CellsYCount = cellYCount;

			bounds = new Rect(x, y, getWidth(), getHeight());
			titleRect = new Rect(x, y - 12, getWidth(), 48);

			float maxWidth  = CellSettings.cellPaddingX + MAX_COUNT_X * CellSettings.cellWidth;
			float maxHeight = CellSettings.cellPaddingY + MAX_COUNT_Y * CellSettings.cellHeight;

			sourceBounds = new Rect(1, 1, getWidth()/ maxWidth, getHeight()/ maxHeight); // расчитываем мколько нужно вырезать из исходной картинки чтобы показать видимые ячейки

			TryInit();

			titleText = CLang.getInstance().get(titleTextId);
			titleStyle = playerInventory.titleStyle;

			OnGUI();

		}

		private void TryInit() {

			if(background==null)
				background = DImageList.getInstance().getImage("external_inventory_background");

			if(correctCellImage==null)
				correctCellImage = DImageList.getInstance().getImage("inventory_correct_cell");

			if(playerInventory==null)
				playerInventory = SingletonNames.getInventory().GetComponent<UInventory>();

			if(drawService==null)
				drawService = new ItemDrawService(playerInventory.iconStyle);

			if(titleStyle==null)
				titleStyle = playerInventory.titleStyle;

			if(titleText==null)
				titleText = CLang.getInstance().get(titleTextId);

		}

#endif

	}

}
