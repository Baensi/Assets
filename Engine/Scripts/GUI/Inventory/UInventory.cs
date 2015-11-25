using UnityEngine;
using System.Collections.Generic;
using UnityStandardAssets.CrossPlatformInput;
using Engine.I18N;
using Engine.Objects;
using Engine.EGUI.PopupMenu;
using Engine.EGUI.ToolTip;

namespace Engine.EGUI.Inventory {

	[RequireComponent(typeof(InventoryPopupMenu))]
	public class UInventory : MonoBehaviour, IInventory, IRendererGUI {

		[SerializeField] public Texture2D selectCellImage;
		[SerializeField] public Texture2D correctCellImage;
		[SerializeField] public Texture2D errorCellImage;

		[SerializeField] public List<RectangleSlot> slots;
		[SerializeField] public bool visible = false;

		[SerializeField] public InventoryPopupMenu popupMenu;
		[SerializeField] public ToolTipBase toolTip;
		[SerializeField] public float toolTipDelay = 1f;

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

		private float              toolTipTimeStamp = 0f;

#if UNITY_EDITOR
		private bool degubMode = false;
#endif

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
		/// <returns>Возвращает число НЕ добавленных экземпляров предмета, 0 - если педмет успешно добавлен</returns>
		public int addItem(Item item){
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

		/// <summary>
		/// Возвращает список текущих сумок
		/// </summary>
		/// <returns></returns>
		public List<RectangleSlot> getSlots() {
			return slots;
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

			degubMode = true;

			    if (algoritm==null)
				    Start();

			Update();
			OnGUI();

			toolTip.redraw();

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

			if (CrossPlatformInputManager.GetButtonDown(SingletonNames.Input.INVENTAR))
				if (!visible)
					show();
				else
					hide();

		}

		private void FindSelectedSlot() {
			RectangleSlot selected = algoritm.getSlot(eventData.cursorPosition.x - offsetX, eventData.cursorPosition.y - offsetY);

			if (selectedSlot != selected)
				selectedSlot = selected;
		}

		public void OnDrawCells() {

			if (popupMenu.isVisible()) // выходим, если пользователь работает с контекстным меню
				return;

			ItemSlot tmpItem = null;

			FindSelectedSlot();

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

					toolTip.hide();
					toolTipTimeStamp = Time.time;
                    return; // Если ничего не выбрано
				}

				if (eventData.eventType == InventoryEvent.None)
					selectedItem = algoritm.getItem(selectedSlot, selectedCell.X, selectedCell.Y);
				else
					tmpItem = algoritm.getItem(selectedSlot, selectedCell.X, selectedCell.Y);

				toolTipTimeStamp = Time.time;
				toolTip.hide();

			} else {

				if (selectedItem != null && !toolTip.isVisible() && (Time.time - toolTipTimeStamp) > toolTipDelay) {
					toolTip.show(eventData.cursorPosition, ItemToolTipService.getInstance().createDescription(selectedItem.item), ItemToolTipService.getInstance().createInformationItems(selectedItem.item));
					toolTipTimeStamp = Time.time;
				}
			}

			if (tmpItem!=null)
				drawService.DrawCellsItem(selectedSlot, offsetX, offsetY, tmpItem, errorCellImage);

            if (selectedItem == null) {
				toolTip.hide();
                return;
            }

			if ((eventData.selected==null || eventData.selected==selectedItem) && (eventData.collision==null || eventData.collision.item.Equals(eventData.selected.item)))
				drawService.DrawCellsItem(selectedSlot, offsetX, offsetY, selectedItem, correctCellImage);
			else
				drawService.DrawCellsItem(selectedSlot, offsetX, offsetY, selectedItem, errorCellImage);
			
		}

		private void resetSelection() {
			selectedCell.X = -1;
			selectedCell.Y = -1;
			selectedSlot = null;
			selectedItem = null;
		}

		private void ReadMouseEvents() {

#if UNITY_EDITOR

			if (degubMode) { // работаем в окне отладчика

				if (Event.current.isMouse) {
					eventData.mouseEvent.RMouseDown = (Event.current.type == EventType.MouseDown && Event.current.button == 1);
                    eventData.mouseEvent.LMouseDown = (Event.current.type == EventType.MouseDown && Event.current.button==0);
					eventData.mouseEvent.LMouseUp   = (Event.current.type == EventType.MouseUp && Event.current.button==0);
				} else {
					eventData.isDivMode = Event.current.shift;
				}

				eventData.cursorPosition = Event.current.mousePosition;

			} else { // в режиме редактора юнити, игра на демонстрации

				eventData.mouseEvent.RMouseDown = Input.GetMouseButtonDown(1);
				eventData.mouseEvent.LMouseDown = Input.GetMouseButtonDown(0);
				eventData.mouseEvent.LMouseUp   = Input.GetMouseButtonUp(0);

				eventData.cursorPosition = new Vector2(Input.mousePosition.x, -Input.mousePosition.y);

			}

#else // игра запущена полноценно

				eventData.mouseEvent.RMouseDown = Input.GetMouseButtonDown(1);
				eventData.mouseEvent.LMouseDown = Input.GetMouseButtonDown(0);
				eventData.mouseEvent.LMouseUp   = Input.GetMouseButtonUp(0);
			
				eventData.cursorPosition = new Vector2(Input.mousePosition.x, -Input.mousePosition.y);

#endif

		}

