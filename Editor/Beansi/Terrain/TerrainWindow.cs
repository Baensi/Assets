using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Engine.Objects;

namespace EngineEditor.Terrain {
	
	public class TerrainWindow : EditorWindow {

		private static Color textColor         = new Color(0.9f,0.9f,0.9f);
		private static Color designToolsColor  = new Color(0.15f, 0.5f, 0.15f);
		private static Color designAddColor    = new Color(0.15f,1.0f,0.15f);
		private static Color designDeleteColor = new Color(1.0f,0.0f,0.0f);
		private static Color designPickColor   = new Color(1.0f, 1.0f, 0.0f);

		private GameObject           selectionObject = null; // Объект выбранный в сцене
		private PickObjectData       pickSceneObject = null; // Объект захваченный для кисточки
		private List<HideObjectData> gameData        = new List<HideObjectData>();

		private EEditorMode          currentMode = EEditorMode.ModeAdd;
		private List<TempObject>     tmpData     = new List<TempObject>();

		private Vector3              oldPosition = new Vector3(0, 0, 0);
		private Quaternion           oldRotation = new Quaternion(0,0,0,0);

		private bool selectModeOn = false;
		private bool addingModeOn = false;
		private bool deleteModeOn = false;
		
		private static WindowGUI windowGUI;

		void OnEnable() {

		}

		public GameObject selection {
			get { return selectionObject; }
			set { selectionObject = value; }
		}

		public PickObjectData pickupObject {
			get { return pickSceneObject; }
			set { pickSceneObject = value; }
		}

		public TerrainWindow() {
			windowGUI = new WindowGUI(this);
		}

		public GameObject getDataContainer() {
			Transform transform = getTerrainContainer().transform.Find("Data");
			GameObject result;

			if (transform==null) {
				result = new GameObject("Data");
				result.transform.parent = getTerrainContainer().transform;
			} else {
				result = transform.gameObject;
			}

			return result;
		}

		public GameObject getTmpContainer() {
			Transform transform = getTerrainContainer().transform.Find("_tmp");
			GameObject result;

				if (transform==null){
					result = new GameObject("_tmp");
					result.transform.parent = getTerrainContainer().transform;
				} else {
					result = transform.gameObject;
				}

			return result;
		}

		public GameObject getTerrainContainer() {
			GameObject result = GameObject.Find(Activator.TERRAIN_DATA);
				if (result==null)
					result = new GameObject(Activator.TERRAIN_DATA);
			return result;
		}

		public void doDropObject() {
			if (pickupObject == null) return;

			foreach (HideObjectData gameObject in gameData) // чистим данные
				gameObject.Restore();

			gameData.Clear();
			pickupObject.Destroy();
			pickupObject = null;
		}

		public void doPickUpObject() {
			doDropObject();
			pickupObject = new PickObjectData(selection);
			foreach (GameObject gameObject in UnityEngine.Object.FindObjectsOfType<GameObject>()) // собираем данные о невыбранных объектах
				gameData.Add(new HideObjectData(gameObject)); // исключаем объекты из редактирования нашим плагином
			pickupObject.doShowRaycast();
		}

		void OnGUI(){

			GUILayout.Width(600f);
			GUILayout.Height(800f);

			windowGUI.CreateBrushSettings();
			windowGUI.CreateModeSettings();
			windowGUI.CreateGenerationSettings();

			windowGUI.CreatePickSettings();

			GUILayout.BeginHorizontal();

				if (GUILayout.Button("Перегенерировать")) {
					OnGenerateObjects(oldPosition, oldRotation);
				}
				windowGUI.CreateHelpSettings();

			GUILayout.EndHorizontal();

		}

		void OnFocus() {
			SceneView.onSceneGUIDelegate -= this.OnSceneGUI;
			SceneView.onSceneGUIDelegate += this.OnSceneGUI;

			getTmpContainer();
			getDataContainer();
		}

		void OnDestroy() {
			SceneView.onSceneGUIDelegate -= this.OnSceneGUI;
			doDropObject();
			OnCleanGenerateObjects();
		}

