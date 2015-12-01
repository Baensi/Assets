using System;
using System.Collections.Generic;
using UnityEngine;
using Engine.I18N;

namespace Engine.EGUI.PopupMenu {
	
	[Serializable]
	public class MenuItem : MonoBehaviour {

		private PopupMenuBase  menu;
		[SerializeField] public List<MenuItem> childs;
		[SerializeField] public bool childVisible = false; // показывать дочерние элементы
		[SerializeField] public string text;
		[SerializeField] public int code;

		[SerializeField] public bool visible;
		[SerializeField] public new bool enabled = true;

		private GUIStyle style;

		private MenuItemSelectListener menuItemSelectListener = null;
		private MenuItemClickListener  menuItemClickListener = null;

		private bool  selected     = false; // итем "выбран"
		private bool  selFlag      = false;

		private Color diffColor; // промежуточный цвет
		private static float minAlpha  = 0.65f;
		private static float stepAlpha = 0.01f;

		private bool isLoaded = false;

        private float transferent  = 0.7f;  // прозрачность выделения
		private float size = 0.0f;

		private int textSize;
		private Vector2 oldOffset = new Vector2(0,0);

		public override int GetHashCode() {
			return code;
		}

		public override bool Equals(object o) {
			MenuItem item = o as MenuItem;

			if (item == null)
				return false;

            return code == item.getCode();
		}

		public PopupMenuBase getMenu() {
			return menu;
		}

		public void setPopupMenuBase(PopupMenuBase menu) {
			this.menu = menu;
			InitTextSize();

				foreach (MenuItem item in childs)
					item.setPopupMenuBase(menu);
			
			isLoaded = true;
		}

			void Start() {
				
			}

		public void InitTextSize(bool childs=false) {

			if (menu == null)
				return;

			size = 0f;
			style = new GUIStyle(menu.data.labelStyle);
			textSize = menu.data.labelStyle.fontSize;

			if (childs)
				foreach (MenuItem item in this.childs)
					item.InitTextSize(true);
		}

		public void setClickListener(MenuItemClickListener menuItemClickListener) {
			this.menuItemClickListener = menuItemClickListener;
        }

		public void setSelectListener(MenuItemSelectListener menuItemSelectListener) {
			this.menuItemSelectListener = menuItemSelectListener;
		}

		public void InitEvents(MenuItemSelectListener menuItemSelectListener, MenuItemClickListener menuItemClickListener){
			this.menuItemSelectListener = menuItemSelectListener;
			this.menuItemClickListener = menuItemClickListener;

			foreach (MenuItem item in childs) // Закидываем интерфейсы дочерним элементам
				item.InitEvents (menuItemSelectListener, menuItemClickListener);
		}

		public bool isEnabled() {
			return enabled;
		}

		public void setEnabled(bool enabled) {
			this.enabled = enabled;
		}

		private GUIStyle getStyle() {

			Color resultColor = selected ? menu.data.selectedColor : menu.data.normalColor;

			float r = diffColor.r > resultColor.r ? diffColor.r - stepAlpha : diffColor.r + stepAlpha;
			float g = diffColor.g > resultColor.g ? diffColor.g - stepAlpha : diffColor.g + stepAlpha;
			float b = diffColor.b > resultColor.b ? diffColor.b - stepAlpha : diffColor.b + stepAlpha;

			diffColor = new Color(r,g,b);

			style.normal.textColor = diffColor;
			style.fontSize = (int)(textSize * size);
			if(style.fontSize==0)
				style.fontSize=1;

            return style;
        }

		private void OnMenuItemSelectListener(){
			if(menuItemSelectListener!=null)
				menuItemSelectListener.onSelect(this);
		}

		private void OnMenuItemClickListener(){
			if (menuItemClickListener != null)
				menuItemClickListener.onClick(this);
		}

		public void setVisible(bool visible){
			this.visible = visible;

			if (!visible)
				InitTextSize();

			foreach (MenuItem item in childs)
				item.setVisible (visible);
		}

		public int getCode(){
			return code;
		}

