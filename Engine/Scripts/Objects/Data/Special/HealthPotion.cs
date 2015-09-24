using System;
using UnityEngine;
using Engine.Objects;
using Engine.Objects.Types;
using Engine.Player;
using Engine.EGUI.Inventory;

namespace Engine.Objects.Special {

	public class HealthPotion : DynamicObject, IUsedType {

		private static Texture2D ICON = Resources.Load<Texture2D>("Objects/health_potion_icon");

		public HealthPotion() : base(DObjectList.S_Health_Potion) {
			
			objectName    = "healthpotion_id_name";
			objectCaption = "healthpotion_id_caption";

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

