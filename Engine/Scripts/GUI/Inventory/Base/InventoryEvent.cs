using UnityStandardAssets.CrossPlatformInput;
using Engine.I18N;
using Engine.Objects;

namespace Engine.EGUI.Inventory {

	/// <summary>
	/// Тип глобального события в инвентаре
	/// <see cref="None"/> (0) - ничего не происходит
	/// <see cref="ItemMove"/> (1) - в данный момент, происходит перемещение предмета
	/// </summary>
	public enum InventoryEvent : int {

		None       = 0x00,
		ItemMove   = 0x01

	};

}
