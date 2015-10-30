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
		
		private EMovementType movementType;       // тип текущего перемещения

			// список контроллеров
		private IMovement ingroundMovement;   // реализация хотьбы по земле
		private IMovement inwaterMovement;    // реализация перемещения на поверхности воды
		private IMovement underwaterMovement; // реализация перемешения под водой

		private IMovement currentMovement;    // текущий класс перемещения

		// Устанавливает класс управляющий персонажем
		public void setMovementType(EMovementType movementType){

			if (this.movementType.Equals(movementType)) return;

			this.movementType=movementType;
			switch(movementType){
				case EMovementType.inground:
					currentMovement = ingroundMovement;
				break;
				case EMovementType.inwater:
					currentMovement = inwaterMovement;
				break;
				case EMovementType.underwater:
					currentMovement = underwaterMovement;
				break;
			}
		}

		/// <summary>
		/// Возвращает тип текущего передвижения
		/// </summary>
		/// <returns></returns>
		public EMovementType getMovementType() {
			return this.movementType;
		}

		/// <summary>
		/// Возвращает класс текущего перемещения
		/// </summary>
		/// <returns></returns>
		public IMovement getCurrentMovement() {
			return currentMovement;
		}

		void Start(){
			
			actions = playerHands.GetComponent<Actions>();
			
			movementType = EMovementType.inground;
			
					// инициализируем контроллеры движения
				ingroundMovement = gameObject.GetComponent<IngroundMovements>();
				if (ingroundMovement==null)
					ingroundMovement = gameObject.AddComponent<IngroundMovements>();
				ingroundMovement.setUp(actions, mouseLook, fovKick, headBob, jumpBob, attackController);

				underwaterMovement = gameObject.GetComponent<UnderwaterMovements>();
				if (underwaterMovement == null)
					underwaterMovement = gameObject.AddComponent<UnderwaterMovements>();
				underwaterMovement.setUp(actions, mouseLook, fovKick, headBob, jumpBob, attackController);

				inwaterMovement = gameObject.GetComponent<InwaterMovements>();
				if (inwaterMovement == null)
					inwaterMovement = gameObject.AddComponent<InwaterMovements>();
				inwaterMovement.setUp(actions, mouseLook, fovKick, headBob, jumpBob, attackController);

			currentMovement = ingroundMovement;
			
		}
		
			// вызываем метод update из класса
		void Update() {

			if (GameConfig.GameMode != GameConfig.MODE_GAME)
				return;

			if (currentMovement!=null)
				currentMovement.update();
		}

			// вызываем метод fixUpdate из класса
		void FixedUpdate() {

			if (GameConfig.GameMode != GameConfig.MODE_GAME)
				return;

			if (currentMovement != null)
				currentMovement.fixUpdate();
		}

	}
}
