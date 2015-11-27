using Engine.Player;

namespace Engine.Objects.Types {

	/// <summary>
	/// Интерфейс модификатор. Предмет даёт какие то статы
	/// </summary>
	public interface IChangedStatesType {

		PlayerStates getStates();

	}

}
