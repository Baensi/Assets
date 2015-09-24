using System;
using System.Collections;
using Engine.Objects;
using UnityEngine;

namespace Engine.EGUI.Inventory {

	[Serializable]
	public class SlotSet : MonoBehaviour {

		[SerializeField] public float OffsetX = 0f;
		[SerializeField] public float OffsetY = 0f;

		[SerializeField] public int CellsXCount = 0;
		[SerializeField] public int CellsYCount = 0;

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

