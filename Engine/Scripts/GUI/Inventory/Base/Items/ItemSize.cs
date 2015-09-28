using System;
using System.Collections;
using Engine.Objects;
using UnityEngine;

namespace Engine.EGUI.Inventory {

	[Serializable]
	public class ItemSize {

		[SerializeField] private int width;
		[SerializeField] private int height;

		public ItemSize(){
		}

		public ItemSize(int width, int height){
			this.width  = width;
			this.height = height;
		}

		public int getWidth(){
			return this.width;
		}

		public int getHeight(){
			return height;
		}

	}
}

