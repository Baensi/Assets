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
		/// Ищет слот в указанной области под курсором
		/// </summary>
		/// <param name="posX"></param>
		/// <param name="posY"></param>
		/// <returns>Если слот не найден вернёт null</returns>
		public RectangleSlot getSlot(float mouseX, float mouseY) {

			if (slots==null) return null;

			foreach (RectangleSlot slot in slots) {
				if (slot.position.OffsetX<=mouseX && slot.position.OffsetX+slot.position.SlotWidth>=mouseX
				 && slot.position.OffsetY<=mouseY && slot.position.OffsetY+slot.position.SlotHeight>=mouseY)
					return slot; // возвращаем слот под курсором
			}

			return null; // слотов в указанной области не найдено
		}

		/// <summary>
		/// Возвращает предмет, который занимает ячейку {cellX,cellY}
		/// </summary>
		/// <param name="cellX"></param>
		/// <param name="cellY"></param>
		/// <returns>Если предмет не найден вернёт null</returns>
		public IItem getItem(RectangleSlot slot, int cellX, int cellY) {

			if (slots==null) return null;

			foreach (IItem item in slot.Items) {

				if ((item.getPosition().X == cellX && item.getPosition().Y == cellY)) // предмет лежит в этой ячейке
					return item;

				if (cellX>=item.getPosition().X
					&& cellX<=item.getPosition().X+item.getSize().getWidth()
					&& cellY>=item.getPosition().Y
					&& cellY<=item.getPosition().Y+item.getSize().getHeight()) // предмет занимает эту ячейку
					return item;

			}

			return null; // предмета в ячейке нет
		}

		public bool addItem(IItem item){

			foreach (RectangleSlot slot in slots) {
				foreach(IItem i in slot.Items){
					if(i.Equals(item) && i.getCount()<i.getMaxCount()){
						i.incCount();
						return true;
					}
				}
			}

			foreach (RectangleSlot slot in slots) {

				for(int y=1;y<=slot.position.CellsYCount;y++){
					for(int x=1;x<=slot.position.CellsXCount;x++){

						bool result = true;
						foreach(IItem i in slot.Items){
							
							if(x>=i.getPosition().X &&
							   x<i.getPosition().X+i.getSize().getWidth() &&
							   y>=i.getPosition().Y &&
							   y<i.getPosition().Y+i.getSize().getHeight()){
								result=false;
								break;
							}
								
						}

						if(result){
							item.setPosition(new ItemPosition(x,y));
							slot.Items.Add(item);
							return true;
						}

					}

				}

			}

			return false;

		}

		public bool removeItem(IItem item){

			List<IItem> removeList = new List<IItem>();

				foreach (RectangleSlot slot in slots) {
					foreach(IItem i in slot.Items)

						if(i.Equals(item)){

							if(i.getCount()>1)
								i.decCount();
							else
								removeList.Add(item);

							return true;
						}

					foreach (IItem i in removeList)
						slot.Items.Remove(i);

					removeList.Clear();
				}

			removeList = null;

			return false;
		}


	}

}

