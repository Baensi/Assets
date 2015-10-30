using System;
using UnityEngine;
using System.Collections;
using Engine.Objects;
using Engine.I18N;

namespace Engine.EGUI.Inventory {
	
	[Serializable]
	public class ItemSlot {

		[SerializeField] public Item item;
		[SerializeField] public ItemPosition position;
		
		public ItemSlot(Item item){
			this.item=item;
			position = new ItemPosition(-1,-1);
		}

		public ItemSlot(Item item, ItemPosition position) {
			this.item=item;
			this.position=position;
		}
		
		public void Clear(){
			item     = null;
			position = null;
		}
		
		public ItemPosition getPosition(){
			return position;
		}
		
		public void setPosition(ItemPosition position){
			this.position=position;
		}
		
	}
	
}