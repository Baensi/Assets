using System;
using System.Collections.Generic;
using UnityEngine;

namespace Engine.Objects {
	
	/// <summary>
	/// Корзина
	/// </summary>
	public class Trash {
	
		public static Trash instance;
		
		private List<GameObject> trashData;
		
			public Trash(){
				trashData = new List<GameObject>();
			}
		
		/// <summary>
		/// Помечает объект removeObject как удалённый и заносит в корзину
		/// </summary>
		/// <param name="removeObject">Объект который планируется удалить</param>
		public void Add(GameObject removeObject){
			trashData.Add(removeObject);
		}
		
		/// <summary>
		/// Удаление всех объектов в корзине и очистка корзины
		/// </summary>
		public void Clean(){
			
			if(trashData.Count<=0)
				return;
			
				foreach(GameObject gameObject in trashData)
					MonoBehaviour.Destroy(gameObject);

			trashData.Clear();
		}
		
		public static Trash getInstance(){
			if(instance==null)
				instance =  new Trash();
			return instance;
		}
	
	}
	
}