using System;
using UnityEngine;
using System.Collections;
using Engine.Objects;
using Engine.I18N;

namespace Engine.EGUI.Inventory {

	[Serializable]
	public struct Item : IItem {

		public static Item NULL = new Item() { gameObject=null };

		private static GUIStyle labelStyle = null;
		private static Color    labelColor = new Color(1.0f,1.0f,0.5f);

		public ItemDescription description;
		public ItemPosition    position;
		public ItemSize        size;
		public Texture         icon;

		public bool         isSelect;
		public int          count;
		public int          maxCount;

		private GameObject  gameObject;

		public static GUIStyle getLabelStyle() {

			if (labelStyle==null) {
				labelStyle = new GUIStyle(GUI.skin.label);
				labelStyle.alignment = TextAnchor.MiddleRight;
				labelStyle.normal.textColor = labelColor;
				labelStyle.fontSize = 15;
				labelStyle.fontStyle = FontStyle.Bold;
			}

			return labelStyle;

		}

		public static bool operator ==(Item i1, Item i2) {
			return i1.gameObject==i2.gameObject;
		}

		public static bool operator !=(Item i1, Item i2) {
			return !(i1==i2);
		}

		public override bool Equals(object obj){
			IItem item = obj as IItem;

				if(item==null) return false;

			return (item.toGameObject().Equals(toGameObject()));

		}

		public override int GetHashCode() {
			return base.GetHashCode();
		}

		public int getMaxCount(){
			return maxCount;
		}

		public GameObject toGameObject(){
			return gameObject;
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

		public Item Create(GameObject gameObject, Texture icon, ItemSize size, int maxCount, ItemDescription description) {
			this.gameObject=gameObject;
			this.icon=icon;
			this.size=size;
			this.maxCount=maxCount;
			this.count=1;
			this.description=description;
			return this;
		}

		public void redraw(float posX, float posY){

			Rect cellRectangle = new Rect(posX+CellSettings.cellPaddingX+(position.X-1)*CellSettings.cellWidth,
					                      posY+CellSettings.cellPaddingY+(position.Y-1)*CellSettings.cellHeight,
					                      (float)CellSettings.cellWidth,
					                      (float)CellSettings.cellHeight);

			GUI.DrawTexture(cellRectangle,icon);

			if(count>1){

				Rect labelRectangle = new Rect(cellRectangle.x,
				                               cellRectangle.y+cellRectangle.height-22.0f,
				                               cellRectangle.width-8,
				                               20.0f);

				GUI.Label(labelRectangle, count.ToString()+CLang.getInstance().get(Dictionary.K_COUNT), getLabelStyle());

			}

		}

	}

}
