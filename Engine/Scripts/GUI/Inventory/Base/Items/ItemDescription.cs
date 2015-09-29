using System;
using System.Collections.Generic;
using Engine.I18N;

namespace Engine.EGUI.Inventory {

	public struct ItemDescription {

		public int    id;

		public float  costValue;

		public string name;
		public string caption;

		public string dName;
		public string dCaption;

		public ItemDescription Create(int id, string name, string caption, float costValue) {
			this.id=id;
			this.name=name;
			this.caption=caption;

				ReCreate();

			this.costValue=costValue;
			return this;
		}

		public void ReCreate() {
			this.dName    = CLang.getInstance().get(name);
			this.dCaption = CLang.getInstance().get(caption);
		}

	}

}
