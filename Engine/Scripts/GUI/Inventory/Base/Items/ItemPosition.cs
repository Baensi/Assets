using System;
using System.Collections;
using Engine.Objects;
using UnityEngine;

namespace Engine.EGUI.Inventory {
	
	[Serializable]
	public class ItemPosition {
		
		[SerializeField] public int X;
		[SerializeField] public int Y;

		public ItemPosition(){

		}

		public ItemPosition(int x, int y){
			this.X = x;
			this.Y = y;
		}
		
	}
}

