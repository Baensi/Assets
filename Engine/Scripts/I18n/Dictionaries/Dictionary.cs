using System;

namespace Engine.I18N {

	public static class Dictionary {

		public const string DictionaryI18nDirectoryName = "assets/resources/lang/";
		public const string DictionaryObjectsFileName   = "assets/resources/objects/data.xml";
		public const string DictionarySoundsFileName    = "assets/resources/sounds/sounds.xml";
		public const string DictionaryImagesFileName    = "assets/resources/images/images.xml";

		public const string K_COUNT = "count";
		
		public const string K_DOOR_OPENED = "door_state_opened";
		public const string K_DOOR_CLOSED = "door_state_closed";
		public const string K_DOOR_LOCKED = "door_state_locked";
		
		public const string K_LEVER_STATE1 = "lever_state_state1";
		public const string K_LEVER_STATE2 = "lever_state_state2";
		public const string K_LEVER_LOCKED = "lever_state_locked";

		public static string COUNT_TEXT;
		public static string DOOR_OPENED_TEXT;
		public static string DOOR_CLOSED_TEXT;
		public static string DOOR_LOCKED_TEXT;

		public static string LEVER_STATE1_TEXT;
		public static string LEVER_STATE2_TEXT;
		public static string LEVER_LOCKED_TEXT;

		public static void Reload() {
			COUNT_TEXT = CLang.getInstance().get(K_COUNT);

			DOOR_OPENED_TEXT = CLang.getInstance().get(K_DOOR_OPENED);
			DOOR_CLOSED_TEXT = CLang.getInstance().get(K_DOOR_CLOSED);
			DOOR_LOCKED_TEXT = CLang.getInstance().get(K_DOOR_LOCKED);

			LEVER_STATE1_TEXT = CLang.getInstance().get(K_LEVER_STATE1);
			LEVER_STATE2_TEXT = CLang.getInstance().get(K_LEVER_STATE2);
			LEVER_LOCKED_TEXT = CLang.getInstance().get(K_LEVER_LOCKED);
		}

	}
	
}