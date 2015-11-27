using UnityEngine;
using Engine.Objects;

namespace Engine.Maps {

	public class CurrentLevel : MonoBehaviour {

		private LevelData data = null;

			void Start() {

			}

		void Update() {

		}

			/// <summary>
			/// Загрузка уровня
			/// </summary>
			/// <param name="level"></param>
		void OnLevelWasLoaded(int level) {

			data = LevelData.getInstance(); // достаём данные
			GameObject player = SingletonNames.getPlayer(); // ищем персонажа

				// позиционируем персонажа
			player.gameObject.transform.position = data.playerStartPosition;
			player.gameObject.transform.rotation = Quaternion.Euler(data.playerStartRotation);

			GameConfig.GameMode = GameConfig.MODE_GAME; // устанавливаем режим "игровой"

		}

	}

}