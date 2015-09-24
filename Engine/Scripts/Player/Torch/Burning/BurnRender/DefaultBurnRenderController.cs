using System;
using UnityEngine;
using System.Collections;

namespace Engine.Player.Torch.Burn {

	public class DefaultBurnRenderController : IBurnRender {

		private Light light;
		private IBurn burn;

		private Texture textureBurnFull;
		private Texture textureBurnEmpty;

		private Sprite spriteBurn;

		private Rect burnRectangle;
		private Rect currentBurnRect;

		private float maxIntensity = 2.0f;

		private Rect textureSourceRectangleEmpty;
		private Rect textureSourceRectangleFull;

		public DefaultBurnRenderController(Texture2D textureBurnFull, Texture2D textureBurnEmpty, Light light) : base() {

			this.light=light;
			this.textureBurnFull=textureBurnFull;
			this.textureBurnEmpty=textureBurnEmpty;

			textureBurnFull.wrapMode = TextureWrapMode.Repeat;

			textureSourceRectangleEmpty = new Rect(0f,0f,1f,1f);
			burnRectangle = new Rect(Screen.width - textureBurnEmpty.width - 12.0f,
			                            12,
			                            textureBurnEmpty.width,
			                            textureBurnEmpty.height);

		}
		
		public void setBurn(IBurn burn){
			this.burn=burn;
		}

		/*
		 * Отрисовка батареек вверху
		 */
		public void drawBurn(){
			GUI.DrawTextureWithTexCoords(burnRectangle, textureBurnEmpty, textureSourceRectangleEmpty, true);

			if(burn!=null)
				GUI.DrawTextureWithTexCoords(currentBurnRect, textureBurnFull, textureSourceRectangleFull,true);
		}


		/*
		 * Установка параметров для источника света
		 */
		public void setupLight(){

			if(burn==null){ // нет батареек
				light.intensity = 0.0f;
				return;
			}

			if(burn.getEnergy()>0){
				light.intensity = maxIntensity * burn.getEnergy() * 0.01f; // расчитываем интенсивность свечения от текущего заряда батареек
			} else {
				light.intensity = 0.0f; // батарейки сели
			}

		}

		public void update(){

			if(light.enabled && burn!=null)
				burn.updateBurn();

				if(burn!=null){
					textureSourceRectangleFull = new Rect(0f,0f,burn.getEnergy() * 0.01f,1f);
					currentBurnRect = new Rect(Screen.width - textureBurnEmpty.width - 12.0f,
												  12,
												  textureBurnEmpty.width * burn.getEnergy() * 0.01f,
												  textureBurnEmpty.height);
				}

			drawBurn();
			setupLight();
			
		}
		
	}
	
}