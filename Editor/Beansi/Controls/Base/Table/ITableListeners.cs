using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EngineEditor.Beansi {

	public interface ITableListeners<T> : ITableCustomRowConstructor<T>, ITableCustomRowEditListener<T> {

	}

}
