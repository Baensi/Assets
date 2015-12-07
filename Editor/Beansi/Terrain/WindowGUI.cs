using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


namespace EngineEditor.Terrain {
	
	public class WindowGUI {

		private static Color lineColor  = new Color(0.7f, 0.7f, 0.8f);
		private static Color lineShadow = new Color(0.4f, 0.4f, 0.4f);

		// Режим редактирвоания
		public bool designMode  = true;
		public bool consoleMode = true;

		// Кисточка
		public float brushSize      = 1.23f;
		public int brushSensitivity = 3;

		public bool positionCenter = true;

		// Группы изменений для генерации
		public bool generateRandomPosition = true;
		public bool generateRandomRotation = true;
		public bool generateChangeScale    = true;
		public bool generateChangeColor    = true;

		public bool  useRandom       = false;
		public bool  useRaycast      = true;
		public float offsetFromValue = 0.0f;
		public float offsetToValue   = 0.0f;
		
		public float maxRotationXValue = 0f;
		public float minRotationXValue = 0f;
		public float maxRotationYValue = 360f;
		public float minRotationYValue = 0f;
		public float maxRotationZValue = 0f;
		public float minRotationZValue = 0f;
		
		public float maxScaleXValue = 1.0f;
		public float minScaleXValue = 1.0f;
		public float maxScaleYValue = 1.0f;
		public float minScaleYValue = 1.0f;
		public float maxScaleZValue = 1.0f;
		public float minScaleZValue = 1.0f;
		
		public Color minColor = new Color(0.15f, 0.50f, 0.09f);
		public Color maxColor = new Color(0.45f, 0.50f, 0.09f);

		private TerrainWindow terrainWindow;

		private List<GameObject> prefabList = new List<GameObject>();

			public WindowGUI(TerrainWindow terrainWindow) {
				this.terrainWindow=terrainWindow;
				prefabList.Add(null);
			}

		public void CreateBrushSettings() {

			GUILayout.Label("Кисть", EditorStyles.boldLabel);

				brushSize        = EditorGUILayout.Slider("Размер", brushSize, 0.05f, 10f);
				brushSensitivity = EditorGUILayout.IntSlider("Число объектов", brushSensitivity, 1, 50);

				positionCenter = EditorGUILayout.Toggle("Позиция в центре объекта",positionCenter);

			EditorGUILayout.Separator();

		}

		public GameObject PrefabObject {
			get {
				int i = UnityEngine.Random.Range(0, prefabList.Count);
				return prefabList[i];
			}
			private set { }
		}

