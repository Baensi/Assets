using UnityEngine;
using UnityEditor;
using System.Collections;

namespace Engine.Objects.Doors {

	/// <summary>
	/// Интерфейс двери
	/// </summary>
	public interface IDoor {

		string    getNameId();
		DoorState getState();
		string    getName();
		string    getCaption();
		string    getCaptionId();

		Vector3 getOpenedAngles();
		Vector3 getClosedAngles();

		DoorControllerType getControllerType();
		IGameObjectAnimation     getAnimator();

			void openDoor();
			void closeDoor();
			void lockDoor();

		TextDisplayed getTextDisplayed();

		GameObject toObject();

		void OnUpdate();

	}

}