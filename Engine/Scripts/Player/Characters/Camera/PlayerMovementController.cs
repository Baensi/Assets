using System;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using UnityStandardAssets.Utility;
using Random = UnityEngine.Random;

using Engine.Player.Movement.Movements;
using Engine.Player.Animations;
using Engine.Player.Attack;

namespace Engine.Player.Movement {
	
	[RequireComponent(typeof (CharacterController))]
	[RequireComponent(typeof (MovementAudioData))]
	public class PlayerMovementController : MonoBehaviour {

		[SerializeField] public GameObject         playerHands;
		[SerializeField] public MouseLook          mouseLook;
		[SerializeField] public AttackController   attackController;

		[SerializeField] public FOVKick            fovKick = new FOVKick();
		[SerializeField] public CurveControlledBob headBob = new CurveControlledBob();
		[SerializeField] public LerpControlledBob  jumpBob = new LerpControlledBob();

		private Actions       actions;
		
		private EMovementType movementType; // ��� �������� �����������
		private IMovement     ingroundMovement;   // ���������� ������ �� �����
		private IMovement     underwaterMovement; // ���������� ����������� ��� �����
		private IMovement     currentMovement; // ������� ����� �����������

		// ������������� ����� ����������� ����������
		public void setMovementType(EMovementType movementType){

			if (this.movementType.Equals(movementType)) return;

			this.movementType=movementType;
			switch(movementType){
				case EMovementType.inground:
					currentMovement = ingroundMovement;
				break;
				case EMovementType.underwater:
					currentMovement = underwaterMovement;
				break;
			}
		}

		public EMovementType getMovementType() {
			return this.movementType;
		}

		public IMovement getCurrentMovement() {
			return currentMovement;
		}

		void Start(){
			
			actions				= playerHands.GetComponent<Actions>();
			
			movementType = EMovementType.inground;
			
				ingroundMovement = gameObject.AddComponent<IngroundMovements>();
				ingroundMovement.setUp(actions, mouseLook, fovKick, headBob, jumpBob, attackController);

				underwaterMovement = gameObject.AddComponent<UnderwaterMovements>();
				underwaterMovement.setUp(actions, mouseLook, fovKick, headBob, jumpBob, attackController);

			currentMovement = ingroundMovement;
			
		}
		
			// �������� ����� update �� ������
		void Update() {

			if (GameConfig.GameMode != GameConfig.MODE_GAME) return;

			if (currentMovement!=null)
				currentMovement.update();
		}

			// �������� ����� fixUpdate �� ������
		void FixedUpdate() {

			if (GameConfig.GameMode != GameConfig.MODE_GAME) return;

			if (currentMovement != null)
				currentMovement.fixUpdate();
		}

	}
}
