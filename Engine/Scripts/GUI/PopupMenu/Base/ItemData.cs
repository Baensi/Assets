using System;
using UnityEngine;

namespace Engine.EGUI.PopupMenu {

	[Serializable]
	public struct ItemData {

		[SerializeField] public Color    normalColor;
		[SerializeField] public Color    selectedColor;

        [SerializeField] public GUIStyle labelStyle;
		
		[SerializeField] public Vector2  itemSize;

		public ItemData Create(GUIStyle labelStyle,
							   Vector2 itemSize,
							   Color normalColor,
							   Color selectedColor) {

			this.normalColor = normalColor;
			this.selectedColor = selectedColor;
            this.labelStyle = labelStyle;
			this.itemSize = itemSize;

			return this;
		}

	}

}
