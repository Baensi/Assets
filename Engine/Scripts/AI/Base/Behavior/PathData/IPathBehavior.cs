using System;
using System.Collections.Generic;
using UnityEngine;

namespace Engine.AI.Behavior {

	public interface IPathBehavior {

		/// <summary>
		/// Возвращает точки патрулирования
		/// </summary>
		/// <returns></returns>
		AIPatrol getPatrol();

	}

}
