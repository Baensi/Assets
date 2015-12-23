using UnityEngine;
using System.Xml;
using System.Collections.Generic;
using Engine.I18N;
using System.IO;

namespace Engine.Sounds {

	/// <summary>
	/// Аудио-словарь
	/// </summary>
	public class DSoundList {

		private static DSoundList instance;

		private SortedDictionary<string, string>    paramsData;
		private SortedDictionary<string, AudioClip> audioData;

		public static DSoundList getInstance() {
			if (instance == null)
				instance = new DSoundList();
			return instance;
        }

			public DSoundList() {

				audioData = new SortedDictionary<string, AudioClip>();
				paramsData = new SortedDictionary<string, string>();

				XmlDocument xDocument = new XmlDocument();
				XmlTextReader reader = new XmlTextReader(Dictionary.DictionarySoundsFileName);
				xDocument.Load(reader);
				XmlNodeList objectsList = xDocument.GetElementsByTagName("item");

					foreach(XmlElement node in objectsList) {

						string name = node.GetAttribute("name");
						string path = node.GetAttribute("path");
						
						AudioClip sound = Resources.Load<AudioClip>(path);
#if UNITY_EDITOR
						if (sound==null)
							Debug.LogError("["+name+"] Не удалось загрузить ресурс '"+path+"'!");
#endif

						audioData.Add(name,sound);

					}

				XmlNodeList paramsList = xDocument.GetElementsByTagName("parameter");

					foreach(XmlElement param in paramsList) {

						string name  = param.GetAttribute("name");
						string value = param.GetAttribute("value");

						paramsData.Add(name, value);

                    }

				reader.Close();
				reader = null;
				xDocument = null;
            }

		/// <summary>
		/// Возвращает аудиофайл из словаря
		/// </summary>
		/// <param name="name">Имя аудиофайла</param>
		/// <returns></returns>
		public AudioClip getSound(string name) {
			AudioClip result = null;

			if(audioData.ContainsKey(name))
				audioData.TryGetValue(name, out result);
#if UNITY_EDITOR
			else
				Debug.LogError("Чтение звука - Записи '"+name+"' не существует в словаре!");
#endif

			return result;

		}

		/// <summary>
		/// Возвращает текстовый параметр из словаря звуков
		/// </summary>
		/// <param name="name">Имя текстового параметра</param>
		/// <returns></returns>
		public string getParameter(string name) {
			string result = null;

			if(paramsData.ContainsKey(name))
				paramsData.TryGetValue(name, out result);
#if UNITY_EDITOR
			else
				Debug.LogError("Чтение параметра звука - Записи '"+name+"' не существует в словаре!");
#endif

			return result;

		}

	}

}
