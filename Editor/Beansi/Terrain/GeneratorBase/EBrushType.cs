using System;

namespace EngineEditor.Terrain {

	/// <summary>
	/// Типы кисточек для генератора
	/// </summary>
	public enum EBrushType : int {
		BrushCircle = 0x00, // окружность
		BrushBox    = 0x01,  // квадрат
		BrushCone   = 0x02
	};

}
