using UnityEngine;
using System.Collections.Generic;
using UnityStandardAssets.CrossPlatformInput;
using Engine.I18N;
using Engine.Objects;
using Engine.EGUI.PopupMenu;
using Engine.EGUI.ToolTip;
using Engine.Images;

namespace Engine.EGUI.Inventory {
	
	public class UInventory : MonoBehaviour, IInventory, IExternalData {

		[SerializeField] public GUIStyle iconStyle;
		[SerializeField] public GUIStyle iconShadow;
		[SerializeField] public GUIStyle titleStyle;

		private Texture2D selectCellImage;
		private Texture2D correctCellImage;
		private Texture2D errorCellImage;

		[SerializeField] public List<RectangleSlot> slots;
		private bool visible = false;

		[SerializeField] public InventoryPopupMenu popupMenu;
		[SerializeField] public ToolTipBase toolTip;

		[SerializeField] public float movXPos = 0f; // смещение положения инвентаря относительно центра
		[SerializeField] public float movYPos = 0f;

		/// <summary> Положение по x окна инвентаря </summary>
		private float     offsetX;
		/// <summary> Положение по y окна инвентаря </summary>
		private float     offsetY;

		private ItemPosition selectedCell;
		private ItemSlot       selectedItem;
		private EventContainer eventData;

		private IExternalInventory externalInventory;

		private Rect          inventarFramePosition;
		private Rect          cellRect;

		private SlotDrawService   drawService;

		/// <summary> Ширина окна инвентаря </summary>
		private float width  = 0f;
		/// <summary> Высота окна инвентаря </summary>
		private float height = 0f;

#if UNITY_EDITOR

			// Вспомогательные переменные для отладки
		private bool  debugMode = false;
		/// <summary> Ширина фрейма отладочного окна </summary>
		private float debugWindowWidth = 0f;
		/// <summary> Высота фрейма отладочного окна </summary>
		private float debugWindowHeight = 0f;

#endif

		private void setupInventoryPosition() {

			width  = InventoryAlgoritm.getInstance().getInventoryWidth(slots);
			height = InventoryAlgoritm.getInstance().getInventoryHeight(slots);

#if UNITY_EDITOR

			if (debugMode) { 
				offsetX = (debugWindowWidth - width) * 0.5f + movXPos;
				offsetY = (debugWindowHeight - height) * 0.5f + movYPos;
			} else {
				offsetX = (Screen.width - width) * 0.5f + movXPos;
				offsetY = (Screen.height - height) * 0.5f + movYPos;
			}

#else
			offsetX = (Screen.width - width) * 0.5f + movXPos;
			offsetY = (Screen.height - height) * 0.5f + movYPos;
#endif

		}

		/// <summary>
		/// Отображает инвентарь
		/// </summary>
		public void show(){

			movXPos = 0f;
			movYPos = 0f;
			setupInventoryPosition();

			externalInventory=null;
			visible = true;
			GameConfig.GameMode = GameConfig.MODE_GUI;
		}

		/// <summary>
		/// Отображает инвентрарь в режиме внешнего инвентаря
		/// </summary>
		/// <param name="externalInventory"></param>
		public void show(IExternalInventory externalInventory) {

			movXPos = externalInventory.getWidth()*0.5f; // смещаем инвентарь вправо, чтобы центровать все инвентари правильно
			movYPos = 0f;
			setupInventoryPosition();

			this.externalInventory=externalInventory;
			visible = true;
			GameConfig.GameMode = GameConfig.MODE_GUI;

			float leftCenterX = (getX() - externalInventory.getWidth()) * 0.5f;
			float leftCenterY = getY() + (getHeight()-externalInventory.getHeight())*0.5f;
			externalInventory.show(this, leftCenterX, leftCenterY); // устанавливаем позицию внешней сумки
			
		}

		public void hide() {

			if (externalInventory != null) { 
				externalInventory.hide();
				externalInventory = null;
			}

			visible=false;
			GameConfig.GameMode = GameConfig.MODE_GAME;

			if(toolTip.isVisible()) // ломаем и прятаем вспомогательные меню и подсказки
				toolTip.hide();

			if(popupMenu.isVisible())
				popupMenu.hide();
		}

		public bool isVisible(){
			return visible;
		}

		public float getX(){
			return offsetX + movXPos;
		}

