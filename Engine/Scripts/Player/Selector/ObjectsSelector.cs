﻿using UnityEngine;
using UnityEditor;
using System;
using System.Collections.Generic;
using Engine.Objects;
using Engine;
using Engine.EGUI;
using Engine.EGUI.Inventory;
using Engine.Player.Torch;
using Engine.Player.Torch.Burn;
using Engine.Objects.Doors;
using Engine.Objects.Types;
using UnityStandardAssets.CrossPlatformInput;

namespace Engine.Player {

	public class ObjectsSelector : MonoBehaviour {
		
		[SerializeField] public float selectDistantion = 1.5f;

		private SelectionGroup selected;

		private Camera         playerCamera;

		private Ray            ray;
		private GameObject     pickedObjectGameObject;
		private Rigidbody      pickedObjectRigidBody;

		private DoorController doorController;
		
		private Vector3        cursorPosition;

		private GUIController guiController;
		private AudioSource   audioSource;

		private UInventory playerInventory;

			void OnEnable() {

			}

		void Start () {

			playerCamera = SingletonNames.getMainCamera();
			playerInventory = SingletonNames.getInventory().GetComponent<UInventory>();

            this.audioSource = gameObject.AddComponent<AudioSource>();

			doorController = new DoorController();

			guiController = SingletonNames.getGUI().GetComponent<GUIController>();

		}

		void Update () {

			// центр экрана
			cursorPosition = GameConfig.CenterScreen;

			RaycastHit hitInfo = new RaycastHit();
			ray = Camera.main.ScreenPointToRay(cursorPosition);

			OnPickMove();

			if (Physics.Raycast(ray, out hitInfo, selectDistantion)) {

				MonoBehaviour obj = hitInfo.transform.GetComponent<MonoBehaviour>();

				var dynamic = obj as IDynamicObject;

				if (selected.selectedObject != null && dynamic != null && selected.selectedObject != dynamic) {
					selected.selectedObject.setSelection(false);
				}

				if (dynamic != null) { // объект типа IDynamicObject
					selected.selectedObject = dynamic;
					selected.selectType = SelectedType.IsDynamic;

					if (OnUse(dynamic))
						return;

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

				var inventory = obj as IExternalInventory;

				if (inventory != null) {
					selected.selectedInventory = inventory;
					selected.selectType = SelectedType.IsInventory;

					OnUse(inventory);

					return;
				}

			}

			ResetSelected();

		}

		public void ResetSelected() {
			
			if (selected.selectedObject != null)
				selected.selectedObject.setSelection(false);

			selected.selectedLever=null;
			selected.selectedDoor=null;
			
			if (selected.selectedObject != null && selected.selectType==SelectedType.IsDynamic)
				selected.selectedObject.setSelection(false);
			
			selected.selectedObject = null;
			selected.selectType = SelectedType.None;
		}

		private bool OnUse(IDynamicObject selectedObject) {

			if (CrossPlatformInputManager.GetButtonDown(SingletonNames.Input.USE)) {    // использовать
				IPickedType pickedType = selectedObject as IPickedType;
				if (pickedType != null)
					if (pickedType.onPick()) {
						ResetSelected();
						return true;
					}
			}

			return false;

		}

		private bool OnUse(IExternalInventory selectedInventory) {

			if(playerInventory.isVisible())
				return false;

			if (CrossPlatformInputManager.GetButtonDown(SingletonNames.Input.USE)) {    // использовать
				if (!playerInventory.isVisible()) {
					playerInventory.show(selectedInventory);
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
				case SelectedType.IsInventory:
					guiController.getInventoryGUIRenderer().printLabel(selected.selectedInventory);
					break;
			}
			
		}

	}
}
