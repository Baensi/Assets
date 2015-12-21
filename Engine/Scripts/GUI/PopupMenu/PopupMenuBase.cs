using System;
using System.Collections.Generic;
using UnityEngine;

namespace Engine.EGUI.PopupMenu {

	/// <summary>
	/// Класс-шина контекстное меню
	/// </summary>
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

		[SerializeField] public float stepSize = 0.1f;

		private float width;
		private float height;

		private bool load = false;

		public virtual void onClick(MenuItem item) { } // делаем пустую реализацию клика
		public virtual void onSelect(MenuItem item) { } // делаем пустую реализацию выделения

		/// <summary>
		/// Показывае, загружены ли стили GUI
		/// </summary>
		/// <returns></returns>
		public bool isLoad() {
			return load;
		}

		void OnValidate() {
			
			foreach (MenuItem item in items)
				item.InitTextSize(true);
		}

		/// <summary>
		/// Устанавливает события всем пунктам меню на виртуальные
		/// </summary>
		public void MenuStart() {
			initEvents(this, this);
		}
		public void MenuUpdate(){ }

		/// <summary>
		/// Устанавливает события всем пунктам меню на указанные
		/// </summary>
		/// <param name="menuItemSelectListener"></param>
		/// <param name="menuItemClickListener"></param>
		public void initEvents(MenuItemSelectListener menuItemSelectListener, MenuItemClickListener menuItemClickListener){
			foreach (MenuItem item in items)
				if(!item.isHaveSpecificClickListener()) // устанавливаем слушателей событий по умолчанию только там, где их нет
					item.InitEvents(menuItemSelectListener, menuItemClickListener);
		}


		/// <summary>
		/// Находит пункт меню в popupMenu по идентификатору
		/// </summary>
		/// <param name="code">Идентификатор пункта меню</param>
		/// <returns>Возвращает ссылку на класс пункта меню</returns>
		public MenuItem findItem(int code){
			foreach (MenuItem item in items)
				if (item.getCode () == code)
					return item;
			return null;
		}

		/// <summary>
		/// Устанавливает позицию высплывающего меню
		/// </summary>
		/// <param name="position"></param>
		public void setPosition(Vector2 position){
			this.position = position;
		}
		
		/// <summary>
		/// Скрывает контекстное меню
		/// </summary>
		public void hide(){
			setVisible(false);
			load=false;
		}
		
		/// <summary>
		/// Отображает контекстное меню в указаном месте
		/// </summary>
		/// <param name="position">координаты контекстного меню</param>
		public void show(Vector2 position){
			this.position=position;
			setVisible(true);
		}
		
		/// <summary>
		/// Возвращает вычисленную высоту графической части всплывающего меню
		/// </summary>
		/// <returns></returns>
		public float getHeight() {
			return height;
		}

		public void setVisible(bool visible){
			this.visible = visible;

			foreach (MenuItem item in items)
				item.setVisible(visible);

			if (visible) {
				float y = 0f;
				foreach (MenuItem item in items)
					if (item.isEnabled())
						y += data.itemSize.y;

				this.height = y;
            }
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

			if (!load &&  Event.current.isMouse && Event.current.button==1 && Event.current.type == EventType.MouseUp)
				load = true;

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
