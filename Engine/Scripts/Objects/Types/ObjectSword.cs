using System;
using UnityEngine;

using Engine.Player;

namespace Engine.Objects {
	
	public class ObjectSword : MonoBehaviour {
		
		[SerializeField] public PlayerStates states;
		
			public ObjectSword(){
				
			}
		
		public PlayerStates getStates(){
			return states;
		}
		
	}
	
}