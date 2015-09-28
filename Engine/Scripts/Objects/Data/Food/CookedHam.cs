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

	public class CookedHam : DynamicObject, IUsedType {

		public CookedHam() : base(DObjectList.S_Cooked_Ham) {

			objectName    = "food_cookedham_name";
			objectCaption = "food_cookedham_caption";

			item = DObjectList.Items.Food.CookedHam;

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