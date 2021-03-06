﻿using UnityEditor;
using UnityEngine;
using System;
using Engine.Objects;
using Engine.Objects.Types;

namespace EngineEditor.Objects {

	[CustomEditor(typeof(DynamicObject),true)]
	public class DynamicObjectEditor : Editor {

		private static string title_caption      = "Динамический объект";
		private static string name_caption       = "Имя: ";
		private static string caption_caption    = "Информация: ";
		private static string properties_caption = "Свойства:";

		private static string usedType_caption      = "Можно использовать";
		private static string destroyedType_caption = "Можно разрушить";
		private static string cookedType_caption    = "Можно приготовить (еда)";
		private static string readedType_caption    = "Можно прочитать";

		void OnEnable() {
			
		}

		public override void OnInspectorGUI() {

			IDynamicObject dynamicObject = target as IDynamicObject;

			if (dynamicObject == null) return;

			EditorGUILayout.LabelField(title_caption);
			EditorGUILayout.Separator();
			EditorGUILayout.LabelField(name_caption + dynamicObject.getName());
			EditorGUILayout.LabelField(caption_caption + dynamicObject.getCaption());

			EditorGUILayout.Separator();
			EditorGUILayout.LabelField(properties_caption);

			if (dynamicObject is IPickedType)
				EditorGUILayout.LabelField("->" + usedType_caption);

			if (dynamicObject is IDestroyedType)
				EditorGUILayout.LabelField("->" + destroyedType_caption);

			if (dynamicObject is ICookedType)
				EditorGUILayout.LabelField("->" + cookedType_caption);
			
			if (dynamicObject is IReadedType)
				EditorGUILayout.LabelField("->" + readedType_caption);

		}

	}

}