using UnityEngine;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using Engine.Objects;

namespace Engine.I18N {

	public class CLang {

		private List<string>                    localizations;
		private SortedDictionary<string,string> mapData;
		private static CLang instance;

		public static CLang getInstance(){
			if (instance == null)
				instance = new CLang();
			return instance;
		}

		// инициализация словаря
		public CLang(){

			mapData = new SortedDictionary<string,string>(); // инициализируем словарь
			localizations = new List<string>(); // инициализируем сисок локализаций

			foreach(string xmlDataBase in Directory.GetFiles(Dictionary.DictionaryI18nDirectoryName, "*.xml")){ // перебираем все xml базы в папке со словорями
                ILangLoader loader = new LangXMLLoader(xmlDataBase);
				loader.getData(ref mapData, ref localizations); // записываем прочитанные данные в словарь
				loader = null;
			}

			GameConfig.Init();
			
		}
		
		public string get(string key){ // возвращаем слово из словаря
			string result = "";

			if(mapData.TryGetValue(GameConfig.Localization+key, out result))
				return result;
			else {

#if UNITY_EDITOR
				Debug.LogError("Попытка доступа к несуществующей надписи в словаре - '" + GameConfig.Localization + key + "' не найден в словаре I18N!");
#endif

				return "";
			}
		}

	}
}