		public void setChildVisible(bool visible){
			this.childVisible=visible;
		}
		
		public bool isChildVisible(){
			return childVisible;
		}

			/// <summary>
			/// Если ветвь итемов selected
			/// </summary>
			/// <returns>Возвращает логическое значение выделенности хотябы одного элемента дерева итемов</returns>
		public bool isFocused(){

			if (selected)
				return true;

			foreach (MenuItem item in childs)
				if (item.isFocused ())
					return true;

			return false;
		}

		public bool isSelected(){
			return selected;
		}
		
		public void setSelected(bool selected){
			this.selected=selected;
		}

		void OnUpdate() {

			if (!visible && size==0 || !enabled || !isLoaded)
				return;

			Vector2 mouse = Event.current.mousePosition;

			if (mouse.x >= oldOffset.x && mouse.x <= oldOffset.x + menu.data.itemSize.x &&
			   mouse.y >= oldOffset.y && mouse.y <= oldOffset.y + menu.data.itemSize.y) {

				if (selected == false)
					OnMenuItemSelectListener();

				if (Event.current.isMouse && Event.current.type == EventType.MouseDown && menu.isLoad())
					OnMenuItemClickListener();

				selected = true;

			} else {

				if (!childVisible) {

					if (selFlag) { // флаг показывает что надо отложить сброс выделения на следующую итерацию
						selFlag = false;
						return;
					}

					selected = false; // сбрасываем выделение

				} else { // пытаемся найти выделение на дочернем элементе

					float x = oldOffset.x + menu.data.itemSize.x;
					float y = oldOffset.y;

					float offsetByEnabled = 0; // неактивные элементы в списке приносят смещения других элементов вверх, делаем поправку на это

					foreach (MenuItem item in childs) {

						if (!item.isEnabled())
							offsetByEnabled -= menu.data.itemSize.y; // наращиваем смещение по неактивности

						if (mouse.x >= x && mouse.x <= x + menu.data.itemSize.x &&
						   mouse.y >= y+offsetByEnabled && mouse.y <= y + menu.data.itemSize.y + offsetByEnabled) { // если выделен дочерний элемент, выделяем родителя
							item.selected = true;
							selected = true;
							selFlag = true;
							return;
						} else {
							item.selected = false;
						}

						y += menu.data.itemSize.y;
					}

					selected = false;
					childVisible = false;

					foreach (MenuItem item in childs)
						item.size = 0f;

				}
			}

		}

		private void decAlhpa() {
			if (transferent > minAlpha) {
				transferent -= stepAlpha;
			} else {
				transferent = minAlpha;
			}
		}

		private void incAlhpa() {
			if (transferent < 1.0f) {
				transferent += stepAlpha;
			} else {
				transferent = 1.0f;
			}
		}

		private void onSelectIteration() {
			if (selected)
				incAlhpa();
			else
				decAlhpa();
        }

		public void draw(float offsetX, float offsetY){

			if (!visible || !enabled || !isLoaded)
				return;

			if (visible) {
				if (size < 1.0f)
					size += menu.stepSize;
				else
					size = 1.0f;
			} else {
				if (size > 0.0f)
					size -= menu.stepSize;
				else
					size = 0.0f;
			}

			oldOffset.x = offsetX;
			oldOffset.y = offsetY;

			OnUpdate();
			childVisible = selected;

			onSelectIteration();

			Rect rect = new Rect(offsetX, offsetY, menu.data.itemSize.x*size, menu.data.itemSize.y);

			Color color = GUI.color;
			GUI.color = new Color(1, 1, 1, transferent);
			GUI.Box(rect, CLang.getInstance().get(text), getStyle());
			GUI.color = color;

			drawChilds (offsetX, offsetY);
		}

		private void drawChilds(float offsetX, float offsetY){

			if (!childVisible || !isLoaded)
				return;

			float y = 0;
				
			foreach (MenuItem item in childs) {
				item.draw(offsetX + menu.data.itemSize.x, offsetY + y);

				if(item.isEnabled())
					y += menu.data.itemSize.y;
			}

		}
		
	}
	
}