using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(Navigator))]
public class NavigatorEditor : Editor {
	Navigator n;
	bool constraints = false;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	public override void OnInspectorGUI () {
		n = (Navigator) target;
		DrawDefaultInspector();
		constraints = EditorGUILayout.Foldout(constraints,"Modify target point coordinates");
		if(constraints){
			n.targetPointConstraints.a = EditorGUILayout.ToggleLeft("x",n.targetPointConstraints.a);
			n.targetPointConstraints.b = EditorGUILayout.ToggleLeft("y",n.targetPointConstraints.b);
			n.targetPointConstraints.c = EditorGUILayout.ToggleLeft("z",n.targetPointConstraints.c);
		}
	}
}