		public void CreateGenerationSettings() {

			EditorGUILayout.Separator();

			GUILayout.Label("Префабы", EditorStyles.boldLabel);

			drawLine();

			EditorGUILayout.BeginHorizontal();
				if (GUILayout.Button("Добавить"))
					prefabList.Add(null);
				if (GUILayout.Button("Удалить"))
					prefabList.RemoveAt(prefabList.Count-1);
			EditorGUILayout.EndHorizontal();

			for (int i=0; i<prefabList.Count; i++) {

				EditorGUILayout.BeginHorizontal();

					prefabList[i] = (GameObject)EditorGUILayout.ObjectField(new GUIContent("Объект ["+i.ToString()+"]"), prefabList[i], typeof(GameObject), true);

				EditorGUILayout.EndHorizontal();

			}

			drawLine();

			EditorGUILayout.Separator();
			EditorGUILayout.Separator();

			generateRandomPosition=EditorGUILayout.BeginToggleGroup(new GUIContent("Разброс позиционирования"), generateRandomPosition);

				useRaycast = EditorGUILayout.Toggle(new GUIContent("Проецировать на плоскость"), useRaycast);
				useRandom  = EditorGUILayout.Toggle(new GUIContent("Смещать случайно"), !useRaycast);

				EditorGUILayout.BeginHorizontal();
					offsetFromValue = EditorGUILayout.FloatField(new GUIContent("Y смещение от"), offsetFromValue);
					offsetToValue   = EditorGUILayout.FloatField(new GUIContent("Y смещение до"), offsetToValue);
				EditorGUILayout.EndHorizontal();
			EditorGUILayout.EndToggleGroup();

			EditorGUILayout.Separator();

			generateRandomRotation = EditorGUILayout.BeginToggleGroup(new GUIContent("Вращение"), generateRandomRotation);
				EditorGUILayout.BeginHorizontal();
					minRotationXValue = EditorGUILayout.FloatField(new GUIContent("X° от"), minRotationXValue);
					maxRotationXValue = EditorGUILayout.FloatField(new GUIContent("X° до"), maxRotationXValue);
				EditorGUILayout.EndHorizontal();
				EditorGUILayout.BeginHorizontal();
					minRotationYValue = EditorGUILayout.FloatField(new GUIContent("Y° от"), minRotationYValue);
					maxRotationYValue = EditorGUILayout.FloatField(new GUIContent("Y° до"), maxRotationYValue);
				EditorGUILayout.EndHorizontal();
				EditorGUILayout.BeginHorizontal();
					minRotationZValue = EditorGUILayout.FloatField(new GUIContent("Z° от"), minRotationZValue);
					maxRotationZValue = EditorGUILayout.FloatField(new GUIContent("Z° до"), maxRotationZValue);
				EditorGUILayout.EndHorizontal();
			EditorGUILayout.EndToggleGroup();

			EditorGUILayout.Separator();

			generateChangeScale = EditorGUILayout.BeginToggleGroup(new GUIContent("Разброс масштабирования"), generateChangeScale);
				EditorGUILayout.BeginHorizontal();
					minScaleXValue = EditorGUILayout.FloatField(new GUIContent("X от"), minScaleXValue);
					maxScaleXValue = EditorGUILayout.FloatField(new GUIContent("X до"), maxScaleXValue);
				EditorGUILayout.EndHorizontal();
				EditorGUILayout.BeginHorizontal();
					minScaleYValue = EditorGUILayout.FloatField(new GUIContent("Y от"), minScaleYValue);
					maxScaleYValue = EditorGUILayout.FloatField(new GUIContent("Y до"), maxScaleYValue);
				EditorGUILayout.EndHorizontal();
				EditorGUILayout.BeginHorizontal();
					minScaleZValue = EditorGUILayout.FloatField(new GUIContent("Z от"), minScaleZValue);
					maxScaleZValue = EditorGUILayout.FloatField(new GUIContent("Z до"), maxScaleZValue);
				EditorGUILayout.EndHorizontal();
			EditorGUILayout.EndToggleGroup();

			EditorGUILayout.Separator();

			generateChangeColor = EditorGUILayout.BeginToggleGroup(new GUIContent("Разброс цвета"), generateChangeColor);
				EditorGUILayout.BeginHorizontal();	
					minColor = EditorGUILayout.ColorField(new GUIContent("От"), minColor);
					maxColor = EditorGUILayout.ColorField(new GUIContent("До"), maxColor);
				EditorGUILayout.EndHorizontal();
			EditorGUILayout.EndToggleGroup();

		}

		public void CreateModeSettings() {

			EditorGUILayout.BeginHorizontal();
				designMode  = EditorGUILayout.Toggle("Режим редактирования", designMode);
				consoleMode = EditorGUILayout.Toggle("Консоль", consoleMode);
			EditorGUILayout.EndHorizontal();

		}

		public void CreatePickSettings() {

			terrainWindow.selection = Selection.activeGameObject;

			EditorGUILayout.Separator();
			EditorGUILayout.Separator();

			if (terrainWindow.pickupObject != null) {
				EditorGUILayout.BeginHorizontal();
				GUILayout.Label("Захвачен объект <" + terrainWindow.pickupObject.toGameObject().name + ">");
				if (GUILayout.Button("Отпустить"))
					terrainWindow.doDropObject();
				EditorGUILayout.EndHorizontal();
			}

			if (terrainWindow.selection != null && (terrainWindow.pickupObject==null || !terrainWindow.pickupObject.toGameObject().Equals(terrainWindow.selection))) {
				EditorGUILayout.BeginHorizontal();
				GUILayout.Label("Выбран объект <" + terrainWindow.selection.name + ">");
				if (GUILayout.Button("Захватить"))
					terrainWindow.doPickUpObject();
				EditorGUILayout.EndHorizontal();
			}

		}

		public void CreateHelpSettings() {

			if (GUILayout.Button("Справка")) {
				EditorUtility.DisplayDialog("Справка", 
					"Горячие клавиши:\n"
					+" F9  - смена режимов (редактирвоание/дизайн)\n"
					+" F10 - консоль (вкл/выкл)\n\n"

					+" левый Ctrl - переводит в режим \"Удаление\"\n"
					+" левый Alt+ЛКМ - захватить выбранный объект\n\n"

					+" левый Shift+ЛКМ - добавить объекты в сцену\n"
					+" левый Ctrl+ПКМ - удалить сгенерированные объекты под кисточкой\n\n"

					+" NumPade \"+\" - увеличивает область кисточки\n"
					+" NumPade \"-\" - уменьшает область кисточки\n",
					"Закрыть");
			}

		}

		public void drawLine() {
			EditorGUI.DrawRect(GUILayoutUtility.GetRect(120, 2), lineColor);
			EditorGUI.DrawRect(GUILayoutUtility.GetRect(120, 2), lineShadow);
		}
	
	}

}