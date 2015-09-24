using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml;

namespace Engine.I18N {

	public class LangXMLLoader : ILangLoader {

		private static string itemName        = "item";
		private static string itemIdAttribute = "id";

		private string fileName;

		public LangXMLLoader(string fileName){
			this.fileName = fileName;
		}

		public void getData(ref SortedDictionary<string,string> data){
			XmlTextReader reader = new XmlTextReader (fileName);

			string key;
			string value;

				reader.Read();

					while (reader.Read()) {
						if(reader.Name.Equals(itemName)){
							key = reader.GetAttribute(itemIdAttribute);
							value = reader.ReadElementString();
							
							if(key!=null)
								data.Add(key,value);
						}
					}

				reader.Close ();

		}

	}
}