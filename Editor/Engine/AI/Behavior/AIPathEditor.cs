using System;
using UnityEditor;
using UnityEngine;
using Engine.AI.Behavior;

namespace EngineEditor.AI {

	public class AIPathEditor {

		private static AIPathEditor instance;

		public static AIPathEditor getInstance() {
			if (instance == null)
				instance = new AIPathEditor();
			return instance;
        }

		public void OnInspectorGUI(AIPatrol patrol, AIPath path) {

			path.color = EditorGUILayout.ColorField("Цвет:", path.color);

			EditorGUILayout.BeginHorizontal();
			GUILayout.Label(path.getPoints().Count.ToString() + " узлов");

			if (GUILayout.Button("Добавить в конце"))
				path.getPoints().Add(new AIPoint());
			
			if (GUILayout.Button("Добавить в начале"))
				path.getPoints().Insert(0,new AIPoint());

				EditorGUILayout.EndHorizontal();

			if (GUILayout.Button("Удалить путь"))
				if (EditorUtility.DisplayDialog("Удаление пути", "Вы действительно хотите удалить путь?", "Да", "Нет"))
					path.markDeleted = true;

		}

	}

}
