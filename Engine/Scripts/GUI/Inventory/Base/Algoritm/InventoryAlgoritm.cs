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
		/// ���������� ������� � ������� ���������� item
		/// </summary>
		/// <param name="slot">����� � ������� ����������� ��������</param>
		/// <param name="posX">��������� ���������� �������� �� x</param>
		/// <param name="posY">��������� ���������� �������� �� y</param>
		/// <returns>���� ������������ ���, ���������� null</returns>
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
					)) // ��������� ��������

					return item;

			}

			return null; // �������� �� ����������
		}

		/// <summary>
		/// ���� ����� � ��������� ������� ��� ��������
		/// </summary>
		/// <param name="posX"></param>
		/// <param name="posY"></param>
		/// <returns>���� ����� �� ������� ����� null</returns>
		public RectangleSlot getSlot(float mouseX, float mouseY) {

			if (slots==null) return null;

			foreach (RectangleSlot slot in slots) {
				if (slot.position.OffsetX<=mouseX && slot.position.OffsetX+slot.position.SlotWidth>=mouseX
				 && slot.position.OffsetY<=mouseY && slot.position.OffsetY+slot.position.SlotHeight>=mouseY)
					return slot; // ���������� ����� ��� ��������
			}

			return null; // ����� � ��������� ������� �� �������
		}

		/// <summary>
		/// �������� �� ���� slot ������� item
		/// </summary>
		/// <param name="slot">����, ������� �����������</param>
		/// <param name="item">������� ������� ������ � ���� �����</param>
		/// <returns>���������� ���������� �������� ����������</returns>
		public bool slotContain(RectangleSlot slot, ItemSlot item) {
			return slot.Items.Contains(item);
		}

		/// <summary>
		/// ���������� �������, ������� �������� ������ {cellX,cellY}
		/// </summary>
		/// <param name="cellX"></param>
		/// <param name="cellY"></param>
		/// <returns>���� ������� �� ������ ����� null</returns>
		public ItemSlot getItem(RectangleSlot slot, int cellX, int cellY) {

			if (slots==null) return null;

			foreach (ItemSlot item in slot.Items) {

				if ((item.getPosition().X == cellX && item.getPosition().Y == cellY)) // ������� ����� � ���� ������
					return item;

				if (cellX>=item.getPosition().X
					&& cellX<item.getPosition().X+item.item.getSize().getWidth()
					&& cellY>=item.getPosition().Y
					&& cellY<item.getPosition().Y+item.item.getSize().getHeight()) // ������� �������� ��� ������
					return item;

			}

			return null; // �������� � ������ ���
		}


		/// <summary>
		/// �������� �������� ������� � ���������
		/// </summary>
		/// <param name="item">����������� �������</param>
		/// <returns>���������� ����� �� ������������ �����������, ���� ������� �� ��� ��������� ��������. 0 - ���� ������� ������� ��������</returns>
		public int addItem(Item item){

			foreach (RectangleSlot slot in slots) { // ���������� ��� �����

				foreach (ItemSlot i in slot.Items) { // �������� ����� ���������� �������� � ����������� ���������� ��
					if (i.Equals(item) && !i.item.isFullCount()) { // ���� ������� ���������, � �� �� ��������� �������������

						if (item.getCount()!=0) // ���� � ��� ���� ������� �����������, ������� ���� ���������
							item.setCount(i.item.incCount(item.getCount()));

						if (item.getCount()==0) // ���������� ���������, ����� ��������
							return 0;

					}
				}

				for(int y=1;y<=slot.position.CellsYCount;y++){ // �������� ����� ��������� �������, � �������� ������� ������� (�� ��������)
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

						if(result){ // �������� ����� ��������� ��������
							slot.Items.Add(new ItemSlot(item, new ItemPosition(x, y)));
							return 0;
						}

					}

				}

			}

			return item.getCount(); // ���������� ����� ����������� (�� �������) ���������

		}

		/// <summary>
		/// ������� ������� �� ���������
		/// </summary>
		/// <param name="item">������� ������� ���� ������� �� ���������</param>
		/// <param name="equals">���� true - ����������� ��������������� (item1.Equals(item2)), ���� false, ����������� ����� (item1==item2)</param>
		/// <param name="full">���� true - ������� ������, ���������� �� ����������</param>
		/// <param name="count">����� ��������� ��������� (��������� ��� full=true)</param>
		/// <returns>��������� ����� ��������</returns>
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
									changeCountList.Add(new DropItemData(slot,i, dropCount-dropResult)); // ��������� ������� ���������
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
				if (dropCount>0) { // ���� �� ��� �������� ������� �������� (�� �������� ���)

					foreach (DropItemData droppedItem in changeCountList)
						droppedItem.item.item.incCount(droppedItem.changeValue); // ���������� �� ����

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

