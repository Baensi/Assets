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

	public class CookedFish : DynamicObject, IUsedType {

		private static Texture2D ICON = Resources.Load<Texture2D>("Objects/Food/cooked_fish_icon");

		public CookedFish() : base(DObjectList.S_Cooked_Fish) {

			objectName    = "food_cookedfish_name";
			objectCaption = "food_cookedfish_caption";

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