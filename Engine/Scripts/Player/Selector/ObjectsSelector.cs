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

	public class ObjectsSelector : MonoBehaviour {

		[SerializeField] public Texture2D  backgroundCaptionTexture;
		[SerializeField] public float      selectRange;

		private SelectionGroup selected;

		private Camera         playerCamera;

		private Ray            ray;
		private GameObject     pickedObjectGameObject;
		private Rigidbody      pickedObjectRigidBody;

		private DoorPhysXController doorController;
		
		private Vector3        cursorPosition;

		private GUIController guiController;
		public AudioSource    audioSource;

		void OnEnable() {

		}

		void Start () {

			playerCamera = SingletonNames.getMainCamera();
			this.audioSource = gameObject.AddComponent<AudioSource>();

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

			if (Physics.Raycast(ray, out hitInfo, selectRange)) {

				MonoBehaviour obj = hitInfo.transform.GetComponent<MonoBehaviour>();

				if (selected.prevousObject != null)
					selected.prevousObject.setSelection(false);

				if (selected.selectType == SelectedType.IsDynamic)
					selected.prevousObject = selected.selectedObject;

				var dynamic = obj as IDynamicObject;

				if (dynamic != null) { // объект типа IDynamicObject
					selected.selectedObject = dynamic;
					selected.selectType = SelectedType.IsDynamic;

					if (OnUse(dynamic)) return;

					OnPickUp(dynamic);

					if (dynamic.getDisplayed().Equals(TextDisplayed.Displayed))
						dynamic.setSelection(true);

					return;
				}

				var door = obj as IDoor;

				if (door != null) {
					selected.selectedDoor = door;
					selected.selectType = SelectedType.IsDoor;
					doorController.update(door);
					return;
				}

				var lever = obj as ILever;

				if (lever != null) {
					selected.selectedLever = lever;
					selected.selectType = SelectedType.IsLever;
					doorController.update(lever);
					return;
				}

			}

			ResetSelected();

		}

		public void ResetSelected() {
			
			if (selected.prevousObject != null)
				selected.prevousObject.setSelection(false);

			selected.selectedLever=null;
			selected.selectedDoor=null;
			selected.selectedObject=null;
			
			if (selected.prevousObject != null && selected.selectType==SelectedType.IsDynamic)
				selected.prevousObject.setSelection(false);
			
			selected.prevousObject=null;

			selected.selectType = SelectedType.None;
		}

		private bool OnUse(IDynamicObject selectedObject) {

			if (CrossPlatformInputManager.GetButtonDown(SingletonNames.Input.USE)) {    // использовать
				IPickedType usedType = selectedObject as IPickedType;
				if (usedType != null)
					if (usedType.onPick()) {
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

		public bool isPickedObject() {
			return selected.selectType == SelectedType.IsDynamic && pickedObjectGameObject!=null;
		}

		public void OnPickDrop(float force) {

			if (pickedObjectRigidBody == null)
				return;

				pickedObjectRigidBody.AddForce(ray.direction * force);

			pickedObjectGameObject = null;
			pickedObjectRigidBody = null;
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

			if (selected.selectType == SelectedType.None)
				return;

			switch (selected.selectType) {
				case SelectedType.IsDynamic:
					guiController.getDynamicObjectGUIRenderer().printLabel(selected.selectedObject);
					break;
				case SelectedType.IsDoor:
					guiController.getDoorGUIRenderer().printLabel(selected.selectedDoor);
					break;
				case SelectedType.IsLever:
					guiController.getDoorGUIRenderer().printLabel(selected.selectedLever);
					break;
			}
			
		}

	}
}
