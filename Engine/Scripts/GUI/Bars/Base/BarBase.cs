using System;
using UnityEngine;

namespace Engine.EGUI.Bars {

	[ExecuteInEditMode]
	public class BarBase : MonoBehaviour {

		[SerializeField] public Texture2D fullPicture;
		[SerializeField] public Texture2D emptyPicture;

		[SerializeField] [Range(0.005f, 0.500f)] public float animationSpeed = 0.015f; // диапазон скоростей анимации
		[SerializeField] protected bool visible;

		private Rect fullPictureRect;
		private Rect emptyPictureRect;
		private Rect fullPictureTransformRect;

		private static Rect emptyPictureTransformRect = new Rect(0f, 0f, 1f, 1f);

		private Vector2 barPosition = Vector2.zero;

		protected float currentValue;
		protected float currentMax;

		/// <summary>
		/// Ширина бара
		/// </summary>
		/// <returns></returns>
		public float getWidth() {
			return fullPicture.width;
		}

		/// <summary>
		/// Высота бара
		/// </summary>
		/// <returns></returns>
		public float getHeight() {
			return fullPicture.height;
		}

		/// <summary>
		/// Виртуальная функция расчёта позиции бара
		/// </summary>
		/// <returns></returns>
		public virtual Vector2 getBarPosition() {
			return Vector2.zero;
		}

		/// <summary>
		/// Отрисовка бара
		/// </summary>
		public void OnDraw() {

			if (!visible)
				return;

			float percent = 1.0f / currentMax * currentValue;
			barPosition = getBarPosition();

			emptyPictureRect = new Rect(barPosition.x,
										barPosition.y,
										getWidth(),
										getHeight());

			fullPictureTransformRect = new Rect(0f, 0f, 1f, percent); // трансформатор
			fullPictureRect = new Rect(barPosition.x,
									   barPosition.y + (1.0f - percent - 0.01f) * getHeight(),
									   getWidth(),
									   getHeight() * percent);

			GUI.DrawTextureWithTexCoords(emptyPictureRect, emptyPicture, emptyPictureTransformRect, true);

			if (currentValue > 0)
				GUI.DrawTextureWithTexCoords(fullPictureRect, fullPicture, fullPictureTransformRect, true);

		}

		/// <summary>
		/// Итерация плавного смещения значения
		/// </summary>
		/// <param name="realValue">Точка к которой идёт смещение</param>
		/// <param name="currentValue">Текущее значение, которое надо сместить</param>
		/// <returns>Возвращает промежуточное знаение между текущим и конечным</returns>
		private float doIteration(float realValue, float currentValue) {

			if (currentValue == realValue) return realValue;

			if (Mathf.Abs(currentValue - realValue) > 1f) {

				if (currentValue > realValue)
					return currentValue - (currentValue - realValue) * animationSpeed;
				else
					return currentValue + (realValue - currentValue) * animationSpeed;

			} else return realValue;

		}

		public void updateValues(float value, float max) {

			if (!visible)
				return;

			currentValue = doIteration(value, currentValue);
			currentMax   = doIteration(max, currentMax);

		}
		

	}

}
