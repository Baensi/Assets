using System;
using UnityEngine;

namespace Engine.Player {

	public class PickUpObject {

		private static PickUpObject instance;
		private Camera playerCamera;

		public static PickUpObject getInstance() {
			if (instance == null)
				instance = new PickUpObject();
			return instance;
		}

		public PickUpObject() {

			playerCamera = SingletonNames.getMainCamera();

		}

		public void pickUpObject(GameObject body, Ray ray) {

			

		}

	}

}
