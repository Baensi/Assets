using System;
using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using Engine.Objects.Types;

namespace Engine.Objects {

	public class ObjectCooked {

		public AudioClip cookingSound;
		public int cookingInSeconds; // Время готовки еды
		
		private bool isCook        = false; // Показывает, готова ли еда
		
		private float    cookState = 0.0f; // текущее состояние готовности еды
		private float    cookSpeed = 0.0f; // скорость готовки еды
		private float    timeStamp = 0.0f; // временной штамп последнего состояния готовки

		private ICookedType cookedType;

		public ObjectCooked(ICookedType cookedType,
							AudioClip   cookingSound,
							int         cookingInSeconds = 5) {

			this.cookedType = cookedType;
			this.cookingSound = cookingSound;
			this.cookingInSeconds = cookingInSeconds;

			cookSpeed = 1.0f / (float)cookingInSeconds; // вычисляем шаг готовки
		}

		public void update() {
			
			if (isCook) return;
			if (Time.time - timeStamp < 1.0f) return;

			timeStamp = Time.time;

			if ((cookState+=cookSpeed) >= 1.0f) 
				cookedType.endCook();
			
		}
		
	}
	
}
