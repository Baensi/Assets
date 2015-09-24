using System;
using UnityEngine;

namespace Engine.Player.Data {
	
	[Serializable]
	public class PlayerLevel : MonoBehaviour {
		
		[SerializeField] public int level = 0;
		
		[SerializeField] public int experience          = 0;
		[SerializeField] public int experienceNextLevel = 50;
		
		[SerializeField] public int specificationsPoint = 0;
		[SerializeField] public int skillsPoint         = 0;

		//public void Setup(Camera camera) {
			//CheckStatus(camera);

			//C/amera = camera;
			//originalFov = camera.fieldOfView;
		//}
		
	}
	                     
}