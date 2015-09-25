using System;
using System.Collections.Generic;
using UnityEngine;

namespace Engine.EGUI.PopupMenu {
	
	public class MenuItem {
		
		private float  selectTransf = 0.0f;  // прозрачность выделения
		private bool   selected     = false; // итем "выбран"
		
		private Rect   bounds;
		private string text;
		
			public MenuItem(string text, Rect bounds){
				this.text= text;
				this.bounds = new Rect();
			}
		
		public String getText(){
			return text;
		}
		
		public Rect getBounds(){
			return bounds;
		}
		
		public bool isSelected(){
			return selected;
		}
		
		public void setSelected(bool selected){
			this.selected=selected;
		}
		
		public void draw(float offsetX, float offsetY){
			
			if(selected)
				if(selectTransf<1f)
					selectTransf+=0.05;
				else
					selectTransf=1f;
			else
				if(selectTransf>0f)
					selectTransf-=0.05;
				else
					selectTransf=0f;
			
			
			GUI.Label(...);
			
			
		}
		
	}
	
}