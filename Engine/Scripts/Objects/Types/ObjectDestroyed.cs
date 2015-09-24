using System;
using UnityEngine;
using UnityEditor;
using Engine.Objects.Types;

namespace Engine.Objects {

    [RequireComponent(typeof(BoxCollider))]
	public class ObjectDestroyed : MonoBehaviour, IDestroyedType {

		private bool isDestroy = false;

		[SerializeField] public float      maxHealth     = 100.0f;
        [SerializeField] public float      currentHealth = 100.0f;

		[SerializeField] public GameObject destroyModel  = null;
		[SerializeField] public GameObject destructPart = null;

		void Start(){
			currentHealth = maxHealth;
		}

		public void onDestroy(){
			Destroy(this.gameObject); // удаляем сломанный объект
		}

        public void addDamage(float damageValue) {
            currentHealth -= damageValue;

			if (currentHealth <= 0 && !isDestroy) {

				isDestroy = true;

				if (destroyModel != null) { // создаём модель разрушенного объекта
					GameObject obj = (GameObject)Instantiate(destroyModel, this.transform.position, this.transform.rotation);
					obj.transform.rotation = this.transform.rotation;
				}

					if (destructPart!=null) // Создаём эффект разрушения
						Instantiate(destructPart, this.transform.position, this.transform.rotation);

					onDestroy();
            }
        }

	}
}

