using System;
using System.Collections.Generic;
using UnityEngine;
using Engine.Images;
using Engine.I18N;
using Engine.Objects;

namespace Engine.EGUI.Inventory {

	public class InventoryExternal : MonoBehaviour, IExternalInventory {

		private GUIStyle titleStyle;

		private const int MAX_COUNT_X = 4;
		private const int MAX_COUNT_Y = 6;

		private Texture2D background;
		private Texture2D correctCellImage;

		[SerializeField] public string titleTextId;
		[SerializeField] public string captionTextId;
		private string titleText;
		private string captionText;

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

				slot = new SlotData();

				foreach(string item in initListItems)
					addItem(DObjectList.getInstance().getItem(item));

				background = DImageList.getInstance().getImage("external_inventory_background");
				correctCellImage = DImageList.getInstance().getImage("inventory_correct_cell");

				playerInventory = SingletonNames.getInventory().GetComponent<UInventory>();
				drawService = new ItemDrawService(playerInventory.iconStyle);
				titleStyle = playerInventory.titleStyle;
				
				titleText = CLang.getInstance().get(titleTextId);
				captionText = CLang.getInstance().get(captionTextId);

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

		public string getTitleText() {
			return titleText;
		}

		public string getCaptionText() {
			return captionText;
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
			inventory = null;
			visible = false;
		}

		public bool isVisible() {
			return visible;
		}

		public int addItem(Item item) {
			int count = slot.Items.Count;
			int result = ExternalInventoryAlgorithm.getInstance().AddItem(slot, item);
			
			if(count!=slot.Items.Count)
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

			if(!visible)
				return;

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

			if(slot.Items.Count>0)
				foreach (ItemSlot item in slot.Items) // рисуем иконки предметов
					drawService.DrawItem(item, slot.position.OffsetX, slot.position.OffsetY);
			
		}

#if UNITY_EDITOR

		public void OnUpdateEditor(float x, float y) {

			visible = true;

			TryInit();

			slot.position.OffsetX = x;
			slot.position.OffsetY = y;

			slot.position.CellsXCount = cellXCount;
			slot.position.CellsYCount = cellYCount;

			bounds = new Rect(x, y, getWidth(), getHeight());
			titleRect = new Rect(x, y - 12, getWidth(), 48);

			float maxWidth  = CellSettings.cellPaddingX + MAX_COUNT_X * CellSettings.cellWidth;
			float maxHeight = CellSettings.cellPaddingY + MAX_COUNT_Y * CellSettings.cellHeight;

			sourceBounds = new Rect(1, 1, getWidth()/maxWidth, getHeight()/maxHeight); // расчитываем мколько нужно вырезать из исходной картинки чтобы показать видимые ячейки

			OnGUI();

		}

		public void TryInit() {

			if(slot==null)
				slot = new SlotData();

			if(slot.position.CellsXCount<1)
				slot.position.CellsXCount=1;

			if(slot.position.CellsYCount<1)
				slot.position.CellsYCount=1;

			if(background==null)
				background = DImageList.getInstance().getImage("external_inventory_background");

			if(correctCellImage==null)
				correctCellImage = DImageList.getInstance().getImage("inventory_correct_cell");

			if(playerInventory==null)
				playerInventory = SingletonNames.getInventory().GetComponent<UInventory>();

			if(drawService==null)
				drawService = new ItemDrawService(playerInventory.iconStyle);

			titleStyle = playerInventory.titleStyle;

			titleText   = CLang.getInstance().get(titleTextId);
			captionText = CLang.getInstance().get(captionTextId);

		}

#endif

	}

}
