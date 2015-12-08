using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Engine.EGUI.Utils;

namespace Engine.EGUI.ToolTip {

	public class ToolTipBase : MonoBehaviour {

		private float width   = 205f;

		[SerializeField] public GUIStyle style;
		[SerializeField] public GUIStyle itemTitleStyle;
		[SerializeField] public GUIStyle itemValueStyle;
		
		[SerializeField] public bool     visible = false;

		private GUIStyle currentStyle;
		private GUIStyle currentItemTitleStyle;
		private GUIStyle currentItemValueStyle;

		private Rect               bounds;
		private List<PropertyItem> items = new List<PropertyItem>();

		[SerializeField] public float sizeStep = 0.01f;
		private float size = 0f;

		private string title;

			void Start() {
				currentStyle          = new GUIStyle(style);
				currentItemTitleStyle = new GUIStyle(itemTitleStyle);
				currentItemValueStyle = new GUIStyle(itemValueStyle);
            }

		void OnValidate() {
			currentStyle = new GUIStyle(style);
			currentItemTitleStyle = new GUIStyle(itemTitleStyle);
			currentItemValueStyle = new GUIStyle(itemValueStyle);
		}

		/// <summary>
		/// Активность/видимость всплывающей подсказки
		/// </summary>
		/// <returns></returns>
		public bool isVisible() {
			return visible;
        }

		/// <summary>
		/// Возвращает рамку-размер, в которой находится подсказка
		/// </summary>
		/// <returns></returns>
		public Rect getBounds() {
			return bounds;
		}

		public float getWidth() {
			return bounds.width;
		}

		public float getHeight() {
			return bounds.height;
		}

		/// <summary>
		/// Вызывает всплывающую подсказку в указанном месте
		/// </summary>
		/// <param name="position">позиция подсказки</param>
		/// <param name="description">описание, посещается в заголовок</param>
		/// <param name="items">свойства, помещаются снизу</param>
		public void show(Vector2 position, string description, List<PropertyItem> items) {
			size = 0f;
			this.items = items;
			title = description;
            CreateCanvasStruct(position);
			visible = true;
        }

		/// <summary>Скрывает подсказку</summary>
		public void hide() {
			if (!visible)
				return;
			visible = false;
			clear();
		}

		/// <summary>очищает свойства</summary>
		private void clear() {
			items.Clear();
			items = null;
			title = null;
        }

		/// <summary>Пересоздаёт структуру надписей внутри контекста</summary>
		private void CreateCanvasStruct(Vector2 position) {
			bounds.x = position.x;
			bounds.y = position.y;
			bounds.width  = width;
			bounds.height = (PropertyItem.SIZE+6) * items.Count + 64f;
		}

		/// <summary>
		/// С каждой итерацией приближает угол поворота к 0,0,0 градусам
		/// </summary>
		/// <param name="rotation">Текущий угол</param>
		/// <returns>Возвращает следующее промежуточное состояние вращения</returns>
		private Quaternion onRotateIteration(Quaternion rotation) {
			if (rotation.eulerAngles.x > 0.01f || rotation.eulerAngles.y > 0.01f || rotation.eulerAngles.z > 0.01f ||
				rotation.eulerAngles.x < -0.01f || rotation.eulerAngles.y < -0.01f || rotation.eulerAngles.z < -0.01f)
				return Quaternion.Euler(rotation.eulerAngles * 0.98f);
			else
				return Quaternion.Euler(Vector3.zero);
		}

		public void redraw() {

			if (!visible)
				return;

			if (size < 1f) {
				size += sizeStep;

				GUI.color = new Color(1, 1, 1, size);

				if (size > 1f)
					size = 1f;
			}

			currentStyle.fontSize = (int)(style.fontSize * size);
			if(currentStyle.fontSize==0)
				currentStyle.fontSize=1;
			currentStyle.normal.textColor = style.normal.textColor * size;

			GUI.Box(bounds, title, currentStyle);

			float x = bounds.x;
			float y = bounds.y+64f;

			foreach (PropertyItem item in items) {
				
				drawItem(item, x, y);
				y += PropertyItem.SIZE + 6;
            }
			
		}

		private void drawItem(PropertyItem item, float offsetX, float offsetY) {

			Rect rectTitle = new Rect(offsetX + 5, offsetY, bounds.width * 0.5f, PropertyItem.SIZE);
			Rect rectValue = new Rect(offsetX - 10 + bounds.width * 0.5f, offsetY, bounds.width * 0.5f, PropertyItem.SIZE);

			currentItemValueStyle.fontSize = (int)(itemValueStyle.fontSize* size);
			if(currentItemValueStyle.fontSize==0)
				currentItemValueStyle.fontSize=1;
			currentItemValueStyle.normal.textColor = item.getColorValue() * size;
			currentItemTitleStyle.fontSize = (int)(itemTitleStyle.fontSize * size);
			if(currentItemTitleStyle.fontSize==0)
				currentItemTitleStyle.fontSize=1;
			GUI.Box(rectTitle, item.getTextTitle()+":", currentItemTitleStyle);
			GUI.Box(rectValue, item.getTextValue(), currentItemValueStyle);

		}

		void OnGUI() {
			redraw();
		}

	}

}
