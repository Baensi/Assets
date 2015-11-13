using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Engine.EGUI.ToolTip {

	public class ToolTipBase : MonoBehaviour {

		[SerializeField] public string titleName = "Panel/Title";

			/// <summary>Префаб элемента-свойства</summary>
		[SerializeField] public CanvasRenderer propertyItemInstance;

			/// <summary>Префаб панели подсказки</summary>
		[SerializeField] public CanvasRenderer panelInstance;
		
			/// <summary>Канвас, на котором происходит рисование</summary>
		[SerializeField] public Canvas         canvas;

		[SerializeField] public float width   = 120f;
		[SerializeField] public bool  visible = false;

			/// <summary>Текущая панель подсказки</summary>
		private CanvasRenderer background    = null;
			/// <summary>Размеры панели подсказки</summary>
		private RectTransform backgroundRect = null;
			/// <summary>Заголовок подсказки</summary>
		private Text               text;

		private Rect               bounds;
		private Vector2            position;
		private List<PropertyItem> items;

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
			visible = true;
        }

		/// <summary>Скрывает подсказку</summary>
		public void hide() {
			visible = false;
			clear();
        }

		/// <summary>очищает свойства</summary>
		private void clear() {
			foreach (PropertyItem item in items)
				Destroy(item.toCanvas());
			items.Clear();
			items = null;
			text  = null;
			Destroy(background);
			background     = null;
			backgroundRect = null;
        }

		/// <summary>Пересоздаёт структуру надписей внутри контекста</summary>
		private void CreateCanvasStruct(string description) {

			background = Instantiate<CanvasRenderer>(panelInstance);
			background.transform.parent = canvas.transform;
			backgroundRect = background.GetComponent<RectTransform>();
			text = canvas.transform.Find(titleName).GetComponent<Text>();
			text.text = description;

			bounds.x = position.x;
			bounds.y = position.y;
			bounds.width = width;


			float panelHeight = backgroundRect.sizeDelta.y;
            float offsetY = 0;

				foreach (PropertyItem item in items) { // инициализируем компоненты каждого свойства
					
					CanvasRenderer label     = Instantiate<CanvasRenderer>(propertyItemInstance);
					RectTransform  labelRect = label.GetComponent<RectTransform>();
					label.transform.parent = background.transform;
					labelRect.position = new Vector3(labelRect.position.x, -panelHeight + labelRect.sizeDelta.y - 6 - offsetY);
					item.setCanvas(label);
					offsetY += labelRect.sizeDelta.y;
					
				}

			backgroundRect.position = new Vector3(position.x, position.y);
			backgroundRect.rotation = Quaternion.Euler(0f, 20f - UnityEngine.Random.Range(5f, 35f), 20f - UnityEngine.Random.Range(5f, 35f)); // произвольно вращаем панель
			backgroundRect.sizeDelta = new Vector2(bounds.width, bounds.height);

			Debug.LogWarning("Ok!");

		}

		void Start() {

			// TEST //

			Inventory.Item item = Engine.Objects.DObjectList.getInstance().getItem("RawFish");

            show(new Vector2(60, 60),
				Engine.EGUI.Inventory.ItemToolTipService.getInstance().createDescription(item),
				Engine.EGUI.Inventory.ItemToolTipService.getInstance().createInformationItems(item));

		}

		/// <summary>
		/// С каждой итерацией приближает угол поворота к 0,0,0 градусам
		/// </summary>
		/// <param name="rotation">Текущий угол</param>
		/// <returns>Возвращает следующее промежуточное состояние вращения</returns>
		private Quaternion onRotateIteration(Quaternion rotation) {
			if (rotation.eulerAngles.x > 0.01f || rotation.eulerAngles.y > 0.01f || rotation.eulerAngles.z > 0.01f)
				return Quaternion.Euler(rotation.eulerAngles * 0.98f);
			else
				return Quaternion.Euler(Vector3.zero);
		}

		void OnGUI() {

			if (!visible)
				return;

			if (backgroundRect != null)
				backgroundRect.rotation = onRotateIteration(backgroundRect.rotation); // постепенно поворачиваем панель "лицом" к пользователю


		}

	}

}