		public float getY(){
			return offsetY + movYPos;
		}

		public float getWidth() {
			return width;
		}

		public float getHeight() {
			return height;
		}

		/// <summary>
		/// Пытается добавить предмет item в инвентарь
		/// </summary>
		/// <param name="item">Добавляемый предмет</param>
		/// <returns>Возвращает число НЕ добавленных экземпляров предмета, 0 - если педмет успешно добавлен</returns>
		public int addItem(Item item){
			return InventoryAlgoritm.getInstance().addItem(slots, item);
		}

		public bool removeItem(Item item) {
			return InventoryAlgoritm.getInstance().removeItem(slots, item, false, true);
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
			return InventoryAlgoritm.getInstance().removeItem(slots, item, equals, full, count);
		}

		/// <summary>
		/// Возвращает список текущих сумок
		/// </summary>
		/// <returns></returns>
		public List<RectangleSlot> getSlots() {
			return slots;
		}
		
		/// <summary>
		/// Метод отрисовки
		/// </summary>
		public void redraw() {

			drawService.DrawSlots(offsetX, offsetY);
			OnDrawCells();
			OnEvents();
			
		}

// Режим редактора
#if UNITY_EDITOR

		/// <summary>
		/// Выполняет update в режиме редактора-отладчика
		/// </summary>
		public void OnEditorUpdate(float debugWindowWidth, float debugWindowHeight) {

			this.debugWindowWidth  = debugWindowWidth;
            this.debugWindowHeight = debugWindowHeight;

			debugMode = true;

				if (eventData == null)
					eventData = new EventContainer();

				if (selectCellImage == null || drawService == null || iconShadow==null || iconStyle ==null) 
				    Start();

			redraw();
			popupMenu.redraw();
			toolTip.redraw();

		}

#endif

		void Start () {

			selectCellImage  = DImageList.getInstance().getImage("inventory_selected_cell");
			correctCellImage = DImageList.getInstance().getImage("inventory_correct_cell");
			errorCellImage   = DImageList.getInstance().getImage("inventory_error_cell");

			drawService = new SlotDrawService(slots, iconStyle, iconShadow);

			eventData    = new EventContainer();
			selectedCell = new ItemPosition(-1, -1);

			popupMenu.SetupListeners(toolTip);

#if UNITY_EDITOR
				debugMode = false;
#endif
			
		}

		void Update () {

			if (!visible) {
				if (CrossPlatformInputManager.GetButtonDown(SingletonNames.Input.INVENTORY))
					show();
			} else
				if (CrossPlatformInputManager.GetButtonDown(SingletonNames.Input.INVENTORY) ||
					CrossPlatformInputManager.GetButtonDown(SingletonNames.Input.ESC))
				hide();

		}

		private void FindSelectedSlot() {
			RectangleSlot selectedSlot = InventoryAlgoritm.getInstance().getSlot(slots, eventData.cursorPosition.x - offsetX, eventData.cursorPosition.y - offsetY);

			if (eventData.endSlot != selectedSlot)
				eventData.endSlot = selectedSlot;
		}

		private void OnDrawCells() {

			if (popupMenu.isVisible()) // выходим, если пользователь работает с контекстным меню
				return;

			ItemSlot tmpItem = null;

			FindSelectedSlot();

			if (eventData.endSlot == null)
				return;

			int cellX = 1 + (int)(eventData.cursorPosition.x - offsetX - CellSettings.cellPaddingX - eventData.endSlot.position.OffsetX) / CellSettings.cellWidth;
			int cellY = 1 + (int)(eventData.cursorPosition.y - offsetY - CellSettings.cellPaddingY - eventData.endSlot.position.OffsetY) / CellSettings.cellHeight;

			if(cellX!=selectedCell.X || cellY!=selectedCell.Y){ // Ячейка новая
				
                selectedCell.X = cellX;
				selectedCell.Y = cellY;

				if (selectedCell.X == -1 || selectedCell.Y == -1 ||
					selectedCell.X > eventData.endSlot.position.CellsXCount ||
					selectedCell.Y > eventData.endSlot.position.CellsYCount) {
					selectedCell.X = 1;
					selectedCell.Y = 1;


                    return; // Если ничего не выбрано
				}

				if (eventData.eventType == InventoryEvent.None)
					selectedItem = InventoryAlgoritm.getInstance().getItem(slots, eventData.endSlot, selectedCell.X, selectedCell.Y);
				else
					tmpItem = InventoryAlgoritm.getInstance().getItem(slots, eventData.endSlot, selectedCell.X, selectedCell.Y);


			}

			if (tmpItem!=null)
				drawService.DrawCellsItem(eventData.endSlot, offsetX, offsetY, tmpItem, errorCellImage);

            if (selectedItem == null)
                return;

			if ((eventData.selected==null || eventData.selected==selectedItem) && (eventData.collision==null || eventData.collision.item.Equals(eventData.selected.item)))
				if(eventData.startSlot!=null)
					drawService.DrawCellsItem(eventData.startSlot, offsetX, offsetY, selectedItem, correctCellImage);
				else
					drawService.DrawCellsItem(eventData.endSlot, offsetX, offsetY, selectedItem, correctCellImage);
			else
				if (eventData.startSlot != null)
					drawService.DrawCellsItem(eventData.startSlot, offsetX, offsetY, selectedItem, errorCellImage);
				else
					drawService.DrawCellsItem(eventData.endSlot, offsetX, offsetY, selectedItem, errorCellImage);

		}

