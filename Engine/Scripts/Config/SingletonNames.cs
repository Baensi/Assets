using System;
using UnityEngine;

namespace Engine {

	public static class SingletonNames {

		public static class PlayerObjectsConstants {

			public static string PLAYER_OBJECT_NAME = "Player";
			public static string PLAYER_HANDS_NAME  = "PlayerHands";
			public static string PLAYER_ATTACK_NAME = "PlayerAttack";
			public static string PLAYER_STATE_NAME  = "PlayerState";
			public static string PLAYER_GUI_NAME    = "PlayerGUI";
		
		}

		public static class Input {

			public static string USE          = "Use";
			public static string ATTACK1      = "Attack1";
			public static string ATTACK_MAGIC = "AttackMagic";
			public static string PICKUP       = "PickUp";
			public static string SITDOWN      = "Sitdown";
			public static string JUMP         = "Jump";

			public static string NEXTPAGE     = "NextPage";
			public static string BACKPAGE     = "BackPage";
			
			public static string MOUSE_X      = "Mouse X";
			public static string MOUSE_Y      = "Mouse Y";

			public static string HORIZONTAL   = "Horizontal";
			public static string VERTICAL     = "Vertical";

		}

		public static GameObject getPlayer() {
			return GameObject.Find(PlayerObjectsConstants.PLAYER_OBJECT_NAME);
		}
		
		public static GameObject getGUI(){
			return GameObject.Find(PlayerObjectsConstants.PLAYER_GUI_NAME);
		}

		public static Camera getMainCamera() {
			return Camera.main;
		}

	}

}