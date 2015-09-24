using System;
using UnityEngine;
using Engine.Player.Animations;

namespace Engine.Player.Attack {
	
	public interface IAttacker {
		
		void setActions(IAnimations actions);
		
		void attack();
		void superAttack();
		void shield();
		
	}
	
}