using System;
using UnityEngine;

namespace Engine.Player.Data {

	[Serializable]
	public class PlayerSpecifications : MonoBehaviour {
		
		[SerializeField] [Range(5, 12)] public byte strength     = 5;
		[SerializeField] [Range(5, 12)] public byte stamina      = 5;
		[SerializeField] [Range(5, 12)] public byte intelligence = 5;
		[SerializeField] [Range(5, 12)] public byte agility      = 5;
		
	}
	                     
}