using System;
using Engine.Objects.Types;
using Engine.Player;

namespace Engine.Objects.Food {

	public class CookedHam : DynamicObject, IPickedType, IUsedType, IChangedStatesType {

		private PlayerStates states = new PlayerStates() { // изменяемые статы
			health = 5.0f,
		};

			void Start() {
				base.OnStart();

				item = DObjectList.getInstance().getItem("CookedHam");
			}

		void OnGUI() {

		}

		public void onUse() {
			GamePlayer.states += getStates();
		}

		public PlayerStates getStates() {
			return states;
		}

		public bool onPick() {
			if (InventoryHelper.AddInInventory(item)) {
				base.Destroy(true);
				return true;
			} return false;
		}
		
	}
	
}