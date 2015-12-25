using System;
using Engine.Objects.Doors;
using Engine.I18N;
using UnityEngine;
using System.Collections;
using Engine.Objects;

namespace Engine.EGUI {

	public class DoorGUIRenderer {

		private static float offsetY = 0f;

		private Color titleColor   = new Color(0.2f,0.7f,0.2f);
		private Color captionColor = new Color(0.7f,0.7f,0.2f);

		private Color stateOpenedColor = new Color(0.2f,0.7f,0.2f);
		private Color stateClosedColor = new Color(0.7f,0.7f,0.2f);
		private Color stateLockedColor = new Color(0.7f,0.2f,0.2f);

		private Rect objectTitleRectangle;
		private Rect objectCaptionRectangle;
		private Rect objectStateRectangle;
		
		private GUIStyle titleStyle=null;
		private GUIStyle captionStyle=null;
		private GUIStyle doorStateStyle=null;
		
		public DoorGUIRenderer(){
			onResizeWindow();
		}
		
		public void initStyles(Texture2D backgroundCaptionTexture){

			if (titleStyle == null) {
				titleStyle = new GUIStyle();
				titleStyle.alignment = TextAnchor.MiddleCenter;
				titleStyle.normal.textColor = titleColor;
				titleStyle.normal.background = backgroundCaptionTexture;
				titleStyle.fontSize = 16;
				titleStyle.fontStyle = FontStyle.Bold;
			}

			if (captionStyle == null) {
				captionStyle = new GUIStyle();
				captionStyle.alignment = TextAnchor.MiddleCenter;
				captionStyle.normal.textColor = captionColor;
				captionStyle.fontSize = 14;
				captionStyle.fontStyle = FontStyle.Bold;
			}

			if (doorStateStyle == null) {
				doorStateStyle = new GUIStyle();
				doorStateStyle.alignment = TextAnchor.MiddleCenter;
				doorStateStyle.fontSize = 12;
				doorStateStyle.fontStyle = FontStyle.Bold;
			}

		}
		
		public void onResizeWindow(){

			offsetY = Screen.height / 9f;

			objectTitleRectangle = new Rect(GameConfig.CenterScreen.x - 120.0f,
											GameConfig.CenterScreen.y - 40.0f + offsetY,
		                                    240.0f,
		                                    75.0f);
											
			objectCaptionRectangle = new Rect(GameConfig.CenterScreen.x - 120.0f,
											  GameConfig.CenterScreen.y - 25.0f + offsetY,
		                                      240.0f,
		                                      60.0f);
											  
			objectStateRectangle = new Rect(GameConfig.CenterScreen.x - 120.0f,
											GameConfig.CenterScreen.y - 10.0f + offsetY,
		                                    240.0f,
		                                    60.0f);
		}
		
		public void printLabel(IDoor doorObject){

			onResizeWindow();

			if (doorObject.getTextDisplayed() == TextDisplayed.None) return;

			GUI.Label(objectTitleRectangle,"["+doorObject.getName()+"]\n",titleStyle);
			GUI.Label(objectCaptionRectangle,doorObject.getCaption(),captionStyle);

				switch(doorObject.getState()){
					case DoorState.Opened: doorStateStyle.normal.textColor = stateOpenedColor; break;
					case DoorState.Closed: doorStateStyle.normal.textColor = stateClosedColor; break;
					case DoorState.Locked: doorStateStyle.normal.textColor = stateLockedColor; break;
				}
			
			GUI.Label(objectStateRectangle,stateToString(doorObject.getState()),doorStateStyle);

		}

		public void printLabel(ILever leverObject) {

			onResizeWindow();

			if (leverObject.getTextDisplayed() == TextDisplayed.None) return;

			GUI.Label(objectTitleRectangle, "[" + leverObject.getName() + "]\n", titleStyle);
			GUI.Label(objectCaptionRectangle, leverObject.getCaption(), captionStyle);

			if (leverObject.isLocked()) {
				GUI.Label(objectStateRectangle, Dictionary.LEVER_LOCKED_TEXT, doorStateStyle);
			} else {
				switch (leverObject.getState()) {
					case LeverState.State1: doorStateStyle.normal.textColor = stateOpenedColor; break;
					case LeverState.State2: doorStateStyle.normal.textColor = stateClosedColor; break;
				}

				GUI.Label(objectStateRectangle, stateToString(leverObject.getState()), doorStateStyle);
			}
		}

		public string stateToString(DoorState state){
				switch(state){
					case DoorState.Opened: return Dictionary.DOOR_OPENED_TEXT;
					case DoorState.Closed: return Dictionary.DOOR_CLOSED_TEXT;
					case DoorState.Locked: return Dictionary.DOOR_LOCKED_TEXT;
				}
			return "";
		}

		public string stateToString(LeverState state) {
			switch (state) {
				case LeverState.State1: return Dictionary.LEVER_STATE1_TEXT;
				case LeverState.State2: return Dictionary.LEVER_STATE2_TEXT;
			}
			return "";
		}

	}

}

