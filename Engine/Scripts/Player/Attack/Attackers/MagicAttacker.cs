using System;
using UnityEngine;
using Engine.Player.Animations;

namespace Engine.Player.Attack {
	
	[Serializable]
	public class MagicAttacker : MonoBehaviour, IAttacker {
		
		private IAnimations actions;

		[SerializeField] public UnityEngine.Object magicPart;
		
		public void setActions(IAnimations actions){
			this.actions=actions;
		}
		
		public void attack(){
			//actions.Attack();

			//UnityEngine.Object instance = 

			Vector3 position = transform.position + Camera.main.transform.forward * 2;

			Instantiate(magicPart, position, transform.rotation);




		}
		
		public void superAttack(){
			
		}
		
		public void shield(){
			
		}
		
	}
	
}