using UnityEngine;
using System.Collections;

public class ViewSelector : MonoBehaviour {
	public Transform camera;
	public Transform charsRoot;
	public Transform[] objects = new Transform[2];

	int curCam = 0;
	string curName = "none";
	// Use this for initialization
	void Start () {
	if(!charsRoot){
		Debug.Log ("Warning: characters root object is not assigned, camera can't follow characters!");	
		}
	}
	
	void OnGUI () {

		if(!camera){
		Debug.Log ("Camera not assigned!");	
			return;
		}

		if(curName == "none"){
		EnableView ();
		}

	GUI.BeginGroup(new Rect(Screen.width*0.5f-80f,Screen.height-60f,160f,60f));
	GUILayout.BeginVertical ();
	GUILayout.Label("Current view:\n"+curName);
	GUILayout.BeginHorizontal ();
	if(GUILayout.Button ("<",GUILayout.Width(40))){
	curCam--;
	curCam = Mathf.Clamp (curCam,0,objects.Length);
	EnableView ();
		}else if(GUILayout.Button (">",GUILayout.Width(40))){
	curCam++;
	curCam = Mathf.Clamp (curCam,0,objects.Length);
	EnableView ();
	}
	GUILayout.EndHorizontal ();
	GUILayout.EndVertical();
	GUI.EndGroup();
	}
	
	void EnableView(){

	}
}
