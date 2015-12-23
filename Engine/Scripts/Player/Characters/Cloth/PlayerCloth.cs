using System;
using System.Collections.Generic;
using Engine.Player;
using Engine.Objects.Types;
using Engine.Objects.Weapon;
using UnityEngine;

namespace Engine {
	
	public class PlayerCloth {

		public IWeaponType weapon;

		private IPlayerCloth head;
		private IPlayerCloth body;
		private IPlayerCloth hands;
		private IPlayerCloth legs;
		private IPlayerCloth foots;

		public void setPlayerMesh(string objectName, SkinnedMeshRenderer mesh){
			GameObject playerObject = GameObject.Find(objectName);
			GameObject.Destroy(playerObject.GetComponent<SkinnedMeshRenderer>());

			playerObject.AddComponent<SkinnedMeshRenderer>();
			
		}

		/// <summary>
		/// √олова
		/// </summary>
		public IPlayerCloth Head {

			get { return head; }

			set {

				if (head!=null) {
					GamePlayer.states-=head.getStates(); // убираем добавленные статы
				}

				head = value;

				if (head!=null) {
					GamePlayer.states += head.getStates(); // добавл€ем новые статы от нового предмета
				}

			}

		}

		/// <summary>
		/// “ело
		/// </summary>
		public IPlayerCloth Body {

			get { return body; }

			set {

				if (body!=null) {
					GamePlayer.states -= body.getStates(); // убираем добавленные статы
				}

				body = value;

				if (body!=null) {
					GamePlayer.states += body.getStates(); // добавл€ем новые статы от нового предмета
				}

			}

		}

		/// <summary>
		/// –уки
		/// </summary>
		public IPlayerCloth Hands {

			get { return hands; }

			set {

				if (hands!=null) {
					GamePlayer.states -= hands.getStates(); // убираем добавленные статы
				}

				hands = value;

				if (hands!=null) {
					GamePlayer.states += hands.getStates(); // добавл€ем новые статы от нового предмета
				}

			}

		}

		/// <summary>
		/// ѕоножи
		/// </summary>
		public IPlayerCloth Legs {

			get { return legs; }

			set {

				if (legs!=null) {
					GamePlayer.states -=legs.getStates(); // убираем добавленные статы
				}

				legs = value;

				if (head!=null) {
					GamePlayer.states +=legs.getStates(); // добавл€ем новые статы от нового предмета
				}

			}

		}

		/// <summary>
		/// Ќоги
		/// </summary>
		public IPlayerCloth Foots {

			get { return foots; }

			set {

				if (foots!=null) {
					GamePlayer.states -=foots.getStates(); // убираем добавленные статы
				}

				foots = value;

				if (foots!=null) {
					GamePlayer.states +=foots.getStates(); // добавл€ем новые статы от нового предмета
				}

			}

		}


	}
	
}