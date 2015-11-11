using System;
using UnityEngine;

namespace Engine.EGUI.ToolTip {

	public class PropertyItem {

		private string textTitle;
		private string textValue;
		private Color colorValue;

			public PropertyItem(string textTitle, string textValue, Color colorValue) {
				this.textTitle = textTitle;
				this.textValue = textValue;
				this.colorValue = colorValue;
			}

		public string getTextTitle() {
			return textTitle;
		}
		public string getTextValue() {
			return textValue;
		}

		public Color getColorValue() {
			return colorValue;
		}

	}

}
