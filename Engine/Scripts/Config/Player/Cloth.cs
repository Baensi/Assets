using System;
using System.Collections.Generic;
using Engine.Player;
using Engine.Objects.Types;
using Engine.Objects.Weapon;
using UnityEngine;

namespace Engine {
	
	public class Cloth {

		public IWeaponType weapon;

		private ICloth head;
		private ICloth body;
		private ICloth hands;
		private ICloth legs;
		private ICloth foots;

		public void setPlayerMesh(string objectName, SkinnedMeshRenderer mesh){
			GameObject playerObject = GameObject.Find(objectName);
			GameObject.Destroy(playerObject.GetComponent<SkinnedMeshRenderer>());

			playerObject.AddComponent<SkinnedMeshRenderer>();
			
		}

		/// <summary>
		/// ������
		/// </summary>
		public ICloth Head {

			get { return head; }

			set {

				if (head!=null) {
					GamePlayer.states-=head.getStates(); // ������� ����������� �����
				}

				head = value;

				if (head!=null) {
					GamePlayer.states += head.getStates(); // ��������� ����� ����� �� ������ ��������
				}

			}

		}

		/// <summary>
		/// ����
		/// </summary>
		public ICloth Body {

			get { return body; }

			set {

				if (body!=null) {
					GamePlayer.states -= body.getStates(); // ������� ����������� �����
				}

				body = value;

				if (body!=null) {
					GamePlayer.states += body.getStates(); // ��������� ����� ����� �� ������ ��������
				}

			}

		}

		/// <summary>
		/// ����
		/// </summary>
		public ICloth Hands {

			get { return hands; }

			set {

				if (hands!=null) {
					GamePlayer.states -= hands.getStates(); // ������� ����������� �����
				}

				hands = value;

				if (hands!=null) {
					GamePlayer.states += hands.getStates(); // ��������� ����� ����� �� ������ ��������
				}

			}

		}

		/// <summary>
		/// ������
		/// </summary>
		public ICloth Legs {

			get { return legs; }

			set {

				if (legs!=null) {
					GamePlayer.states -=legs.getStates(); // ������� ����������� �����
				}

				legs = value;

				if (head!=null) {
					GamePlayer.states +=legs.getStates(); // ��������� ����� ����� �� ������ ��������
				}

			}

		}

		/// <summary>
		/// ����
		/// </summary>
		public ICloth Foots {

			get { return foots; }

			set {

				if (foots!=null) {
					GamePlayer.states -=foots.getStates(); // ������� ����������� �����
				}

				foots = value;

				if (foots!=null) {
					GamePlayer.states +=foots.getStates(); // ��������� ����� ����� �� ������ ��������
				}

			}

		}


	}
	
}