﻿using System;
using System.Collections.Generic;
using UnityEngine;
using Engine;

namespace EngineEditor.Terrain {

	/// <summary>
	/// Класс отображатель объекта для рейкаста
	/// </summary>
	public class PickObjectData {

		private EColliderType collider = EColliderType.None;
		private GameObject    gameObject;
		private int           defaultLayer;

		public PickObjectData(GameObject gameObject) {
			this.gameObject = gameObject;
			
			defaultLayer = gameObject.layer; // запоминаем настройки слоя объекта

				if(gameObject.GetComponent<BoxCollider>())
					collider = EColliderType.BoxCollider;
				if (gameObject.GetComponent<SphereCollider>())
					collider = EColliderType.SphereCollider;
				if (gameObject.GetComponent<CapsuleCollider>())
					collider = EColliderType.CapsuleCollider;
				if (gameObject.GetComponent<WheelCollider>())
					collider = EColliderType.WheelCollider;
				if (gameObject.GetComponent<MeshCollider>())
					collider = EColliderType.MeshCollider;

			if (collider == EColliderType.None) { // нас интерисует только меш-коллидер

				gameObject.AddComponent<MeshCollider>();

			} else {

				if (collider==EColliderType.MeshCollider) return;

				MonoBehaviour.DestroyImmediate(gameObject.GetComponent<Collider>());
				gameObject.AddComponent<MeshCollider>();
				
			}

		}

		/// <summary>
		/// Ставит объект на слой доступный для рейкаста
		/// </summary>
		public void doShowRaycast() {
			gameObject.layer = SingletonNames.Layers.DEFAULT;
		}

		public GameObject toGameObject() {
			return this.gameObject;
		}

		/// <summary>
		/// Возвращает настройки объекта
		/// </summary>
		public void Destroy() {

			if (collider == EColliderType.None) {
				MonoBehaviour.DestroyImmediate(gameObject.GetComponent<MeshCollider>());
				return;
			}

			if (collider == EColliderType.MeshCollider) return;

			switch (collider) { // восстанавливаем исходный коллидер
				case EColliderType.BoxCollider:
					MonoBehaviour.DestroyImmediate(gameObject.GetComponent<MeshCollider>());
					gameObject.AddComponent<BoxCollider>();
					break;
				case EColliderType.SphereCollider:
					MonoBehaviour.DestroyImmediate(gameObject.GetComponent<MeshCollider>());
					gameObject.AddComponent<SphereCollider>();
					break;
				case EColliderType.CapsuleCollider:
					MonoBehaviour.DestroyImmediate(gameObject.GetComponent<MeshCollider>());
					gameObject.AddComponent<CapsuleCollider>();
					break;
				case EColliderType.WheelCollider:
					MonoBehaviour.DestroyImmediate(gameObject.GetComponent<MeshCollider>());
					gameObject.AddComponent<WheelCollider>();
					break;
			}

			gameObject.layer = defaultLayer; // возвращаем объект на исходный слой

		}

	}

}
