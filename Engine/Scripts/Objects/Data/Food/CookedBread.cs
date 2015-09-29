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

		public CookedBread() : base() {
			item = DObjectList.Items.Food.CookedBread;
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