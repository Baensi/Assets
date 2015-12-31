using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EngineEditor.Baensi {

	public interface ITableListener<T> : IHandlersListener<T>  {

		T OnConstruct();

		void OnEdit(List<T> data, int index, T item);

	}

}
