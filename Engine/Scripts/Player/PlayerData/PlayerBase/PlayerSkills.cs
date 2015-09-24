using System;
using UnityEngine;

namespace Engine.Player.Data {

	[Serializable]
	public class PlayerSkills : MonoBehaviour {
	
		[SerializeField] [Range(0,100)] public int fencing     = 0;
		[SerializeField] [Range(0,100)] public int shooting    = 0;
		[SerializeField] [Range(0,100)] public int magic       = 0;
		
		[SerializeField] [Range(0,100)] public int intuition   = 0;
		[SerializeField] [Range(0,100)] public int alchemy     = 0;
		[SerializeField] [Range(0,100)] public int engineering = 0;
		
	}
	
}