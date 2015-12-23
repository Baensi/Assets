using UnityEngine;
using System.Collections.Generic;

namespace Engine.EGUI.Inventory {

	public class ExternalInventoryAlgorithm {

		public static ExternalInventoryAlgorithm instance;

		public static ExternalInventoryAlgorithm getInstance() {
			if(instance==null)
				instance = new ExternalInventoryAlgorithm();
			return instance;
		}

		/// <summary>
		/// Пытается добавить предмет item в слот slotData
		/// </summary>
		/// <param name="slotData">Сумка, в которую пытаются добавить предмет</param>
		/// <param name="item">Добавляемый предмет</param>
		/// <returns>Возвращает число экземпляров item НЕ ДОБАВЛЕННЫХ в слот</returns>
		public int AddItem(SlotData slotData, Item item) {

			foreach (ItemSlot i in slotData.Items) { // пытаемся найти идентичные предметы и попробовать объединить их
				if (i.item.Equals(item) && !i.item.isFullCount()) { // если предмет идентичен, и он не полностью укомплектован

					if (item.getCount() != 0) // пока у нас есть остаток экземпляров, которые надо раскидать
						item.setCount(i.item.incCount(item.getCount()));

					if (item.getCount() == 0) // экземпляры кончились, можно выходить
						return 0;

				}
			}

			for (int y = 1; y <= slotData.position.CellsYCount; y++) { // пытаемся найти свободную область, и засунуть предмет целиком (не разделяя)
				for (int x = 1; x <= slotData.position.CellsXCount; x++) {

					bool result = true;
					foreach (ItemSlot i in slotData.Items) {

						if (x >= i.getPosition().X &&
						   x < i.getPosition().X + i.item.getSize().getWidth() &&
						   y >= i.getPosition().Y &&
						   y < i.getPosition().Y + i.item.getSize().getHeight()) {
							result = false;
							break;
						}

					}

					if (result) { // предметы можно полностью добавить
						slotData.Items.Add(new ItemSlot(item.Clone(), new ItemPosition(x, y)));
						return 0;
					}

				}

			}

			return item.getCount();
		}

		/// <summary>
		/// Возвращает предмет, который занимает ячейку {cellX,cellY}
		/// </summary>
		/// <param name="cellX"></param>
		/// <param name="cellY"></param>
		/// <returns>Если предмет не найден вернёт null</returns>
		public ItemSlot getItem(SlotData slot, int cellX, int cellY) {

			foreach (ItemSlot item in slot.Items) {

				if ((item.getPosition().X == cellX && item.getPosition().Y == cellY)) // предмет лежит в этой ячейке
					return item;

				if (cellX >= item.getPosition().X
					&& cellX < item.getPosition().X + item.item.getSize().getWidth()
					&& cellY >= item.getPosition().Y
					&& cellY < item.getPosition().Y + item.item.getSize().getHeight()) // предмет занимает эту ячейку
					return item;

			}

			return null;

		}

		/// <summary>
		/// Пытается удалить предмет item из сумки slotData
		/// </summary>
		/// <param name="slotData">Сумка, из которой удаляют предмет</param>
		/// <param name="item">Предмет который удаляют из сумки</param>
		/// <returns>Возвращает логическое значение совершения операции удаления</returns>
		public bool RemoveItem(SlotData slotData, ItemSlot item) {
			return slotData.Items.Remove(item);
		}

		/// <summary>
		/// Пытается выполнить сортировку предметов в сумке
		/// </summary>
		/// <param name="slotData">Сумка, в которой выполняется сортировка</param>
		/// <returns>Возвращает логический результат операции сортировки</returns>
		public bool SortData(SlotData slotData) {
			List<ItemBounds> bounds    = new List<ItemBounds>();

			foreach (ItemSlot item in slotData.Items)
				bounds.Add(new ItemBounds(item)); // формируем все рамки

			if (sortByLeftTop(slotData, bounds)) // сортируем по левому краю сверху
				return true;

			if (sortByTopLeft(slotData, bounds)) // сортируем по верху слева
				return true;

			return false;
		}


