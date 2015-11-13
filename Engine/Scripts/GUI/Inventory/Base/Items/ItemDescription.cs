using System;
using System.Collections.Generic;
using Engine.I18N;

namespace Engine.EGUI.Inventory {

	/// <summary>
	/// Структура описания предмета в инвентаре
	/// </summary>
	public struct ItemDescription {

		/// <summary>
		/// код предмета
		/// </summary>
		public int    id;

		/// <summary>
		/// стоимость предмета
		/// </summary>
		public float  costValue;

		/// <summary>
		/// id названия предмета в словаре
		/// </summary>
		public string name;

		/// <summary>
		/// id описания предмета в словаре
		/// </summary>
		public string caption;

		/// <summary>
		/// название предмета (переведено через словарь)
		/// </summary>
		public string dName;

		/// <summary>
		/// описание предмета (переведено через словарь)
		/// </summary>
		public string dCaption; 
		
		/// <summary>
		/// Инициализирует экземпляр структуры и возвращает его
		/// </summary>
		/// <param name="id">идентификатор</param>
		/// <param name="name">id азвания предмета</param>
		/// <param name="caption">id описания предмета</param>
		/// <param name="costValue">стоимость предмета</param>
		/// <returns>Возвращает текущий инициализированный экземпляр</returns>
		public ItemDescription Create(int id, string name, string caption, float costValue) {
			this.id=id;
			this.name=name;
			this.caption=caption;

				ReCreate();

			this.costValue=costValue;
			return this;
		}

		/// <summary>
		/// Читает навзание и описание предмета из словаря
		/// </summary>
		public void ReCreate() {
			this.dName    = CLang.getInstance().get(name);
			this.dCaption = CLang.getInstance().get(caption);
		}

	}

}
