using System;
using System.Collections.Generic;
using UnityEngine;
using Engine.Player.Animations;
using Engine.Objects.Weapon;

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

		private WeaponTypes currentAttackerType;
		private IAttacker   currentAttacker; // текущий атакер
		
		void Start(){
			
			swordAttacker = GetComponent<SwordAttacker>(); // Получаем компаненты аттакеров
			rangeAttacker = GetComponent<RangeAttacker>();
			magicAttacker = GetComponent<MagicAttacker>();
			
				swordAttacker.setActions(actions); // Передаём им контроллер над анимацией
				rangeAttacker.setActions(actions);
				magicAttacker.setActions(actions);
			
			currentAttacker = swordAttacker; // выбираем атакер по умолчанию
			
		}
		
		/// <summary>
		/// Устанавливает текущий атакер по типу оружия
		/// </summary>
		/// <param name="attackerType"></param>
		public void setAttacker(WeaponTypes attackerType){ // Меняем атакер по умолчанию
			currentAttackerType=attackerType;
			switch(attackerType){
				case WeaponTypes.Sword:
					currentAttacker=swordAttacker;
					break;
				case WeaponTypes.Range:
					currentAttacker=rangeAttacker;
					break;
				case WeaponTypes.Magic:
					currentAttacker=magicAttacker;
					break;
			}
		}

		public WeaponTypes getCurrentAttackerType() {
			return currentAttackerType;
		}

		public IAttacker getCurrentAttacker() {
			return currentAttacker;
		}
		
		public void startAttack(){ // посылаем команду атакеру "Атака"

			if (GamePlayer.Cloth.weapon == null) // если игрок безоружен - выходим
				return;

			if (GamePlayer.states > GamePlayer.Cloth.weapon.getAttackRequireStates()) { // проверяем, можно ли наносить удар

				GamePlayer.states -= GamePlayer.Cloth.weapon.getAttackRequireStates(); // тратим статы на удар
				currentAttacker.attack(); // выполняем анимацию

			}

        }
		
	}
	
}
