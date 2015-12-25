using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Engine.AI.Behavior;
using EngineEditor.Beansi;

namespace EngineEditor.AI {
	public class AIStayPointsEditor : ITableListeners<AIPoint> {

		private static AIStayPointsEditor instance;

		public static AIStayPointsEditor getInstance() {
			if (instance == null)
				instance = new AIStayPointsEditor();
			return instance;
		}

		public bool OnInspectorGUI(PathBehavior path) {

			List<AIPoint> points = path.getStayPoints();

				if (points == null) {
					path.setStayPoints(new List<AIPoint>());
					points = path.getStayPoints();
				}

			Tables.DrawTable<AIPoint>("table", path.getStayPoints(), this);
            return true;

		}

		public void OnEdit(List<AIPoint> data, int index, AIPoint item) {
			item.setData(Tables.Vector3Field(item.getData()));
		}

		public AIPoint OnConstruct() {
			return new AIPoint();
        }
	}
}
