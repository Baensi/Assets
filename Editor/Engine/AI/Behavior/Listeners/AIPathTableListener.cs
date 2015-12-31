using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Engine.AI.Behavior;
using EngineEditor.Baensi;

namespace EngineEditor.AI {
	public class AIPathTableListener : ITreeTableListener<AIPath,AIPoint> {

		private static AIPathTableListener instance;

		public static AIPathTableListener getInstance() {
			if(instance==null)
				instance = new AIPathTableListener();
			return instance;
		}

		public List<AIPoint> GetItems(AIPath tree) {
			return tree.getPoints();
		}

		public AIPath OnConstruct() {
            return ScriptableObject.CreateInstance<AIPath>();
		}

		public AIPoint OnConstructItem() {
			return ScriptableObject.CreateInstance<AIPoint>();
		}

		public void OnEdit(List<AIPath> data, int index, AIPath item) {

			item.editMode = Tables.BoolEditField(item.editMode);
			item.color    = EditorGUILayout.ColorField(item.color);

		}

		public void OnEditItem(List<AIPoint> items, int index, AIPoint item) {

			item.setData(Tables.Vector3Field(item.getData(),64));

		}

		public void OnHandlers(AIPath item) {

			if (Selection.activeGameObject == null)
				return;

			Quaternion zero = Quaternion.Euler(0, 0, 0);

			if(item==null)
				return;

			List<AIPoint> points = item.getPoints();

			if (points == null || points.Count==0)
				return;

			Vector3 start = Selection.activeGameObject.transform.position;

			foreach (AIPoint point in points) {

				if (item.editMode) {
					Vector3 pos = Handles.DoPositionHandle(point.getData(), zero);
					NavMeshHit Hit;
					NavMesh.SamplePosition(pos, out Hit, 1000f, NavMesh.AllAreas);
					point.setData(Hit.position);
				}

				Handles.color = item.color;
				Handles.DrawLine(start, point.getData());

				Handles.color = new Color(0.1f, 1f, 0.1f, 0.5f);
				Handles.CubeCap(0, point.getData(), zero, 2f); // рисуем область, в которой может перемещаться AI

				start = point.getData();
			}

		}

	}
}
