using System;
using UnityEngine;
using System.Collections.Generic;
using Engine.EGUI.ToolTip;
using Engine.I18N;
using Engine.Objects;
using Engine.Objects.Types;
using Engine.Player;

namespace Engine.EGUI.Inventory {

	/// <summary>
	/// Сервис для работы с описанием предметов в инвентаре
	/// </summary>
	public class ItemToolTipService {

		private static ItemToolTipService instance;

		private static Color statesPositiveColor = new Color(0f,0.8f,0f);
		private static Color statesNegativeColor = new Color(0.8f,0f,0f);
		private static Color itemTypeColor       = new Color(0.8f,0.7f,0f);

		public static ItemToolTipService getInstance() {
			if (instance == null)
				instance = new ItemToolTipService();
			return instance;
        }

		/// <summary>
		/// Создаёт строку с описанием предмета
		/// </summary>
		/// <param name="item">Описываемй предмет</param>
		/// <returns>Возвращает строку с описанием</returns>
		public string createDescription(Item item) {
			ItemDescription description = item.getDescription();
            string result = description.dName;

			if (item.getMaxCount() > 1)
				result += " [" + item.getCount().ToString() + "/" + item.getMaxCount().ToString() + CLang.getInstance().get(Dictionary.K_COUNT) + "]";

			result += "\n" + description.dCaption;

			return result;
		}

		/// <summary>
		/// Создаёт список свойств, которые даёт предмет
		/// </summary>
		/// <param name="item">Предмет, свойства которого анализируются</param>
		/// <returns>Возвращает список свойств</returns>
		public List<PropertyItem> createInformationItems(Item item) {
			if (item == null || item.toGameObject() == null)
				return null;

			DynamicObject dynamicObject = item.toGameObject().GetComponent<DynamicObject>();

#if UNITY_EDITOR
			if (dynamicObject == null) {
				Debug.LogError("Предмет "+item.description.dName+" не имеет инициализированного gameObject!");
			}
#endif

			List<PropertyItem> result = new List<PropertyItem>();
				checkStates(dynamicObject, ref result);
				checkTypes(dynamicObject, ref result);
			return result;
		}

		private string toString(float value, bool trunc = false) {
			return trunc ?
					value > 0 ? value.ToString("+0.00") : value.ToString("-0.00") 
				  : value > 0 ? value.ToString("+0.")   : value.ToString("-0.");
		}

		private Color toColor(float value) {
			return value > 0 ? statesPositiveColor : statesNegativeColor;
		}

		private PropertyItem create(string id, float value, bool trunc) {
			return new PropertyItem(CLang.getInstance().get(id), toString(value, trunc), toColor(value));
        }

		/// <summary>
		/// формирует список свойств из статов
		/// </summary>
		/// <param name="dynamicObject"></param>
		/// <param name="list"></param>
		private void checkStates(IDynamicObject dynamicObject, ref List<PropertyItem> list) {

			if (dynamicObject == null)
				return;

			IChangedStatesType changedStates = (IChangedStatesType)dynamicObject;

			if (changedStates == null)
				return;
			
            PlayerStates states = changedStates.getStates();

			if (states == null)
				return;

			// общие статы
			if (states.maxHealth != 0f) list.Add(create(DictionaryPlayer.States.K_HEALTH,states.maxHealth, true));
			if (states.maxEnergy != 0f) list.Add(create(DictionaryPlayer.States.K_ENERGY,states.maxEnergy, true));
			if (states.maxMana   != 0f) list.Add(create(DictionaryPlayer.States.K_MANA,  states.maxMana,   true));

            if (states.health != 0f) list.Add(create(DictionaryPlayer.States.K_HEALTH_REST,states.health, true));
            if (states.mana   != 0f) list.Add(create(DictionaryPlayer.States.K_MANA_REST,states.mana, true));
            if (states.energy != 0f) list.Add(create(DictionaryPlayer.States.K_ENERGY_REST,states.energy, true));
		}

		/// <summary>
		/// формирует список свойств из типов
		/// </summary>
		/// <param name="dynamicObject"></param>
		/// <param name="list"></param>
		private void checkTypes(IDynamicObject dynamicObject, ref List<PropertyItem> list) {

			IPickedType pickedType = dynamicObject as IPickedType;
            if (pickedType != null)
				create(ref list, pickedType.K_PICKED_TYPE_NAME(), pickedType.K_PICKED_TYPE_CAPTION());

			ICookedType cookedType = dynamicObject as ICookedType;
			if (cookedType != null)
				create(ref list, cookedType.K_COOKED_TYPE_NAME(), cookedType.K_COOKED_TYPE_CAPTION());

			IReadedType readedType = dynamicObject as IReadedType;
			if (readedType != null)
				create(ref list, readedType.K_READED_TYPE_NAME(), readedType.K_READED_TYPE_CAPTION());

			IUsedType usedType = dynamicObject as IUsedType;
			if (usedType != null)
				create(ref list, usedType.K_USED_TYPE_NAME(), usedType.K_USED_TYPE_CAPTION());

		}

		private void create(ref List<PropertyItem> list, string name, string text) {
			list.Add(new PropertyItem(CLang.getInstance().get(name), CLang.getInstance().get(text), itemTypeColor));
		}

	}

}
