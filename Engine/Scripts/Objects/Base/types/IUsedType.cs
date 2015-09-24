using UnityEngine;
using System.Collections;

namespace Engine.Objects.Types {

	/// <summary>
	/// Предмет можно использовать
	/// </summary>
	public interface IUsedType {

		bool onUse(); // предмет использовали, возвращает true, если предмет был удалён

	}

}