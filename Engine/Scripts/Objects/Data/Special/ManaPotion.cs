using System;
using UnityEngine;
using Engine.Objects;
using Engine.Objects.Types;
using Engine.Player;
using Engine.EGUI.Inventory;

namespace Engine.Objects.Special {

	public class ManaPotion : DynamicObject, IUsedType {

		private static Texture2D ICON = Resources.Load<Texture2D>("Objects/mana_potion");

		public ManaPotion() : base(DObjectList.S_Mana_Potion) {
			
			objectName    = "manapotion_id_name";
			objectCaption = "manapotion_id_caption";

			item = new Item(this, ICON, new ItemSize(1, 1), 5);
			
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

