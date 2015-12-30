using System;
using System.Collections.Generic;
using UnityEngine;
namespace Engine.AI.Behavior {

	/// <summary>
	/// Набор точек патруля
	/// </summary>
	public class AIPath : ScriptableObject {

#if UNITY_EDITOR
		public bool  editMode = false;
		public Color color = new Color(1,0,0);
		public bool  markDeleted = false;
#endif

		[SerializeField] public List<AIPoint> points;

			void OnEnable() {
				if (points == null)
					points = new List<AIPoint>();
			}

		public List<AIPoint> getPoints() {
			return points;
		}

	}

}
