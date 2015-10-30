using UnityEngine;
using System.Collections.Generic;

namespace Engine.Maps {

	public class LevelData {

		private static LevelData instance;

		public Vector3 playerStartPosition = Vector3.zero;
		public Vector3 playerStartRotation = Vector3.zero;

		public void putPlayerData(Vector3 playerStartPosition, Vector3 playerStartRotation) {
			this.playerStartPosition = playerStartPosition;
			this.playerStartRotation = playerStartRotation;
		}

		public static LevelData getInstance() {
			if (instance == null)
				instance = new LevelData();
			return instance;
		}

	}

}