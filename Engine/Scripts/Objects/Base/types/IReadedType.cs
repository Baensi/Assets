using UnityEngine;
using System.Collections;

namespace Engine.Objects.Types {

	/// <summary>
	/// Предмет можно прочитать
	/// </summary>
	public interface IReadedType {
		
		void onReadStart(); // предмет начали читать
		void onReadEnd();   // предмет закончили читать
		void onReadChangePage(int page); // в предмете перелистнули страницу на page
		void onPageReaded(int page); // в предмете прочитали страницу page
		
	}

}