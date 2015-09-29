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

	public class RawBread : DynamicObject, ICookedType {

		private static string    COOKED     = "Objects/Food/cooked_bread";
		private static AudioClip COOK_SOUND = Resources.Load<AudioClip>("Objects/Food/raw_bread_cook");
	
		private List<CookingZone> zones;
		private ObjectCooked      cookTemplate;
		private bool              isCooked = false;

		public RawBread() : base() {
			item = DObjectList.Items.Food.RawBread;
		}

		void Start() {
			base.OnStart();

			cookTemplate = new ObjectCooked(this, COOK_SOUND, 7);
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

		void Update() {

			if (!isCooked) return;

			
			base.Destroy(); // Добавляем текущий экземпляр в корзину
			Instantiate(Resources.Load(COOKED), this.transform.position, this.transform.rotation); // создаём новый экземпляр объекта
			
		}
		
	}
	
}