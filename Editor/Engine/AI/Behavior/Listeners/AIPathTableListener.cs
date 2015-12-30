using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Engine.AI.Behavior;
using EngineEditor.Beansi;

namespace EngineEditor.AI {
	public class AIPathTableListener : ITableListeners<AIPath> {

		private static AIPathTableListener instance;

		public static AIPathTableListener getInstance() {
			if(instance==null)
				instance = new AIPathTableListener();
			return instance;
		}

		public AIPath OnConstruct() {
            return ScriptableObject.CreateInstance<AIPath>();
		}

		public void OnEdit(List<AIPath> data, int index, AIPath item) {

			item.editMode = Tables.BoolEditField(item.editMode);

				if(GUILayout.Button("+v",GUILayout.Width(30), GUILayout.Height(20))) {
					item.getPoints().Add(ScriptableObject.CreateInstance<AIPoint>());
				}

			item.color = EditorGUILayout.ColorField(item.color);

		}

		public void OnHandlers(AIPath item) {

			Quaternion zero = Quaternion.Euler(0, 0, 0);

			if(item==null)
				return;

			List<AIPoint> points = item.getPoints();

			if (points == null || points.Count==0)
				return;

			Vector3 start = points[0].getData();

			foreach (AIPoint point in points) {

				if (item.editMode) {
					Vector3 pos = Handles.DoPositionHandle(point.getData(), zero);
					NavMeshHit Hit;
					NavMesh.SamplePosition(pos, out Hit, 1000f, NavMesh.AllAreas);
					point.setData(Hit.position);
				}

				Handles.color = new Color(0.1f, 1f, 0.1f, 0.6f);
				Handles.DrawLine(start, point.getData());

				Handles.color = new Color(0.1f, 1f, 0.1f, 0.2f);
				Handles.SphereCap(0, point.getData(), zero, point.getRange()); // рисуем область, в которой может перемещаться AI

				start = point.getData();
			}

		}

	}
}
