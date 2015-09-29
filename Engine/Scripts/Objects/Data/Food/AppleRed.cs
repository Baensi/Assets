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

	public class AppleRed : DynamicObject, ICookedType, IUsedType {

		private static string    COOKED     = "Objects/Food/cooked_red_apple";
		private static AudioClip COOK_SOUND = Resources.Load<AudioClip>("Objects/Food/raw_red_apple_cook");

		private List<CookingZone> zones;
		private ObjectCooked      cookTemplate;
		private bool              isCooked = false;

		public AppleRed() : base() {
			item = DObjectList.Items.Food.AppleRed;
		}

		void Start() {
			base.OnStart();

			cookTemplate = new ObjectCooked(this, COOK_SOUND, 5);
			zones        = new List<CookingZone>();
		}

		public void onCook() {
			cookTemplate.update();
		}

		public void onEnterCookingZone(CookingZone cookingZone) {
			if (!zones.Contains(cookingZone))
				zones.Add(cookingZone);
		}

		public void onExitCookingZone(CookingZone cookingZone) {
			if (zones.Contains(cookingZone))
				zones.Remove(cookingZone);
		}

		public void onDestroyCook(CookingZone cookingZone) {
			zones.Remove(cookingZone);
		}

		public void endCook(){

			foreach (CookingZone zone in zones)
				zone.removeFood(this);

			isCooked = true;

		}

		void OnGUI() {

		}
		
		public bool onUse(){
			
			
			base.Destroy(true);
			return true;
		}

		void Update() {

			if (!isCooked) return;

			base.Destroy(); // Добавляем текущий экземпляр в корзину
			Instantiate(Resources.Load(COOKED), this.transform.position, this.transform.rotation); // создаём новый экземпляр объекта

		}
		
	}
	
}