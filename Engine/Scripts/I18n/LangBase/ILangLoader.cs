using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Engine.I18N {

	public interface ILangLoader {
		
		void getData(ref SortedDictionary<string,string> data, ref List<string> localizations);

	}

}