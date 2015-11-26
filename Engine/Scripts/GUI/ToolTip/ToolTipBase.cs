using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Engine.I18N;
using Engine.EGUI.Utils;

namespace Engine.EGUI.ToolTip {

	public class ToolTipBase : MonoBehaviour {

		private static string PANEL_NAME = "Panel";
		[SerializeField] public string titleName = "Title";
		[SerializeField] public string valueName = "Value";

			/// <summary>Префаб элемента-свойства</summary>
		[SerializeField] public CanvasRenderer propertyItemInstance;

			/// <summary>Префаб панели подсказки</summary>
		[SerializeField] public CanvasRenderer panelInstance;
		
			/// <summary>Канвас, на котором происходит рисование</summary>
		[SerializeField] public Canvas         canvas;

		[SerializeField] public float width   = 190f;
		[SerializeField] public bool  visible = false;

			/// <summary>Текущая панель подсказки</summary>
		private CanvasRenderer background    = null;
			/// <summary>Размеры панели подсказки</summary>
		private RectTransform backgroundRect = null;
			/// <summary>Заголовок подсказки</summary>
		private Text               text;

		private Rect               bounds;
		private Vector2            position;
		private List<PropertyItem> items = new List<PropertyItem>();

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

		/// <summary>
		/// Вызывает всплывающую подсказку в указанном месте
		/// </summary>
		/// <param name="position">позиция подсказки</param>
		/// <param name="description">описание, посещается в заголовок</param>
		/// <param name="items">свойства, помещаются снизу</param>
		public void show(Vector2 position, string description, List<PropertyItem> items) {
			this.items = items;
			this.position = position;
            CreateCanvasStruct(description);
            canvas.enabled=true;
			visible = true;
        }

		/// <summary>Скрывает подсказку</summary>
		public void hide() {
			if (!visible)
				return;
            canvas.enabled=false;
			visible = false;
			clear();
		}

		/// <summary>очищает свойства</summary>
		private void clear() {
            
			foreach (PropertyItem item in items) {

#if UNITY_EDITOR
				DestroyImmediate(item.toCanvas().gameObject);
#else
				Destroy(item.toCanvas().gameObject);
#endif

			}

			items.Clear();
			items = null;
			text  = null;

#if UNITY_EDITOR
			DestroyImmediate(background.gameObject);
#else
            Destroy(background.gameObject);
#endif

			background     = null;
			backgroundRect = null;
        }

		/// <summary>Пересоздаёт структуру надписей внутри контекста</summary>
		private void CreateCanvasStruct(string description) {

			background = Instantiate<CanvasRenderer>(panelInstance);
			background.transform.SetParent(canvas.transform);
			background.transform.name = PANEL_NAME;
			backgroundRect = background.GetComponent<RectTransform>();
			text = canvas.transform.Find(PANEL_NAME+"/"+titleName).GetComponent<Text>();
			text.text = description;

			bounds.x = position.x;
			bounds.y = position.y;
			bounds.width = width;

			float panelHeight = backgroundRect.sizeDelta.y;
            float offsetY = 0;

			foreach (PropertyItem item in items) { // инициализируем компоненты каждого свойства
					
					CanvasRenderer label     = Instantiate<CanvasRenderer>(propertyItemInstance);
					RectTransform  labelRect = label.GetComponent<RectTransform>();
					label.transform.SetParent(background.transform);

					Text labelTitle = label.transform.Find(titleName).GetComponent<Text>();
					Text labelValue = label.transform.Find(valueName).GetComponent<Text>();
				
					labelTitle.text = item.getTextTitle() + ":";
					labelValue.text = item.getTextValue();
					labelValue.color = item.getColorValue();

					labelRect.setHorizontalAncorBounds(5f, panelHeight+offsetY-items.Count*6,labelRect.sizeDelta.y);
				
                    item.setCanvas(label);
					offsetY += labelRect.sizeDelta.y;
					
				}
			
			
			backgroundRect.rotation = Quaternion.Euler(0f, 20f - UnityEngine.Random.Range(20f, 35f), 20f - UnityEngine.Random.Range(20f, 35f)); // произвольно вращаем панель
			bounds.height = 30 * (items.Count - 1) + 64;
			backgroundRect.sizeDelta = new Vector2(width, bounds.height);
			backgroundRect.position = new Vector3(position.x+(bounds.height*0.5f), position.y+(bounds.height*0.5f));

		}

		void Start() {

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


			if (backgroundRect != null)
				backgroundRect.rotation = onRotateIteration(backgroundRect.rotation); // постепенно поворачиваем панель "лицом" к пользователю

		}

		void OnGUI() {
			redraw();
		}

	}

}
