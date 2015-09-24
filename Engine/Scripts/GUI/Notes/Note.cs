using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace Engine.Player.Notepad {

	class Note : MonoBehaviour, INote {

		[SerializeField] public string   captionId;
		[SerializeField] public string   textId;
		[SerializeField] public NoteType type;
		[SerializeField] public bool	 active;
		
		private Texture2D noteIcon;

		public Note(NoteType type, string captionId, string textId) : this(type, captionId, textId, true){}
		public Note(NoteType type, string captionId, string textId, bool active) {
			this.type = type;
			this.captionId = captionId;
			this.textId = textId;
			this.active = active;
		}

		public Texture2D getNoteIcon(){
			return noteIcon;
		}
		
		public string getCaptionId() {
			return captionId;
		}

		public string getTextId() {
			return textId;
		}

		public NoteType getType() {
			return type;
		}

		public bool isActive() {
			return active;
		}

	}

}
