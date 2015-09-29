using System;
using UnityEngine;
using UnityEditor;

namespace EngineEditor.Terrain {

	public class TempObject {

		private GameObject gameObject;

		private Vector3    positionRandom = new Vector3(0,0,0);

		private Vector3    positionOffset = new Vector3(0,0,0);
		private Vector3    scaleOffset    = new Vector3(1,1,1);
		private Quaternion rotationOffset = new Quaternion(0,0,0,0);
		private Color      colorOffset    = new Color(0,0,0);

		private Vector3    position;
		private Quaternion rotation;

		public Vector3 PositionRandom {
			get { return positionRandom; }
			set { positionRandom = value; }
		}

		public Vector3 PositionOffset {
			get { return positionOffset; }
			set {
				positionOffset = value;
				gameObject.transform.position = position + positionOffset;
				Handles.DrawDottedLine(position, position+positionOffset, 2f);
			}
		}

		public Vector3 ScaleOffset {
			get { return scaleOffset; }
			set {
				scaleOffset = value;
				gameObject.transform.localScale = scaleOffset;
			}
		}

		public Quaternion RotationOffset {
			get { return rotationOffset; }
			set {
				rotationOffset = value;
				gameObject.transform.rotation = Quaternion.Euler(rotation.eulerAngles + rotationOffset.eulerAngles);
			}
		}

		public Color ColorOffset {
			get { return colorOffset; }
			set { colorOffset = value; }
		}

		public TempObject(GameObject gameObject) {
			this.gameObject = gameObject;

			gameObject.layer = 2;
		}

		public GameObject toGameObject() {
			return gameObject;
		}

		public void SetupSettings(Vector3 position, Quaternion rotation) {
			this.position = position;
			this.rotation = rotation;
			gameObject.transform.position = position + positionOffset;
			gameObject.transform.rotation = Quaternion.Euler(rotation.eulerAngles+rotationOffset.eulerAngles);
			gameObject.transform.localScale = scaleOffset;
		}

	}


}
