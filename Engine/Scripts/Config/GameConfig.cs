using System;
using UnityEngine;
using System.Collections.Generic;

namespace Engine {

	public struct GameConfig {

			/// <summary> Путь к файлу конфигураций</summary>
		private const string CONFIG_FILE = "./configuration.ini";

		public const string  CONFIG_CURRENT_LANGUAGE = "lang";

			/// <summary> Игровой режим - отключает курсор</summary>
		public const int MODE_GAME = 0x00;

			/// <summary> Режим GUI - включает курсор, игнорирует модуль Inground и камеру</summary>
		public const int MODE_GUI  = 0x01;

		public static string Localization = "ru";
		private static int   gameMode     = MODE_GAME;

			/// <summary>Устанавливает текущий режим</summary>
		public static int GameMode {

			get { return gameMode; }
			set {

				gameMode = value;

				switch (gameMode) {
					case MODE_GAME:
						Cursor.visible = false;
						break;
					case MODE_GUI:
						Cursor.visible = true;
						break;
				}

			}

		}


		public static void Init() {
			ConfigReader reader = new ConfigReader();
			reader.readConfig(CONFIG_FILE);
		}
			

	}

}
