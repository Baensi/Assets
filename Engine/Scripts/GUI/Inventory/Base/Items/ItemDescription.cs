using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Engine.EGUI.Inventory {

	public struct ItemDescription {

		public int    id;

		public float  costValue;

		public string name;
		public string caption;

		public ItemDescription Create(int id, string name, string caption, float costValue) {
			this.id=id;
			this.name=name;
			this.caption=caption;
			this.costValue=costValue;
			return this;
		}

	}

}
