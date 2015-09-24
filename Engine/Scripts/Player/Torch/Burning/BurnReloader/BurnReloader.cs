using System;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using Engine.I18N;

namespace Engine.Player.Torch.Burn {

	public class BurnReloader {
	
		private IBurnRender burnRender;
		private List<IBurn> burnList;

		private Rect countRectangle;

	
		public BurnReloader(IBurnRender burnRender){
			this.burnRender=burnRender;

			burnList = new List<IBurn>();
			
			countRectangle = new Rect(Screen.width-48.0f,
			                          28.0f,
			                          32.0f,
			                          20.0f);
			burnRender.setBurn((IBurn)burnList[0]);
			
		}

		public void addBurn(IBurn burn){
			burnList.Add(burn);
		}

		public bool nextBurn(){
		
			if(burnList.Count>0){

				IBurn burn = (IBurn)burnList[0];

				if(burn.getEnergy()>0f && burnList.Count==1) return false;

				burnList.Remove(burn);

				if(burnList.Count>0){
					burnRender.setBurn((IBurn)burnList[0]);
					return true;
				} else {
					burnRender.setBurn(null);
				}

			} else {

				burnRender.setBurn(null);

			}

			return false;
			
		}
		
		public void update(){
		
			GUI.Label(countRectangle,burnList.Count+CLang.getInstance().get(Dictionary.K_COUNT));
		
		}
	
	}

}