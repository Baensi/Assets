using UnityEditor;
using UnityEngine;
using Engine.Maps;

namespace EngineEditor.Maps {
	public class TeleportJumpWindow : EditorWindow {

		private TeleportJump teleportJump;

		public void setTeleportJump(TeleportJump teleportJump) {
			this.teleportJump = teleportJump;
		}

			void OnGUI() {

				if(teleportJump==null)
					return;

				teleportJump.playerNewPosition = EditorGUILayout.Vector3Field("Позиция",teleportJump.playerNewPosition);

				if(teleportJump.useRotation)
					teleportJump.playerNewRotation = EditorGUILayout.Vector4Field("Угол",teleportJump.playerNewRotation);
			}

		void OnFocus() {
			try { 
				SceneView.onSceneGUIDelegate -= this.OnSceneGUI;
				SceneView.onSceneGUIDelegate += this.OnSceneGUI;
			} catch { }
			}
		void OnDestroy() {
			try {
				SceneView.onSceneGUIDelegate -= this.OnSceneGUI;
			} catch { }
		}

		public void OnSceneGUI(SceneView sceneView) {

			if(teleportJump==null)
				return;

			teleportJump.playerNewPosition = Handles.DoPositionHandle(teleportJump.playerNewPosition, Quaternion.Euler(teleportJump.playerNewRotation));
			
			Vector3 point = teleportJump.playerNewPosition;
			Quaternion startRot = Quaternion.Euler(teleportJump.playerNewRotation);

            Handles.color = new Color(0f, 1f, 0f);

			Handles.color = new Color(1f, 0f, 0f);
			Handles.DrawLine(point, point + (startRot * new Vector3(-1f, 0, 0)));
			Handles.DrawLine(point, point + (startRot * new Vector3(1f, 0, 0)));
			Handles.DrawLine(point, point + (startRot * new Vector3(0, 1f, 0)));
			Handles.DrawLine(point, point + (startRot * new Vector3(0, -1f, 0)));

			if(!teleportJump.useRotation)
				return;

			Handles.color = new Color(1f, 1f, 0f);
			Vector3 seePoint = teleportJump.playerNewPosition + startRot*new Vector3(0f,0f,1f);
			Handles.DrawLine(point, seePoint);

			Handles.ConeCap(0,seePoint,startRot,0.1f);

        }

	}
}
