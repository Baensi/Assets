using System;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using Engine.Objects.Types;
using Engine.Objects;
using Engine.Skills;
using Engine.Player;
using Engine.EGUI.Inventory;

namespace Engine.Objects.Food {

	public class CookedRedApple : DynamicObject, IUsedType {

		private static Texture2D ICON = Resources.Load<Texture2D>("Objects/Food/cooked_red_apple_icon");

		public CookedRedApple() : base(DObjectList.S_Cooked_Red_Apple) {

			objectName    = "food_cookedredapple_name";
			objectCaption = "food_cookedredapple_caption";

			item = new Item(this, ICON, new ItemSize(1,1), 1);


		}

		void Start() {
			base.OnStart();
		}

		void OnGUI() {

		}

		public bool onUse(){
			
			
			base.Destroy(true);
			return true;
		}
		
	}
	
}