		/// <summary>
		/// Горячие клавиши
		/// </summary>
		private void onControls() {

			if (Event.current.keyCode == KeyCode.KeypadPlus) {
				windowGUI.brushSize += 0.02f;
				this.Repaint();
			}

			if (Event.current.keyCode == KeyCode.KeypadMinus) {
				windowGUI.brushSize -= 0.02f;
				this.Repaint();
			}

			if (Event.current.keyCode == KeyCode.F9 && Event.current.type == EventType.KeyDown) {
				windowGUI.designMode=!windowGUI.designMode;
				this.Repaint();
			}

			if (Event.current.keyCode == KeyCode.F10 && Event.current.type == EventType.KeyDown) {
				windowGUI.consoleMode=!windowGUI.consoleMode;
				this.Repaint();
			}

			if (Event.current.type == EventType.KeyDown && Event.current.keyCode == KeyCode.Escape) {
				doDropObject();
				this.Repaint();
			}

			if (Event.current.alt) 
				currentMode = EEditorMode.ModePick;

			if (Event.current.control)
				currentMode = EEditorMode.ModeDelete;

			if (!Event.current.alt && !Event.current.control)
				currentMode = EEditorMode.ModeAdd;

			selectModeOn = Event.current.alt && Event.current.type == EventType.MouseUp && Event.current.button == 0;
			addingModeOn = Event.current.shift && Event.current.type == EventType.MouseDown;
			deleteModeOn = Event.current.control && Event.current.type == EventType.mouseDown;

		}

		/// <summary>
		/// Метод обрабатывающий лучи добра, посланные в окно SceveView
		/// </summary>
		/// <param name="sceneView"></param>
		/// <param name="hitInfo">Объект, до которого дотронулись лучи добра</param>
		private void OnRaycast(SceneView sceneView, RaycastHit hitInfo) {

			Collider   collider    = hitInfo.collider;
			Vector3    cameraPoint = sceneView.camera.transform.position + sceneView.camera.transform.forward + sceneView.camera.transform.right;
			Quaternion startRot    = Quaternion.LookRotation(hitInfo.normal);

			oldPosition = hitInfo.point;
			oldRotation = startRot;

			switch (currentMode) {
				case EEditorMode.ModeDelete:

					OnCleanGenerateObjects();

					Handles.color = designDeleteColor;

					Handles.CircleCap(0, hitInfo.point, startRot, windowGUI.brushSize);
					Handles.CircleCap(2, hitInfo.point, startRot, windowGUI.brushSize*0.9f);
					Handles.CircleCap(3, hitInfo.point, startRot, windowGUI.brushSize*0.8f);

					if (deleteModeOn)
						foreach (AutoGen obj in UnityEngine.Object.FindObjectsOfType<AutoGen>())
							if (Vector3.Distance(obj.transform.position, hitInfo.point) <= windowGUI.brushSize) 
								DestroyImmediate(obj.gameObject);

					break;
				case EEditorMode.ModeAdd:

					if (windowGUI.baseObjectPrefab!=null) {
						if (tmpData.Count != windowGUI.brushSensitivity) {
							OnGenerateObjects(hitInfo.point, startRot);
						} else {

							foreach (TempObject tmp in tmpData)
								OnSetupSettings(tmp, hitInfo.point, startRot, false);

						}

						if (addingModeOn) {
							OnAddObjectToTerrainData();
							addingModeOn=false;
						}

					}

					if(Event.current.shift)
						Handles.color = designPickColor;
					else
						Handles.color = designAddColor; 

					break;
				case EEditorMode.ModePick:

					Handles.color = designPickColor;
					Handles.DotCap(0, hitInfo.collider.bounds.center, startRot, 0.2f); // рисуем центр выбранного объекта

						if (selectModeOn) { // если надо захватить объект
							selection = hitInfo.collider.gameObject;
							doPickUpObject();
							this.Repaint();
							selectModeOn=false;
						}
					
					break;
			}

				Handles.DrawLine(hitInfo.point, cameraPoint);

					// рисуем разметку кисточки
				Handles.color = designToolsColor;
				Handles.DrawLine(hitInfo.point, hitInfo.point + (startRot * new Vector3(-windowGUI.brushSize, 0, 0)));
				Handles.DrawLine(hitInfo.point, hitInfo.point + (startRot * new Vector3(windowGUI.brushSize, 0, 0)));
				Handles.DrawLine(hitInfo.point, hitInfo.point + (startRot * new Vector3(0, windowGUI.brushSize, 0)));
				Handles.DrawLine(hitInfo.point, hitInfo.point + (startRot * new Vector3(0, -windowGUI.brushSize, 0)));
				
					// направление нормали
				Handles.DrawLine(hitInfo.point, hitInfo.point + (startRot * new Vector3(0, 0, windowGUI.brushSize*0.2f)));

				if (windowGUI.consoleMode) {

					Handles.color = textColor;

					Handles.Label(collider.transform.position, new GUIContent(Utils.ToString(collider.transform.position)));
					Handles.Label(hitInfo.point, new GUIContent("\n"+Utils.ToString(hitInfo.normal)+"\n"+Utils.ToString(hitInfo.barycentricCoordinate)));
					Handles.Label(hitInfo.point, Utils.ToString(startRot));

				}
			
			sceneView.Repaint();
		}

