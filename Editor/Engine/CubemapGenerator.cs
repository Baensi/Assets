using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

public class CubemapGenerator : ScriptableWizard {

	[SerializeField] public Camera cameraViewPort;
	[SerializeField] public Cubemap resultCubemap;

	void OnWizardCreate() {
		helpString = "Выберите объект относительно которого надо создать Cubemap";
		isValid = (Selection.activeGameObject != null) && (resultCubemap != null);
	}

	void OnWizardUpdate() {

	}

	[MenuItem("GameObject/Сгенерировать Cubemap")]
	static void CreateWizard () {
		ScriptableWizard.DisplayWizard<CubemapGenerator>("Создание Cubemap префаба", "Закрыть", "Генерировать");
	}

	void OnWizardOtherButton() {
		GameObject gameObject = Selection.activeGameObject;
		if (gameObject == null || resultCubemap == null) return;

		Vector3    tmpPos = cameraViewPort.transform.position;
		Quaternion tmpRot = cameraViewPort.transform.rotation;

		GameObject hands = GameObject.Find("PlayerHands");
		hands.SetActive(false);


		SortedDictionary<Camera, bool> visibles = new SortedDictionary<Camera, bool>();
		foreach (Camera c in Camera.allCameras) {
			if (c != cameraViewPort) {
				visibles.Add(c, c.enabled);
				c.enabled = false;
			}
		}

		cameraViewPort.transform.position = gameObject.transform.position;
		cameraViewPort.transform.rotation = gameObject.transform.rotation;
		cameraViewPort.RenderToCubemap(resultCubemap);

		foreach (Camera c in Camera.allCameras)
			c.enabled = visibles[c];

		visibles.Clear();

		hands.SetActive(true);
		cameraViewPort.transform.position = tmpPos;
		cameraViewPort.transform.rotation = tmpRot;
	}


}

