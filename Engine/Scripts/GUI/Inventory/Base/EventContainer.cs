using System.Collections.Generic;
using UnityStandardAssets.CrossPlatformInput;
using UnityEngine;
using Engine.I18N;
using Engine.Objects;

namespace Engine.EGUI.Inventory {

	public class EventContainer {

		public Vector2 cursorPosition;
		public MouseEvent mouseEvent = new MouseEvent();
		public InventarEvent eventType = InventarEvent.None;

		public bool isDivMode = false; // Зажат шифт и предметы надо делить

		public ItemSlot selected;
		public ItemSlot collision;

	}

}
