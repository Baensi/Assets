using System;
using Engine.Objects.Doors;
using Engine.I18N;
using UnityEngine;
using System.Collections;
using Engine.Objects;

namespace Engine.EGUI {

	public class DoorGUIRenderer : IRendererGUI {

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
				titleStyle = new GUIStyle(GUI.skin.label);
				titleStyle.alignment = TextAnchor.MiddleCenter;
				titleStyle.normal.textColor = titleColor;
				titleStyle.normal.background = backgroundCaptionTexture;
				titleStyle.fontSize = 16;
				titleStyle.fontStyle = FontStyle.Bold;
			}

			if (captionStyle == null) {
				captionStyle = new GUIStyle(GUI.skin.label);
				captionStyle.alignment = TextAnchor.MiddleCenter;
				captionStyle.normal.textColor = captionColor;
				captionStyle.fontSize = 14;
				captionStyle.fontStyle = FontStyle.Bold;
			}

			if (doorStateStyle == null) {
				doorStateStyle = new GUIStyle(GUI.skin.label);
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

			if (doorObject.getTextDisplayed() == TextDisplayed.None) return;

			GUI.Label(objectTitleRectangle,"["+CLang.getInstance().get(doorObject.getId())+"]\n",titleStyle);
			GUI.Label(objectCaptionRectangle,doorObject.getCaption(),captionStyle);

				switch(doorObject.getState()){
					case DoorState.Opened: doorStateStyle.normal.textColor = stateOpenedColor; break;
					case DoorState.Closed: doorStateStyle.normal.textColor = stateClosedColor; break;
					case DoorState.Locked: doorStateStyle.normal.textColor = stateLockedColor; break;
				}
			
			GUI.Label(objectStateRectangle,stateToString(doorObject.getState()),doorStateStyle);

		}

		public void printLabel(ILever leverObject) {

			if (leverObject.getTextDisplayed() == TextDisplayed.None) return;

			GUI.Label(objectTitleRectangle, "[" + CLang.getInstance().get(leverObject.getId()) + "]\n", titleStyle);
			GUI.Label(objectCaptionRectangle, leverObject.getCaption(), captionStyle);

			if (leverObject.isLocked()) {
				GUI.Label(objectStateRectangle, CLang.getInstance().get(Dictionary.K_LEVER_LOCKED), doorStateStyle);
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
					case DoorState.Opened: return CLang.getInstance().get (Dictionary.K_DOOR_OPENED);
					case DoorState.Closed: return CLang.getInstance().get (Dictionary.K_DOOR_CLOSED);
					case DoorState.Locked: return CLang.getInstance().get (Dictionary.K_DOOR_LOCKED);
				}
			return "";
		}

		public string stateToString(LeverState state) {
			switch (state) {
				case LeverState.State1: return CLang.getInstance().get(Dictionary.K_LEVER_STATE1);
				case LeverState.State2: return CLang.getInstance().get(Dictionary.K_LEVER_STATE2);
			}
			return "";
		}

	}

}

