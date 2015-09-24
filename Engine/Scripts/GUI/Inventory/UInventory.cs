using UnityEngine;
using System.Collections.Generic;
using UnityStandardAssets.CrossPlatformInput;

namespace Engine.EGUI.Inventory {

	[ExecuteInEditMode]
	public class UInventory : MonoBehaviour, IInventory, IRendererGUI {

		[SerializeField] public Texture2D selectCellImage;
		[SerializeField] public Texture2D correctCellImage;
		[SerializeField] public Texture2D errorCellImage;

		[SerializeField] public List<RectangleSlot> slots;
		[SerializeField] public bool visible;
		
		public float     offsetX;
		public float     offsetY;

		private float mouseX;
		private float mouseY;

		private RectangleSlot selectedSlot = null;
		private ItemPosition  selectedCell = new ItemPosition(-1, -1);
		private Rect          inventarFramePosition;
		private Rect          cellRect;

		private InventoryAlgoritm algoritm = new InventoryAlgoritm();

		public void show(){
			visible = true;
			GameConfig.Game.GameMode = GameConfig.Game.MODE_GUI;
		}
		public void hide(){
			visible=false;
			GameConfig.Game.GameMode = GameConfig.Game.MODE_GAME;
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

		public bool addItem(IItem item){
			return algoritm.addItem(item);
		}

		public bool removeItem(IItem item){
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

			int cellX = (int)(mouseX-offsetX-CellSettings.cellPaddingX-selectedSlot.position.OffsetX) / CellSettings.cellWidth;
			int cellY = (int)(mouseY-offsetY-CellSettings.cellPaddingY-selectedSlot.position.OffsetY) / CellSettings.cellHeight;

			if(cellX!=selectedCell.X || cellY!=selectedCell.Y){ // Ячейка новая

				selectedCell.X = cellX;
				selectedCell.Y = cellY;

				if (selectedCell.X == -1 || selectedCell.Y == -1 ||
					selectedCell.X > selectedSlot.position.CellsXCount ||
					selectedCell.Y > selectedSlot.position.CellsYCount) return; // Если ничего не выбрано

				cellRect.x = offsetX + cellX * CellSettings.cellWidth  + CellSettings.cellPaddingX + selectedSlot.position.OffsetX;
				cellRect.y = offsetY + cellY * CellSettings.cellHeight + CellSettings.cellPaddingY + selectedSlot.position.OffsetY;

				cellRect.width  = CellSettings.cellWidth;
				cellRect.height = CellSettings.cellHeight;

			}

			if (selectedCell.X==-1 || selectedCell.Y==-1 ||
				selectedCell.X > selectedSlot.position.CellsXCount ||
				selectedCell.Y > selectedSlot.position.CellsYCount) return; // Если ничего не выбрано

			GUI.DrawTexture(cellRect, correctCellImage);

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
