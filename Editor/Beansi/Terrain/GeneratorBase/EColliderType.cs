using System;
using System.Collections.Generic;
using UnityEngine;
using Engine;

namespace EngineEditor.Terrain {

	/// <summary>
	/// Список коллидеров
	/// </summary>
	public enum EColliderType : int {

		None            = -0x01,

		BoxCollider     =  0x00,
		SphereCollider  =  0x01,
		CapsuleCollider =  0x02,
		WheelCollider   =  0x03,
		MeshCollider    =  0x04
	
	};

}
