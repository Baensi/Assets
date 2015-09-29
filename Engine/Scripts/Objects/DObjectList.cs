using System;
using UnityEngine;
using Engine.EGUI.Inventory;

namespace Engine.Objects {

	public static class DObjectList {

			// предметы в инвентаре
		public static class Items {

			public static Item HealthPotion = new Item().Create(
				Resources.Load<GameObject>("Objects/health_potion"),
				Resources.Load<Texture2D>("Objects/health_potion_icon"),
				new ItemSize(1, 1), 5, new ItemDescription().Create(S_Health_Potion, "healthpotion_id_name", "healthpotion_id_caption", 1f)
			);

			public static Item ManaPotion   = new Item().Create(
				Resources.Load<GameObject>("Objects/mana_potion"),
				Resources.Load<Texture2D>("Objects/mana_potion_icon"),
				new ItemSize(1, 1), 5, new ItemDescription().Create(S_Mana_Potion, "manapotion_id_name", "manapotion_id_caption", 1.1f)
			);

			public static class Food {

				public static Item RawBread    = new Item().Create(
					Resources.Load<GameObject>("Objects/Food/raw_bread"),
					Resources.Load<Texture2D>("Objects/Food/raw_bread_icon"),
					new ItemSize(1, 1), 1, new ItemDescription().Create(S_Raw_Bread, "rawbread_id_name", "rawbread_id_caption", 0.1f)
				);
				public static Item RawHam      = new Item().Create(
					Resources.Load<GameObject>("Objects/Food/raw_ham"),
					Resources.Load<Texture2D>("Objects/Food/raw_ham_icon"),
					new ItemSize(1, 1), 1, new ItemDescription().Create(S_Raw_Ham, "rawham_id_name", "rawham_id_caption", 0.3f)
				);
				public static Item RawFish     = new Item().Create(
					Resources.Load<GameObject>("Objects/Food/raw_fish"),
					Resources.Load<Texture2D>("Objects/Food/raw_fish_icon"),
					new ItemSize(1, 1), 1, new ItemDescription().Create(S_Raw_Fish, "rawfish_id_name", "rawfish_id_caption", 0.2f)
				);

				public static Item CookedBread = new Item().Create(
					Resources.Load<GameObject>("Objects/Food/cooked_bread"),
					Resources.Load<Texture2D>("Objects/Food/cooked_bread_icon"),
					new ItemSize(1, 1), 1, new ItemDescription().Create(S_Cooked_Bread, "cookedbread_id_name", "cookedbread_id_caption", 0.2f)
				);
				public static Item CookedHam   = new Item().Create(
					Resources.Load<GameObject>("Objects/Food/cooked_ham"),
					Resources.Load<Texture2D>("Objects/Food/cooked_ham_icon"),
					new ItemSize(1, 1), 1, new ItemDescription().Create(S_Cooked_Ham, "cookedham_id_name", "cookedham_id_caption", 0.4f)
				);
				public static Item CookedFish  = new Item().Create(
					Resources.Load<GameObject>("Objects/Food/cooked_fish"),
					Resources.Load<Texture2D>("Objects/Food/cooked_fish_icon"),
					new ItemSize(1, 1), 1, new ItemDescription().Create(S_Cooked_Fish, "cookedfish_id_name", "cookedfish_id_caption", 0.3f)
				);

				public static Item AppleGreen  = new Item().Create(
					Resources.Load<GameObject>("Objects/Food/apple_green"),
					Resources.Load<Texture2D>("Objects/Food/apple_green_icon"),
					new ItemSize(1, 1), 3, new ItemDescription().Create(S_Apple_Green, "applegreen_id_name", "applegreen_id_caption", 0.07f)
				);
				public static Item AppleYellow = new Item().Create(
					Resources.Load<GameObject>("Objects/Food/apple_yellow"),
					Resources.Load<Texture2D>("Objects/Food/apple_yellow_icon"),
					new ItemSize(1, 1), 3, new ItemDescription().Create(S_Apple_Yellow, "appleyellow_id_name", "appleyellow_id_caption", 0.07f)
				);
				public static Item AppleRed    = new Item().Create(
					Resources.Load<GameObject>("Objects/Food/apple_red"),
					Resources.Load<Texture2D>("Objects/Food/apple_red_icon"),
					new ItemSize(1, 1), 3, new ItemDescription().Create(S_Apple_Red, "applered_id_name", "applered_id_caption", 0.07f)
				);
				public static Item CookedApple = new Item().Create(
					Resources.Load<GameObject>("Objects/Food/cooked_apple"),
					Resources.Load<Texture2D>("Objects/Food/cooked_apple_icon"),
					new ItemSize(1, 1), 3, new ItemDescription().Create(S_Cooked_Apple, "cookedapple_id_name", "cookedapple_id_caption", 0.1f)
				);
			}

		}

		public const int S_Health_Potion = 0xf001;
		public const int S_Mana_Potion   = 0xf002;
		
		public const int S_Raw_Bread     = 0x0003;
		public const int S_Cooked_Bread  = 0x0004;
		public const int S_Raw_Ham       = 0x0005;
		public const int S_Cooked_Ham    = 0x0006;
		public const int S_Raw_Fish      = 0x0007;
		public const int S_Cooked_Fish   = 0x0008;
										 
		public const int S_Apple_Green   = 0x0009;
		public const int S_Apple_Yellow  = 0x000a;
		public const int S_Apple_Red     = 0x000b;
										 
		public const int S_Cooked_Apple  = 0x000c;

	}

}
