using System;
using UnityEngine;

namespace Engine {

	public static class SingletonNames {

		public static class Constants {

			public static string PLAYER_OBJECT_NAME = "Player";
			public static string PLAYER_CAMERA_NAME = "PlayerCamera";

			public static string PLAYER_HEAD_NAME     = "Head";
			public static string PLAYER_BODY_NAME     = "Body";
			public static string PLAYER_HANDS_NAME    = "Hands";
			public static string PLAYER_FOOTS_NAME    = "Foots";
			public static string PLAYER_LEGGENS_NAME  = "Leggens";

			public static string PLAYER_ATTACK_NAME = "PlayerAttack";
			public static string PLAYER_STATE_NAME  = "PlayerState";
			public static string PLAYER_GUI_NAME    = "PlayerGUI";

			public static class GUI {

				public static string INVENTORY = "PlayerGUI/Inventory";
                
				public static string HEALTH_BAR = "PlayerGUI";
				public static string MANA_BAR   = "PlayerGUI";
				public static string ENERGY_BAR = "PlayerGUI";

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

		public static class Input {

			public static string ESC          = "Cancel";

			public static string USE          = "Use";
			public static string ATTACK1      = "Attack1";
			public static string ATTACK_MAGIC = "AttackMagic";
			public static string PICKUP       = "PickUp";
			public static string SITDOWN      = "Sitdown";
			public static string JUMP         = "Jump";

			public static string INVENTAR     = "Inventar";

			public static string NEXTPAGE     = "NextPage";
			public static string BACKPAGE     = "BackPage";
			
			public static string MOUSE_X      = "Mouse X";
			public static string MOUSE_Y      = "Mouse Y";

			public static string HORIZONTAL   = "Horizontal";
			public static string VERTICAL     = "Vertical";

		}

		public static GameObject getPlayer() {
			return GameObject.Find(Constants.PLAYER_OBJECT_NAME);
		}


        public static GameObject getInventory(){
			return GameObject.Find(Constants.GUI.INVENTORY);
		}

		public static GameObject getGUI(){
			return GameObject.Find(Constants.PLAYER_GUI_NAME);
		}

		public static Camera getMainCamera() {
			return Camera.main;
		}

	}

}