		private void resetSelection() {
			selectedCell.X = -1;
			selectedCell.Y = -1;
			eventData.endSlot = null;
			eventData.startSlot = null;
			selectedItem = null;
		}

		private void ReadMouseEvents() {

			if (Event.current.isMouse) {
				eventData.mouseEvent.RMouseDown = (Event.current.type == EventType.MouseDown && Event.current.button == 1);
				eventData.mouseEvent.RMouseUp   = (Event.current.type == EventType.MouseUp && Event.current.button == 1);
                eventData.mouseEvent.LMouseDown = (Event.current.type == EventType.MouseDown && Event.current.button==0);
				eventData.mouseEvent.LMouseUp   = (Event.current.type == EventType.MouseUp && Event.current.button==0);
			} else {
				eventData.isDivMode = Event.current.shift;
			}

			eventData.cursorPosition = Event.current.mousePosition;

		}

		private void resetEventsSelections() {
			selectedCell.X = -1;
			selectedCell.Y = -1; // сбрасываем последнюю выбранную ячейку, чтобы в следующей итерации пересчитать предмет под курсором
			eventData.selected  = null; // сбрасываем перемещение предмета
			eventData.collision = null; // сбрасываем коллизию
			eventData.startSlot = null;
			eventData.endSlot   = null;
            eventData.eventType = InventoryEvent.None; // устанавливаем эвент по умолчанию
			selectedItem = null;
		}

