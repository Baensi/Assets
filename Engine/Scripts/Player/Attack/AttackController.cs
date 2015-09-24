using System;
using System.Collections.Generic;
using UnityEngine;
using Engine.Player.Animations;

namespace Engine.Player.Attack {
	
	[RequireComponent(typeof (SwordAttacker))]
	[RequireComponent(typeof (RangeAttacker))]
	[RequireComponent(typeof (MagicAttacker))]
	public class AttackController : MonoBehaviour {

		[SerializeField] public Actions actions;
	
			// Атакеры
		private IAttacker swordAttacker; // атакер мечём
		private IAttacker rangeAttacker; // атакер стрельбой
		private IAttacker magicAttacker; // атакер магией
		
		private IAttacker currentAttacker; // текущий атакер
		
		void Start(){
			
			swordAttacker = GetComponent<SwordAttacker>(); // Получаем компаненты аттакеров
			rangeAttacker = GetComponent<RangeAttacker>();
			magicAttacker = GetComponent<MagicAttacker>();
			
				swordAttacker.setActions(actions); // Передаём им контроллер над анимацией
				rangeAttacker.setActions(actions);
				magicAttacker.setActions(actions);
			
			currentAttacker = swordAttacker; // выбираем атакер по умолчанию
			
		}
		
		public void setAttacker(EAttacker attackerType){ // Меняем атакер по умолчанию
			switch(attackerType){
				case EAttacker.swordAttacker:
					currentAttacker=swordAttacker;
					break;
				case EAttacker.rangeAttacker:
					currentAttacker=rangeAttacker;
					break;
				case EAttacker.magicAttacker:
					currentAttacker=magicAttacker;
					break;
			}
		}
		
		public void startAttack(){ // посылаем команду атакеру "Атака"
			currentAttacker.attack();
		}
		
	}
	
}
