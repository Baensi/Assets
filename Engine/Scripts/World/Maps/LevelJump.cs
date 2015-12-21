using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;
using System.Collections;

namespace Engine.Maps {

	[RequireComponent(typeof(BoxCollider))]
	public class LevelJump : MonoBehaviour {

		[SerializeField] public string  sceneName;
		[SerializeField] public Vector3 playerStartPosition;
		[SerializeField] public Vector3 playerStartRotation;

		private bool       errorState = false;
		private GameObject player;

		void Start() {
			player = SingletonNames.getPlayer();
		}

		void OnValidate() {

			foreach (UnityEditor.EditorBuildSettingsScene scene in UnityEditor.EditorBuildSettings.scenes) {

				string name = scene.path.Substring(scene.path.LastIndexOf('/')+1);

				if (name.Equals(sceneName+".unity")) {
					errorState = false;
					return;
				}

			}

			errorState = true;
			Debug.LogError("Уровень '" + sceneName+"' не найден!");

		}

		void Update() {

#if UNITY_EDITOR

			if(errorState)
				Debug.LogError("Уровень '" + sceneName+"' не найден!");

#endif

		}

		void OnTriggerEnter(Collider other) {

			if (other.gameObject != player)
				return;

			LevelData.getInstance().putPlayerData(playerStartPosition,playerStartRotation);

			SceneManager.LoadScene(sceneName);
		}

	}

}