using UnityEngine;
using UnityEngine.UI;

namespace Engine.EGUI.Utils {

	public static class TransformRectUtils {

		/// <summary>
		/// Устанавливает значения рамки RectTransform, которая растянута по ширине
		/// </summary>
		/// <param name="leftRight">отступы слева и справа (при leftRight=5 -> слева=5, справа=5)</param>
		/// <param name="y">позиция по y</param>
		/// <param name="height">высота</param>
		/// <param name="offsetX">смещение по x от центра родителя (по умолчанию 0)</param>
		public static void setHorizontalAncorBounds(this RectTransform labelRect, float leftRight, float y, float height, float offsetX = 0f) {
			labelRect.sizeDelta = new Vector2(-leftRight*2, height);
			labelRect.position = new Vector3(offsetX,-y, 0f);
		}

	}

}
