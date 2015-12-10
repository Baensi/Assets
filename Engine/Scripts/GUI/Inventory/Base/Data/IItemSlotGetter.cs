using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Engine.EGUI.Inventory {

	/// <summary>
	/// Интерфейс "принемающего" предметы
	/// </summary>
	public interface IItemSlotGetter {

		void GetExternalItem(ItemSlot item);

	}

}
