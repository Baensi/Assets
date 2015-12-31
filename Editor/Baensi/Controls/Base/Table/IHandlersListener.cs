using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EngineEditor.Baensi {

	public interface IHandlersListener<T> {

		void OnHandlers(T item);

	}

}
