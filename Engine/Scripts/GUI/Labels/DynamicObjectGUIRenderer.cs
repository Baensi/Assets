using System;
using Engine.Objects;
using UnityEngine;
using System.Collections;
using Engine.I18N;

namespace Engine.EGUI {

	public class DynamicObjectGUIRenderer : IRendererGUI {

		private static float offsetY = 0f;

		private Color titleColor   = new Color(0.2f,0.7f,0.2f);
		private Color captionColor = new Color(0.7f,0.7f,0.2f);

		private Rect objectTitleRectangle;
		private Rect objectCaptionRectangle;
		
		private Texture2D backgroundCaptionTexture;
		
		private GUIStyle titleStyle   = null;
		private GUIStyle captionStyle = null;
		
		public DynamicObjectGUIRenderer(){
			onResizeWindow();
		}
		
		public void onResizeWindow(){

			offsetY = Screen.height / 9f;

			objectTitleRectangle = new Rect(GameConfig.CenterScreen.x - 120.0f,
											GameConfig.CenterScreen.y - 30.0f + offsetY,
		                                    240.0f,
		                                    75.0f);
											
			objectCaptionRectangle = new Rect(GameConfig.CenterScreen.x - 120.0f,
											  GameConfig.CenterScreen.y - 10.0f + offsetY,
		                                      240.0f,
		                                      60.0f);
			
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

		}
															  
		public void printLabel(IDynamicObject dynamicObject){

			if(dynamicObject.getDisplayed().Equals(TextDisplayed.None)) return;

			GUI.Label(objectTitleRectangle,"["+CLang.getInstance().get(dynamicObject.getName())+"]\n",titleStyle);
			GUI.Label(objectCaptionRectangle,CLang.getInstance().get(dynamicObject.getCaption()),captionStyle);

		}

	}

}

