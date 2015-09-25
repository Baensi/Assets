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
		protected int    id   = 0x0;
		
		protected TextDisplayed displayed  = TextDisplayed.Displayed;
		protected string objectName        = "Dynamic";
		protected string objectCaption     = "DynamicObject";
		protected float  costValue         = 0.0f;

		protected Renderer renderer;

		protected static Shader selectedShader = null;
		protected Shader defaultShader;

		protected bool selected;
		protected bool destroy = false;

		protected ObjectsSelector objectsSelector;

		public void OnStart() {
			
			Trash.getInstance().Clean(); // Пытаемся очистить корзину
			
			gameObject.AddComponent<Rigidbody>();
			gameObject.AddComponent<BoxCollider>();
			objectsSelector = SingletonNames.getMainCamera().GetComponent<ObjectsSelector>();

			renderer = gameObject.GetComponent<Renderer>();
			defaultShader = renderer.material.shader;
			
			if(selectedShader==null)
				selectedShader = Shader.Find("DynamicObject/Selected");

		}

		public float getCostValue(){
			return this.costValue;
		}
		
		public override bool Equals(object obj){
			IDynamicObject dynamicObject = obj as IDynamicObject;

			if(dynamicObject==null) return false;

			return dynamicObject.getId().Equals(this.getId());
		}

		public void setSelection(bool selected) {
			this.selected = selected;

			if (destroy) return;

			if (selected) {
				renderer.material.shader = selectedShader;
			} else {
				renderer.material.shader = defaultShader;
			}

		}

		public bool isSelected() {
			return selected;
		}

		public override int GetHashCode() {
			return id;
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
				Destroy(this.gameObject);
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
	
		public DynamicObject(int id){
			this.id=id;
		}

		public GameObject toObject(){
			if (destroy) return null;
			return this.gameObject;
		}

		public string getName(){
			return objectName;
		}

		public string getCaption(){
			return objectCaption;
		}

		public int getId(){
			return id;
		}
	
	}

}