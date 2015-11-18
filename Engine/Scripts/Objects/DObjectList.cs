using System;
using System.Xml;
using System.IO;
using System.Collections.Generic;
using UnityEngine;
using Engine.EGUI.Inventory;
using Engine.I18N;

namespace Engine.Objects {

	public class DObjectList {

		private static DObjectList instance;
		private static SortedDictionary<string,Item> items = null;

		public static DObjectList getInstance() {
			if (instance==null)
				instance = new DObjectList();
			return instance;
		}

			public DObjectList() {
				items=readItems(Dictionary.DictionaryObjectsFileName);
            }

		/// <summary>
		/// Возвращает коллекцию предметов из словаря
		/// </summary>
		/// <returns></returns>
		public List<Item> getItemList() {
			return new List<Item>(items.Values);
        }

		/// <summary>
		/// Инициализирует словарь
		/// </summary>
		/// <param name="fileName"></param>
		/// <returns></returns>
		public SortedDictionary<string, Item> readItems(string fileName) {
			SortedDictionary<string, Item> result = new SortedDictionary<string, Item>();

				XmlDocument xDocument = new XmlDocument();
				XmlTextReader reader = new XmlTextReader(fileName);
				xDocument.Load(reader);
				XmlNodeList objectsList = xDocument.GetElementsByTagName("item");

				foreach (XmlElement item in objectsList) {

					XmlElement  property    = (XmlElement)item.GetElementsByTagName("property")[0];
					XmlElement  description = (XmlElement)item.GetElementsByTagName("description")[0];

					string     name = item.GetAttribute("name");
					string     gameObjectPath = item.GetAttribute("gameObject");
                    GameObject gameObject = Resources.Load<GameObject>(gameObjectPath);
					int        id = Convert.ToInt32(item.GetAttribute("id"));

#if UNITY_EDITOR
					if (gameObject == null)
							Debug.LogError("Не удалось найти префаб для объекта "+ name+", проверьте файл 'Assets/Resources/"+ gameObjectPath + "'!");
#endif

					string iconPath = description.GetAttribute("icon");
                    Texture2D  icon = Resources.Load<Texture2D>(iconPath);

#if UNITY_EDITOR
					if (icon == null)
						Debug.LogError("Не удалось найти иконку для объекта " + name + ", проверьте файл 'Assets/Resources/" + iconPath + "'!");
#endif

					List<SoundPack> soundList = null;
					XmlNodeList sounds = description.GetElementsByTagName("sound");
					string soundPath = null;

					if (sounds.Count > 0) {
						soundList = new List<SoundPack>();
						foreach (XmlElement sound in sounds) {
							soundPath = sound.GetAttribute("sound");

#if UNITY_EDITOR
							if (icon == null)
								Debug.LogError("Не удалось найти звуковой файл для объекта " + name + ", проверьте файл 'Assets/Resources/" + soundPath + "'!");
#endif

						soundList.Add(new SoundPack(Resources.Load<AudioClip>(soundPath), sound.GetAttribute("tag")));
						}
					}

					int width  = Convert.ToInt32(property.GetAttribute("width"));
					int height = Convert.ToInt32(property.GetAttribute("height"));
					int count  = Convert.ToInt32(property.GetAttribute("count"));

					ItemResource resource = new ItemResource(icon, soundList);
					ItemSize     size = new ItemSize(width,height);

					string textName    = description.GetAttribute("textName");
					string textCaption = description.GetAttribute("textCaption");
					float  costValue   = Convert.ToSingle(description.GetAttribute("costValue"));

					ItemDescription itemDescription = new ItemDescription().Create(id,textName,textCaption,costValue);

					result.Add(name, new Item().Create(gameObject, resource, size, count, itemDescription));
				}

				reader.Close();
				reader    = null;
				xDocument = null;

			return result;
		}

		/// <summary>
		/// Возвращает предмет из словаря
		/// </summary>
		/// <param name="key"></param>
		/// <returns></returns>
		public Item getItem(string key) {

			if (items==null)
				items = readItems(Dictionary.DictionaryObjectsFileName);

			Item result;

			items.TryGetValue(key, out result);
			return result;
		}

		/// <summary>
		/// Пересоздаёт ресурсы у предмета (например, может пересоздать метки при смене языка)
		/// </summary>
		public void ReCreate() {
			foreach (Item value in items.Values)
				value.description.ReCreate();
		}

	}

}
