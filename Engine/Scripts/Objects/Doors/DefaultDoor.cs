using UnityEngine;
using System.Collections;
using Engine.I18N;
using Engine.Objects;

namespace Engine.Objects.Doors {

	public class DefaultDoor : MonoBehaviour, IDoor {

		private AudioSource audioSource;
		
		[SerializeField]public Vector3 openedAngles;
		[SerializeField]public Vector3 closedAngles;

		[SerializeField]public GameObjectAnimatorCollection  doorAnimator;
		[SerializeField]public DoorSoundDataCollection soundData;

		[SerializeField]public float speed = 1.0f;
		[SerializeField]public AnimationDirection direction;

		[SerializeField]public DoorControllerType controllerType = DoorControllerType.Standalone;
		[SerializeField]public TextDisplayed      textDisplayed  = TextDisplayed.Displayed;
		[SerializeField]public string             idName;
		[SerializeField]public string             idCaption;
		[SerializeField]public DoorState          state;

		private string doorName;
		private string doorCaption;

		private IGameObjectAnimation animator;
		private IDoorSoundData sound;

		/// <summary>
		/// Возвращает идентификатор двери
		/// </summary>
		/// <returns>Уникальный код двери в строковом формате (нужен для идентификации и сравнения кода двери и кода ключа)</returns>
		public string getNameId(){
			return idName;
		}

		public string getCaptionId() {
			return idCaption;
		}

		public string getName() {
			return doorName;
		}

		public string getCaption() {
			return doorCaption;
		}

		/// <summary>
		/// Возвращает статус двери
		/// </summary>
		/// <returns>Статус двери - открыта/закрыта/закрыта на ключ</returns>
		public DoorState getState(){
			return state;
		}

		public GameObject toObject(){
			return this.gameObject;
		}

		public DoorControllerType getControllerType() {
			return controllerType;
		}

		public void openDoor(){

			if (state == DoorState.Opened) return;

			state = DoorState.Opened;

#if UNITY_EDITOR
			if (audioSource == null) return;
#endif
				
			audioSource.clip = sound.openDoorSound();
			audioSource.Play();
		}

		public void closeDoor(){

			if (state == DoorState.Closed) return;

			state = DoorState.Closed;

#if UNITY_EDITOR
			if (audioSource == null) return;
#endif

			audioSource.clip = sound.closeDoorSound();
			audioSource.Play();
		}

		public void lockDoor(){

			if (state == DoorState.Locked) return;

			state = DoorState.Locked;

#if UNITY_EDITOR
			if (audioSource == null) return;
#endif

			audioSource.clip = sound.lockDoorSound();
			audioSource.Play();
		}

		public TextDisplayed getTextDisplayed() {
			return textDisplayed;
		}

		public Vector3 getOpenedAngles(){
			return openedAngles;
		}

		public Vector3 getClosedAngles(){
			return closedAngles;
		}

		#if UNITY_EDITOR

				public void OnStart() {
					if (animator == null || animator!=GameObjectAnimatorCollectionConverter.getAnimator(doorAnimator))
						animator = GameObjectAnimatorCollectionConverter.getAnimator(doorAnimator);

					if (sound == null)
						sound = DoorSoundDataCollectionConverter.getData(soundData);
				}

				public bool IsEnd() {
					switch (state) {
						case DoorState.Opened:
							return this.transform.localEulerAngles.Equals(openedAngles);
						case DoorState.Closed:
							return this.transform.localEulerAngles.Equals(closedAngles);
						case DoorState.Locked:
							return this.transform.localEulerAngles.Equals(closedAngles);
						default:
							return false;
					}
				}

		#endif

		void Start () {

			doorName = CLang.getInstance().get(idName);
			doorCaption = CLang.getInstance().get(idCaption);

			if (gameObject.layer != 0)
				gameObject.layer = 0;

			audioSource = gameObject.GetComponent<AudioSource>();

			if (audioSource==null)
				audioSource = gameObject.AddComponent<AudioSource>();

			Rigidbody body = gameObject.GetComponent<Rigidbody>();
			
			if(body==null)
				body = gameObject.AddComponent<Rigidbody>();

			body.useGravity  = false;
			body.isKinematic = true;

			BoxCollider boxCollider = gameObject.GetComponent<BoxCollider>();
			if(boxCollider==null)
				boxCollider = gameObject.AddComponent<BoxCollider>();

			animator = GameObjectAnimatorCollectionConverter.getAnimator(doorAnimator);
			sound = DoorSoundDataCollectionConverter.getData(soundData);
		}

		public IGameObjectAnimation getAnimator(){
			return animator;
		}

		public void OnUpdate() {

				// Режим в эдиторе
			#if UNITY_EDITOR
				OnStart();
			#endif

			int directionValue = direction == AnimationDirection.DirectionPositive ? 1 : -1;

			if (state.Equals(DoorState.Opened))
				animator.update(this.gameObject, openedAngles, directionValue, speed);

			if (state.Equals(DoorState.Closed))
				animator.update(this.gameObject, closedAngles, directionValue, speed);
		}

		void Update () {
			OnUpdate();
		}
		
	}

}