using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace EngineEditor.Beansi {

	public static class Tables {

		private static GUIStyle splitter;

		private static Color DELIM_COLOR = new Color(0.2f, 0.2f, 0.3f);

		private const int BUTTON_SIZE = 20;
		private const int LABEL_SIZE = 24;

		private static System.Collections.ArrayList removeList = new System.Collections.ArrayList();

		public static Vector3 Vector3Field(Vector3 vector) {
			EditorGUILayout.BeginHorizontal();
				GUILayout.Label("X:", GUILayout.Width(20),GUILayout.Height(20));
				vector.x = EditorGUILayout.FloatField(vector.x);
				GUILayout.Label("Y:", GUILayout.Width(20),GUILayout.Height(20));
				vector.y = EditorGUILayout.FloatField(vector.y);
				GUILayout.Label("Z:", GUILayout.Width(20),GUILayout.Height(20));
				vector.z = EditorGUILayout.FloatField(vector.z);
			EditorGUILayout.EndHorizontal();
			return vector;
		}

		public static Vector4 Vector4Field(Vector4 vector) {
			EditorGUILayout.BeginHorizontal();
				GUILayout.Label("X:", GUILayout.Width(20), GUILayout.Height(20));
				vector.x = EditorGUILayout.FloatField(vector.x);
				GUILayout.Label("Y:", GUILayout.Width(20), GUILayout.Height(20));
				vector.y = EditorGUILayout.FloatField(vector.y);
				GUILayout.Label("Z:", GUILayout.Width(20), GUILayout.Height(20));
				vector.z = EditorGUILayout.FloatField(vector.z);
				GUILayout.Label("W:", GUILayout.Width(20), GUILayout.Height(20));
				vector.w = EditorGUILayout.FloatField(vector.w);
			EditorGUILayout.EndHorizontal();
			return vector;
		}

		public static Vector2 Vector2Field(Vector2 vector) {
			EditorGUILayout.BeginHorizontal();
				GUILayout.Label("X:", GUILayout.Width(20), GUILayout.Height(20));
				vector.x = EditorGUILayout.FloatField(vector.x);
				GUILayout.Label("Y:", GUILayout.Width(20), GUILayout.Height(20));
				vector.y = EditorGUILayout.FloatField(vector.y);
			EditorGUILayout.EndHorizontal();
			return vector;
		}

		public static int IntField(int value) {

		}

		public static void Splitter(Color rgb, float thickness = 1) {

			if (splitter == null) {
				GUISkin skin = GUI.skin;
				splitter = new GUIStyle();
				splitter.normal.background = EditorGUIUtility.whiteTexture;
				splitter.stretchWidth = true;
				splitter.margin = new RectOffset(0, 0, 1, 1);
			}

			Rect position = GUILayoutUtility.GetRect(GUIContent.none, splitter, GUILayout.Height(thickness));

			if (Event.current.type == EventType.Repaint) {
				Color restoreColor = GUI.color;
				GUI.color = rgb;
				splitter.Draw(position, false, false, false, false);
				GUI.color = restoreColor;
			}
		}

		public static void DrawTable<T>(string title, List<T> data, ITableListeners<T> listener) {

			Splitter(DELIM_COLOR, 3);
			GUILayout.Label(title, EditorStyles.boldLabel);
			EditorGUILayout.Separator();
			Splitter(DELIM_COLOR, 5);

			if (data.Count == 0) {

				EditorGUILayout.BeginHorizontal();
				GUILayout.Label("<Нет элементов>", EditorStyles.boldLabel);

					if (GUILayout.Button("Добавить")) {
						data.Add(listener.OnConstruct());
						return;
					}

				EditorGUILayout.EndHorizontal();

			} else
				for (int i = 0; i < data.Count; i++) {

					EditorGUILayout.BeginHorizontal();

					GUILayout.Label(i.ToString(), GUILayout.Width(LABEL_SIZE), GUILayout.Height(20));

					listener.OnEdit(data, i, data[i]); // вызываем отрисовку строчки
					
                    if (GUILayout.Button("-",GUILayout.Width(BUTTON_SIZE),GUILayout.Height(BUTTON_SIZE)))
						removeList.Add(data[i]);

					if (GUILayout.Button("+",GUILayout.Width(BUTTON_SIZE),GUILayout.Height(BUTTON_SIZE))) {
						data.Insert(data.Count-1, listener.OnConstruct()); // добавляем новый элемент в нужное место
						return;
					}

					EditorGUILayout.EndHorizontal();

					Splitter(DELIM_COLOR, 2);

				}

			EditorGUILayout.Separator();

			if (removeList.Count > 0) {
				foreach (T item in removeList)
					if(data.Contains(item))
						data.Remove(item);
				removeList.Clear();
			}

		}


	}

}
