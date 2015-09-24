using System;
using UnityEngine;

namespace EngineEditor {
	
	public static class Utils {
		
		public static string ToString(Vector3 vector) {
			return "x=" + vector.x.ToString() + ", y=" + vector.y.ToString() +", z=" + vector.z.ToString();
		}
		
		public static string ToString(Quaternion quaternion) {
			Vector3 vector = quaternion.eulerAngles;
			return "rot{x=" + vector.x.ToString() + "; y=" + vector.y.ToString() +"; z=" + vector.z.ToString()+"}";
		}
		
	}
	
}