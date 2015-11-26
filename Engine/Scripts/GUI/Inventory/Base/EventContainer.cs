using System.Collections.Generic;
using UnityStandardAssets.CrossPlatformInput;
using UnityEngine;
using Engine.I18N;
using Engine.Objects;

namespace Engine.EGUI.Inventory {

	/// <summary>
	/// Контейнер метода событий в инвентаре
	/// </summary>
	public class EventContainer {

		public Vector2        cursorPosition;
		public MouseEvent     mouseEvent = new MouseEvent();
		public InventoryEvent eventType  = InventoryEvent.None;

		public bool isDivMode = false; // Зажат шифт и предметы надо делить

		public RectangleSlot endSlot; // текущий слот
		public RectangleSlot startSlot; // слот в котором выбрали selected предмет

		public ItemSlot selected;  // выбранный предмет, который тащут
		public ItemSlot collision; // предмет, с которым пытаются объединить выбранный предмет

	}

}
