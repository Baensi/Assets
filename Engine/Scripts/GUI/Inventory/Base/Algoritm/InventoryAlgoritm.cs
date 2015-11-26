using System;
using System.Collections.Generic;
using UnityEngine;

namespace Engine.EGUI.Inventory {

	public class InventoryAlgoritm {

		private List<RectangleSlot> slots;

		public List<RectangleSlot> getSlots() {
			return slots;
		}

		public void setSlots(List<RectangleSlot> slots) {
			this.slots = slots;
		}

		public float getInventoryWidth() {

			if (slots==null) return 0f;

			float maxWidth = 0;
			float tmp;

				foreach (RectangleSlot slot in slots) {

					tmp = slot.position.OffsetX+slot.position.SlotWidth;

					if (tmp>maxWidth)
						maxWidth=tmp;
				}

			return maxWidth;
		}

		public float getInventoryHeight() {

			if (slots==null) return 0f;

			float maxHeight = 0;
			float tmp;

			foreach (RectangleSlot slot in slots) {

				tmp = slot.position.OffsetY+slot.position.SlotWidth;

				if (tmp>maxHeight)
					maxHeight=tmp;
			}

			return maxHeight;
		}

		/// <summary>
		/// Возвращает предмет с которым столкнулся item
		/// </summary>
		/// <param name="slot">сумка в которой проверяются коллизии</param>
		/// <param name="posX">положение выбранного предмета по x</param>
		/// <param name="posY">положение выбранного предмета по y</param>
		/// <returns>если столкновения нет, возвращает null</returns>
		public ItemSlot getCollisionItem(RectangleSlot slot, ItemSlot target, int posX, int posY) {

			foreach (ItemSlot item in slot.Items) {

				if (item!=target &&

					(item.position.X<=posX &&
					item.position.Y<=posY &&
					item.position.X+item.item.getSize().getWidth()>=posX+target.item.getSize().getWidth() &&
					item.position.Y+item.item.getSize().getHeight()>=posY+target.item.getSize().getHeight()
					||
					item.position.X>=posX &&
					item.position.Y>=posY &&
					item.position.X+item.item.getSize().getWidth()<=posX+target.item.getSize().getWidth() &&
					item.position.Y+item.item.getSize().getHeight()<=posY+target.item.getSize().getHeight()
					)) // проверяем коллизию

					return item;

			}

			return null; // коллизий не обнаружено
		}

		/// <summary>
		/// Ищет сумку в указанной области под курсором
		/// </summary>
		/// <param name="posX"></param>
		/// <param name="posY"></param>
		/// <returns>Если сумка не найдена вернёт null</returns>
		public RectangleSlot getSlot(float mouseX, float mouseY) {

			if (slots==null) return null;

			foreach (RectangleSlot slot in slots) {
				if (slot.position.OffsetX<=mouseX && slot.position.OffsetX+slot.position.SlotWidth>=mouseX
				 && slot.position.OffsetY<=mouseY && slot.position.OffsetY+slot.position.SlotHeight>=mouseY)
					return slot; // возвращаем сумку под курсором
			}

			return null; // сумок в указанной области не найдено
		}

		/// <summary>
		/// Содержит ли слот slot предмет item
		/// </summary>
		/// <param name="slot">Слот, который проверяется</param>
		/// <param name="item">Предмет который ищется в этом слоте</param>
		/// <returns>Возвращает логическое значение результата</returns>
		public bool slotContain(RectangleSlot slot, ItemSlot item) {
			return slot.Items.Contains(item);
		}

		/// <summary>
		/// Возвращает предмет, который занимает ячейку {cellX,cellY}
		/// </summary>
		/// <param name="cellX"></param>
		/// <param name="cellY"></param>
		/// <returns>Если предмет не найден вернёт null</returns>
		public ItemSlot getItem(RectangleSlot slot, int cellX, int cellY) {

			if (slots==null) return null;

			foreach (ItemSlot item in slot.Items) {

				if ((item.getPosition().X == cellX && item.getPosition().Y == cellY)) // предмет лежит в этой ячейке
					return item;

				if (cellX>=item.getPosition().X
					&& cellX<item.getPosition().X+item.item.getSize().getWidth()
					&& cellY>=item.getPosition().Y
					&& cellY<item.getPosition().Y+item.item.getSize().getHeight()) // предмет занимает эту ячейку
					return item;

			}

			return null; // предмета в ячейке нет
		}


