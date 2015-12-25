using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EngineEditor.Beansi {

	public interface ITableCustomRowEditListener<T> {

		void OnEdit(List<T> data, int index, T item);

	}

}
