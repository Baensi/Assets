using System;
using System.Collections;
using System.Collections.Generic;

namespace Engine.Player.Notepad {
	
	public struct Notes {
		
		public List<INote> value;
		
		public Notes(List<INote> value) {
			this.value = value;
		}
		
	}

}