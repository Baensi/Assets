using UnityEngine;
using UnityEditor;
using System.Collections;
using Engine.EGUI.Inventory;
using Engine.Objects.Types;
using Engine.Objects.Special;
using Engine.Player;

namespace Engine.Objects {

	/// <summary>
	/// Класс предмета. Пользователь может взаимодействовать с этим объектом, если реализовать
	/// необходимые интерфейсы.
	/// </summary>
	public class DynamicObject : MonoBehaviour, IDynamicObject {

		public Item      item = null;
		
		protected TextDisplayed displayed  = TextDisplayed.Displayed;
		protected Renderer      currentRenderer;

		protected static Shader selectedShader = null;
		protected Shader defaultShader;

		protected bool selected;
		protected bool destroy = false;

		protected ObjectsSelector objectsSelector;

			public void OnStart() {
			
				Trash.getInstance().Clean(); // Пытаемся очистить корзину
			
				if(gameObject.GetComponent<Rigidbody>()==null)
					gameObject.AddComponent<Rigidbody>();

				if(gameObject.GetComponent<Collider>()==null)
					gameObject.AddComponent<BoxCollider>();

				objectsSelector = SingletonNames.getMainCamera().GetComponent<ObjectsSelector>();
			
				currentRenderer = gameObject.GetComponent<Renderer>();
				defaultShader   = currentRenderer.material.shader;
			
				if(selectedShader==null)
					selectedShader = Shader.Find("DynamicObject/Selected");

			}

			public void OnUpdate() {
				if(transform.position.y<-1000f) // предмет упал слишком низко
					Destroy(true);
			}


		public float getCostValue(){
			return this.item.description.costValue;
		}
		
		public override bool Equals(object obj){
			IDynamicObject dynamicObject = obj as IDynamicObject;

			if(dynamicObject==null) return false;

			return dynamicObject.getId().Equals(this.getId());
		}

		public void setSelection(bool selected) {
			this.selected = selected;

			if (destroy || currentRenderer==null) return;

			if (selected) {
				currentRenderer.material.shader = selectedShader;
			} else {
				currentRenderer.material.shader = defaultShader;
			}

		}

		public bool isSelected() {
			return selected;
		}

		public override int GetHashCode() {
			return item.description.id;
		}

		/// <summary>
		/// Уничтожение этого экземпляра объекта
		/// </summary>
		/// <param name="presently">Если положительно (true), данный объект уничтожается сразу,
		/// иначе, почемается как удалённый и очищается позже, в момент создания других объектов</param>
		public void Destroy(bool presently = false) {
			destroy = true; // помечаем объект как удалённый
			objectsSelector.ResetSelected(); // сбрасываем выделение, чтобы исключить попытку доступа к удалённому объекту
			
			if(presently)
				GameObject.Destroy(this.gameObject);
			else
				Trash.getInstance().Add(this.gameObject); // добавляем этот экземпляр в корзину
		}

		public bool isDestroy() {
			return destroy;
		}

		public TextDisplayed getDisplayed(){
			return displayed;
		}

		public IItem getItem(){
			return item;
		}
	
		public DynamicObject(){

		}

		public GameObject toObject(){
			if (destroy) return null;
			return this.gameObject;
		}

		public string getName(){
			return item.description.name;
		}

		public string getCaption(){
			return item.description.caption;
		}

		public int getId(){
			return item.description.id;
		}
	
	}

}