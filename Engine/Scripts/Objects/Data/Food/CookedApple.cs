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

	public class CookedApple : DynamicObject, IUsedType {

		public CookedApple() : base(DObjectList.S_Cooked_Apple) {

			objectName    = "food_cookedapple_name";
			objectCaption = "food_cookedapple_caption";

			item = DObjectList.Items.Food.CookedApple;

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