		private bool sortByTopLeft(SlotData slotData, List<ItemBounds> bounds) {

			foreach (ItemBounds bound in bounds)
				bound.Reset();

			ItemBounds i = null;

			while ((i = findMaxSizeY(bounds)) != null) {

				for (int x = 1; x <= slotData.position.CellsXCount; x++) {
					for (int y = 1; y <= slotData.position.CellsYCount; y++) {

						i.X = x;
						i.Y = y;

						if (!getCollision(bounds, i)) // предмет нашёл пустое место
							i.setFix(true);

						if (i.isFix())
							break;

					}

					if (i.isFix())
						break;
				}


				if (!i.isFix()) { // сортировка не удалась
					return false;
				}

			}

			slotData.Items.Clear();

			foreach (ItemBounds bound in bounds)
				slotData.Items.Add(bound.createItemSlot()); // перемещаем предметы

			return true;
		}

		private bool sortByLeftTop(SlotData slotData, List<ItemBounds> bounds) {

			foreach (ItemBounds bound in bounds)
				bound.Reset();

			ItemBounds i = null;

			while ((i = findMaxSizeY(bounds)) != null) {

				for (int y = 1; y <= slotData.position.CellsYCount; y++) {
					for (int x = 1; x <= slotData.position.CellsXCount; x++) {

						i.X = x;
						i.Y = y;

						if (!getCollision(bounds, i)) // предмет нашёл пустое место
							i.setFix(true);

						if (i.isFix())
							break;

					}

					if (i.isFix())
						break;
				}


				if (!i.isFix()) { // сортировка не удалась
					return false;
				}

			}

			slotData.Items.Clear();

			foreach (ItemBounds bound in bounds)
				slotData.Items.Add(bound.createItemSlot()); // перемещаем предметы

			return true;
		}

		private bool getCollision(List<ItemBounds> bounds, ItemBounds bound) {
			foreach (ItemBounds item in bounds) {

				if(item != bound &&
				   (item.X <= bound.X &&
					item.Y <= bound.Y &&
					item.X + item.W >= bound.X + bound.W &&
					item.Y + item.H >= bound.Y + bound.H
						||
					item.X >= bound.X &&
					item.Y >= bound.Y &&
					item.X + item.W <= bound.X + bound.W &&
					item.Y + item.H <= bound.Y + bound.H
					))
					return true;
			}

			return false;
		}

		/// <summary>Ищет бОльший предмет по высоте из списка</summary>
		private ItemBounds findMaxSizeY(List<ItemBounds> bounds) {
			if (bounds == null || bounds.Count == 0)
				return null;

			if (bounds.Count == 1)
				return bounds[0].isFix() ? null : bounds[0];

			ItemBounds link = findFirstNotFix(bounds);

			if (link == null)
				return null;

			foreach (ItemBounds bound in bounds)
				if (!bound.isFix() && bound.H > link.H)
					link = bound;

			return link;
		}

		private ItemBounds findFirstNotFix(List<ItemBounds> bounds) {
			foreach (ItemBounds bound in bounds)
				if (!bound.isFix())
					return bound;
			return null;
		}

		/// <summary>
		/// Пытается найти предмет в сумке
		/// </summary>
		/// <param name="slotData">Сумка, в которой ведётся поиск</param>
		/// <param name="item">Искомый предмет</param>
		/// <returns>Возвращает контейнер предмета + позиция этого предмета в сумке</returns>
		public ItemSlot FindItemSlot(SlotData slotData, Item item) {
			if(slotData==null)
				return null;

			foreach(ItemSlot itemSlot in slotData.Items)
				if(itemSlot.item == item)
					return itemSlot;
				
			return null;
		}

	}

}
