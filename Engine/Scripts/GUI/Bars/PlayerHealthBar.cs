using System;
using UnityEngine;

namespace Engine.EGUI.Bars {

	[ExecuteInEditMode]
	public class PlayerHealthBar : MonoBehaviour {
		
		[SerializeField] public Texture2D fullHealthPicture;
		[SerializeField] public Texture2D emptyHealthPicture;
		[SerializeField] [Range(0.005f,0.500f)]public float animationSpeed = 0.005f; // диапазон скоростей анимации
		[SerializeField] private bool visible;

		private Rect fullPictureRect;
		private Rect emptyPictureRect;
		private Rect fullPictureTransformRect;
		private Rect emptyPictureTransformRect;

		private float barPositionX = 0f;
		private float barPositionY = 0f;

		[SerializeField] public float scaleSize = 1.0f;

		private float currentValue; // стремится к ~GamePlayer.states.health
		private float currentMax;   // стремится к ~GamePlayer.states.maxHealth

		public int getWidth() {
			return fullHealthPicture.width;
		}

		public int getHeight() {
			return fullHealthPicture.height;
		}
		
		void Start(){

			

            
			
			emptyPictureTransformRect = new Rect(0f,0f,1f,1f);
			
		}

		void OnGUI(){

			if(!visible)
				return;

			float percent = 1.0f/currentMax*currentValue;

			barPositionX = emptyHealthPicture.width + 5;
			barPositionY = Screen.height - emptyHealthPicture.height - 5;

			emptyPictureRect = new Rect(barPositionX,
										barPositionY,
										emptyHealthPicture.width * scaleSize,
										emptyHealthPicture.height * scaleSize);

			fullPictureTransformRect = new Rect(0f,0f,1f,percent); // трансформатор
			fullPictureRect          = new Rect(barPositionX,
				                                barPositionY+(1.0f-percent-0.01f)*fullHealthPicture.height,
				                                fullHealthPicture.width*scaleSize,
				                                fullHealthPicture.height*percent*scaleSize);
			
			GUI.DrawTextureWithTexCoords(emptyPictureRect, emptyHealthPicture, emptyPictureTransformRect, true);
			
			if(currentValue>0)
				GUI.DrawTextureWithTexCoords(fullPictureRect, fullHealthPicture, fullPictureTransformRect,true);
			
		}

		private float doIteration(float realValue, float currentValue){

			if(currentValue==realValue) return realValue;
			
			if(Mathf.Abs(currentValue-realValue)>1f){
				
				if(currentValue>realValue)
					return currentValue-(currentValue-realValue)*animationSpeed; // шаг стремления 0.2
				else
					return currentValue+(realValue-currentValue)*animationSpeed;
				
			} else return realValue;

		}

		void Update(){

			if(!visible) return;

			currentValue = doIteration(GamePlayer.states.health,currentValue);
			currentMax   = doIteration(GamePlayer.states.maxHealth,currentMax);
		}
		
	}
	
}