		private void OnSetupSettings(TempObject tmp, Vector3 position, Quaternion rotation, bool newRandom = false) {

			Renderer renderer = tmp.toGameObject().GetComponent<Renderer>();
			Bounds   bounds;

				if (renderer!=null) {
					bounds = renderer.bounds;
				} else {
					bounds = new Bounds();
				}

			if (newRandom)
				Generator.generateRandom(tmp);

			if (windowGUI.generateRandomPosition)
				tmp.PositionOffset = Generator.generatePosition(tmp.PositionRandom, position, rotation, bounds, tmp.toGameObject().transform.lossyScale, windowGUI.offsetFromValue, windowGUI.offsetToValue, windowGUI.brushSize, windowGUI.useRaycast);

			if (windowGUI.generateRandomRotation)
				tmp.RotationOffset = Generator.generateRotation(tmp.PositionRandom, windowGUI.minRotationXValue, windowGUI.maxRotationXValue, windowGUI.minRotationYValue, windowGUI.maxRotationYValue, windowGUI.minRotationZValue, windowGUI.maxRotationZValue);

			if (windowGUI.generateChangeScale)
				tmp.ScaleOffset = Generator.generateScale(tmp.PositionRandom, windowGUI.minScaleXValue, windowGUI.maxScaleXValue, windowGUI.minScaleYValue, windowGUI.maxScaleYValue, windowGUI.minScaleZValue, windowGUI.maxScaleZValue);

			tmp.SetupSettings(position, rotation); // устанавливаем свойства

		}

		private void OnGenerateObject(Vector3 position, Quaternion rotation) {

			if (windowGUI.baseObjectPrefab==null) return;

			TempObject tmp = new TempObject((GameObject)Instantiate(windowGUI.baseObjectPrefab, position, rotation));
			tmp.toGameObject().transform.parent = getTmpContainer().transform;
			
				OnSetupSettings(tmp, position, rotation, true);
			
			tmpData.Add(tmp);
		}

		private void OnCleanGenerateObjects() {
			if (tmpData.Count > 0) {
				foreach (TempObject tmp in tmpData)
					DestroyImmediate(tmp.toGameObject());
				tmpData.Clear();
			}

			Transform container = getTmpContainer().transform;
			while (container.childCount > 0)
				DestroyImmediate(container.GetChild(0).gameObject);
		}

		private void OnGenerateObjects(Vector3 position, Quaternion rotation) {

			OnCleanGenerateObjects();

			for (int i=1; i<=windowGUI.brushSensitivity; i++) // генерируем нужное число объектов
				OnGenerateObject(position, rotation);

		}

		private void OnAddObjectToTerrainData() {

			if (tmpData==null || tmpData.Count==0)
				return;

				foreach(TempObject tmp in tmpData){

					GameObject obj = tmp.toGameObject();
						obj.layer = 0;
						obj.transform.parent = getDataContainer().transform;
						obj.AddComponent<AutoGen>();

				}

			tmpData.Clear();

		}

		/// <summary>
		/// Отрисовка в окне сцены
		/// </summary>
		/// <param name="sceneView"></param>
		public void OnSceneGUI(SceneView sceneView) {

			onControls();

			if (!windowGUI.designMode) {
				if (Selection.activeGameObject != selection)
					this.Repaint();
				return;
			}

			Selection.activeObject = null;

			Vector3 mousePosition = new Vector3(Event.current.mousePosition.x, sceneView.camera.pixelHeight - Event.current.mousePosition.y, 0);
			RaycastHit hitInfo = new RaycastHit();
			Ray ray = sceneView.camera.ScreenPointToRay(mousePosition); // генерируем луч добра
			
			if (Physics.Raycast(ray, out hitInfo)) // одобряем всё и вся нашим лучом
				OnRaycast(sceneView,hitInfo); // дёргаем метод обрабатывающий одобреный объект

		}

		
	}
	
}
