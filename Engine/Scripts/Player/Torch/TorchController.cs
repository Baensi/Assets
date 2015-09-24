using UnityEngine;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;

using Engine.Player.Torch.Burn;

namespace Engine.Player.Torch {

	public class TorchController : MonoBehaviour {

		public Camera     mainCamera;
		public Light      lightPoint;

		public GameObject torchModel;

		public AudioClip torchModelClickOn;
		public AudioClip torchModelClickOff;

		public AudioClip torchModelReloadBurn;

		public Texture2D textureBurnFull;
		public Texture2D textureBurnEmpty;

		private AudioSource  audioSource;
        private IBurnRender  burnRender;
		private BurnReloader burnReloader;

		public float      lightPointDistance;

        public Light getLight() {
            return lightPoint;
        }

        public Camera getCamera() {
            return mainCamera;
        }

		public void addBurn(IBurn burn){
			burnReloader.addBurn(burn);
		}


		void Start () {

			audioSource = gameObject.AddComponent<AudioSource>();

				burnRender   = new DefaultBurnRenderController(textureBurnFull,textureBurnEmpty,lightPoint);
				burnReloader = new BurnReloader(burnRender);

			Update();
		}


		void Update () {
		
			Vector3 position = mainCamera.transform.position;
			position+=mainCamera.transform.forward*lightPointDistance + mainCamera.transform.right*0.1f;

			lightPoint.transform.position = position;
			lightPoint.transform.rotation = mainCamera.transform.rotation;


			torchModel.transform.position = mainCamera.transform.position
									- mainCamera.transform.forward*2.8f
									- mainCamera.transform.right*1.95f
									+ mainCamera.transform.up*1.40f;

			torchModel.transform.rotation=mainCamera.transform.rotation;

			if(CrossPlatformInputManager.GetButtonDown("Light"))
				if(lightPoint.enabled)
                    lightPointOff();
				else
                    lightPointOn();

			if (CrossPlatformInputManager.GetButtonDown ("ReloadBurn")) {
				reloadBurn ();

			}

		}

		void OnGUI(){
			burnRender.update();
			burnReloader.update();
		}


        private void lightPointOn() {
            audioSource.clip = torchModelClickOn;
            audioSource.Play();
            lightPoint.enabled = true;
        }

        private void lightPointOff() {
            audioSource.clip = torchModelClickOff;
            audioSource.Play();
            lightPoint.enabled = false;
        }

		private void reloadBurn(){

			if (burnReloader.nextBurn ()) {

				audioSource.clip = torchModelReloadBurn;
				audioSource.Play ();

			}
		}

	}
}
