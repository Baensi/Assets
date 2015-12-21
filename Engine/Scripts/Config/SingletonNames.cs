using System;
using UnityEngine;

namespace Engine {

	public static class SingletonNames {

		public static class Constants {

			public const string PLAYER_OBJECT_NAME = "Player";
			public const string PLAYER_CAMERA_NAME = "PlayerCamera";
			
			public const string PLAYER_HEAD_NAME     = "Head";
			public const string PLAYER_BODY_NAME     = "Body";
			public const string PLAYER_HANDS_NAME    = "Hands";
			public const string PLAYER_FOOTS_NAME    = "Foots";
			public const string PLAYER_LEGGENS_NAME  = "Leggens";
			
			public const string PLAYER_ATTACK_NAME = "PlayerAttack";
			public const string PLAYER_STATE_NAME  = "PlayerState";
			public const string PLAYER_GUI_NAME    = "PlayerGUI";

			public static class GUI {

				public const string INVENTORY = "PlayerGUI/Inventory";
				
				public const string UI        = "PlayerUI";

			}

		}

		public static class Player {

			public static GameObject getPlayerBody() {
				return GameObject.Find(Constants.PLAYER_CAMERA_NAME+"/"+Constants.PLAYER_BODY_NAME);
			}

			public static GameObject getHead() {
				return GameObject.Find(Constants.PLAYER_CAMERA_NAME + "/" + Constants.PLAYER_HEAD_NAME);
			}

			public static GameObject getHands() {
				return GameObject.Find(Constants.PLAYER_CAMERA_NAME + "/" + Constants.PLAYER_HANDS_NAME);
			}

			public static GameObject getFoots() {
				return GameObject.Find(Constants.PLAYER_CAMERA_NAME + "/" + Constants.PLAYER_FOOTS_NAME);
			}

			public static GameObject getLeggens() {
				return GameObject.Find(Constants.PLAYER_CAMERA_NAME + "/" + Constants.PLAYER_LEGGENS_NAME);
			}

		}

		/// <summary>
		/// Набор слоёв
		/// </summary>
		public static class Layers {

			public const int DEFAULT        = 0x00;
			public const int TRANSPARENT_FX = 0x01;
			public const int IGNORE_RAYCAST = 0x02;
			public const int WATER          = 0x04;
			public const int UI             = 0x05;
			
			public const int PLAYER         = 0x09;

		}

		/// <summary>
		/// Набор команд ввода (клавиши, мышь, контроллеры)
		/// </summary>
		public static class Input {

			public const string ESC          = "Cancel";
			
			public const string USE          = "Use";
			public const string ATTACK1      = "Attack1";
			public const string ATTACK_MAGIC = "AttackMagic";
			public const string PICKUP       = "PickUp";
			public const string SITDOWN      = "Sitdown";
			public const string JUMP         = "Jump";
			
			public const string INVENTORY    = "Inventory";
			
			public const string NEXTPAGE     = "NextPage";
			public const string BACKPAGE     = "BackPage";
			
			public const string MOUSE_X      = "Mouse X";
			public const string MOUSE_Y      = "Mouse Y";
			
			public const string HORIZONTAL   = "Horizontal";
			public const string VERTICAL     = "Vertical";

		}

		public static GameObject getPlayer() {
			return GameObject.Find(Constants.PLAYER_OBJECT_NAME);
		}


        public static GameObject getInventory(){
			return GameObject.Find(Constants.GUI.INVENTORY);
		}

		/// <summary>
		/// Возвращает графический игровой интерфейс с панелями и барами жизни, энергии, маны и пр.
		/// </summary>
		/// <returns></returns>
		public static GameObject getUI() {
			return GameObject.Find(Constants.GUI.UI);
		}

		/// <summary>
		/// Возвращает графический интерфес управления - инвентарь, журнал заданий, книги и пр.
		/// </summary>
		/// <returns></returns>
		public static GameObject getGUI(){
			return GameObject.Find(Constants.PLAYER_GUI_NAME);
		}

		public static Camera getMainCamera() {
			return Camera.main;
		}

	}

}
