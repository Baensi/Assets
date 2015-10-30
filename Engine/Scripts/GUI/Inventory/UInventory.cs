using UnityEngine;
using System.Collections.Generic;
using UnityStandardAssets.CrossPlatformInput;
using Engine.I18N;
using Engine.Objects;

namespace Engine.EGUI.Inventory {

	public enum InventarEvent : int {

		None       = 0x00,
		ItemMove   = 0x01

	};

	public class MouseEvent {

		public bool MouseDrag = false;
		public bool MouseDown = false;
		public bool MouseUp   = false;

	}

	public class EventContainer {

		public Vector2       cursorPosition;
		public MouseEvent    mouseEvent = new MouseEvent();
		public InventarEvent eventType = InventarEvent.None;

		public bool isDivMode = false; // Зажат шифт и предметы надо делить

		public ItemSlot selected;
		public ItemSlot collision;

	}

	public class UInventory : MonoBehaviour, IInventory, IRendererGUI {

		[SerializeField] public Texture2D selectCellImage;
		[SerializeField] public Texture2D correctCellImage;
		[SerializeField] public Texture2D errorCellImage;

		[SerializeField] public List<RectangleSlot> slots;
		[SerializeField] public bool visible = false;
		
		public float     offsetX;
		public float     offsetY;

		private RectangleSlot  selectedSlot = null;
		private ItemPosition   selectedCell = new ItemPosition(-1, -1);
		private ItemSlot       selectedItem = null;
		private EventContainer eventData    = new EventContainer();

		private Rect          inventarFramePosition;
		private Rect          cellRect;

		private InventoryAlgoritm algoritm;
		private SlotDrawService   drawService;

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

		/// <summary>
		/// Пытается добавить предмет item в инвентарь
		/// </summary>
		/// <param name="item">Добавляемый предмет</param>
		/// <returns>Возвращает результат операции</returns>
		public bool addItem(Item item){
			return algoritm.addItem(item);
		}

		/// <summary>
		/// Удаляет предмет Item из инвентаря
		/// </summary>
		/// <param name="item">Удаляемый предмет</param>
		/// <param name="equals">если true - проверяется эквивалентность (item1.Equals(item2)), если false, проверяется адрес (item1==item2)</param>
		/// <param name="full">При full=true, удаляет предмет независимо от колличества экземпляров</param>
		/// <param name="count">Число удаляемых экземпляров (только при full=false)</param>
		/// <returns>Возвращает результат операции удаления (false = предмет не найден, или найденных экземпляров меньше count)</returns>
		public bool removeItem(Item item, bool equals = true, bool full = true, int count = 1){
			return algoritm.removeItem(item, equals, full, count);
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

			drawService.DrawSlots(offsetX, offsetY);

			OnDrawCells();
			OnEvents();
			
		}

	// Режим редактора
#if UNITY_EDITOR

		public void onResizeWindow(float width, float height){
			if (algoritm==null)
				Start();

			offsetX = (width - algoritm.getInventoryWidth())   * 0.5f;
			offsetY = (height - algoritm.getInventoryHeight()) * 0.5f;

		}

		public void OnEditorUpdate() {
			if (!visible) return;

			if (algoritm==null)
				Start();

			Update();
			OnGUI();

		}

#endif

		public void onResizeWindow() {
			if (algoritm!=null)
				offsetX = (Screen.width - algoritm.getInventoryWidth())   * 0.5f;
				offsetY = (Screen.height - algoritm.getInventoryHeight()) * 0.5f;
		}

		void Start () {

			algoritm = new InventoryAlgoritm();
			algoritm.setSlots(slots);
			drawService = new SlotDrawService(slots);

			onResizeWindow();

			if(visible)
				redraw();
			
		}

		void Update () {

#if !UNITY_EDITOR

			eventData.cursorPosition       = Input.mousePosition;

			eventData.mouseEvent.MouseDown = Input.GetMouseButtonDown(0);
			eventData.mouseEvent.MouseUp   = Input.GetMouseButtonUp(0);

#endif

			if (CrossPlatformInputManager.GetButtonDown("Inventar"))
				if (!visible)
					show();
				else
					hide();

		}


