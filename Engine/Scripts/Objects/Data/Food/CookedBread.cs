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

	public class CookedBread : DynamicObject, IUsedType {

		private static Texture2D ICON = Resources.Load<Texture2D>("Objects/Food/cooked_bread_icon");

		public CookedBread() : base(DObjectList.S_Cooked_Bread) {

			objectName    = "food_cookedbread_name";
			objectCaption = "food_cookedbread_caption";

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