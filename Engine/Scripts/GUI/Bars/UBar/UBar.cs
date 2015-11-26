using System;
using UnityEngine;

namespace Engine.EGUI.Bars {

	public enum OrientationType : int {
		horizontalLeftToRight,
		verticalBottomToTop,
		verticalTopToBottom
	}; // список ориентаций прогрессбара

	public class UBar : MonoBehaviour {
		
		[SerializeField] public Texture2D fullHealthPicture;
		[SerializeField] public Texture2D emptyHealthPicture;
		
		[SerializeField] public float barPositionX;
		[SerializeField] public float barPositionY;

		[SerializeField] public OrientationType orientation;

		[SerializeField] [Range(0.005f,0.500f)]public float animationSpeed = 0.005f; // диапазон скоростей анимации

		private Rect fullPictureRect;
		private Rect emptyPictureRect;
		private Rect fullPictureTransformRect;
		private Rect emptyPictureTransformRect;

		[SerializeField] public float max;
		[SerializeField] public float value;

		[SerializeField] public bool visible;

		private float currentValue; // стремится к ~value
		private float currentMax;   // стремится к ~max

			public float getValue(){
				return value;
			}
			
			public void  setValue(float value){
				if(value<=max && value>=0)
					this.value=value;
			}
			
			public float getMax(){
				return max;
			}
			
			public void  setMax(float max){
				if(max>0)
					this.max=max;
			}
		
		void Start(){
			
			emptyPictureRect = new Rect(barPositionX,
										barPositionY,
										emptyHealthPicture.width,
										emptyHealthPicture.height);
										
			emptyPictureTransformRect = new Rect(0f,0f,1f,1f);
			
		}

		void OnValidate(){
			if(max<1f)    max = 1f;
			if(value>max) value=max;
			if(value<0f)  value=0f;
		}

		void OnGUI(){

			if(!visible) return;

			float percent = 1.0f/currentMax*currentValue;

			switch(orientation){
				case OrientationType.horizontalLeftToRight:
				
					fullPictureTransformRect = new Rect(0f,0f,percent,1f); // трансформатор
					fullPictureRect          = new Rect(barPositionX,
														barPositionY,
					                                    fullHealthPicture.width*percent,
					                                    fullHealthPicture.height);
					break;
				case OrientationType.verticalBottomToTop:
					fullPictureTransformRect = new Rect(0f,0f,1f,percent); // трансформатор
					fullPictureRect          = new Rect(barPositionX,
						                                barPositionY+(1.0f-percent-0.01f)*fullHealthPicture.height,
						                                fullHealthPicture.width,
						                                fullHealthPicture.height*percent);
					break;
				case OrientationType.verticalTopToBottom:
					fullPictureTransformRect = new Rect(0f,1f-percent,1f,percent); // трансформатор
					fullPictureRect          = new Rect(barPositionX,
					                                    barPositionY,
					                                    fullHealthPicture.width,
					                                    fullHealthPicture.height*percent);
					break;
			}
			
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

			currentValue = doIteration(value,currentValue);
			currentMax   = doIteration(max,currentMax);
		}
		
	}
	
}