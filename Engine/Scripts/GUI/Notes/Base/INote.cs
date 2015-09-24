using System;
using UnityEngine;

namespace Engine.Player.Notepad {

	public interface INote {

		Texture2D getNoteIcon();
		string    getCaptionId();
		string    getTextId();
		NoteType  getType();

		bool	 isActive();

	}

}