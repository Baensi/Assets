using System;
using UnityEngine;
using Engine.EGUI.Inventory;

namespace Engine.Objects {

	public static class DObjectList {

			// предметы в инвентаре
		public static class Items {

			public static Item HealthPotion = new Item().Create(Resources.Load<GameObject>("Objects/health_potion"), Resources.Load<Texture2D>("Objects/health_potion_icon"), new ItemSize(1, 1), 5);
			public static Item ManaPotion   = new Item().Create(Resources.Load<GameObject>("Objects/mana_potion"),   Resources.Load<Texture2D>("Objects/mana_potion_icon"),   new ItemSize(1, 1), 5);

			public static class Food {

				public static Item RawBread    = new Item().Create(Resources.Load<GameObject>("Objects/Food/raw_bread"),    Resources.Load<Texture2D>("Objects/Food/raw_bread_icon"),    new ItemSize(1, 1), 1);
				public static Item RawHam      = new Item().Create(Resources.Load<GameObject>("Objects/Food/raw_ham"),      Resources.Load<Texture2D>("Objects/Food/raw_ham_icon"),      new ItemSize(1, 1), 1);
				public static Item RawFish     = new Item().Create(Resources.Load<GameObject>("Objects/Food/raw_fish"),     Resources.Load<Texture2D>("Objects/Food/raw_fish_icon"),     new ItemSize(1, 1), 1);

				public static Item CookedBread = new Item().Create(Resources.Load<GameObject>("Objects/Food/cooked_bread"), Resources.Load<Texture2D>("Objects/Food/cooked_bread_icon"), new ItemSize(1, 1), 1);
				public static Item CookedHam   = new Item().Create(Resources.Load<GameObject>("Objects/Food/cooked_ham"),   Resources.Load<Texture2D>("Objects/Food/cooked_ham_icon"),   new ItemSize(1, 1), 1);
				public static Item CookedFish  = new Item().Create(Resources.Load<GameObject>("Objects/Food/cooked_fish"),  Resources.Load<Texture2D>("Objects/Food/cooked_fish_icon"),  new ItemSize(1, 1), 1);

				public static Item AppleGreen  = new Item().Create(Resources.Load<GameObject>("Objects/Food/apple_green"),  Resources.Load<Texture2D>("Objects/Food/apple_green_icon"),  new ItemSize(1, 1), 3);
				public static Item AppleYellow = new Item().Create(Resources.Load<GameObject>("Objects/Food/apple_yellow"), Resources.Load<Texture2D>("Objects/Food/apple_yellow_icon"), new ItemSize(1, 1), 3);
				public static Item AppleRed    = new Item().Create(Resources.Load<GameObject>("Objects/Food/apple_red"),    Resources.Load<Texture2D>("Objects/Food/apple_red_icon"),    new ItemSize(1, 1), 3);
				public static Item CookedApple = new Item().Create(Resources.Load<GameObject>("Objects/Food/cooked_apple"), Resources.Load<Texture2D>("Objects/Food/cooked_apple_icon"), new ItemSize(1, 1), 3);
			}

		}

		public const int S_Health_Potion  = 0xf001;
		public const int S_Mana_Potion    = 0xf002;
		
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
