using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EngineEditor.Baensi {

	public interface ITreeTableListener<T,U> : IHandlersListener<T> {

		T OnConstruct();

		U OnConstructItem();

		List<U> GetItems(T tree);

		void OnEdit(List<T> data, int index, T tree);

		void OnEditItem(List<U> items, int index, U item);

	}

}
