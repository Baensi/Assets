using UnityEngine;
using System.Collections;

namespace Engine.Player.Cross {

	public class CrossSprite : MonoBehaviour {

		[SerializeField] public Texture2D crossImage;

		private Rect     crossRectangle;

		private float posX;
		private float posY;

			void Start () {

			}
		
		void OnGUI() {

			GUI.DrawTexture(new Rect(posX, posY, crossImage.width, crossImage.height), crossImage);
			
		}
		
		void Update () {

			GameConfig.setCenterScreen(Screen.width  * 0.5f,
									   Screen.height * 0.5f);

			posX = GameConfig.CenterScreen.x - crossImage.width  * 0.5f;
			posY = GameConfig.CenterScreen.y - crossImage.height * 0.5f;

		}

	}

}