using UnityEngine;
using System.Collections;

namespace Engine.Maps {

	public class CurrentLevel : MonoBehaviour {

		void Start() {

		}

		void Update() {

		}

			/// <summary>
			/// Загрузка уровня
			/// </summary>
			/// <param name="level"></param>
		void OnLevelWasLoaded(int level) {

			LevelData data    = LevelData.getInstance(); // достаём данные
			GameObject player = SingletonNames.getPlayer(); // ищем персонажа

				// позиционируем персонажа
			player.gameObject.transform.position = data.playerStartPosition;
			player.gameObject.transform.rotation = Quaternion.Euler(data.playerStartRotation);

		}

	}

}