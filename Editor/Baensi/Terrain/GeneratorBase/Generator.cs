using System;
using UnityEngine;
using UnityEditor;

namespace EngineEditor.Terrain {

	/// <summary>
	/// Генерирует положения в пространстве в ограниченной прямоугольником области
	/// </summary>
	public static class Generator {

		private static float minX(Vector3[] vectors) {
			float result = vectors[0].x;
				foreach (Vector3 v in vectors)
					if (v.x<result)
						result=v.x;
			return result;
		}
		private static float minY(Vector3[] vectors) {
			float result = vectors[0].y;
			foreach (Vector3 v in vectors)
				if (v.y<result)
					result=v.y;
			return result;
		}
		private static float minZ(Vector3[] vectors) {
			float result = vectors[0].z;
			foreach (Vector3 v in vectors)
				if (v.z<result)
					result=v.z;
			return result;
		}

		private static float maxX(Vector3[] vectors) {
			float result = vectors[0].x;
			foreach (Vector3 v in vectors)
				if (v.x>result)
					result=v.x;
			return result;
		}
		private static float maxY(Vector3[] vectors) {
			float result = vectors[0].y;
			foreach (Vector3 v in vectors)
				if (v.y>result)
					result=v.y;
			return result;
		}
		private static float maxZ(Vector3[] vectors) {
			float result = vectors[0].z;
			foreach (Vector3 v in vectors)
				if (v.z>result)
					result=v.z;
			return result;
		}

		public static void generateRandom(TempObject tmp) {
			tmp.PositionRandom = new Vector3(UnityEngine.Random.value, UnityEngine.Random.value, UnityEngine.Random.value);
		}

		public static Vector3 generatePosition(Vector3    random,
											   Vector3    position,
											   Quaternion rotation,
											   Bounds     bounds,
											   Vector3    scale,
											   float      offsetFromValue,
											   float      offsetToValue,
											   float      brushSize,
											   bool       useRayCast = false) {

			Vector3 left   = (rotation * new Vector3(-brushSize/2, 0, 0));
			Vector3 top    = (rotation * new Vector3(0, brushSize/2, 0));
			Vector3 right  = (rotation * new Vector3(brushSize/2,0, 0));
			Vector3 bottom = (rotation * new Vector3(0, -brushSize/2, 0));

			if (Event.current.shift) {
				Handles.color=new Color(1f, 1f, 0f);

				Handles.Label(position+left, new GUIContent("v1l"));
				Handles.Label(position+bottom, new GUIContent("v2b"));
				Handles.Label(position+right, new GUIContent("v3r"));
				Handles.Label(position+top, new GUIContent("v4t"));

					Handles.DrawLine(position+left, position+top);
					Handles.DrawLine(position+top, position+right);
					Handles.DrawLine(position+right, position+bottom);
					Handles.DrawLine(position+bottom, position+left);
			}

			Vector3[] vectors = new Vector3[] { left, top, right, bottom };

			float x = position.x + minX(vectors) + (maxX(vectors) - minX(vectors)) * random.x;
			float y = position.y + minY(vectors) + (maxY(vectors) - minY(vectors)) * random.y;
			float z = position.z + minZ(vectors) + (maxZ(vectors) - minZ(vectors)) * random.z;

			Vector3 result = new Vector3(x, y, z); // вычисляем ожидание с фиксированным x

			if (useRayCast) {

				RaycastHit hitInfo = new RaycastHit();

				//Handles.color=new Color(1f, 0f, 0f);
				result += rotation * new Vector3(0, 0, 1f);

				//Handles.DrawLine(position, result);

				Ray ray = new Ray(result, rotation * new Vector3(0, 0, -1)); // генерируем луч в сторону поверхности

				if (Physics.Raycast(ray, out hitInfo)) { // луч упал на поверхность

					Handles.color=new Color(0f, 0f, 1f);
					Handles.DotCap(0, hitInfo.point, rotation, 0.02f);
					//Handles.DrawLine(result, hitInfo.point);

					result = hitInfo.point;// + (rotation * new Vector3(0, 0, 1)); // прибиваем траекторию к поверхности

					//Handles.color=new Color(1f, 0f, 0f);
					//Handles.DotCap(0, hitInfo.point + bounds.min, rotation, 0.025f);

					//Handles.color=new Color(1f, 0f, 0f);
					//Handles.DotCap(0, hitInfo.point + bounds.max, rotation, 0.03f);

					//Handles.color=new Color(1f, 1f, 0f);
					
					//Handles.DotCap(0, hitInfo.point + bounds.max, rotation, 0.03f);


				} else {
					result -= rotation * new Vector3(0, 0, 1); // смещаем луч назад
				}


			} else {

				//result += (rotation * new Vector3(0, 0, offsetFromValue + (offsetToValue-offsetFromValue)*UnityEngine.Random.value)); // смещение "вверх"

			}

			return result-position;
		}

		public static Vector3 mul(Vector3 v1, Vector3 v2) {
				v1.x*=v2.x;
				v1.y*=v2.y;
				v1.z*=v2.z;
			return v1;
		}

		public static Vector3 generateScale(Vector3    random,
											float      minX,
											float      maxX,
											float      minY,
											float      maxY,
											float      minZ,
											float      maxZ) {

			return new Vector3(minX + random.x * (maxX - minX),
							   minY + random.y * (maxY - minY),
							   minZ + random.z * (maxZ - minZ));

		}

		public static Quaternion generateRotation(Vector3    random,
												  float      minX,
												  float      maxX,
												  float      minY,
												  float      maxY,
												  float      minZ,
												  float      maxZ) {

			return Quaternion.Euler(minX + random.x * (maxX - minX),
									minY + random.y * (maxY - minY),
									minZ + random.z * (maxZ - minZ));

		}

	}

}
