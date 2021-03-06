﻿using UnityEngine;
using System.Collections;

namespace Engine.Objects.Types {

	/// <summary>
	/// Интерфейс модификатор. Предмет можно сломать
	/// </summary>
	public interface IDestroyedType {

		void addDamage(float damageValue); // предмет получил урон damageValue
		void onDestroy(); // предмет разрушен

	}

}