using UnityEngine;
using System.Collections;

namespace Engine.Player.Cross {

	public class CrossSprite : MonoBehaviour {

		[SerializeField] public Texture2D crossImage;

		private Rect     crossRectangle;
		private int      resolutionWidth;
		private int      resolutionHeight;

		private float posX;
		private float posY;

		private void evalCenter(){
			posX = (Screen.width  - crossImage.width)  * 0.5f;
			posY = (Screen.height - crossImage.height) * 0.5f;
			crossRectangle = new Rect(posX, posY, crossImage.width, crossImage.height);
			resolutionWidth  = Screen.width;
			resolutionHeight = Screen.height;

		}

		void Start () {
			evalCenter();
		}
		
		void OnGUI(){
			
			GUI.DrawTexture(crossRectangle, crossImage);
			
		}
		
		void Update () {

			if(Screen.width!=resolutionWidth || Screen.height!=resolutionHeight)
				evalCenter();

		}

	}

}