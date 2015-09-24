using UnityEngine;
using System.IO;
using System.Collections;
using System.Collections.Generic;

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

			foreach(string xmlDataBase in Directory.GetFiles(Dictionary.DictionaryDirectoryName, "*.xml")){ // перебираем все xml базы в папке со словорями
                ILangLoader loader = new LangXMLLoader(xmlDataBase);
				loader.getData(ref mapData, ref localizations); // записываем прочитанные данные в словарь
				loader = null;
			}
			
		}
		
		public string get(string key){ // возвращаем слово из словаря
			string result = "";
				mapData.TryGetValue (GameConfig.getInstance().getData(GameConfig.CONFIG_CURRENT_LANGUAGE)+key, out result);
			return result;
		}

	}
}