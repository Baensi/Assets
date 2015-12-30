using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace EngineEditor.Beansi {

	public enum Icons : int {

		Empty   = 0x00,
			    
		Delete  = 0x01,
		Add     = 0x02,
		Remove  = 0x03,

		Edit    = 0x04,
		EditOff = -0x03

	};

	public class IconsFactory {

		private const string PATH = "./Assets/Editor/baensi/icons/data/";

		private static IconsFactory instance;

		private SortedDictionary<Icons, Texture2D> data;

		public static IconsFactory getInstance() {
			if (instance == null)
				instance = new IconsFactory();
			return instance;
        }

		private Texture2D loadIcon(string path) {
			Texture2D result = new Texture2D(1, 1);
				result.LoadImage(System.IO.File.ReadAllBytes(PATH+path));
			result.Apply();
			return result;
        }

		public IconsFactory() {
			data = new SortedDictionary<Icons, Texture2D>();

			data.Add(Icons.Empty,   loadIcon("empty.png"));
								    
			data.Add(Icons.Delete,  loadIcon("delete.png"));
			data.Add(Icons.Add,     loadIcon("add.png"));
			data.Add(Icons.Remove,  loadIcon("remove.png"));
			data.Add(Icons.Edit,    loadIcon("edit.png"));
			data.Add(Icons.EditOff, loadIcon("edit_off.png"));
		}
		

		public Texture2D getIcon(Icons code) {
			Texture2D result = null;

				if (data.ContainsKey(code))
					data.TryGetValue(code, out result);

			return result;
		}

	}

}
