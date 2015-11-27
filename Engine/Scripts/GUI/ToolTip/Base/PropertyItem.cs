using System;
using UnityEngine;

namespace Engine.EGUI.ToolTip {

	/// <summary>
	/// Элемент свойства, содержит заголовок и значение + цвет значения
	/// </summary>
	public class PropertyItem {

		public const float SIZE = 22f;

		private CanvasRenderer canvas;
		private string textTitle;
		private string textValue;
		private Color  colorValue;

			public PropertyItem(string textTitle, string textValue, Color colorValue) {
				this.textTitle = textTitle;
				this.textValue = textValue;
				this.colorValue = colorValue;
			}

		/// <summary>
		/// Устанавливает отрисовываемый элемент
		/// </summary>
		/// <param name="canvas"></param>
		public void setCanvas(CanvasRenderer canvas) {
			this.canvas = canvas;
		}

		/// <summary>
		/// Возвращает отрисовываемый элемент
		/// </summary>
		/// <returns></returns>
		public CanvasRenderer toCanvas() {
			return canvas;
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
