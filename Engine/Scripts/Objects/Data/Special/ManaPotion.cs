using System;
using UnityEngine;
using Engine.Objects;
using Engine.Objects.Types;
using Engine.Player;
using Engine.EGUI.Inventory;

namespace Engine.Objects.Special {

	public class ManaPotion : DynamicObject, IUsedType {

		public ManaPotion() : base(DObjectList.S_Mana_Potion) {
			
			objectName    = "manapotion_id_name";
			objectCaption = "manapotion_id_caption";

			item = DObjectList.Items.ManaPotion;
			
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