		/// <summary>
		/// Пытается добавить предмет в инвентарь
		/// </summary>
		/// <param name="item">Добавляемый предмет</param>
		/// <returns>Возвращает число НЕ вместившихся экземпляров, если предмет не был полностью добавлен. 0 - если предмет успешно добавлен</returns>
		public int addItem(Item item){

			foreach (RectangleSlot slot in slots) { // перебираем все сумки

				foreach (ItemSlot i in slot.Items) { // пытаемся найти идентичные предметы и попробовать объединить их
					if (i.Equals(item) && !i.item.isFullCount()) { // если предмет идентичен, и он не полностью укомплектован

						if (item.getCount()!=0) // пока у нас есть остаток экземпляров, которые надо раскидать
							item.setCount(i.item.incCount(item.getCount()));

						if (item.getCount()==0) // экземпляры кончились, можно выходить
							return 0;

					}
				}

				for(int y=1;y<=slot.position.CellsYCount;y++){ // пытаемся найти свободную область, и засунуть предмет целиком (не разделяя)
					for(int x=1;x<=slot.position.CellsXCount;x++){

						bool result = true;
						foreach(ItemSlot i in slot.Items){
							
							if(x>=i.getPosition().X &&
							   x<i.getPosition().X+i.item.getSize().getWidth() &&
							   y>=i.getPosition().Y &&
							   y<i.getPosition().Y+i.item.getSize().getHeight()){
								result=false;
								break;
							}
								
						}

						if(result){ // предметы можно полностью добавить
							slot.Items.Add(new ItemSlot(item, new ItemPosition(x, y)));
							return 0;
						}

					}

				}

			}

			return item.getCount(); // возвращаем число отсавшихтся (не влезших) предметов

		}

		/// <summary>
		/// Удаляет предмет из инвентаря
		/// </summary>
		/// <param name="item">предмет который надо удалить из инвентаря</param>
		/// <param name="equals">если true - проверяется эквивалентность (item1.Equals(item2)), если false, проверяется адрес (item1==item2)</param>
		/// <param name="full">если true - удаляет предме, независимо от количества</param>
		/// <param name="count">число удаляемых предметов (действует при full=true)</param>
		/// <returns>Возращает успех операции</returns>
		public bool removeItem(Item item, bool equals=true, bool full = true, int count = 1){

			bool flagResult = false;

			int dropCount = count;
			List<DropItemData> changeCountList = new List<DropItemData>();

			ItemSlot       removedItem=null;
			RectangleSlot  removedSlot=null;

				foreach (RectangleSlot slot in slots) {

					foreach(ItemSlot i in slot.Items){

						if ((equals && i.item.Equals(item)) || (!equals && i.item==item)) {

							if(full) {

								removedItem=i;
								removedSlot = slot;

								flagResult = true;
								break;
							} else {

								if (dropCount>0) {

									int dropResult = i.item.decCount(dropCount);
									changeCountList.Add(new DropItemData(slot,i, dropCount-dropResult)); // сохраняем историю изменений
									dropCount = dropResult;

								} else {

									flagResult = true;
									break;
								}

							}

						}

					}

					if (flagResult)
						break;

				}

			if (!full) {
				if (dropCount>0) { // если не все предметы удалось выкинуть (их попросту нет)

					foreach (DropItemData droppedItem in changeCountList)
						droppedItem.item.item.incCount(droppedItem.changeValue); // возвращаем всё взад

				} else {

					foreach (DropItemData droppedItem in changeCountList) {

						if (droppedItem.item.item.getCount()==0)
							droppedItem.slot.Items.Remove(droppedItem.item);

						droppedItem.Clear();
					}

					changeCountList.Clear();

				}
			} else {

				if (removedItem!=null)
					removedSlot.Items.Remove(removedItem);

			}

			changeCountList = null;

			return flagResult;
		}


	}

}

