using System;
using UnityEditor;
using UnityEngine;

namespace EngineEditor.Terrain {

	/// <summary>
	/// Активатор редактора террайна
	/// </summary>
	public static class Activator {

		public static string TERRAIN_DATA = "TerrainEnviroment";

		[MenuItem("[Baensi]/Редактор ландшафта")]
		public static void ShowWindow() {
			EditorWindow window = EditorWindow.GetWindow(typeof(TerrainWindow));
			window.titleContent = new GUIContent("Ландшафт");
		}

	}

}
