using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using Engine.Objects.Doors;

namespace Engine.Player {

	public class DoorController {
		
		public DoorController(){
			
		}
		
		public void update(IDoor obj) {

			if (obj.getControllerType() != DoorControllerType.Standalone)
				return;

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