using UnityEngine;
using Engine.AI.Behavior;

namespace Engine.AI {

	/// <summary>
	/// Интерфейс персонажа, подходит под поведение как игрового, так и неигрового персонажа
	/// </summary>
	public interface AIC : IStateAI, IModelBehaviorAI {

	}

}
