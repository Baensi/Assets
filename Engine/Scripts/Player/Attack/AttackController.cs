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
	
			// �������
		private IAttacker swordAttacker; // ������ �����
		private IAttacker rangeAttacker; // ������ ���������
		private IAttacker magicAttacker; // ������ ������
		
		private IAttacker currentAttacker; // ������� ������
		
		void Start(){
			
			swordAttacker = GetComponent<SwordAttacker>(); // �������� ���������� ���������
			rangeAttacker = GetComponent<RangeAttacker>();
			magicAttacker = GetComponent<MagicAttacker>();
			
				swordAttacker.setActions(actions); // ������� �� ���������� ��� ���������
				rangeAttacker.setActions(actions);
				magicAttacker.setActions(actions);
			
			currentAttacker = swordAttacker; // �������� ������ �� ���������
			
		}
		
		public void setAttacker(EAttacker attackerType){ // ������ ������ �� ���������
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
		
		public void startAttack(){ // �������� ������� ������� "�����"
			currentAttacker.attack();
		}
		
	}
	
}