		private void resetEventsSelections() {
			selectedCell.X = -1;
			selectedCell.Y = -1; // сбрасываем последнюю выбранную ячейку, чтобы в следующей итерации пересчитать предмет под курсором
			eventData.selected = null; // сбрасываем перемещение предмета
			eventData.collision = null; // сбрасываем коллизию
			eventData.eventType = InventoryEvent.None; // устанавливаем эвент по умолчанию
			selectedItem = null;
		}

		public void OnEvents() {

			ReadMouseEvents(); // читаем события мыши

			if (popupMenu.isVisible()) {
				if (!eventData.mouseEvent.RMouseDown && !eventData.mouseEvent.LMouseDown && !CrossPlatformInputManager.GetButtonDown(SingletonNames.Input.ESC)) {
					return;
				} else {

					if (popupMenu.isFocused())
						return;

					popupMenu.hide();
					resetEventsSelections();
                }
			}

			if (eventData.mouseEvent.LMouseUp && eventData.eventType == InventoryEvent.ItemMove) {

				if (eventData.collision==null
					&& selectedCell.X+eventData.selected.item.getSize().getWidth()<=selectedSlot.position.CellsXCount+1 && 
					selectedCell.Y+eventData.selected.item.getSize().getHeight()<=selectedSlot.position.CellsYCount+1) { // двигаем только в том случае, если нет коллизий, и предмет находится в "сумке"

						if (eventData.isDivMode && eventData.selected.item.getCount()>1) { // можно разделить?

							if (selectedCell.X != eventData.selected.position.X ||
							   selectedCell.Y != eventData.selected.position.Y) { // смотрим, действительно ли предмет разделелили в разные ячейки?

								int count = eventData.selected.item.getCount() / 2;
								eventData.selected.item.decCount(count);

								Item newItem = eventData.selected.item.Clone();
								newItem.setCount(count);

								selectedSlot.Items.Add(new ItemSlot(newItem, new ItemPosition(selectedCell.X, selectedCell.Y)));

							}

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

				resetEventsSelections();
			}

			if(eventData.mouseEvent.RMouseDown && selectedItem != null) { // если пользователь вызвал контекстное меню какого то предмета

				eventData.mouseEvent.RMouseDown = false;
                PopupMenuService.getInatance().SetupPopupMenu(popupMenu, selectedItem.item); // устанавливаем пункты меню
				popupMenu.show(eventData.cursorPosition); // отображаем меню в нужном месте

			}

			if (eventData.mouseEvent.LMouseDown && selectedItem!=null && eventData.selected==null && eventData.eventType == InventoryEvent.None) {

				eventData.eventType = InventoryEvent.ItemMove;
				eventData.selected = selectedItem;
				
			}

			if (eventData.selected!=null) { // если предмет переносится

				drawService.getItemDrawService().DrawItem(eventData.selected, eventData.cursorPosition.x, eventData.cursorPosition.y, false);

				if (selectedSlot!=null) { // если предмет перенесли в какую то сумку и он не вылазиет за рамки сумки

					eventData.collision = algoritm.getCollisionItem(selectedSlot, eventData.selected, selectedCell.X, selectedCell.Y); // вычисляем коллизии внутри зоны перемещения

					if (eventData.collision==null) { // если нет коллизии

						drawService.DrawCells(selectedSlot, offsetX, offsetY, selectedCell, eventData.selected.item.getSize(), correctCellImage);

					} else {

						if (eventData.collision.item.Equals(eventData.selected.item) && !eventData.collision.item.isFullCount()) { // предметы однородные (можно объединить)

							drawService.DrawCells(selectedSlot, offsetX, offsetY, selectedCell, eventData.selected.item.getSize(), selectCellImage);

						} else {

							drawService.DrawCells(selectedSlot, offsetX, offsetY, selectedCell, eventData.selected.item.getSize(), errorCellImage); // подчёркиваем перемещаемую зону как ошибочную
							drawService.DrawCellsItem(selectedSlot, offsetX, offsetY, eventData.collision, errorCellImage); // подчёркиванием причину ошибки - коллизируемый итем

						}
					}

				}

			}

		}

		void OnGUI(){

			if(!visible) return;

			redraw();

			popupMenu.redraw();

		}


	}

}
