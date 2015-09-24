using UnityEngine;
using System.Collections;

namespace Engine.Objects.Doors {

	/// <summary>
	/// Door state - состояние двери
	/// opened - дверь открыта
	/// closed - дверь прикрыта, но её можно открыть
	/// locked - дверь закрыта на ключ
	/// </summary>
	public enum DoorState : int {
		Opened = 0,
		Closed = 1,
		Locked = 2
	};
}
