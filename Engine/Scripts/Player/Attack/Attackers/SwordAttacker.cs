using System;
using UnityEngine;
using Engine.Player.Animations;

namespace Engine.Player.Attack {

	[Serializable]
	public class SwordAttacker : MonoBehaviour, IAttacker {
		
		private IAnimations actions;
		
		public void setActions(IAnimations actions){
			this.actions=actions;
		}
		
		public void attack(){
			actions.Attack();
		}
		
		public void superAttack(){
			
		}
		
		public void shield(){
			
		}
		
	}
	
}