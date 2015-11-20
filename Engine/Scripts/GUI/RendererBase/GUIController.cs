using System;
using System.Collections.Generic;
using UnityEngine;
using Engine.EGUI.Bars;

namespace Engine.EGUI {
	
	public class GUIController : MonoBehaviour {
		
		private List<IRendererGUI> gui;
		
		private DoorGUIRenderer          doorGUIRenderer = null;
		private DynamicObjectGUIRenderer dynamicObjectGUIRenderer = null;
		private ReadedPageGUIRenderer    readedPageGUIRenderer = null;

		[SerializeField] public PlayerEnergyBar playerEnergyBar;
		[SerializeField] public PlayerManaBar   playerManaBar;
		[SerializeField] public PlayerHealthBar playerHealthBar;

		private bool initGUIState = false;

		private int screenWidth;
		private int screenHeight;
		
			void Start(){
				gui = new List<IRendererGUI>();
				
				doorGUIRenderer = new DoorGUIRenderer();
				gui.Add(doorGUIRenderer);
				
				dynamicObjectGUIRenderer = new DynamicObjectGUIRenderer();
				gui.Add(dynamicObjectGUIRenderer);
				
				readedPageGUIRenderer = new ReadedPageGUIRenderer();
				gui.Add(readedPageGUIRenderer);
				
					screenWidth=Screen.width;
					screenHeight = Screen.height;
				
				onResizeWindow();
			}

		public bool isInitGUIState() {
			return initGUIState;
		}

		public void setInitGUIState(bool initGUIState) {
			this.initGUIState = initGUIState;
		}

		public DoorGUIRenderer getDoorGUIRenderer(){
			return doorGUIRenderer;
		}
		
		public DynamicObjectGUIRenderer getDynamicObjectGUIRenderer(){
			return dynamicObjectGUIRenderer;
		}
		
		public ReadedPageGUIRenderer getReadedPageGUIRenderer(){
			return readedPageGUIRenderer;
		}
		
		void Update(){
			if(screenWidth!=Screen.width || screenHeight!=Screen.height){
				screenWidth=Screen.width;
				screenHeight=Screen.height;
				onResizeWindow();
			}
		}
		
		private void onResizeWindow(){

			foreach(IRendererGUI item in gui){
				item.onResizeWindow();
			}

			float guiBarsCenter = Screen.width/10f; // мнимый центр прогрессбаров

				// позиционируем прогрессбары
			//playerHealthBar.barPositionY = Screen.height-playerHealthBar.getHeight();
			//playerHealthBar.barPositionX = guiBarsCenter - playerHealthBar.getWidth()/2f;

			//playerEnergyBar.barPositionY = Screen.height-playerEnergyBar.getHeight();
			//playerEnergyBar.barPositionX = playerHealthBar.barPositionX+playerHealthBar.getWidth()+playerEnergyBar.getWidth();

			//playerManaBar.barPositionY = Screen.height-playerManaBar.getHeight();
			//playerManaBar.barPositionX = playerHealthBar.barPositionX-playerManaBar.getWidth();
		}
		
	}
	
}