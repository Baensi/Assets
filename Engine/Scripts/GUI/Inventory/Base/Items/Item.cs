using System;
using UnityEngine;
using System.Collections;
using Engine.Objects;
using Engine.I18N;

namespace Engine.EGUI.Inventory {

	[Serializable]
	public class Item : IItem {

		private string itemName;

		[SerializeField] public ItemDescription description;
		[SerializeField] public ItemSize        size;
		[SerializeField] public ItemResource    resource = new ItemResource(null, null);

		[SerializeField] public int  count;
		[SerializeField] public int  maxCount;

		private GameObject gameObject;

		public override bool Equals(object obj){
			IItem item = obj as IItem;

				if(item==null)
					return false;

				if (item.toGameObject()==null || toGameObject()==null)
					return false;

			return (item.toGameObject().Equals(toGameObject()));

		}

		public override int GetHashCode() {
			return gameObject.GetHashCode();
		}

		/// <summary>
		/// Число экземпляров равно максимальному числу экземпляров
		/// </summary>
		/// <returns></returns>
		public bool isFullCount() {
			return count==maxCount;
		}

		public int getMaxCount(){
			return maxCount;
		}

		public GameObject toGameObject(){
			return gameObject;
		}

		/// <summary>
		/// Увеличивает число предметов на value
		/// </summary>
		/// <param name="value">Число добавляемых экземпляров</param>
		/// <returns>Если предметы не влезли, возвращает остаток</returns>
		public int incCount(int value) {

			if (count+value>maxCount) {

				int result = value+count - maxCount;
				count = maxCount;

				return result;

			} else {

				count+=value;

			}

			return 0;
		}

		/// <summary>
		/// Уменьшает число предметов на value
		/// </summary>
		/// <param name="value">Число отнимаемых экземпляров</param>
		/// <returns>Если уменьшено до числа меньшего 0, возвращает остаток</returns>
		public int decCount(int value) {

			if (count<value) {

				int result = value - count;
				count = 0;

				return result;

			}

			count-=value;

			return 0;
		}

		public int incCount(){
			if (count==maxCount)
				return 1;

			count++;
			return 0;
		}

		public int decCount(){
			if (count==0)
				return 1;

			count--;
			return 0;
		}

		public int getCount(){
			return count;
		}
		public void setCount(int count){
			this.count=count;
		}

		public ItemDescription getDescription() {
			return description;
		}

		public Texture getIcon(){
			return resource.icon;
		}

		public ItemSize getSize(){
			return size;
		}

		public void setSize(ItemSize size){
			this.size=size;
		}

		public Item Clone() {
			Item result = new Item();

				result.gameObject  = gameObject;
				result.description = description;
				result.resource    = resource;
				result.size        = size;

				result.count    = count;
				result.maxCount = maxCount;

				result.itemName = itemName;

			return result;
		}

		public Item Create(GameObject gameObject, ItemResource resource, ItemSize size, int maxCount, ItemDescription description) {
			this.itemName=resource.files.itemName;
			this.gameObject=gameObject;
			this.resource=resource;
			this.size=size;
			this.maxCount=maxCount;
			this.count=1;
			this.description=description;
			return this;
		}

	}

}
