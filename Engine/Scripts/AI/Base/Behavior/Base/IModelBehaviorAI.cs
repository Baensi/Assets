using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Engine.AI.Behavior {

	public interface IModelBehaviorAI {

		/// <summary>
		/// Возвращает аудио данные по текущему типу поведения
		/// </summary>
		/// <returns></returns>
		IAudioBehavior getAudioBehavior();

		/// <summary>
		/// Возвращает анимационные поведения
		/// </summary>
		/// <returns></returns>
		IAnimationBehavior getAnimationBehavior();

	}

}
