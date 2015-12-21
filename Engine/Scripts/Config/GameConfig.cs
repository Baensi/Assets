using System;
using UnityEngine;
using System.Collections.Generic;
using Engine.I18N;

namespace Engine {

	public struct GameConfig {

			/// <summary> Путь к файлу конфигураций</summary>
		private const string CONFIG_FILE = "./configuration.ini";

		public const string  CONFIG_CURRENT_LANGUAGE = "lang";

			/// <summary> Игровой режим - отключает курсор</summary>
		public const int MODE_GAME = 0x00;

			/// <summary> Режим GUI - включает курсор, игнорирует модуль Inground и камеру</summary>
		public const int MODE_GUI  = 0x01;

		private static string localization = "ru";
		private static int   gameMode     = MODE_GAME;

		private static Vector3 centerScreen = Vector3.zero;

		public static string Localization {
			get { return localization; }
			set {
				localization = value;
				CLang.getInstance().Reload();
			}
		}

		public static Vector3 CenterScreen {
			get { return centerScreen; }
			private set { }
		}

		public static void setCenterScreen(float x, float y) {
			centerScreen.x = x;
			centerScreen.y = y;
		}

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