		private void OnEvents() {

			ReadMouseEvents(); // читаем события мыши

			if (externalInventory != null) {

				if(eventData.mouseEvent.LMouseDown && selectedItem != null && eventData.endSlot != null) {

					int result = externalInventory.addItem(selectedItem.item);

						if (result == 0)
							eventData.endSlot.Items.Remove(selectedItem);
						else
							selectedItem.item.setCount(result);

				}

				if(CrossPlatformInputManager.GetButtonDown(SingletonNames.Input.ESC) || 
					CrossPlatformInputManager.GetButtonDown(SingletonNames.Input.INVENTORY) ||
					CrossPlatformInputManager.GetButtonDown(SingletonNames.Input.USE)) {
					hide();
				}

				return;
			}

			if (popupMenu.isVisible() || toolTip.isVisible()) {
				
				if(eventData.isPopupShow && eventData.mouseEvent.RMouseUp)
					eventData.isPopupShow = false;

				if (!eventData.isPopupShow && eventData.mouseEvent.RMouseDown) {
					toolTip.hide();
					popupMenu.hide();
				}

				if (eventData.mouseEvent.LMouseDown || CrossPlatformInputManager.GetButtonDown(SingletonNames.Input.ESC)) {

					if (popupMenu.isFocused())
						return;

					toolTip.hide();
					popupMenu.hide();

					resetEventsSelections();

                } else {
					return;
				}

			}

			if (eventData.mouseEvent.LMouseUp && eventData.eventType == InventoryEvent.ItemMove) {

				if (eventData.collision==null &&
					selectedCell.X+eventData.selected.item.getSize().getWidth()<= eventData.endSlot.position.CellsXCount+1 && 
					selectedCell.Y+eventData.selected.item.getSize().getHeight()<= eventData.endSlot.position.CellsYCount+1) { // двигаем только в том случае, если нет коллизий, и предмет находится в "сумке"

						if (eventData.isDivMode && eventData.selected.item.getCount()>1) { // можно разделить?

							if (selectedCell.X != eventData.selected.position.X ||
							   selectedCell.Y != eventData.selected.position.Y) { // смотрим, действительно ли предмет разделелили в разные ячейки?

								int count = eventData.selected.item.getCount() / 2;
								eventData.selected.item.decCount(count);

								Item newItem = eventData.selected.item.Clone();
								newItem.setCount(count);

								eventData.endSlot.Items.Add(new ItemSlot(newItem, new ItemPosition(selectedCell.X, selectedCell.Y)));

							}

						} else { // нужно перемещать

							if (InventoryAlgoritm.getInstance().slotContain(eventData.endSlot, eventData.selected)){ // предмет перемещается внутри одной сумки

								eventData.selected.getPosition().X = selectedCell.X; // перемещаем предмет
								eventData.selected.getPosition().Y = selectedCell.Y;

							} else { // предмет перемещается из сумки в сумку

								ItemSlot item = eventData.selected;
								eventData.startSlot.Items.Remove(item); // удаляем из старой сумки
								eventData.endSlot.Items.Add(item); // добавляем новую сумку
								item.getPosition().X = selectedCell.X; // перемещаем предмет
								item.getPosition().Y = selectedCell.Y;

							}

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

			if(eventData.mouseEvent.RMouseDown && selectedItem != null && eventData.eventType==InventoryEvent.None) { // если пользователь вызвал контекстное меню какого то предмета

				eventData.mouseEvent.RMouseDown = false;
                PopupMenuService.getInatance().SetupPopupMenu(popupMenu, selectedItem.item); // устанавливаем пункты меню
                popupMenu.show(eventData.cursorPosition); // отображаем меню в нужном месте

				Vector2 pos = new Vector2(eventData.cursorPosition.x, eventData.cursorPosition.y+ popupMenu.getHeight());
                toolTip.show(pos, ItemToolTipService.getInstance().createDescription(selectedItem.item), ItemToolTipService.getInstance().createInformationItems(selectedItem.item));

				eventData.isPopupShow = true;
			}

			if (eventData.mouseEvent.LMouseDown && selectedItem!=null && eventData.selected==null && eventData.eventType == InventoryEvent.None) {

				eventData.eventType = InventoryEvent.ItemMove; // меняем событие
				eventData.selected = selectedItem; // указываем предмет который перемносим
				eventData.startSlot = eventData.endSlot; // указываем слот предмета который переносим

			}

			if (eventData.selected!=null) { // если предмет переносится

				if (eventData.endSlot != null) { // если предмет перенесли в какую то сумку и он не вылазиет за рамки сумки

					eventData.collision = InventoryAlgoritm.getInstance().getCollisionItem(eventData.endSlot, eventData.selected, selectedCell.X, selectedCell.Y); // вычисляем коллизии внутри зоны перемещения

					if (eventData.collision==null) { // если нет коллизии

						drawService.DrawCells(eventData.endSlot, offsetX, offsetY, selectedCell, eventData.selected.item.getSize(), correctCellImage);

					} else {

						if (eventData.collision.item.Equals(eventData.selected.item) && !eventData.collision.item.isFullCount()) { // предметы однородные (можно объединить)

							drawService.DrawCells(eventData.endSlot, offsetX, offsetY, selectedCell, eventData.selected.item.getSize(), selectCellImage);

						} else {

							drawService.DrawCells(eventData.endSlot, offsetX, offsetY, selectedCell, eventData.selected.item.getSize(), errorCellImage); // подчёркиваем перемещаемую зону как ошибочную
							drawService.DrawCellsItem(eventData.endSlot, offsetX, offsetY, eventData.collision, errorCellImage); // подчёркиванием причину ошибки - коллизируемый итем

						}
					}

				}

				drawService.getItemDrawService().DrawItem(eventData.selected, eventData.cursorPosition.x, eventData.cursorPosition.y, false); // рисуем предмет возле курсора

			}

		}

		void OnGUI(){

			if(!visible)
				return;

			redraw();
			popupMenu.redraw();
			toolTip.redraw();
		}


	}

}