		public void OnDrawCells() {

			ItemSlot tmpItem = null;
			RectangleSlot selected = algoritm.getSlot(eventData.cursorPosition.x - offsetX, eventData.cursorPosition.y - offsetY);

			if (selectedSlot!=selected)
				selectedSlot=selected;

			if (selectedSlot==null) return;

			int cellX = 1 + (int)(eventData.cursorPosition.x - offsetX - CellSettings.cellPaddingX - selectedSlot.position.OffsetX) / CellSettings.cellWidth;
			int cellY = 1 + (int)(eventData.cursorPosition.y - offsetY - CellSettings.cellPaddingY - selectedSlot.position.OffsetY) / CellSettings.cellHeight;

			if(cellX!=selectedCell.X || cellY!=selectedCell.Y){ // Ячейка новая

				selectedCell.X = cellX;
				selectedCell.Y = cellY;

				if (selectedCell.X == -1 || selectedCell.Y == -1 ||
					selectedCell.X > selectedSlot.position.CellsXCount ||
					selectedCell.Y > selectedSlot.position.CellsYCount) {
					selectedCell.X = 1;
					selectedCell.Y = 1;
					return; // Если ничего не выбрано
				}

				if (eventData.eventType == InventarEvent.None)
					selectedItem = algoritm.getItem(selectedSlot, selectedCell.X, selectedCell.Y);
				else
					tmpItem = algoritm.getItem(selectedSlot, selectedCell.X, selectedCell.Y);

			}

			if (tmpItem!=null)
				drawService.DrawCellsItem(selectedSlot, offsetX, offsetY, tmpItem, errorCellImage);

			if (selectedItem==null)
				return;

			if ((eventData.selected==null || eventData.selected==selectedItem) && (eventData.collision==null || eventData.collision.item.Equals(eventData.selected.item)))
				drawService.DrawCellsItem(selectedSlot, offsetX, offsetY, selectedItem, correctCellImage);
			else
				drawService.DrawCellsItem(selectedSlot, offsetX, offsetY, selectedItem, errorCellImage);

			GUI.Box(new Rect(eventData.cursorPosition.x, eventData.cursorPosition.y, 240, 30), selectedItem.item.getDescription().dName + " - " + selectedItem.item.getDescription().dCaption);

		}

		private void resetSelection() {
			selectedCell.X = -1;
			selectedCell.Y = -1;
			selectedSlot = null;
			selectedItem = null;
		}

		public void OnEvents() {

			if (Event.current.isMouse) {
				eventData.mouseEvent.MouseDown = (Event.current.type == EventType.MouseDown);
				eventData.mouseEvent.MouseDrag = (Event.current.type == EventType.MouseDrag);
				eventData.mouseEvent.MouseUp   = (Event.current.type == EventType.MouseUp);
			} else {
				eventData.isDivMode = Event.current.shift;
			}


			eventData.cursorPosition = Event.current.mousePosition;

			if (eventData.mouseEvent.MouseUp && eventData.eventType == InventarEvent.ItemMove) {

				if (eventData.collision==null
					&& selectedCell.X+eventData.selected.item.getSize().getWidth()<=selectedSlot.position.CellsXCount+1 && 
					selectedCell.Y+eventData.selected.item.getSize().getHeight()<=selectedSlot.position.CellsYCount+1) { // двигаем только в том случае, если нет коллизий, и предмет находится в "сумке"

						if (eventData.isDivMode && eventData.selected.item.getCount()>1) { // можно разделить?

							int count = eventData.selected.item.getCount() / 2;
							eventData.selected.item.decCount(count);

							Item newItem = eventData.selected.item.Clone();
							newItem.setCount(count);

							selectedSlot.Items.Add(new ItemSlot(newItem, new ItemPosition(selectedCell.X,selectedCell.Y)));

						} else { // нужно перемещать

							eventData.selected.getPosition().X=selectedCell.X; // перемещаем предмет
							eventData.selected.getPosition().Y=selectedCell.Y;

						}

				} else {

					int itemCount   = eventData.selected.item.getCount();

					if (eventData.collision.item.Equals(eventData.selected.item) && !eventData.collision.item.isFullCount()) { // у второго предмета есть запас, можно передать ему экземпляры первого

						itemCount = eventData.collision.item.incCount(itemCount); // передаём экземпляры второму предмету

						if(itemCount==0) // остатка нет
							removeItem(eventData.selected.item,false); // удаляем перемещяемый предмет
						else
							eventData.selected.item.setCount(itemCount); // устанавливаем остаток экземпляров перемещаемому предмету

					}

				}

				eventData.selected = null; // сбрасываем перемещение предмета
				eventData.collision = null; // сбрасываем коллизию
				eventData.eventType = InventarEvent.None; // устанавливаем эвент по умолчанию
				
			}

			if (eventData.mouseEvent.MouseDown && selectedItem!=null && eventData.selected==null && eventData.eventType == InventarEvent.None) {

				eventData.eventType = InventarEvent.ItemMove;
				eventData.selected = selectedItem;
				
			}

			if (eventData.selected!=null) { // если предмет переносится

				drawService.getItemDrawService().DrawItem(eventData.selected, eventData.cursorPosition.x, eventData.cursorPosition.y, false);

				if (selectedSlot!=null) { // если предмет перенесли в какой-то слот и он не вылазиет за рамки слота

					eventData.collision = algoritm.getCollisionItem(selectedSlot, eventData.selected, selectedCell.X, selectedCell.Y); // вычисляем коллизии внутри зоны перемещения

					if (eventData.collision==null || (eventData.collision.item.Equals(eventData.selected.item) && !eventData.collision.item.isFullCount())) { // если нет коллизии, либо предметы однородные (можно объединить)

						drawService.DrawCells(selectedSlot, offsetX, offsetY, selectedCell, eventData.selected.item.getSize(), correctCellImage);

					} else {

						drawService.DrawCells(selectedSlot, offsetX, offsetY, selectedCell, eventData.selected.item.getSize(), errorCellImage); // подчёркиваем перемещаемую зону как ошибочную
						drawService.DrawCellsItem(selectedSlot, offsetX, offsetY, eventData.collision, errorCellImage); // подчёркиванием причину ошибки - коллизируемый итем

					}

				}



			}

		}

		void OnGUI(){

			if(!visible) return;

			redraw();

		}


	}

}
