using System;
using System.Collections.Generic;
using UnityEngine;

namespace Engine.EGUI.ToolTip {

	public class ToolTip : MonoBehaviour {

		[SerializeField] public bool visible;

		private Vector2 position;
		private List<PropertyItem> items;


		public bool isVisible() {
			return visible;
        }

		public void show(Vector2 position, List<PropertyItem> items) {
			this.position = position;
			this.items = items;

				

			visible = true;
        }

		public void hide() {
			visible = false;
        }

		public void Clear() {
			items.Clear();
        }

		void Start() {



		}

		public void draw() {

		}

	}

}
