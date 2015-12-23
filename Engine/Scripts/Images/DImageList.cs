using UnityEngine;
using System.Collections.Generic;
using System.Xml;
using Engine.I18N;

namespace Engine.Images {

	public class DImageList {

		private static DImageList instance;

		private SortedDictionary<string, Texture2D> imageData;

		public static DImageList getInstance() {
			if (instance == null)
				instance = new DImageList();
			return instance;
        }

			public DImageList() {

				imageData = new SortedDictionary<string, Texture2D>();

				XmlDocument xDocument = new XmlDocument();
				XmlTextReader reader = new XmlTextReader(Dictionary.DictionaryImagesFileName);
				xDocument.Load(reader);
				XmlNodeList objectsList = xDocument.GetElementsByTagName("item");

				foreach (XmlElement node in objectsList) {

					string name = node.GetAttribute("name");
					string path = node.GetAttribute("path");

					Texture2D image = Resources.Load<Texture2D>(path);
#if UNITY_EDITOR
					if (image == null)
						Debug.LogError("[" + name + "] Не удалось загрузить ресурс '" + path + "'!");
#endif

					imageData.Add(name, image);

				}

				reader.Close();
				reader = null;
				xDocument = null;

			}

		public Texture2D getImage(string name) {
			Texture2D result = null;
			
			if(imageData.ContainsKey(name))
				imageData.TryGetValue(name, out result);
#if UNITY_EDITOR
			else 
				Debug.LogError("Попытка доступа к несуществующей картинке в словаре - '" + name + "' не найден в словаре!");
#endif

			return result;

		}
	}

}
