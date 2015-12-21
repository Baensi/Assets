using UnityEngine;
using UnityEditor;
using System.Collections;

namespace Engine.Objects.Doors {

	/// <summary>
	/// Интерфейс рычага
	/// </summary>
	public interface ILever {

		string     getNameId();
		LeverState getState();
		string     getCaptionId();

		string getName();
		string getCaption();

		Vector3 getState1Angles();
		Vector3 getState2Angles();

		IGameObjectAnimation getAnimator();

		void leverState1();
		void leverState2();

		TextDisplayed getTextDisplayed();

		bool       isLocked();
		IDoor      getDoor();
		GameObject toObject();

		void OnUpdate();

	}

}
