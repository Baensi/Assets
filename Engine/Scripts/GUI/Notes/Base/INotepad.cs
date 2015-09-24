using System;
using System.Collections;
using System.Collections.Generic;

namespace Engine.Player.Notepad {

	public interface INotepad {
		Notes getNotes();
		void  setNotes(Notes notes);
	}

}