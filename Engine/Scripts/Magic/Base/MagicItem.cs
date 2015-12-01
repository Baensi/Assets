using System;
using System.Collections.Generic;
using UnityEngine;

namespace Engine.Magic {

	/// <summary>
	/// Элемент магической последовательности
	/// Последовательность может состоять из 1 элемента
	/// Каждый элемент может запускать паралелльно несколько других магических последовательностей, или последовательно активировать магические эффекты
	/// </summary>
	public class MagicItem : MonoBehaviour, IMagic {

		/// <summary> Время длительности текущего эффекта</summary>
		[SerializeField] public float timeEffect = 1f;

		/// <summary> Элементы магии, которые активируется СРАЗУ вместе с текущим</summary>
		[SerializeField] public List<MagicItem> asyncMagicItems;

		/// <summary> Элемент магии, которы активируется ПОСЛЕ окончания эффекта этого элемента</summary>
		[SerializeField] public MagicItem nextMagicItem = null;

			void Start() {
				MagicRun();
			}

		void Update() {
			MagicUpdate();
		}

		/// <summary>
		/// Виртуальный метод активации магического эффекта
		/// </summary>
		public virtual void MagicRun() {

		}

		/// <summary>
		/// Виртуальный метод просчёта итерации магического эффекта
		/// </summary>
		public virtual void MagicUpdate() {

		}

	}

}
