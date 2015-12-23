using UnityEngine;

namespace Engine.EGUI.Inventory {

	public class ItemBounds {

		private Item item;

		private int x;
		private int y;
		private int w;
		private int h;

		private ItemSlot old;
		private bool fix;

			public ItemBounds(ItemSlot itemSlot) {
				old = itemSlot;
				this.item = itemSlot.item;
				Reset();
			}

		public bool isFix() {
			return fix;
		}

		public void setFix(bool fix) {
			this.fix = fix;
		}

		public void Reset() {

			x = old.position.X;
			y = old.position.Y;

			w = item.getSize().getWidth();
			h = item.getSize().getHeight();

			fix = false;

		}


		public Item toItem() {
			return item;
		}

		public ItemSlot createItemSlot() {
			return new ItemSlot(item, new ItemPosition(x, y));
		}

		public int X {
			get { return x; }
			set { x = value; }
		}
		public int Y {
			get { return y; }
			set { y = value; }
		}
		public int W {
			get { return w; }
			private set { }
		}
		public int H {
			get { return h; }
			private set {}
		}

	}

}
