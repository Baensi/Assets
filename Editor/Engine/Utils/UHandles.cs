using System;
using UnityEditor;
using UnityEngine;

namespace EngineEditor.EUtils {

	/// <summary>
	/// Состояния
	/// </summary>
	public enum DragHandleResult : int {

		None = 0,

		LeftMouseButtonPress,
		LeftMouseButtonClick,
		LeftMouseButtonDoubleClick,
		LeftMouseButtonDrag,
		LeftMouseButtonRelease,

		RightMouseButtonPress,
		RightMouseButtonClick,
		RightMouseButtonDoubleClick,
		RightMouseButtonDrag,
		RightMouseButtonRelease,

	};

	/// <summary>
	/// Автор реализации "higekun"
	/// url: http://answers.unity3d.com/questions/463207/how-do-you-make-a-custom-handle-respond-to-the-mou.html
	/// </summary>
	public class UHandles {

		static int s_DragHandleHash = "DragHandleHash".GetHashCode();
		static Vector2 s_DragHandleMouseStart;
		static Vector2 s_DragHandleMouseCurrent;
		static Vector3 s_DragHandleWorldStart;
		static float s_DragHandleClickTime = 0;
		static int s_DragHandleClickID;
		static float s_DragHandleDoubleClickInterval = 0.5f;
		static bool s_DragHandleHasMoved;

		public static int lastDragHandleID;
		
		/// <summary>
		/// Отрисовывает перемещаемую "точку" в редиме редактировния
		/// </summary>
		/// <param name="position">Предыдущее положение точки</param>
		/// <param name="handleSize">Размер видимой перемещаемой точки</param>
		/// <param name="capFunc">Функция отрисовки перемещаемой точки</param>
		/// <param name="colorSelected">Цвето точки при выделении</param>
		/// <param name="result">Текущее событие</param>
		/// <returns>Возвращает новую позицию точки</returns>
		public static Vector3 DragHandle(Vector3 position, float handleSize, Handles.DrawCapFunction capFunc, Color colorSelected, out DragHandleResult result) {
			int id = GUIUtility.GetControlID(s_DragHandleHash, FocusType.Passive);
			lastDragHandleID = id;

			Vector3 screenPosition = Handles.matrix.MultiplyPoint(position);
			Matrix4x4 cachedMatrix = Handles.matrix;

			result = DragHandleResult.None;

			switch (Event.current.GetTypeForControl(id)) {
				case EventType.MouseDown:
					if (HandleUtility.nearestControl == id && (Event.current.button == 0 || Event.current.button == 1)) {
						GUIUtility.hotControl = id;
						s_DragHandleMouseCurrent = s_DragHandleMouseStart = Event.current.mousePosition;
						s_DragHandleWorldStart = position;
						s_DragHandleHasMoved = false;

						Event.current.Use();
						EditorGUIUtility.SetWantsMouseJumping(1);

						if (Event.current.button == 0)
							result = DragHandleResult.LeftMouseButtonPress;
						else if (Event.current.button == 1)
							result = DragHandleResult.RightMouseButtonPress;
					}
					break;

				case EventType.MouseUp:
					if (GUIUtility.hotControl == id && (Event.current.button == 0 || Event.current.button == 1)) {
						GUIUtility.hotControl = 0;
						Event.current.Use();
						EditorGUIUtility.SetWantsMouseJumping(0);

						if (Event.current.button == 0)
							result = DragHandleResult.LeftMouseButtonRelease;
						else if (Event.current.button == 1)
							result = DragHandleResult.RightMouseButtonRelease;

						if (Event.current.mousePosition == s_DragHandleMouseStart) {
							bool doubleClick = (s_DragHandleClickID == id) &&
								(Time.realtimeSinceStartup - s_DragHandleClickTime < s_DragHandleDoubleClickInterval);

							s_DragHandleClickID = id;
							s_DragHandleClickTime = Time.realtimeSinceStartup;

							if (Event.current.button == 0)
								result = doubleClick ? DragHandleResult.LeftMouseButtonDoubleClick : DragHandleResult.LeftMouseButtonClick;
							else if (Event.current.button == 1)
								result = doubleClick ? DragHandleResult.RightMouseButtonDoubleClick : DragHandleResult.RightMouseButtonClick;
						}
					}
					break;

				case EventType.MouseDrag:
					if (GUIUtility.hotControl == id) {
						s_DragHandleMouseCurrent += new Vector2(Event.current.delta.x, -Event.current.delta.y);
						Vector3 position2 = Camera.current.WorldToScreenPoint(Handles.matrix.MultiplyPoint(s_DragHandleWorldStart))
							+ (Vector3)(s_DragHandleMouseCurrent - s_DragHandleMouseStart);
						position = Handles.matrix.inverse.MultiplyPoint(Camera.current.ScreenToWorldPoint(position2));

						if (Camera.current.transform.forward == Vector3.forward || Camera.current.transform.forward == -Vector3.forward)
							position.z = s_DragHandleWorldStart.z;
						if (Camera.current.transform.forward == Vector3.up || Camera.current.transform.forward == -Vector3.up)
							position.y = s_DragHandleWorldStart.y;
						if (Camera.current.transform.forward == Vector3.right || Camera.current.transform.forward == -Vector3.right)
							position.x = s_DragHandleWorldStart.x;

						if (Event.current.button == 0)
							result = DragHandleResult.LeftMouseButtonDrag;
						else if (Event.current.button == 1)
							result = DragHandleResult.RightMouseButtonDrag;

						s_DragHandleHasMoved = true;

						GUI.changed = true;
						Event.current.Use();
					}
					break;

				case EventType.Repaint:
					Color currentColour = Handles.color;
					if (id == GUIUtility.hotControl && s_DragHandleHasMoved)
						Handles.color = colorSelected;

					Handles.matrix = Matrix4x4.identity;
					capFunc(id, screenPosition, Quaternion.identity, handleSize);
					Handles.matrix = cachedMatrix;

					Handles.color = currentColour;
					break;

				case EventType.Layout:
					Handles.matrix = Matrix4x4.identity;
					HandleUtility.AddControl(id, HandleUtility.DistanceToCircle(screenPosition, handleSize));
					Handles.matrix = cachedMatrix;
					break;
			}

			return position;
		}
	}

}
