using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Engine.AI.Behavior;
using EngineEditor.Baensi;

namespace EngineEditor.AI {
	public class AIPointTableListener : ITableListener<AIPoint> {

		private static AIPointTableListener instance;

		public static AIPointTableListener getInstance() {
			if(instance==null)
				instance = new AIPointTableListener();
			return instance;
		}

		public AIPoint OnConstruct() {
			return ScriptableObject.CreateInstance<AIPoint>();
        }

		public void OnEdit(List<AIPoint> data, int index, AIPoint item) {

			item.editMode = Tables.BoolEditField(item.editMode);

			item.setData(Tables.Vector3Field(item.getData()));
			item.setRange(Tables.FloatField("Радиус:",item.getRange(),50,32));
		}

		public void OnHandlers(AIPoint item) {

			Quaternion zero = Quaternion.Euler(0, 0, 0);

			if(item==null)
				return;

			if (item.editMode) {

				Vector3 point = Handles.DoPositionHandle(item.getData(), zero);
				NavMeshHit Hit;
				NavMesh.SamplePosition(point, out Hit, 1000f, NavMesh.AllAreas);

				item.setData(Hit.position);

			}

			Handles.color = new Color(0.1f,1f,0.1f,0.2f);
			Handles.SphereCap(0,item.getData(),zero,item.getRange()); // рисуем область, в которой может перемещаться AI

		}

	}
}
