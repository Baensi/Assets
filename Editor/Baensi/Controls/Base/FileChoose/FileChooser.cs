using System;
using UnityEditor;
using UnityEngine;
using System.IO;

namespace EngineEditor.Beansi {

	public static class FileChooser {

		private const string DEFAULT_PATH = "./Assets/Engine/Scripts/";

		private static string getAssetsPath(string path) {
			int start = path.IndexOf("/Assets/");
			return "."+path.Substring(start,path.Length-start);
		}

		public static string SettingsFileField(string fileName) {

			bool newFileMode = (fileName == null || fileName.Equals(""));
			string file = newFileMode? "" : fileName;

			if(newFileMode && File.Exists(file))
				file = "<Нет>";
			else
				file = Path.GetFileNameWithoutExtension(file);

			GUILayout.BeginHorizontal();

				GUILayout.Label("Файл:", GUILayout.Width(98), GUILayout.Height(20));

				GUI.color = new Color(0.9f,1f,0.95f);
				GUILayout.TextField(file);
				GUI.color = Color.white;

				if (newFileMode && GUILayout.Button("*", GUILayout.Width(20),GUILayout.Height(17))) {
					string path = EditorUtility.SaveFilePanel("Конфигурационный файл",DEFAULT_PATH,"new file name","baensicfg");
					if (path.Length != 0) {
						fileName = getAssetsPath(path);
						File.Create(path).Close();
					}
				}

				if (GUILayout.Button("...", GUILayout.Width(24),GUILayout.Height(17))) {
					string path = EditorUtility.OpenFilePanel("Конфигурационный файл",DEFAULT_PATH,"baensicfg");
					if (path.Length != 0)
						fileName = getAssetsPath(path);
					
				}

			GUILayout.EndHorizontal();

			return fileName;
		}


	}

}
