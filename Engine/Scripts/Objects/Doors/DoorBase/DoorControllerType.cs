using System;
using UnityEngine;

namespace Engine.Objects.Doors {

	/// <summary>
	/// Тип управления дверью
	/// </summary>
	public enum DoorControllerType : int {
		Standalone = 0x00, // дверь управляется напрямую
		Remote     = 0x01  // дверь управляется удалённо, через рычаги или кнопки
	};

}
