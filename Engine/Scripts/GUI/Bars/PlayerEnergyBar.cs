using System;
using UnityEngine;

namespace Engine.EGUI.Bars {

	[Serializable]
	public class PlayerEnergyBar : MonoBehaviour {
		
		[SerializeField] public Texture2D fullEnergyPicture;
		[SerializeField] public Texture2D emptyEnergyPicture;
		
		public float barPositionX;
		public float barPositionY;

		[SerializeField] [Range(0.005f,0.500f)]public float animationSpeed = 0.005f; // диапазон скоростей анимации

		private Rect fullPictureRect;
		private Rect emptyPictureRect;
		private Rect fullPictureTransformRect;
		private Rect emptyPictureTransformRect;

		[SerializeField] private float max;
		[SerializeField] private float value;
                         
		[SerializeField] private bool visible;

		private float currentValue; // стремится к ~value
		private float currentMax;   // стремится к ~max

		public int getWidth() {
			return fullEnergyPicture.width;
		}

		public int getHeight() {
			return fullEnergyPicture.height;
		}

		public float Value {
			
			get { return this.value; }
			set {
				
				if(this.value<=max && this.value>=0){
					this.value=value;
					return;
				}
				
				if(this.value>max)
					this.value=max;
				else
					this.value=0;

			}
			
		}
		
		public float Max {
			
			get { return max; }
			set {
				if(max>0)
					this.max=value;
			}
			
		}
		
		void Start(){
			
			emptyPictureRect = new Rect(barPositionX,
										barPositionY,
										emptyEnergyPicture.width,
										emptyEnergyPicture.height);
										
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

			fullPictureTransformRect = new Rect(0f,0f,1f,percent); // трансформатор
			fullPictureRect          = new Rect(barPositionX,
				                                barPositionY+(1.0f-percent-0.01f)*fullEnergyPicture.height,
				                                fullEnergyPicture.width,
				                                fullEnergyPicture.height*percent);
			
			GUI.DrawTextureWithTexCoords(emptyPictureRect, emptyEnergyPicture, emptyPictureTransformRect, true);
			
			if(currentValue>0)
				GUI.DrawTextureWithTexCoords(fullPictureRect, fullEnergyPicture, fullPictureTransformRect,true);
			
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