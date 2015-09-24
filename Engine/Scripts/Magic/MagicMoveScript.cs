using System;
using System.Collections.Generic;
using UnityEngine;
using Engine.Objects;
using Engine.Player.Movement;

namespace Engine.Scripts.Magic {

	[RequireComponent(typeof (Collider))]
	class MagicMoveScript : MonoBehaviour {

		[SerializeField] public GameObject magicObject;
		[SerializeField] public float mooveSpeed  = 0.50f;
		[SerializeField] public float maxDistance = 2000.0f;

		[SerializeField] public float destroyDelay = 2.0f;

		private float timeStamp;
		private bool  notMove = false;

		private Vector3 forward;

		private float currentDistance = 0.0f;

		void Start() {
			forward = Camera.main.transform.forward;
		}

		void Update() {

			if (!notMove) {

				currentDistance += mooveSpeed;
				if (currentDistance >= maxDistance) {
					destroy();
					return;
				}


				magicObject.transform.position += forward * mooveSpeed;

			} else {

				if (Time.time - timeStamp >= destroyDelay)
					destroy();

			}

		}

		void OnTriggerEnter(Collider other) {
			ObjectDestroyed obj = other.gameObject.GetComponent<ObjectDestroyed>();
			if (obj != null) {
				obj.addDamage(20.0f);
			}

			stop();
		}

		void onCollisionEnter(Collision other) {

			stop();

		}

		private void stop() {
			timeStamp = Time.time;
			notMove = true;

			Renderer renderer = GetComponent<Renderer>();
			renderer.enabled = false;

			SphereCollider collider = GetComponent<SphereCollider>();
			collider.radius += 50f;

		}

		private void destroy() {
			Destroy(magicObject);
		}

	}

}
