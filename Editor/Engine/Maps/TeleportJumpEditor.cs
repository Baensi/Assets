using UnityEditor;
using UnityEngine;
using Engine.Maps;

namespace EngineEditor.Maps {

	[CustomEditor(typeof(TeleportJump))]
	public class TeleportJumpEditor : Editor {

		private TeleportJumpWindow window;

		public override void OnInspectorGUI() {
			base.OnInspectorGUI();

			if (GUILayout.Button("Редактировать")) {

				if(window!=null)
					window.Close();

				window = (TeleportJumpWindow)EditorWindow.GetWindow(typeof(TeleportJumpWindow));
				window.setTeleportJump(target as TeleportJump);
                window.titleContent.text = "Телепорт";

			}

		}



	}

}
