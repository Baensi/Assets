using System;
using System.Collections.Generic;
using UnityEngine;
using Engine.EGUI.Bars;

namespace Engine.EGUI {
	
	public class GUIController : MonoBehaviour {
		
		private DoorGUIRenderer          doorGUIRenderer = null;
		private DynamicObjectGUIRenderer dynamicObjectGUIRenderer = null;
		private ReadedPageGUIRenderer    readedPageGUIRenderer = null;

		private bool initGUIState = false;
		
			void Start(){

				doorGUIRenderer = new DoorGUIRenderer();
				dynamicObjectGUIRenderer = new DynamicObjectGUIRenderer();
				readedPageGUIRenderer = new ReadedPageGUIRenderer();

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
		
	}
	
}