using System;

namespace EngineEditor.Terrain {

	/// <summary>
	/// Режимы редактирования
	/// </summary>
	public enum EEditorMode : int {
		ModeAdd    = 0x00, // режим добавления
		ModeDelete = 0x01, // режим удаления
		ModePick   = 0x02  // режим захвата
	};

}
