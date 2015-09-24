using UnityEngine;
using System.Collections;
using Engine.I18N;
using Engine.Objects;

namespace Engine.Objects.Doors {

	public class DoorPushLever : MonoBehaviour, ILever  {

		private AudioSource audioSource;

		[SerializeField] public Vector3 state1Angles;
		[SerializeField] public Vector3 state2Angles;

		[SerializeField] public GameObjectAnimatorCollection leverAnimator;
		[SerializeField] public LeverSoundDataCollection     soundData;

		[SerializeField] public float speed = 1.0f;
		[SerializeField] public AnimationDirection direction;

		[SerializeField] public TextDisplayed textDisplayed = TextDisplayed.Displayed;
		[SerializeField] public string     idCode;
		[SerializeField] public string     captionId;
		[SerializeField] public LeverState state;

		[SerializeField] public bool mayBeState1ToState2 = true;
		[SerializeField] public bool mayBeState2ToState1 = true;

		[SerializeField] public bool locked = false;

		[SerializeField] public DefaultDoor door;

		private IGameObjectAnimation animator;
		private ILeverSoundData      sound;

		private LeverState prevState;
		private bool       turnLever = false;

		/// <summary>
		/// Возвращает идентификатор двери
		/// </summary>
		/// <returns>Уникальный код двери в строковом формате (нужен для идентификации и сравнения кода двери и кода ключа)</returns>
		public string getId() {
			return idCode;
		}

		/// <summary>
		/// Возвращает метку двери
		/// </summary>
		/// <returns>Метку из xml файла с идентификатором captionId</returns>
		public string getCaption() {
			return CLang.getInstance().get(captionId);
		}

		/// <summary>
		/// Возвращает статус рычага
		/// </summary>
		/// <returns>Статус рычага - состояние1/состояние2</returns>
		public LeverState getState() {
			return state;
		}

		public bool isLocked() {
			return locked;
		}

		/// <summary>
		/// Возвращает дверь, которой манипулирует этот рычаг
		/// </summary>
		/// <returns></returns>
		public IDoor getDoor() {
			return (door as IDoor);
		}

		public GameObject toObject() {
			return this.gameObject;
		}

		public void leverState1() {

			if (state == LeverState.State1 || !mayBeState2ToState1) return;

			prevState = state;
			state = LeverState.State1;

#if UNITY_EDITOR
			if (audioSource == null) return;
#endif

			audioSource.clip = sound.leverState1Sound();
			audioSource.Play();
		}

		public void leverState2() {

			if (state == LeverState.State2 || !mayBeState1ToState2) return;

			prevState = state;
			state = LeverState.State2;

#if UNITY_EDITOR
			if (audioSource == null) return;
#endif

			audioSource.clip = sound.leverState2Sound();
			audioSource.Play();
		}

		public TextDisplayed getTextDisplayed() {
			return textDisplayed;
		}

		public Vector3 getState1Angles() {
			return state1Angles;
		}

		public Vector3 getState2Angles() {
			return state2Angles;
		}

#if UNITY_EDITOR

		public void OnStart() {
			if (animator == null || animator != GameObjectAnimatorCollectionConverter.getAnimator(leverAnimator))
				animator = GameObjectAnimatorCollectionConverter.getAnimator(leverAnimator);

			if (sound == null)
				sound = LeverSoundDataCollectionConverter.getData(soundData);
		}

		public bool IsEnd() {
			switch (state) {
				case LeverState.State1:
					return this.transform.localEulerAngles.Equals(state1Angles);
				case LeverState.State2:
					return this.transform.localEulerAngles.Equals(state2Angles);
				default:
					return false;
			}
		}

#endif

		void Start() {

			audioSource = gameObject.GetComponent<AudioSource>();

			if (audioSource == null)
				audioSource = gameObject.AddComponent<AudioSource>();

			Rigidbody body = gameObject.GetComponent<Rigidbody>();

			if (body == null)
				body = gameObject.AddComponent<Rigidbody>();

			body.useGravity = false;
			body.isKinematic = true;

			BoxCollider boxCollider = gameObject.GetComponent<BoxCollider>();
			if (boxCollider == null)
				boxCollider = gameObject.AddComponent<BoxCollider>();

			animator = GameObjectAnimatorCollectionConverter.getAnimator(leverAnimator);
			sound    = LeverSoundDataCollectionConverter.getData(soundData);
		}

		public IGameObjectAnimation getAnimator() {
			return animator;
		}

		public void OnUpdate() {

			// Режим в эдиторе
#if UNITY_EDITOR
			OnStart();
#endif

			int directionValue = direction == AnimationDirection.DirectionPositive ? 1 : -1;

			switch (state) {
				case LeverState.State1:

					animator.update(this.gameObject, state1Angles, directionValue, speed);

					if (turnLever && animator.isComplete(this.gameObject, state1Angles, directionValue, speed)) {
						door.openDoor();
						turnLever = false;
					}

					break;
				case LeverState.State2:

					animator.update(this.gameObject, state2Angles, directionValue, speed);

					if (turnLever && animator.isComplete(this.gameObject, state2Angles, directionValue, speed)) {
						door.closeDoor();
						turnLever = false;
					}

					break;
			}


			if (prevState != state)
				turnLever = true;

			prevState = state;
			

		}

		void Update() {
			OnUpdate();
		}
		

	}

}
