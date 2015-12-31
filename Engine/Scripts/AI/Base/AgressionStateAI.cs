using System;

namespace Engine.AI {

	/// <summary>
	/// Текущее состояние AI
	/// Normal - обычный режим,
	/// Warning - режим повышенного внимания, враг близко
	/// Enemy - AI охотится на врага
	/// </summary>
	public enum AgressionStateAI : int {

		Normal  = 0x00,
		Warning = 0x01,
		Enemy   = 0x02
		
	};

}
