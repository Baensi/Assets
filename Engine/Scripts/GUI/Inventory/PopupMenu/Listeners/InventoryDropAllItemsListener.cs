using UnityEngine;
using Engine.EGUI.Inventory;

namespace Engine.EGUI.Inventory.PopupMenu {

	public class InventoryDropAllItemsListener {

		private UInventory inventory;
		private Camera mainCamera;

			public InventoryDropAllItemsListener() {
				inventory = SingletonNames.getInventory().GetComponent<UInventory>();
				mainCamera = SingletonNames.getMainCamera();
			}

		public void DropAllItem(Item selectedItem) {

			for(int i = 1; i <= selectedItem.getCount(); i++)
				createNewInstance(selectedItem.toGameObject());
			
			inventory.removeItem(selectedItem, false, true);

		}

		private void createNewInstance(GameObject instance) {
			Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width * 0.5f, Screen.height * 0.5f, 0f));
			GameObject obj = GameObject.Instantiate<GameObject>(instance);

			obj.transform.position = mainCamera.transform.position + (mainCamera.transform.forward * 2);
			obj.transform.rotation = Quaternion.Euler(UnityEngine.Random.Range(0, 360), UnityEngine.Random.Range(0, 360), UnityEngine.Random.Range(0, 360));

			Rigidbody pickedObjectRigidBody = obj.GetComponent<Rigidbody>();
			pickedObjectRigidBody.AddForce(ray.direction * 5f);
		}

	}

}
