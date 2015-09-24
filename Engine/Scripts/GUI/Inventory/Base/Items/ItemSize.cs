using System.Collections;
using Engine.Objects;

namespace Engine.EGUI.Inventory {

	public class ItemSize {

		private int width;
		private int height;

		public ItemSize(){
		}

		public ItemSize(int width, int height){
			this.width  = width;
			this.height = height;
		}

		public int getWidth(){
			return this.width;
		}

		public int getHeight(){
			return height;
		}

	}
}

