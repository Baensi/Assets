using UnityEngine;
using System.Collections.Generic;
using UnityStandardAssets.CrossPlatformInput;

namespace Engine.EGUI.Inventory {

	public enum InventarEvent : int {

		None       = 0x00,
		ItemPickUp = 0x01,
		ItemMove   = 0x02,
		ItemPut    = 0x03

	};

	public class EventContainer {

		public InventarEvent eventType;
		public Item          item;

	}

	public class UInventory : MonoBehaviour, IInventory, IRendererGUI {

		[SerializeField] public Texture2D selectCellImage;
		[SerializeField] public Texture2D correctCellImage;
		[SerializeField] public Texture2D errorCellImage;

		[SerializeField] public List<RectangleSlot> slots;
		[SerializeField] public bool visible = false;
		
		public float     offsetX;
		public float     offsetY;

		private float mouseX;
		private float mouseY;

		private RectangleSlot  selectedSlot = null;
		private ItemPosition   selectedCell = new ItemPosition(-1, -1);
		private Item           selectedItem = Item.NULL;
		private EventContainer eventData    = new EventContainer();

		private Rect          inventarFramePosition;
		private Rect          cellRect;

		private static Rect       textCoords = new Rect(1, 1, 1, 1);
		private InventoryAlgoritm algoritm = new InventoryAlgoritm();

		public void show(){
			visible = true;
			GameConfig.GameMode = GameConfig.MODE_GUI;
		}
		public void hide(){
			visible=false;
			GameConfig.GameMode = GameConfig.MODE_GAME;
		}
		public bool isVisible(){
			return visible;
		}

		public float getX(){
			return offsetX;
		}

		public float getY(){
			return offsetY;
		}

		public bool addItem(Item item){
			return algoritm.addItem(item);
		}

		public bool removeItem(Item item){
			return algoritm.removeItem(item);
		}

		public List<RectangleSlot> getSlots() {
			return slots;
		}

		public void AddSlot() {

			algoritm.setSlots(slots);
		}

		public void RemoveSlot() {

			algoritm.setSlots(slots);
		}

			public UInventory(){

			}

		public void redraw(){

			foreach (RectangleSlot slot in slots)
				if (slot!=null)
					slot.redraw(offsetX,offsetY);

			OnDrawCells(mouseX, mouseY);

			OnDrawPicked(mouseX, mouseY);
			
		}

	// Режим редактора
#if UNITY_EDITOR

		public void onResizeWindow(float width, float height){
			if (algoritm!=null)
				offsetX = (width - algoritm.getInventoryWidth())   * 0.5f;
				offsetY = (height - algoritm.getInventoryHeight()) * 0.5f;
		}

#endif

		public void onResizeWindow() {
			if (algoritm!=null)
				offsetX = (Screen.width - algoritm.getInventoryWidth())   * 0.5f;
				offsetY = (Screen.height - algoritm.getInventoryHeight()) * 0.5f;
		}

		void Start () {

			algoritm.setSlots(slots);

			onResizeWindow();

			if(visible)
				redraw();
			
		}

		void Update () {

			if (CrossPlatformInputManager.GetButtonDown("Inventar"))
				if (!visible)
					show();
				else
					hide();

		}


		public void OnDrawCells(float mouseX, float mouseY) {

			RectangleSlot selected = algoritm.getSlot(mouseX-offsetX, mouseY-offsetY);

			if (selectedSlot!=selected)
				selectedSlot=selected;

			if (selectedSlot==null) return;

			int cellX = 1+(int)(mouseX-offsetX-CellSettings.cellPaddingX-selectedSlot.position.OffsetX) / CellSettings.cellWidth;
			int cellY = 1+(int)(mouseY-offsetY-CellSettings.cellPaddingY-selectedSlot.position.OffsetY) / CellSettings.cellHeight;

			if(cellX!=selectedCell.X || cellY!=selectedCell.Y){ // Ячейка новая

				selectedCell.X = cellX;
				selectedCell.Y = cellY;

				if (selectedCell.X == -1 || selectedCell.Y == -1 ||
					selectedCell.X > selectedSlot.position.CellsXCount ||
					selectedCell.Y > selectedSlot.position.CellsYCount) return; // Если ничего не выбрано

				selectedItem = algoritm.getItem(selectedSlot, selectedCell.X, selectedCell.Y);

			}

			if (selectedItem.getSize()==null)
				return;

			DrawCellsItem(selectedSlot, offsetX, offsetY, selectedItem, correctCellImage);

		}

		public void OnDrawPicked(float mouseX, float mouseY) {

			if (Input.GetMouseButtonDown(0) && eventData.item==Item.NULL) {
				
			}

			//if (pickItem.icon==null)
			//	return;

			//pickItem.redraw(mouseX, mouseY);

		}

		public void DrawCellsItem(RectangleSlot slot, float offsetX, float offsetY, Item item, Texture2D image) {
			Rect cellRect = new Rect();
				cellRect.x = offsetX + (item.getPosition().X-1) * CellSettings.cellWidth  + CellSettings.cellPaddingX + slot.position.OffsetX;
				cellRect.y = offsetY + (item.getPosition().Y-1) * CellSettings.cellHeight + CellSettings.cellPaddingY + slot.position.OffsetY;
				cellRect.width  = CellSettings.cellWidth * item.getSize().getWidth();
				cellRect.height = CellSettings.cellHeight * item.getSize().getHeight();
			GUI.DrawTextureWithTexCoords(cellRect, image, textCoords);
		}

		void OnGUI(){

			if(!visible) return;

#if !UNITY_EDITOR

			mouseX = Input.mousePosition.x;
			mouseY = Input.mousePosition.y;

#endif
			redraw();

		}

		#if UNITY_EDITOR

		public void OnEditorUpdate(float x, float y){
			if (!visible) return;

			algoritm.setSlots(slots);

			mouseX=x;
			mouseY=y;

			Update();
			OnGUI();

		}

		#endif

	}

}
