using UnityEngine;
using System.Collections;
using Engine.Objects;
using Engine.I18N;

namespace Engine.EGUI.Inventory {

	public class Item : IItem {

		private static Color labelColor = new Color(1.0f,1.0f,0.5f);

		private ItemPosition position;
		private ItemSize     size;
		private Texture      icon;
		private bool         isSelect;
		private int          count = 1;
		private int          maxCount;

		private IDynamicObject dynamicObject;

		public override bool Equals(object obj){
			IItem item = obj as IItem;

				if(item==null) return false;

			return (item.toDynamicObject().Equals(toDynamicObject()));

		}

		public override int GetHashCode() {
			return base.GetHashCode();
		}

		public int getMaxCount(){
			return maxCount;
		}

		public IDynamicObject toDynamicObject(){
			return dynamicObject;
		}

		public void incCount(){
			count++;
		}

		public void decCount(){
			count--;
		}

		public int getCount(){
			return count;
		}
		public void setCount(int count){
			this.count=count;
		}

		public Texture getIcon(){
			return icon;
		}
		public void setIcon(Texture icon){
			this.icon=icon;
		}

		public ItemPosition getPosition(){
			return position;
		}
		public void setPosition(ItemPosition position){
			this.position=position;
		}

		public ItemSize getSize(){
			return size;
		}
		public void setSize(ItemSize size){
			this.size=size;
		}

		public bool isSelected(){
			return isSelect;
		}

		public void setSelected(bool selected){
			this.isSelect=selected;
		}

		public Item(IDynamicObject dynamicObject, Texture icon, ItemSize size, int maxCount){
			this.dynamicObject=dynamicObject;
			this.icon=icon;
			this.size=size;
			this.maxCount=maxCount;
			this.count=1;
		}

		public void redraw(float posX, float posY){

			Rect cellRectangle = new Rect(posX+CellSettings.cellPaddingX+(position.X-1)*CellSettings.cellWidth,
					                      posY+CellSettings.cellPaddingY+(position.Y-1)*CellSettings.cellHeight,
					                      (float)CellSettings.cellWidth,
					                      (float)CellSettings.cellHeight);

			GUI.DrawTexture(cellRectangle,icon);

			if(count>1){

				Rect labelRectangle = new Rect(cellRectangle.x,
				                               cellRectangle.y+cellRectangle.height-18.0f,
				                               cellRectangle.width,
				                               20.0f);

				GUIStyle style = new GUIStyle(GUI.skin.label);
				style.alignment = TextAnchor.MiddleCenter;
				style.normal.textColor = labelColor;
				style.fontSize = 12;
				style.fontStyle = FontStyle.Normal;

				GUI.Label(labelRectangle,count+CLang.getInstance().get(Dictionary.K_COUNT));
			}

		}

	}

}
