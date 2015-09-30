﻿using System;
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

		void Start() {
			base.OnStart();

			item = DObjectList.getInstance().getItem("CookedBread");
		}

		void OnGUI() {

		}

		public bool onUse(){
			
			
			base.Destroy(true);
			return true;
		}
		
	}
	
}