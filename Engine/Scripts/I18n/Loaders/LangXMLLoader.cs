using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml;

namespace Engine.I18N {

	public class LangXMLLoader : ILangLoader {

		private static string LOCALIZATION_NAME      = "lang";
		private static string LOCALIZATION_ATTRIBUTE = "name";
		private static string ITEM_NAME              = "item";
		private static string ITEM_ATTRIBUTE         = "id";

		private string fileName;

		public LangXMLLoader(string fileName){
			this.fileName = fileName;
		}

		public void getData(ref SortedDictionary<string,string> data, ref List<string> localizations){
			XmlTextReader reader = new XmlTextReader (fileName);

			string currentLocal=GameConfig.Localization;
			string key;
			string value;

				reader.Read();

				while (reader.Read()) {

					if (reader.Name.Equals(LOCALIZATION_NAME)) {
						currentLocal = reader.GetAttribute(LOCALIZATION_ATTRIBUTE);

						if (!localizations.Contains(currentLocal))
							localizations.Add(currentLocal);

					}

					if(reader.Name.Equals(ITEM_NAME)){

						key = reader.GetAttribute(ITEM_ATTRIBUTE);
						value = reader.ReadElementString();

						if (key!=null) 
							data.Add(currentLocal+key, value);

					}
				}

			reader.Close();
			reader  = null;

		}

	}
}