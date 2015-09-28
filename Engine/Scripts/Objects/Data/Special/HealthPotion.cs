using System;
using UnityEngine;
using Engine.Objects;
using Engine.Objects.Types;
using Engine.Player;
using Engine.EGUI.Inventory;

namespace Engine.Objects.Special {

	public class HealthPotion : DynamicObject, IUsedType {

		public HealthPotion() : base(DObjectList.S_Health_Potion) {
			
			objectName    = "healthpotion_id_name";
			objectCaption = "healthpotion_id_caption";

			item = DObjectList.Items.HealthPotion;

		}

		void Start() {
			base.OnStart();
		}

		public bool onUse() {


			base.Destroy(true);
			return true;
		}

		
	}
	
}

