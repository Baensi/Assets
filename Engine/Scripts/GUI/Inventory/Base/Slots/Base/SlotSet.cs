using System;
using System.Collections;
using Engine.Objects;
using UnityEngine;

namespace Engine.EGUI.Inventory {

	[Serializable]
	public struct SlotSet {

		[SerializeField] public float OffsetX;
		[SerializeField] public float OffsetY;

		[SerializeField] public int CellsXCount;
		[SerializeField] public int CellsYCount;

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

