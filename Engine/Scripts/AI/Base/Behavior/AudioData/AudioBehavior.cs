using System;
using System.Collections.Generic;
using UnityEngine;

namespace Engine.AI.Behavior {

	public class AudioBehavior : IAudioBehavior {

		private int oldNormalIndex = -1;
		private List<AudioClip> normalMessagesList;

		private int oldWarningIndex = -1;
		private List<AudioClip> warningMessagesList;

		private int oldEnemyIndex = -1;
		private List<AudioClip> enemyMessagesList;

			public AudioBehavior(List<AudioClip> normalMessagesList  = null,
								 List<AudioClip> warningMessagesList = null,
								 List<AudioClip> enemyMessagesList   = null) {

				this.normalMessagesList = normalMessagesList;
				this.warningMessagesList = warningMessagesList;
				this.enemyMessagesList = enemyMessagesList;

            }

		private AudioClip getCollectionItem(ref int oldIndex, ref List<AudioClip> collection) {
			if (collection == null)
				return null;

			int index = -1;

			while (index != -1 && index != oldIndex) {
				index = UnityEngine.Random.Range(0, collection.Count - 1);
			}

			oldIndex = index;

			return collection[index];
		}

		/// <summary>
		/// Возвращает аудиофайл сообщения соответствующего статуса AgressionStateAI
		/// </summary>
		/// <param name="state">Статус агрессии AI</param>
		/// <returns>Возвращает аудиофайл</returns>
		public AudioClip getStateMessage(AgressionStateAI state) {

			switch (state) {
				case AgressionStateAI.Normal:

					return getCollectionItem(ref oldNormalIndex, ref normalMessagesList);

				case AgressionStateAI.Warning:

					return getCollectionItem(ref oldWarningIndex, ref warningMessagesList);

				case AgressionStateAI.Enemy:

					return getCollectionItem(ref oldEnemyIndex, ref enemyMessagesList);

				default:

					return null;
			}

		}

	}

}
