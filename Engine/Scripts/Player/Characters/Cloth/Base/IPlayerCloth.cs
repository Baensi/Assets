using System;
using System.Collections.Generic;
using UnityEngine;

namespace Engine.Player {

	/// <summary>
	/// Интерфейс одежды
	/// </summary>
	public interface IPlayerCloth {

		ClothRequire getRequire(); // требования к герою
		PlayerStates getStates();  // статистики от одежды

		SkinnedMeshRenderer getModel(); // модель одежды

	}

}
