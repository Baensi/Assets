using UnityEngine;
using System.Collections;

namespace Engine.Objects.Types {

	/// <summary>
	/// Предмет можно взять в инвентарь
	/// </summary>
	public interface IPickedType {

		void onPick(); // предмет взяли в инвентарь

	}

}