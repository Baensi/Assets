using System;
using System.Collections.Generic;
using Engine.Player;
using UnityEngine;

namespace Engine {

	public struct GamePlayer {

		public static PlayerLevel          level;
		public static PlayerStates         states;
		public static PlayerSpecifications specifications;
		public static PlayerSkills         skills;

		public class Cloth {

			private static ICloth head;
			private static ICloth body;
			private static ICloth hands;
			private static ICloth legs;
			private static ICloth foots;

			public static void setPlayerMesh(string objectName, SkinnedMeshRenderer mesh){
				GameObject playerObject = GameObject.Find(objectName);
				MonoBehaviour.Destroy(playerObject.GetComponent<SkinnedMeshRenderer>());

				playerObject.AddComponent<SkinnedMeshRenderer>();
				
			}

			/// <summary>
			/// Голова
			/// </summary>
			public static ICloth Head {

				get { return head; }

				set {

					if (head!=null) {
						states-=head.getStates(); // убираем добавленные статы
					}

					head = value;

					if (head!=null) {
						states+=head.getStates(); // добавляем новые статы от нового предмета
					}

				}

			}

			/// <summary>
			/// Тело
			/// </summary>
			public static ICloth Body {

				get { return body; }

				set {

					if (body!=null) {
						states-=body.getStates(); // убираем добавленные статы
					}

					body = value;

					if (body!=null) {
						states+=body.getStates(); // добавляем новые статы от нового предмета
					}

				}

			}

			/// <summary>
			/// Руки
			/// </summary>
			public static ICloth Hands {

				get { return hands; }

				set {

					if (hands!=null) {
						states-=hands.getStates(); // убираем добавленные статы
					}

					hands = value;

					if (hands!=null) {
						states+=hands.getStates(); // добавляем новые статы от нового предмета
					}

				}

			}

			/// <summary>
			/// Поножи
			/// </summary>
			public static ICloth Legs {

				get { return legs; }

				set {

					if (legs!=null) {
						states-=legs.getStates(); // убираем добавленные статы
					}

					legs = value;

					if (head!=null) {
						states+=legs.getStates(); // добавляем новые статы от нового предмета
					}

				}

			}

			/// <summary>
			/// Ноги
			/// </summary>
			public static ICloth Foots {

				get { return foots; }

				set {

					if (foots!=null) {
						states-=foots.getStates(); // убираем добавленные статы
					}

					foots = value;

					if (foots!=null) {
						states+=foots.getStates(); // добавляем новые статы от нового предмета
					}

				}

			}


		}

	}

}
