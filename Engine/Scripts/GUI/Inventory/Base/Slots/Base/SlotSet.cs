using System;
using System.Collections;
using Engine.Objects;
using UnityEngine;

namespace Engine.EGUI.Inventory {

	[Serializable]
	public struct SlotSet {

		public float OffsetX;
		public float OffsetY;

		public int CellsXCount;
		public int CellsYCount;

		public float SlotWidth {
			set { }
			get { return CellSettings.cellPaddingX+CellSettings.cellWidth*CellsXCount;  }
		}

		public float SlotHeight {
			set { }
			get { return CellSettings.cellPaddingY+CellSettings.cellHeight*CellsYCount; }
		}

	}
}

