using UnityEngine;
using UnityEditor;
using System;
using System.Collections.Generic;
using Engine.Objects;
using Engine;
using Engine.EGUI;
using Engine.Player.Torch;
using Engine.Player.Torch.Burn;
using Engine.Objects.Doors;
using Engine.Objects.Types;
using UnityStandardAssets.CrossPlatformInput;

namespace Engine.Player {

	public enum SelectedType : int {

		IsDynamic = 0x00, 
		IsDoor    = 0x01,
		IsLever   = 0x02

	};

	public class ObjectsSelector : MonoBehaviour {

		[SerializeField] public Texture2D  backgroundCaptionTexture;
		[SerializeField] public float      selectRange;

		private SelectedType   selectType;
		private MonoBehaviour  selectVariant  = null;
		private IDynamicObject prevousObject;

		private Camera         playerCamera;

		private Ray            ray;
		private GameObject     pickedObjectGameObject;
		private Rigidbody      pickedObjectRigidBody;

		private DoorPhysXController doorController;
		
		private Vector3        cursorPosition;

		private GUIController guiController;

		void Start () {

			playerCamera = SingletonNames.getMainCamera();
			AudioSource audioSource = gameObject.AddComponent<AudioSource>();

				// центр экрана
			cursorPosition = new Vector3(Screen.width/2.0f,
			                             Screen.height/2.0f,
			                             0.0f);
			
			doorController = new DoorPhysXController();

			guiController = SingletonNames.getGUI().GetComponent<GUIController>();

		}

		void Update () {

			RaycastHit hitInfo = new RaycastHit();
			ray = Camera.main.ScreenPointToRay(cursorPosition);

			OnPickMove();

			if (Physics.Raycast(ray, out hitInfo, selectRange, 1)) {

				MonoBehaviour obj = hitInfo.transform.GetComponent<MonoBehaviour>();

				if (prevousObject != null)
					prevousObject.setSelection(false);
				
				if(selectType==SelectedType.IsDynamic)
					prevousObject = selectVariant as IDynamicObject;

				var dynamic = obj as IDynamicObject;

				if (dynamic != null) { // объект типа IDynamicObject
					selectVariant = obj;
					selectType = SelectedType.IsDynamic;

					if (OnUse(dynamic)) return;

						OnPickUp(dynamic);

						if (dynamic.getDisplayed().Equals(TextDisplayed.Displayed))
							dynamic.setSelection(true);

					return;
				} 

				var door = obj as IDoor;

				if(door!=null){
					selectVariant = obj;
					selectType = SelectedType.IsDoor;
					doorController.update(door);
					return;
				}

				var lever = obj as ILever;

				if (lever != null) {
					selectVariant = obj;
					selectType = SelectedType.IsLever;
					doorController.update(lever);
					return;
				}

				selectVariant = null;
	
			}
		}

		public void ResetSelected() {
			if (prevousObject != null)
				prevousObject.setSelection(false);
			prevousObject  = null;

			if (selectVariant != null && selectType==SelectedType.IsDynamic)
				(selectVariant as IDynamicObject).setSelection(false);

			selectVariant  = null;
		}

		private bool OnUse(IDynamicObject selectedObject) {

			if (CrossPlatformInputManager.GetButtonDown(SingletonNames.Input.USE)) {    // использовать
				IUsedType usedType = selectedObject as IUsedType;
				if (usedType != null)
					if (usedType.onUse()) {
						ResetSelected();
						return true;
					}
			}

			return false;

		}

		private void OnPickMove() {

			if (pickedObjectRigidBody != null) {

				pickedObjectGameObject.transform.position = playerCamera.transform.position + ray.direction * 1;
				pickedObjectRigidBody.velocity = Vector3.zero;

			}

		}

		private void OnPickUp(IDynamicObject selectedObject) {

			if (CrossPlatformInputManager.GetButtonDown(SingletonNames.Input.PICKUP)) {

				if (pickedObjectGameObject == null) {

					if (selectedObject != null) {
						pickedObjectGameObject = selectedObject.toObject();
						pickedObjectRigidBody  = pickedObjectGameObject.GetComponent<Rigidbody>();
					}

				} else {

					pickedObjectGameObject = null;
					pickedObjectRigidBody = null;

				}

			}

		}

		void OnGUI(){

			if (!guiController.isInitGUIState()) {
				guiController.getDynamicObjectGUIRenderer().initStyles(backgroundCaptionTexture);
				guiController.getDoorGUIRenderer().initStyles(backgroundCaptionTexture);
				guiController.setInitGUIState(true);
			}

			if (selectVariant == null)
				return;

			switch(selectType){
				case SelectedType.IsDynamic:
					guiController.getDynamicObjectGUIRenderer().printLabel(selectVariant as IDynamicObject);
					break;
				case SelectedType.IsDoor:
					guiController.getDoorGUIRenderer().printLabel(selectVariant as IDoor);
					break;
				case SelectedType.IsLever:
					guiController.getDoorGUIRenderer().printLabel(selectVariant as ILever);
					break;
			}
			
		}

	}
}
