using UnityEngine;
using Engine.Objects;
using Engine.Objects.Doors;
using System;

namespace Engine.Player {

	public struct SelectionGroup {

		public SelectedType selectType;

		public IDynamicObject selectedObject;
		public ILever selectedLever;
		public IDoor selectedDoor;

	}

}