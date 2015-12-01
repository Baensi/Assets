using UnityEngine;

namespace Engine.Objects {

	public class GameObjectBounds {

		/// <summary>
		/// Возвращает размеры меша
		/// </summary>
		/// <param name="gameObject">Объект размеры которого надо получить</param>
		/// <returns>Возвращает границы меша объекта</returns>
		public static Bounds GetMeshBounds(GameObject gameObject) {
			MeshFilter[] meshes = gameObject.GetComponentsInChildren<MeshFilter>();

			if (meshes.Length > 0) {
				Bounds result = meshes[0].mesh.bounds;

				foreach (MeshFilter mesh in meshes)
					result.Encapsulate(mesh.mesh.bounds);

				return result;

			} else {

				return new Bounds();

			}

		}

	}
}
