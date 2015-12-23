using System;
using System.Collections.Generic;
using UnityEngine;
using Engine.EGUI.Bars;
using Engine.Images;

namespace Engine.EGUI {
	
	public class GUIController : MonoBehaviour {
		
		private DoorGUIRenderer          doorGUIRenderer = null;
		private DynamicObjectGUIRenderer dynamicObjectGUIRenderer = null;
		private ReadedPageGUIRenderer    readedPageGUIRenderer = null;
		private InventoryGUIRenderer     inventoryGUIRenderer = null;

			void Start(){

			Texture2D backgroundCaptionTexture = DImageList.getInstance().getImage("gui_label_background");

				doorGUIRenderer          = new DoorGUIRenderer();
				dynamicObjectGUIRenderer = new DynamicObjectGUIRenderer();
				readedPageGUIRenderer    = new ReadedPageGUIRenderer();
				inventoryGUIRenderer     = new InventoryGUIRenderer();

				dynamicObjectGUIRenderer.initStyles(backgroundCaptionTexture);
				doorGUIRenderer.initStyles(backgroundCaptionTexture);
				inventoryGUIRenderer.initStyles(backgroundCaptionTexture);

			}

		public DoorGUIRenderer getDoorGUIRenderer(){
			return doorGUIRenderer;
		}
		
		public DynamicObjectGUIRenderer getDynamicObjectGUIRenderer(){
			return dynamicObjectGUIRenderer;
		}
		
		public InventoryGUIRenderer getInventoryGUIRenderer() {
			return inventoryGUIRenderer;
        }

		public ReadedPageGUIRenderer getReadedPageGUIRenderer(){
			return readedPageGUIRenderer;
		}
		
	}
	
}