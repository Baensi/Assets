using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Engine.AI.Behavior {

	/// <summary>
	/// Список поведений AI
	/// </summary>
	public class DBehaviorList {

		private static DBehaviorList instance;
		private static SortedDictionary<string, IModelBehaviorAI> behaviors = null;

			public DBehaviorList() {

			}

		public static DBehaviorList getInstance() {
			if (instance == null)
				instance = new DBehaviorList();
			return instance;
        }

		public IModelBehaviorAI getBehavior(string behavior) {
			return behaviors[behavior];
        }

	}

}
