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

	public class AppleGreen : DynamicObject, ICookedType, IPickedType, IUsedType, IChangedStatesType {

		private List<CookingZone> zones;
		private ObjectCooked      cookTemplate;
		private bool              isCooked = false;

		private PlayerStates states = new PlayerStates() { // изменяемые статы
			health = 1.0f,
		};

		void OnEnable() {
			
		}

			void Start() {
				base.OnStart();

				item         = DObjectList.getInstance().getItem("AppleGreen");

				cookTemplate = new ObjectCooked(this, item.resource.sounds["cook"], 5);
				zones        = new List<CookingZone>();

             states.health = 1.0f;
			}

		public void onUse() {
			GamePlayer.states += getStates();
        }

		public PlayerStates getStates() {
			return states;
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
		
		public bool onPick(){
            
			if (InventoryHelper.AddInInventory(item)) {
				base.Destroy(true);
				return true;
			} return false;

		}

		void Update() {
			base.OnUpdate();

			if (!isCooked) return;

			base.Destroy(); // Добавляем текущий экземпляр в корзину

			GameObject cookedObject = DObjectList.getInstance().getItem("CookedApple").toGameObject();
			Instantiate(cookedObject, this.transform.position, this.transform.rotation); // создаём новый экземпляр объекта
			
		}
		
	}
	
}