using UnityEditor;
using UnityEngine;
using System.Collections.Generic;

namespace EngineEditor.Data {

	/// <summary>
	/// Словарь активных окон редактора
	/// </summary>
	public class EditorFactory {

		private static EditorFactory instance;

		private SortedDictionary<string, EditorWindow> windows = new SortedDictionary<string, EditorWindow>();

		public static EditorFactory getInstance() {
			if (instance == null)
				instance = new EditorFactory();
            return instance;
		}

		/// <summary>
		/// Ищет и возвращает окно редактора из словаря по имени
		/// </summary>
		/// <param name="name"></param>
		/// <returns></returns>
		public EditorWindow FindWindow(string name) {
			EditorWindow result = null;

			if (windows.TryGetValue(name, out result))
				return result;
			else
				return null;

        }

		/// <summary>
		/// Регистрирует окно редактора в словаре
		/// </summary>
		/// <param name="name">Имя окно редактора</param>
		/// <param name="window"></param>
		public void RegWindow(string name, EditorWindow window) {
			windows[name] = window;
		}

		public void UnReg(string name) {
			windows.Remove(name);
		}

	}

}
