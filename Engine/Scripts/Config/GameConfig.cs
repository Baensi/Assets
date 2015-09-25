using System;
using System.Collections.Generic;

namespace Engine {

	public struct GameConfig {

		private static string CONFIG_FILE = "./configuration.ini";

		public static string  CONFIG_CURRENT_LANGUAGE = "lang";

		public static int MODE_GAME = 0x00;
		public static int MODE_GUI  = 0x01;

		public static string Localization = "ru";
		public static int    GameMode     = MODE_GAME;

		public static void Init() {
			ConfigReader reader = new ConfigReader();
			reader.readConfig(CONFIG_FILE);
		}
			

	}

}
