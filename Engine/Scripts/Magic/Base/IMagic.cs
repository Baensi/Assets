using System;

namespace Engine.Magic {

	/// <summary>
	/// Интерфес фрагмента магии
	/// </summary>
	public interface IMagic {

		/// <summary>
		/// Метод выполняющий действия магии
		/// </summary>
		void MagicRun();

		/// <summary>
		/// Метод выполняющи одну итерацию магии
		/// </summary>
		void MagicUpdate();

	}

}
