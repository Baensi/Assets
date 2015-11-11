using System;
using System.Collections.Generic;
using UnityEngine;

namespace Engine.EGUI.PopupMenu {

	public class PopupMenuBase : MonoBehaviour,
									  MenuOnGUIListener,
									  MenuUpdatetListener,
									  MenuStartListener,
									  MenuItemClickListener,
									  MenuItemSelectListener {

		[SerializeField] public ItemData data;
		[SerializeField] public List<MenuItem> items;

		[SerializeField] public Vector2 position;
		[SerializeField] public bool    visible;

		public virtual void onClick(MenuItem item) { } // делаем пустую реализацию клика
		public virtual void onSelect(MenuItem item) { } // делаем пустую реализацию выделения

		void OnValidate() {
			
			foreach (MenuItem item in items)
				item.InitTextSize(true);
		}

		public void MenuStart() {
			initEvents(this, this);
		}
		public void MenuUpdate(){ }

		public void initEvents(MenuItemSelectListener menuItemSelectListener, MenuItemClickListener menuItemClickListener){
			foreach (MenuItem item in items)
				item.InitEvents(menuItemSelectListener, menuItemClickListener);
		}

		public MenuItem findItem(int code){
			foreach (MenuItem item in items)
				if (item.getCode () == code)
					return item;
			return null;
		}

		public void setPosition(Vector2 position){
			this.position = position;
		}
		
		/// <summary>
		/// Скрывает контекстное меню
		/// </summary>
		public void hide(){
			setVisible(false);
		}
		
		public void show(){
			setVisible(true);
		}
		
		/// <summary>
		/// Отображает контекстное меню в указаном месте
		/// </summary>
		/// <param name="position">координаты контекстного меню</param>
		public void show(Vector2 position){
			this.position=position;
			setVisible(true);
		}
		
		public void setVisible(bool visible){
			this.visible = visible;

			foreach (MenuItem item in items)
				item.setVisible(visible);
		}
		
		/// <summary>
		/// возвращает видимость контекстного меню
		/// </summary>
		/// <returns></returns>
		public bool isVisible(){
			return visible;
		}

		public bool isFocused(){
			foreach (MenuItem item in items)
				if (item.isFocused ())
					return true;

			return false;
		}

		public Vector2 getPosition(){
			return position;
		}

		/// <summary>
		/// Отрисовывает контекстное меню, в случае перегрузки данного метода, рекомендуется вызвать <b>base.MenuOnGUI()</b>
		/// </summary>
		public void MenuOnGUI(){

			if (!visible)
				return;

			float y = 0;

			foreach (MenuItem item in items) {
				item.draw(position.x, position.y + y);

				if(item.isEnabled())
					y += data.itemSize.y;
			}

			if (Input.GetMouseButtonDown (0) && !isFocused ())
				visible = false;

		}

	
	}

}
