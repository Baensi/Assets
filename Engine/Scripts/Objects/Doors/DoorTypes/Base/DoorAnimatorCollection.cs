using UnityEngine;
using System.Collections;

namespace Engine.Objects.Doors {

	public enum GameObjectAnimatorCollection : int {
		Rotation = 0x00,
		Position = 0x01
	};

	public static class GameObjectAnimatorCollectionConverter {

		public static IGameObjectAnimation getAnimator(GameObjectAnimatorCollection type){
			switch(type){
				case GameObjectAnimatorCollection.Rotation: return RotationGameObjectAnimation.getInstance();
				case GameObjectAnimatorCollection.Position: return PositionGameObjectAnimator.getInstance();
				default : return RotationGameObjectAnimation.getInstance();
			}
		}

	}

}