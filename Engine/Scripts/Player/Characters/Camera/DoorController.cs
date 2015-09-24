using UnityEngine;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;
using Engine.Objects.Doors;

namespace Engine.Player {

	public class DoorPhysXController {

		private Vector3 push;
		private Vector3 direct;

		private float horizontal;
		private float vertical;

		public DoorPhysXController(){
			
		}
		
		public void update(IDoor obj) {

			if (obj.getControllerType() != DoorControllerType.Standalone) return;

			if(CrossPlatformInputManager.GetButtonDown("Use")){

				switch(obj.getState()){
					case DoorState.Closed:
						obj.openDoor();
						break;
					case DoorState.Locked:

						break;
					case DoorState.Opened:
						obj.closeDoor();
						break;
				}


			}
		}

		public void update(ILever obj) {

			if (obj.isLocked()) return;

			if (CrossPlatformInputManager.GetButtonDown("Use")) {

				switch (obj.getState()) {
					case LeverState.State1:
						obj.leverState2();
						break;
					case LeverState.State2:
						obj.leverState1();
						break;
				}

			}
		}

	}

}