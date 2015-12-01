using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;

public class NAPSWindow : EditorWindow{
	string tempStr,buttonName,button2Name;
	PathTypeData [] PathsTypes = new PathTypeData[0];
	string[] buttonNames = {"start placing","stop placing"};
	string[] button2Names = {"Manual connection","Done"};
	bool placing = false;
	bool drawInterface = false;
	bool fileDialog = false;
	bool manualConnection = false;
	bool showLinkMenu = false;
	bool options = false;
	bool drawAreas = false;
	bool drawCurves = false;
	bool trajectoryDialog = false;
	bool createFile = false;
	bool autoWeight = true;
	bool saveCurves = true;
	bool playMode = false;
	bool temp;
	int i,curTypeId,selectionId;
	string curType,errorText,selectedNodeType;
	Camera sceneCam;
	GameObject curPoint,selectedObject,testSelectedObject;
	int lastNumber;
	float distToSurface = 1.23f;
	float pointRadius = 0.7f;
	Link[] Links = new Link[0];
	Link tempLink;
	Color bipointC = Color.yellow;
	Color atypicalC = Color.grey;
	string curFilePath = "";
	string curFileName = "";
	WindowSettings ws;
	PathsSettings ps;
	HierarchyState lastHierarchyState;
	//trajectory data------------------------------------------------
	Link curLink = new Link();
	CurveId curTangent = new CurveId();
	Vector3 handlePos = Vector3.zero;
	//---------------------------------------------------------------
	Dictionary<string,int> indexOfPath = new Dictionary<string, int>();
	GameObject tempGO;
	string removePath = "";
	Vector2 scr1Pos;
	Vector2 scr2Pos;
	Vector2 scr3Pos;
	Vector2 scr4Pos;
/*connectionNodes contain two nodes that can be connected, 0 element of array - node you want connect to another node and
 * 1 element of array - node that should be connected to the first node
 */
	Pathfinding.Node[] connectionNodes = {new Pathfinding.Node(-1,""),new Pathfinding.Node(-1,"")};
//========================================
	//Debug window data	
	bool showDebug = false;
//======================================== 

	
	[MenuItem("Window/NAPS")]
	public static void ShowWindow()
	{
 	NAPSWindow NAPSEditor = (NAPSWindow)EditorWindow.GetWindow(typeof(NAPSWindow));
	NAPSEditor.minSize = new Vector2(300,400);
	}
	
	void OnEnable(){
        this.title = "NAPS";
		EditorApplication.update += this.ConstantUpdate;
		if(EditorApplication.isPlaying)
			playMode = true;
		else
		InitData();
	}
	
	void InitData(){
	    SceneView.onSceneGUIDelegate+= this.OnSceneGUI;
		//Pathfinding.pathsRootObject = "Test";
		tempStr = "";
		curType = "";
		errorText = "";
		buttonName = buttonNames[0];
		button2Name = button2Names[0];
		curTypeId = 0;
		ws = BINS.Load(Application.dataPath+"/NAPS","Settings.wst") as WindowSettings;
		if(ws!=null)
			LoadWindowSettings(ws);
		Pathfinding.Reset ();		
	}
		
	void ConstantUpdate(){
	if(EditorApplication.isPlaying)
			playMode = true;
		else playMode = false;
		testSelectedObject = Selection.activeGameObject;
		if(testSelectedObject==null){
			ClearSelectedNodes();
		}else{
			selectedNodeType = IsNodeOfPath(testSelectedObject);
			if(selectedNodeType!=""){
		 if(Pathfinding.NodeIsEmpty(connectionNodes[0]) || !showLinkMenu){
				connectionNodes[0] = new Pathfinding.Node(int.Parse (testSelectedObject.name)-1,selectedNodeType);
				connectionNodes[1] = new Pathfinding.Node(-1,"");
				}else{
				connectionNodes[1] = new Pathfinding.Node(int.Parse (testSelectedObject.name)-1,selectedNodeType);
				if(Pathfinding.NodesEqual(connectionNodes[0],connectionNodes[1]))
					connectionNodes[1] = new Pathfinding.Node(-1,"");					
				}
		}else ClearSelectedNodes();
	}
	}
	
	void ClearSelectedNodes(){
		connectionNodes[0] = new Pathfinding.Node(-1,"");
		connectionNodes[1] = new Pathfinding.Node(-1,"");		
	}

	void OnGUI(){
//=====================================
		if(playMode){
			if(GUILayout.Button("Debug window")){
			if(showDebug)
				showDebug=false;
				else
				showDebug = true;
			}
//=====================================
		}else{
	//======================================
		if(!drawInterface)
			if(!fileDialog)
			PathsObjectNameDialog();	
				else
			FileDialog();
	//======================================
		else{
	OpenOther_SaveFileDialog();
	NewPathAddDialog();
	if(PathsTypes.Length>0){
	ShowPathsDialog();
	PointPlacingDialog();
	ManualConnectionDialog ();
	OptionalSettingsDialog();
			}
		}
	}
		if(GUI.changed)
		SceneView.currentDrawingSceneView.Repaint();
	}
	
	
	//interface funnctions
	//==============================================================================================================
	void OpenOther_SaveFileDialog(){
	GUILayout.BeginHorizontal ();
	if(GUILayout.Button ("Open other file",GUILayout.Width(120)))
			ReturnToFileSelection();
	if(GUILayout.Button ("Save file"))
			SaveData ();
	GUILayout.EndHorizontal ();
	}
	
	void FileDialog(){
		if(!createFile){
	if(GUILayout.Button("Open existing file")){
		curFilePath = EditorUtility.OpenFilePanel("Select path file","/NAPS/Paths","nvf");	
		}
	if(GUILayout.Button ("Create new file")){
		createFile = true;	
		}
		if(curFilePath !=""){
		if(System.IO.File.Exists(curFilePath)){
			Debug.Log (curFilePath);
			FileInfo fileInfo = new FileInfo(curFilePath);
				if(fileInfo.Exists){
				Debug.Log ("file directory:"+fileInfo.DirectoryName+";file name:"+fileInfo.Name);
				curFilePath = fileInfo.DirectoryName;
				curFileName = fileInfo.Name;
				}
				Pathfinding.DeserializePaths(curFilePath,curFileName);
				ReadPathsTypes();

				ps = BINS.Load (curFilePath,curFileName.Remove(curFileName.IndexOf("."))+".est") as PathsSettings;
				LoadPathsSettings(ps);
				this.title = curFileName.Remove(curFileName.IndexOf("."));
				drawInterface = true;
			}
		}
		}else if(createFile){
		NewFileDialog();	
		}
		
	}
	
	
	void NewFileDialog(){
	GUILayout.BeginHorizontal ();
		if(GUILayout.Button ("Select folder"))
	curFilePath = EditorUtility.OpenFolderPanel("Select folder",Application.dataPath+"/NAPS","");
		if(curFilePath =="")
			return;
	curFileName = GUILayout.TextField(curFileName);
		if(GUILayout.Button ("Create")){
		if(curFileName !=""){
				//return;
			//}
			this.title = curFileName;
			curFileName+=".nvf";
		Pathfinding.PathsRoot = GameObject.Find (Pathfinding.pathsRootObject);
			if(Pathfinding.PathsRoot!=null){
				DestroyImmediate(Pathfinding.PathsRoot);
			}
		Pathfinding.PathsRoot = new GameObject();
		Pathfinding.PathsRoot.name = Pathfinding.pathsRootObject;
		 ReadPathsTypes();
		createFile = false;
		drawInterface = true;
		}
		}
	GUILayout.EndHorizontal ();
	}
	
	void PathsObjectNameDialog(){
		GUILayout.BeginHorizontal ();
		Pathfinding.pathsRootObject = EditorGUILayout.TextField("Paths object name:",Pathfinding.pathsRootObject);
		if(GUILayout.Button ("OK")){
		 if(ws!=null)
				ws.pathsRootName = Pathfinding.pathsRootObject;
		fileDialog = true;	
		}
		GUILayout.EndHorizontal ();
		
	}
	
	void NewPathAddDialog(){
	GUILayout.BeginVertical ();
	GUILayout.BeginHorizontal();
	GUILayout.Label("New path:",GUILayout.Width(60));
	tempStr = GUILayout.TextField(tempStr);
	if(GUILayout.Button("Add",GUILayout.Width (40))){
			if(!string.IsNullOrEmpty(tempStr) && NameIsValid(tempStr)){
			errorText ="";
			AddNewPathType();
			}else 
			errorText = "Wrong path name!";
		}
	GUILayout.EndHorizontal();	
		if(!string.IsNullOrEmpty(errorText))
		GUILayout.Label (errorText);
	GUILayout.EndVertical();
	}
	
	
	void ShowPathsDialog(){
		if(PathsTypes.Length<1){
		GUILayout.Label("No paths found",GUILayout.Width (80));	
		}else{
		GUILayout.BeginHorizontal();
		GUILayout.BeginVertical ();
	GUILayout.Label("Show paths:",GUILayout.Width (80));
	GUILayout.Space(20);
	GUILayout.Label ("Remove paths:",GUILayout.Width (90));
		GUILayout.EndVertical();
		scr1Pos = EditorGUILayout.BeginScrollView(scr1Pos,GUILayout.Height(80));
		GUILayout.BeginHorizontal();
	for(i=0;i<PathsTypes.Length;i++){
		GUILayout.BeginVertical();
			GUILayout.Label(PathsTypes[i].type);
			PathsTypes[i].show = GUILayout.Toggle(PathsTypes[i].show,"");
			if(GUILayout.Button ("x",GUILayout.Width(19))){
			temp = EditorUtility.DisplayDialog("Removing path","Do you want to remove path '"+PathsTypes[i].type+"'?","Remove","No");
				if(temp)
				RemovePath (PathsTypes[i].type);
			}
		GUILayout.EndVertical ();
		}

		GUILayout.EndHorizontal ();
		EditorGUILayout.EndScrollView();
		GUILayout.EndHorizontal();
		GUILayout.BeginHorizontal ();
	drawAreas = GUILayout.Toggle(drawAreas,"Draw areas");
	drawCurves = GUILayout.Toggle(drawCurves,"Draw curves");
		GUILayout.EndHorizontal();
	}
	}
	
	void PointPlacingDialog(){
		//Debug.Log ("curTypeId:"+curTypeId);
		GUILayout.BeginVertical ();
		curType = PathsTypes[curTypeId].type;
		GUILayout.BeginHorizontal();
		GUILayout.Label("Current path type: "+curType,GUILayout.Width(115+curType.Length*8));
		if(GUILayout.Button("<",GUILayout.Width(40))){
		curTypeId--;	
		}
		if(GUILayout.Button(">",GUILayout.Width(40))){
		curTypeId++;	
		}
		GUILayout.EndHorizontal();
		curTypeId = Mathf.Clamp(curTypeId,0,PathsTypes.Length-1);
		GUILayout.BeginHorizontal();
		if(GUILayout.Button(buttonName,GUILayout.Height(40),GUILayout.Width(80))){	
		if(!placing){
				buttonName = buttonNames[1];
				placing = true;
			}else{
				buttonName = buttonNames[0];
				placing = false;
			}
		}
		GUILayout.EndHorizontal();
		GUILayout.EndVertical();
	}
	
	void ManualConnectionDialog(){
		GUILayout.BeginVertical();
	if(GUILayout.Button(button2Name,GUILayout.MinWidth (292))){
		if(!manualConnection){
		button2Name = button2Names[1];
		manualConnection = true;
			}else{
		button2Name = button2Names[0];
		manualConnection = false;
			}
		}
		if(manualConnection){
		if(Pathfinding.NodeIsEmpty(connectionNodes[0])){
		GUILayout.Label("Select node");
		}
		}
		GUILayout.EndVertical();
		if(manualConnection && !Pathfinding.NodeIsEmpty(connectionNodes[0]))
		ManualConnectionPropertiesDialog(connectionNodes[0]);
	}
	
	void ManualConnectionPropertiesDialog(Pathfinding.Node firstNode){
	GUILayout.BeginVertical ();
	GUILayout.Label ("Selected node: "+(firstNode.number+1).ToString()+" of type "+firstNode.type);
		if(!showLinkMenu && !trajectoryDialog)
		if(GUILayout.Button("Add link",GUILayout.Width(88))){
		showLinkMenu = true;
		}
		if(showLinkMenu && !trajectoryDialog)
		LinkMenuDialog(firstNode);
		if(!trajectoryDialog)
		GUILayout.Label ("Connected to:");
		GUILayout.BeginHorizontal();
			GUILayout.BeginVertical ();
				if(!trajectoryDialog){
				GUILayout.Label ("Node number",GUILayout.Width(80));
				GUILayout.Label ("Node type",GUILayout.Width(80));
				GUILayout.Label ("Link weigth",GUILayout.Width(80),GUILayout.Height(25));
				GUILayout.Label ("Trajectory",GUILayout.Width(80),GUILayout.Height(20));
				GUILayout.Label ("Remove",GUILayout.Width(80));	
		      }
			GUILayout.EndVertical();
		ReadConnections(firstNode);
		//do this for all connected nodes {	
			if(Links.Length>0){
			if(trajectoryDialog)
				trajectoryDialog = TrajectoryDialog(firstNode,curLink);
			else{
			scr4Pos = EditorGUILayout.BeginScrollView(scr4Pos);
			GUILayout.BeginHorizontal();
			for(i=0;i<Links.Length;i++){
			GUILayout.BeginVertical ();
			//if(!trajectoryDialog){
			GUILayout.TextField (Links[i].nodeNumber,GUILayout.Width(40));
			GUILayout.TextField (Links[i].nodeType,GUILayout.Width(40));
			GUILayout.TextField (Links[i].linkWeight,GUILayout.Width(30));
			
				if(GUILayout.Button ("Open",GUILayout.Width (40))){
				trajectoryDialog = true;
				curLink = Links[i];
					}
			GUILayout.Space(8);
			if(GUILayout.Button ("X",GUILayout.Width(20),GUILayout.Height(10)))
					Links[i].linkRemove = true;

			GUILayout.EndVertical ();
		}
			GUILayout.EndHorizontal();
			GUILayout.EndScrollView();
		}
		}
		//}
		GUILayout.EndHorizontal();
		RemoveLinks(firstNode);	
	GUILayout.EndVertical();
	}
	
	
	
	bool TrajectoryDialog(Pathfinding.Node newThisNode,Link newlink){
	GUILayout.BeginVertical();
	string buttonName = "",info="";
	Pathfinding.Node thisNode = new Pathfinding.Node(newThisNode.number,newThisNode.type);
	Link link = new Link();
	bool result,buttonResult;
		link.nodeNumber = newlink.nodeNumber;
		link.nodeType = newlink.nodeType;
	Pathfinding.Node linkNode = new Pathfinding.Node(int.Parse (link.nodeNumber),link.nodeType);
		linkNode.number--;
		if(!Pathfinding.CurveExistBetween(thisNode,linkNode))
			buttonName = "Add";
		else 
			buttonName = "Remove";
		if(buttonName == "Add")
		info = " curve to "+(linkNode.number+1).ToString()+linkNode.type;	
			else
		info = " curve from "+(linkNode.number+1).ToString()+linkNode.type;		
		GUILayout.BeginHorizontal();
		buttonResult = GUILayout.Button (buttonName,GUILayout.Width(80));
		if(buttonResult){
			if(buttonName == "Add")
				SetCurve (thisNode,linkNode);			
			else
				RemoveCurve (thisNode,linkNode);
			result = false;
		}
		GUILayout.Label (info);
		GUILayout.EndHorizontal();
		if(GUILayout.Button ("Two-sided",GUILayout.Width (80)))
			SetBackCurve (linkNode,thisNode);
		if(GUILayout.Button ("Done",GUILayout.Width (80))){
		curLink = new Link();
			result = false;
		}
		else result = true;
		GUILayout.EndVertical();
		return result;
	}
	
	
	

	
	
	void LinkMenuDialog(Pathfinding.Node node){
		if(!Pathfinding.NodeIsEmpty(connectionNodes[1])){
			tempLink = new Link();
			tempLink.nodeNumber = (connectionNodes[1].number+1).ToString();
			tempLink.nodeType = connectionNodes[1].type;
		}else if(tempLink==null)
		tempLink = new Link();
	GUILayout.BeginVertical ();	
	GUILayout.Space(10);
	GUILayout.Label ("Enter the properties of the node or just click on it");
	GUILayout.Space (10);
	GUILayout.Label("link with");
	tempLink.nodeNumber = EditorGUILayout.TextField("Node number",tempLink.nodeNumber);
	tempLink.nodeType = EditorGUILayout.TextField ("Node type",tempLink.nodeType);
	autoWeight = EditorGUILayout.Toggle("Auto weight",autoWeight);
		if(!autoWeight)
	tempLink.linkWeight = EditorGUILayout.TextField ("Link weigth",tempLink.linkWeight);
		else tempLink.linkWeight = "0";
		GUILayout.BeginHorizontal ();
		if(GUILayout.Button("Apply",GUILayout.Width (60))){
			if(NodeExist(tempLink)){
			SetLink(node,tempLink);
			showLinkMenu = false;
			connectionNodes[1] = new Pathfinding.Node(-1,"");
			SelectFirstNode();
			}
			tempLink = null;
			connectionNodes[1] = new Pathfinding.Node(-1,"");
		}
		if(GUILayout.Button ("Cancel")){
		showLinkMenu = false;
		tempLink = null;
		connectionNodes[1] = new Pathfinding.Node(-1,"");
		SelectFirstNode();
		}
		GUILayout.EndHorizontal ();
		if(errorText!="")
		GUILayout.Label(errorText);
		GUILayout.EndVertical();
	}
	
	void OptionalSettingsDialog(){
		GUILayout.BeginVertical();
		options = GUILayout.Toggle(options,"Optional settings");
		if(options){
		scr2Pos = EditorGUILayout.BeginScrollView (scr2Pos,GUILayout.MaxHeight(150));
		EditorGUILayout.BeginVertical();
	for(i=0;i<PathsTypes.Length;i++){
		PathsTypes[i].color = EditorGUILayout.ColorField("type "+PathsTypes[i].type,PathsTypes[i].color);	
		}
		EditorGUILayout.EndVertical();
		EditorGUILayout.EndScrollView ();
		bipointC = EditorGUILayout.ColorField ("Bipoint color",bipointC);
		distToSurface = EditorGUILayout.FloatField("Distance to surface ",distToSurface);
		pointRadius = EditorGUILayout.FloatField("Point radius ",pointRadius);
		saveCurves = EditorGUILayout.Toggle ("Save curves ",saveCurves);
		}
		GUILayout.EndVertical();
	}
	


	
//non-interface functions
//=================================================================================================
	
	void ReturnToFileSelection(){
	if(Pathfinding.Paths.Length>0)
		Pathfinding.SerializePaths(curFilePath,curFileName);
	if(PathsTypes.Length>0)
		SavePathsSettings();
		drawInterface = false;
		PathsTypes = new PathTypeData[0];
		Pathfinding.Reset ();
		SceneView.RepaintAll();
		curFileName="";
		curFilePath="";
		this.title = "NAPS";
	}
	
	
	bool NameIsValid(string name){
	bool result = false;
		if(string.IsNullOrEmpty(name))
			return result;
		foreach(char c in name){
			if(c != ' ')
				result = true;
		}
		return result;
	}
	
	
	void SetCurve(Pathfinding.Node newFirst,Pathfinding.Node newSecond){
		int a,b,size,i,j,lastSize;
		Pathfinding.Node first = new Pathfinding.Node(newFirst.number,newFirst.type);
		Pathfinding.Node second = new Pathfinding.Node(newSecond.number,newSecond.type);
		Pathfinding.CurveData[,] tempCurvesData;
		string aStr = first.number.ToString()+first.type,bStr = second.number.ToString()+second.type;
		size = Pathfinding.trajectoryData.indexOf.Count;
		Debug.Log ("curLink:"+curLink.nodeNumber+curLink.nodeType);
		if(size<2){
		i = 0;
			Debug.Log ("first:"+aStr+"; second:"+bStr);
		Pathfinding.trajectoryData.nodes = new ArrayList();
		Pathfinding.trajectoryData.indexOf = new Dictionary<string, int>();
		Pathfinding.trajectoryData.curvesData = new Pathfinding.CurveData[2,2];
		Pathfinding.trajectoryData.indexOf.Add(aStr,i);
		Pathfinding.trajectoryData.nodes.Add (first);
		i++;
		Pathfinding.trajectoryData.indexOf.Add(bStr,i);	
		Pathfinding.trajectoryData.nodes.Add (second);
		//setting start curve point------------------------
		Pathfinding.trajectoryData.curvesData[0,0] = new Pathfinding.CurveData();
		Pathfinding.trajectoryData.curvesData[0,0].tangents = new Pathfinding.Vector3S[1];
		Pathfinding.trajectoryData.curvesData[0,0].tangents[0] = 
			new Pathfinding.Vector3S(Pathfinding.GetWaypointInSpace(0.5f,first.number,first.type));
		//---------------------------------------------
			//setting tangents--------------------------------------
		Pathfinding.trajectoryData.curvesData[0,1] = new Pathfinding.CurveData();
		Pathfinding.trajectoryData.curvesData[0,1].tangents = new Pathfinding.Vector3S[2];
			//-----------------------------------------------------------
		//setting first tangent-------------------------
		Pathfinding.trajectoryData.curvesData[0,1].tangents[0] = new Pathfinding.Vector3S(Pathfinding.GetWaypointInSpace(0.5f,first.number,first.type));	
		//----------------------------------------------
			//setting second tangent-------------------------------------
		Pathfinding.trajectoryData.curvesData[0,1].tangents[1] = new Pathfinding.Vector3S(Pathfinding.GetWaypointInSpace(0.5f,second.number,second.type));	
			//-----------------------------------------------------------
		//setting end curve point-----------------------
		Pathfinding.trajectoryData.curvesData[1,1] = new Pathfinding.CurveData();
	    Pathfinding.trajectoryData.curvesData[1,1].tangents = new Pathfinding.Vector3S[1];
		Pathfinding.trajectoryData.curvesData[1,1].tangents[0] = 
			new Pathfinding.Vector3S(Pathfinding.GetWaypointInSpace(0.5f,second.number,second.type));
		//----------------------------------------------
		}else {
		 tempCurvesData = new Pathfinding.CurveData[size,size];
		 lastSize = size;
			for(i=0;i<size;i++)
			for(j=0;j<size;j++){
			tempCurvesData[i,j] = Pathfinding.trajectoryData.curvesData[i,j];	
			}
			
		if(!Pathfinding.trajectoryData.indexOf.ContainsKey(aStr)){
		Pathfinding.trajectoryData.indexOf.Add (aStr,size);	
		Pathfinding.trajectoryData.nodes.Add (first);
		size++;
			}
		if(!Pathfinding.trajectoryData.indexOf.ContainsKey(bStr)){
		Pathfinding.trajectoryData.indexOf.Add (bStr,size);	
		Pathfinding.trajectoryData.nodes.Add (second);
		size++;
			}
		Pathfinding.trajectoryData.curvesData = new Pathfinding.CurveData[size,size];
		for(i=0;i<size;i++)
		for(j=0;j<size;j++){
			if(i<lastSize && j<lastSize)
		Pathfinding.trajectoryData.curvesData[i,j] = tempCurvesData[i,j];
		else
		Pathfinding.trajectoryData.curvesData[i,j] = new Pathfinding.CurveData();	
		}
		a = Pathfinding.trajectoryData.indexOf[aStr];
		b = Pathfinding.trajectoryData.indexOf[bStr];
			if(Pathfinding.trajectoryData.curvesData[a,a] == null)
		Pathfinding.trajectoryData.curvesData[a,a] = new Pathfinding.CurveData();
			if(Pathfinding.trajectoryData.curvesData[b,b] == null)
		Pathfinding.trajectoryData.curvesData[b,b] = new Pathfinding.CurveData();
			if(Pathfinding.trajectoryData.curvesData[a,b] == null)
		Pathfinding.trajectoryData.curvesData[a,b] = new Pathfinding.CurveData();
			
		Pathfinding.trajectoryData.curvesData[a,a].tangents = new Pathfinding.Vector3S[1];
		Pathfinding.trajectoryData.curvesData[b,b].tangents = new Pathfinding.Vector3S[1];
		Pathfinding.trajectoryData.curvesData[a,b].tangents = new Pathfinding.Vector3S[2];
		Pathfinding.trajectoryData.curvesData[a,a].tangents[0] = new Pathfinding.Vector3S(
			Pathfinding.GetWaypointInSpace(0.5f,first.number,first.type));
		Pathfinding.trajectoryData.curvesData[b,b].tangents[0] = new Pathfinding.Vector3S(
			Pathfinding.GetWaypointInSpace(0.5f,second.number,second.type));
		Pathfinding.trajectoryData.curvesData[a,b].tangents[0] = 
			Pathfinding.trajectoryData.curvesData[a,a].tangents[0];
		Pathfinding.trajectoryData.curvesData[a,b].tangents[1] = 
			Pathfinding.trajectoryData.curvesData[b,b].tangents[0];
		tempCurvesData = null;
	}
	}
	
	void SetBackCurve(Pathfinding.Node first,Pathfinding.Node second){
	int a,b;
	string aStr,bStr;
		Vector3 point = Vector3.zero;
		//Pathfinding.CurveData newCurveData = new Pathfinding.CurveData();
		if(Pathfinding.trajectoryData.indexOf.Count<2)
			return;
		if(!Pathfinding.LinkExist(first,second))
			return;
		aStr = first.number.ToString ()+first.type;
		bStr = second.number.ToString()+second.type;
		if(!Pathfinding.trajectoryData.indexOf.ContainsKey(aStr) || !Pathfinding.trajectoryData.indexOf.ContainsKey(bStr))
			return;
		a = Pathfinding.trajectoryData.indexOf[aStr];
		b = Pathfinding.trajectoryData.indexOf[bStr];
Pathfinding.trajectoryData.curvesData[a,b] = new Pathfinding.CurveData();
Pathfinding.trajectoryData.curvesData[a,b].tangents = new Pathfinding.Vector3S[2];
		point = Pathfinding.trajectoryData.curvesData[b,a].tangents[1].GetVector3 (); 
Pathfinding.trajectoryData.curvesData[a,b].tangents[0] = new Pathfinding.Vector3S(point);
		point = Pathfinding.trajectoryData.curvesData[b,a].tangents[0].GetVector3 ();
Pathfinding.trajectoryData.curvesData[a,b].tangents[1] = new Pathfinding.Vector3S(point);
	}
	
	
	void RemoveCurve(Pathfinding.Node first,Pathfinding.Node second){
		int a,b;
		string aStr = first.number.ToString()+first.type,bStr = second.number.ToString()+second.type;
		if(Pathfinding.trajectoryData.indexOf.Count<2)
			return;
		if(!Pathfinding.trajectoryData.indexOf.ContainsKey(aStr) ||!Pathfinding.trajectoryData.indexOf.ContainsKey(bStr))
			return;
		a = Pathfinding.trajectoryData.indexOf[aStr];
		b = Pathfinding.trajectoryData.indexOf[bStr];
		Pathfinding.trajectoryData.curvesData[a,b].tangents = new Pathfinding.Vector3S[0];
	}
	
	bool NodeExist(Link link){
	int result2;
		if(link == null){
			errorText = "Error:link is empty!";
		Debug.Log (errorText);
			return false;
		}
	int pathIndex = Pathfinding.FindPathOfType(link.nodeType);
		if(pathIndex<0){
			errorText = "Error:path of type "+link.nodeType+" not exist!";
		Debug.Log (errorText);
		return false;
		}
		if(!int.TryParse(link.nodeNumber,out result2)){
		errorText = "Error:Node number is not an integer!";
		Debug.Log (errorText);
			return false;
		}
	int nodeNumber = int.Parse (link.nodeNumber);
		nodeNumber--;
		if(Pathfinding.Paths[pathIndex].size<=nodeNumber){
		errorText = "Error:Node "+link.nodeNumber+" not exist in type "+link.nodeType+"!";
			Debug.Log (errorText);
			return false;
		}
		return true;	
	}
	
	void SetLink(Pathfinding.Node node,Link link){
	int pathIndex = Pathfinding.FindPathOfType(node.type);
		if(pathIndex<0){
		Debug.Log ("Error: Paths of type "+node.type+" not found!");	
			return;
		}
		int n1,n2,i;
		float weight = 0f;
		n1 = node.number;
		n2 = int.Parse (link.nodeNumber);
		n2--;
			if(link.linkWeight!="0")
			weight = float.Parse (link.linkWeight);
			else 
			weight = Vector3.Distance (Pathfinding.GetWaypointInSpace(0.5f,node.number,node.type),Pathfinding.GetWaypointInSpace(0.5f,n2,link.nodeType));
		if(node.type == link.nodeType)
Pathfinding.Paths[pathIndex].connectionLength[n1,n2]= weight;
	else{
 if(Pathfinding.Paths[pathIndex].Waypoints[n1].atypicalConnections.Length<1){
		Pathfinding.Paths[pathIndex].Waypoints[n1].atypicalConnections = new Pathfinding.AtypicalNode[1];
		Pathfinding.Paths[pathIndex].Waypoints[n1].atypicalConnections[0] = new Pathfinding.AtypicalNode();
		Pathfinding.Paths[pathIndex].Waypoints[n1].atypicalConnections[0].linkWeigth = weight;
		Pathfinding.Paths[pathIndex].Waypoints[n1].atypicalConnections[0].number = n2;
		Pathfinding.Paths[pathIndex].Waypoints[n1].atypicalConnections[0].type = link.nodeType;
			}else{
			ArrayList tempConnections = new ArrayList();
			Pathfinding.AtypicalNode tempNode = new Pathfinding.AtypicalNode();
				tempNode.linkWeigth = weight;
				tempNode.number = n2;
				tempNode.type = link.nodeType;
			Pathfinding.Node newNode;
				for(i=0;i<Pathfinding.Paths[pathIndex].Waypoints[n1].atypicalConnections.Length;i++){
					newNode = new Pathfinding.Node(Pathfinding.Paths[pathIndex].Waypoints[n1].atypicalConnections[i].number,
						Pathfinding.Paths[pathIndex].Waypoints[n1].atypicalConnections[i].type);
			if(!Pathfinding.NodesEqual(newNode,new Pathfinding.Node(tempNode.number,tempNode.type))){
				tempConnections.Add (Pathfinding.Paths[pathIndex].Waypoints[n1].atypicalConnections[i]);
				}
				}
				
				tempConnections.Add (tempNode);
Pathfinding.Paths[pathIndex].Waypoints[n1].atypicalConnections = (Pathfinding.AtypicalNode[])tempConnections.ToArray(typeof(Pathfinding.AtypicalNode));
			}
	}
	}
	
	void RemoveLinks(Pathfinding.Node node){
		if(Links.Length<1){
		//Debug.Log ("No one link found!");	
			return;
		}
		int i;
		for(i=0;i<Links.Length;i++){
		if(Links[i].linkRemove){
				RemoveLink(node,Links[i]);
			}
		}
	}
	
	void RemoveLink(Pathfinding.Node node,Link link){
	int pathIndex = Pathfinding.FindPathOfType(node.type);
		if(pathIndex<0){
		Debug.Log ("Error:path of type "+node.type+" not found!");
			return;
		}
	int n1 = node.number;
	int n2 = int.Parse (link.nodeNumber);
		n2--;
	int i;
		
		if(node.type == link.nodeType){
		Pathfinding.Paths[pathIndex].connectionLength[n1,n2] = Mathf.Infinity;	
		}else{
		ArrayList tempConnections = new ArrayList();
		Pathfinding.AtypicalNode tempNode;
		for(i=0;i<Pathfinding.Paths[pathIndex].Waypoints[n1].atypicalConnections.Length;i++){
		tempNode = Pathfinding.Paths[pathIndex].Waypoints[n1].atypicalConnections[i];
				if(tempNode.number != n2 && tempNode.type != link.nodeType)
		tempConnections.Add (Pathfinding.Paths[pathIndex].Waypoints[n1].atypicalConnections[i]);		
			}

Pathfinding.Paths[pathIndex].Waypoints[n1].atypicalConnections = new Pathfinding.AtypicalNode[tempConnections.Count];			
		for(i=0;i<tempConnections.Count;i++){
Pathfinding.Paths[pathIndex].Waypoints[n1].atypicalConnections[i] = tempConnections[i] as Pathfinding.AtypicalNode;				
			}
			}
		Pathfinding.Node node2 = new Pathfinding.Node(n2,link.nodeType);
		RemoveCurve(node,node2);
		}
	
	void ReadPathsTypes(){
	if(Pathfinding.Paths.Length<1){
		Debug.Log ("No one paths found!");	
			return;
		}
		indexOfPath = new Dictionary<string, int>();
		PathsTypes = new PathTypeData[Pathfinding.Paths.Length];
		for(i=0;i<Pathfinding.Paths.Length;i++){
		PathsTypes[i] = new PathTypeData();
		PathsTypes[i].type = Pathfinding.Paths[i].type;
			//using only to draw curves with appropriate colors
		indexOfPath.Add(PathsTypes[i].type,i);
		}
	}
	
	string IsNodeOfPath(GameObject g){
	 string result = "";
	if(Pathfinding.PathsRoot==null ){
		Debug.Log ("Error: Paths object not found!");
					return result;
	}
		if(g==null){
		return result;	
		}
	foreach(Transform type in Pathfinding.PathsRoot.transform){
		if(g.transform.IsChildOf(type)){
			result = type.name;
		}else if(g.transform.parent){
		if(g.transform.parent.IsChildOf(type))
				result = type.name;
		}		
	}
		return result;
	}
	
	void AddNewPathType(){
	bool flag = false;
	int i;
		if(Pathfinding.pathsTypes.Length>0)
	for(i=0;i<Pathfinding.pathsTypes.Length;i++){
		if(tempStr ==" " || tempStr =="" || tempStr == Pathfinding.pathsTypes[i]){
			Debug.Log ("Path type empty or already exist.");
				flag = true;
				break;
			}
		}
		if(flag)
		return;
		ArrayList tempPaths = new ArrayList();
		
		if(Pathfinding.Paths.Length>0)
		for(i=0;i<Pathfinding.Paths.Length;i++){
		tempPaths.Add (Pathfinding.Paths[i] as Pathfinding.Path);
		}
		if(!Pathfinding.PathsRoot){
		Debug.Log ("Error: paths object not found!");	
			return;
		}
		GameObject newType = new GameObject();
		newType.name = tempStr;
		newType.transform.parent = Pathfinding.PathsRoot.transform;
		newType.transform.position = Vector3.zero;
		
		Pathfinding.Path newPath = new Pathfinding.Path();
		newPath.type = tempStr;
		tempPaths.Add (newPath as Pathfinding.Path);
		Pathfinding.Paths = new Pathfinding.Path[tempPaths.Count];
		Pathfinding.pathsTypes = new string[tempPaths.Count];
		for(i=0;i<tempPaths.Count;i++){
		Pathfinding.Paths[i] = tempPaths[i] as Pathfinding.Path;
		Pathfinding.pathsTypes[i] = Pathfinding.Paths[i].type;
		}
		
		PathTypeData[] tempPathsTypes = new PathTypeData[PathsTypes.Length];
		if(PathsTypes.Length>0){
		for(i=0;i<PathsTypes.Length;i++)
			tempPathsTypes[i] = PathsTypes[i];
		}
		ReadPathsTypes();
		if(tempPathsTypes.Length>0){
		int j;
		for(i=0;i<PathsTypes.Length;i++)
			for(j=0;j<tempPathsTypes.Length;j++){
			if(tempPathsTypes[j].type == PathsTypes[i].type)
				PathsTypes[i] = tempPathsTypes[j];
		}
		}
		tempPathsTypes = null;
		tempStr ="";
		curTypeId = Mathf.Clamp (curTypeId,0,PathsTypes.Length-1);
		EditorUtility.SetDirty (EditorWindow.GetWindow(typeof(NAPSWindow)));
	}
	
	void ReadConnections(Pathfinding.Node node){
	if(Pathfinding.PathsRoot==null){
	Debug.Log ("Error: paths object not found!");
			return;
		}
		int pathIndex = Pathfinding.FindPathOfType(node.type);
		if(pathIndex<0){
		Debug.Log ("Error: can't find path of type '"+node.type+"'!");	
			return;
		}
		if(node.number<0){
		Debug.Log ("Error: node number < 0!");	
			return;
		}
		int i;
		ArrayList tempLinks = new ArrayList();
		Link curLink ;
		float curWeigth;
		
		for(i=0;i<Pathfinding.Paths[pathIndex].size;i++){
			if(node.number != i){
				curWeigth = Pathfinding.Paths[pathIndex].connectionLength[node.number,i]; 
			if(curWeigth>0 && curWeigth<Mathf.Infinity){
					curLink = new Link();
					curLink.linkWeight = curWeigth.ToString();
					curLink.nodeType = Pathfinding.Paths[pathIndex].type;
					curLink.nodeNumber = (i+1).ToString();
					tempLinks.Add (curLink);
					}
				}

		}
			if(Pathfinding.Paths[pathIndex].Waypoints[node.number].atypicalConnections.Length>0)
				for(i=0;i<Pathfinding.Paths[pathIndex].Waypoints[node.number].atypicalConnections.Length;i++){
				curLink = new Link();
				curLink.linkWeight = Pathfinding.Paths[pathIndex].Waypoints[node.number].atypicalConnections[i].linkWeigth.ToString();
				curLink.nodeNumber = (Pathfinding.Paths[pathIndex].Waypoints[node.number].atypicalConnections[i].number+1).ToString();
				curLink.nodeType = Pathfinding.Paths[pathIndex].Waypoints[node.number].atypicalConnections[i].type;
				tempLinks.Add (curLink);
					}
		curLink = null;
		Links = new Link[tempLinks.Count];
		for(i=0;i<Links.Length;i++){
		Links[i] = tempLinks[i] as Link;
		}
		tempLinks = null;
	}
	

	
	void CheckHierarchy(){
	if(Pathfinding.PathsRoot==null){
		Debug.Log ("Error:Paths root object no exist!");
			return;
		}
		int i;
		int removedNode=-1;
		Transform curType;
		for(i=0;i<Pathfinding.Paths.Length;i++){
			//if(Pathfinding.Paths[i].size<1)
				//return;
		curType = Pathfinding.PathsRoot.transform.Find(Pathfinding.Paths[i].type);
			if(curType){
			if(curType.childCount>0){
				removedNode = CheckNumbers(curType);
					if(removedNode !=-2)
					Pathfinding.RebuildPath(curType.name,removedNode,curType.childCount);
				}else{
				Pathfinding.Paths[i] = new Pathfinding.Path();
				Pathfinding.Paths[i].type = curType.name;
				}
			}
		}
		if(removePath!="")
			RemovePath(removePath);
	}
	
	int CheckNumbers(Transform type){
		int result = -1;
		int number = 1;
		int curNumber;
		string pointName;
		foreach(Transform point in type){
		pointName = point.name;
		curNumber = int.Parse (pointName);
			if(curNumber != number){
				if(curNumber< number){
				DestroyImmediate(point.gameObject);
					if(result == -1)
						result = -2;
				}
				else{
			pointName = number.ToString();
			point.name = pointName;
				if(result == -1)
					result = number-1;
			   }
			}
			number++;
		}
		return result;
	}
	
	void OnSceneGUI(SceneView sceneView){
		if(!playMode){
		if(PathsTypes.Length<1)
			return;
		if(!sceneCam){
			sceneCam = SceneView.currentDrawingSceneView.camera;
		}
		//Debug.Log ("scene camera fov:"+sceneCam.fieldOfView+"; far = "+sceneCam.far);
		if(placing && curType!="")
	   		PointPlacing();
		if(HierarchyChanged()){
	if(Pathfinding.Paths.Length<1)
			return;
		CheckHierarchy();
		//Pathfinding.SerializePaths(curFilePath,curFileName);
		EditorUtility.SetDirty (EditorWindow.GetWindow(typeof(NAPSWindow)));
		SceneView.RepaintAll();
		Debug.Log ("Hierarchy was changed");
		}
		}
	DrawPaths();
		if(drawCurves)
	DrawTrajectories();
		SceneView.RepaintAll();
		
	}
	
	bool HierarchyChanged(){
		int i;
		bool result = false;
		if(Pathfinding.PathsRoot==null){
			Debug.Log ("Paths root not found!");
			return result;
		}
	if(lastHierarchyState ==null){
			lastHierarchyState = new HierarchyState();
	lastHierarchyState.PathsTypes = new NAPSWindow.HierarchyState.PathType[Pathfinding.PathsRoot.transform.childCount];
				for(i=0;i<lastHierarchyState.PathsTypes.Length;i++){
		lastHierarchyState.PathsTypes[i] = new NAPSWindow.HierarchyState.PathType();
		lastHierarchyState.PathsTypes[i].nodeCount = Pathfinding.PathsRoot.transform.GetChild(i).childCount;
				}
			
		}else {
		HierarchyState curHierarchyState = new HierarchyState();
			Transform child;
			if(Pathfinding.PathsRoot.transform.childCount>0){
			curHierarchyState.PathsTypes = new NAPSWindow.HierarchyState.PathType[Pathfinding.PathsRoot.transform.childCount];
				for(i=0;i<curHierarchyState.PathsTypes.Length;i++){
				curHierarchyState.PathsTypes[i] = new NAPSWindow.HierarchyState.PathType();
					child = Pathfinding.PathsRoot.transform.GetChild (i);
					if(child)
						if(child.childCount>0)
					curHierarchyState.PathsTypes[i].nodeCount = child.childCount;
				}
			}
			if(lastHierarchyState.PathsTypes.Length != curHierarchyState.PathsTypes.Length)
				result = true;
			else if(lastHierarchyState.PathsTypes.Length>0 && curHierarchyState.PathsTypes.Length>0){
			for(i=0;i<lastHierarchyState.PathsTypes.Length;i++){
			if(lastHierarchyState.PathsTypes[i].nodeCount != curHierarchyState.PathsTypes[i].nodeCount)
				result = true;
				}		
			}
		lastHierarchyState = curHierarchyState;	
		}
		return result;
	}

	
	void DrawPaths(){
	if(PathsTypes.Length<1){
			ReadPathsTypes();
	Debug.Log ("Eror:PathsTypes is empty!");
			return;
		}
	int i,j,n,pathIndex;
	Transform p1,p2;
		
		//draw atypical links first=======================================
		for(n=0;n<PathsTypes.Length;n++){
		if(PathsTypes[n].show){
		pathIndex = Pathfinding.FindPathOfType(PathsTypes[n].type);
			if(pathIndex>=0){
					if(Pathfinding.Paths[pathIndex].size>0){	
					for(i=0;i<Pathfinding.Paths[pathIndex].size;i++){
			if(Pathfinding.Paths[pathIndex].Waypoints[i]==null){
					//Debug.Log ("Paths of type "+Pathfinding.Paths[pathIndex].type+" waypoint"+i+" is null!");
							return;
						}
			if(Pathfinding.Paths[pathIndex].Waypoints[i].transform==null){
			//Debug.Log ("Paths of type "+Pathfinding.Paths[pathIndex].type+" waypoint"+i+" transform is null!");
							return;
						}
		p1 = Pathfinding.Paths[pathIndex].Waypoints[i].transform;
				if(Pathfinding.Paths[pathIndex].Waypoints[i].atypicalConnections.Length>0){
					for(j=0;j<Pathfinding.Paths[pathIndex].Waypoints[i].atypicalConnections.Length;j++)
				DrawAtypicalLink(p1,i,Pathfinding.Paths[pathIndex].Waypoints[i].atypicalConnections[j]);			
						}	
					}
				  }//else Debug.Log ("Paths of type "+Pathfinding.Paths[pathIndex].type+" size <1!");
				}
			}
		}
		//================================================================
		//Draw links and buttons +++++++++++++++++++++++++++++++++++++++++++++++
		
		for(n=0;n<PathsTypes.Length;n++){
		if(PathsTypes[n].show){
		pathIndex = Pathfinding.FindPathOfType(PathsTypes[n].type);
			if(pathIndex>=0){
					if(Pathfinding.Paths[pathIndex].size>0){
					
		for(i=0;i<Pathfinding.Paths[pathIndex].size;i++){
			if(Pathfinding.Paths[pathIndex].Waypoints[i]==null)
							return;
			if(Pathfinding.Paths[pathIndex].Waypoints[i].transform==null)
							return;
		p1 = Pathfinding.Paths[pathIndex].Waypoints[i].transform;

			for(j=0;j<Pathfinding.Paths[pathIndex].size;j++){
			if(Pathfinding.Paths[pathIndex].Waypoints[j]==null)
							return;
			if(Pathfinding.Paths[pathIndex].Waypoints[j].transform==null)
							return; 
					
				//if(Pathfinding.Paths[pathIndex].connectionLength[i,j]  != Mathf.Infinity){
	p2 = Pathfinding.Paths[pathIndex].Waypoints[j].transform;
	DrawLink(PathsTypes[n].color,p1,p2,n,i,j,PathsTypes[n].type,Pathfinding.Paths[pathIndex].connectionLength[i,j]);		
						//}
						}

					}
				}//else Debug.Log ("Paths of type "+Pathfinding.Paths[pathIndex].type+" size <1!");
			}
		}
		}
		//++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
		//Draw nodes names===========================================
		Vector3 waypointPos = Vector3.zero;
		Vector3 waypointDir = Vector3.zero;
		for(n=0;n<PathsTypes.Length;n++){
		if(PathsTypes[n].show){
		pathIndex = Pathfinding.FindPathOfType(PathsTypes[n].type);
			if(pathIndex>=0){
					if(Pathfinding.Paths[pathIndex].size>0){	
			for(i=0;i<Pathfinding.Paths[pathIndex].size;i++){
			waypointPos = Pathfinding.GetWaypointInSpace(0.5f,i,Pathfinding.Paths[pathIndex].type);				
			waypointDir = waypointPos - sceneCam.transform.position;
			if(!Physics.Linecast(waypointPos,sceneCam.transform.position))
			if(Vector3.Angle(waypointDir,sceneCam.transform.forward)<60)
Handles.Label (waypointPos,Pathfinding.Paths[pathIndex].Waypoints[i].transform.name+Pathfinding.Paths[pathIndex].type);
						}
						}//else Debug.Log ("Paths of type "+Pathfinding.Paths[pathIndex].type+" size <1!");
				}else
			Debug.Log ("Can't draw path of type "+PathsTypes[n].type+" (path not found)!");
				}
			}
		//=============================================================
	}
	
	
	void DrawLink(Color c,Transform p1,Transform p2,int pathIndex,int point1,int point2,string type,float linkLength){
		Transform child;
		Vector3[] points = new Vector3[4];
	Handles.color = bipointC;
				
	if(Handles.Button(p1.position,Quaternion.identity,pointRadius,pointRadius,Handles.SphereCap)){
			Selection.activeGameObject = p1.gameObject;
			EditorUtility.SetDirty(EditorWindow.GetWindow(typeof(NAPSWindow)));
			curTangent = new CurveId();
			trajectoryDialog = false;
		}
		points[0] = p1.position;
		if(p1.childCount>0){
			child = p1.GetChild (0);
			points[3] = child.position;
			Handles.DrawLine(p1.position,child.position);
			if(Handles.Button(child.position,Quaternion.identity,pointRadius,pointRadius,Handles.SphereCap)){
				Selection.activeGameObject = child.gameObject;
				EditorUtility.SetDirty(EditorWindow.GetWindow(typeof(NAPSWindow)));
				curTangent = new CurveId();
				trajectoryDialog = false;
			}
		}else points[3] = points[0];

	if(Handles.Button(p2.position,Quaternion.identity,pointRadius,pointRadius,Handles.SphereCap)){
			Selection.activeGameObject = p2.gameObject;
			EditorUtility.SetDirty(EditorWindow.GetWindow(typeof(NAPSWindow)));
			curTangent = new CurveId();
			trajectoryDialog = false;
		}
		points[1] = p2.position;
		if(p2.childCount>0){
			child = p2.GetChild (0);
			points[2] = child.position;
			Handles.DrawLine(p2.position,child.position);
			if(Handles.Button(child.position,Quaternion.identity,pointRadius,pointRadius,Handles.SphereCap)){
				Selection.activeGameObject = child.gameObject;
				EditorUtility.SetDirty(EditorWindow.GetWindow(typeof(NAPSWindow)));	
				curTangent = new CurveId();
				trajectoryDialog = false;
			}
		}else points[2] = points[1];
		if(linkLength<Mathf.Infinity){
			//c = new Color(c.r,c.g,c.b,0.5f);
	Handles.color = c;
	if(!drawAreas){
		Handles.DrawLine(Pathfinding.GetWaypointInSpace(0.5f,point1,type),Pathfinding.GetWaypointInSpace(0.5f,point2,type));
		}else{
		Handles.DrawSolidRectangleWithOutline(points,c,c);
		}
	}
	}
	
	void DrawAtypicalLink(Transform point1, int point1Number, Pathfinding.AtypicalNode link){
	Transform point2 = Pathfinding.GetPointTransform(link.number,link.type);
		if(point2==null){
		return;	
		}
	Vector3[] points = new Vector3[4];
		points[0] = point1.position;
		points[1] = points[0];
		points[2] = point2.position;
		points[3] = points[2];
		if(point1.childCount>0){
		points[1] = point1.GetChild(0).position;
			}
		if(point2.childCount>0){
		points[3] = point2.GetChild (0).position;		
			}
		Handles.color = atypicalC;
	if(!drawAreas){
			if(points[0] != points[1]){
		points[1] = (points[1] - points[0])*0.5f;
		points[0] = points[0]+points[1];
			}
			if(points[2] !=points[3]){
		points[2] = (points[2] - points[3])*0.5f;
		points[3] = points[3] + points[2];
			}
		Handles.DrawLine (points[0],points[3]);
		}else{
		Handles.DrawSolidRectangleWithOutline(points,atypicalC,atypicalC);
		}
	}
	
	
	void DrawTrajectories(){
		int i,j,size,path1Type,path2Type;
		Vector3 drawPos = Vector3.zero;
		Vector3[] curvePoints = new Vector3[4];
		string point1Type,point2Type;
		Color curveC = Color.red;
		Pathfinding.Node tempNode = new Pathfinding.Node(-1,"");
		size = Pathfinding.trajectoryData.indexOf.Count;
		//Debug.Log ("trajectories count:"+size);
		if(size<2)
		return;
	for(i=0;i<size;i++)
	for(j=0;j<size;j++){
			tempNode = Pathfinding.trajectoryData.nodes[i] as Pathfinding.Node; 
		point1Type  = tempNode.type;
			tempNode =  Pathfinding.trajectoryData.nodes[j] as Pathfinding.Node; 
		point2Type  = tempNode.type;
		path1Type = indexOfPath[point1Type];
		path2Type = indexOfPath[point2Type];
		if(PathsTypes[path1Type].show && PathsTypes[path2Type].show){
	if(Pathfinding.trajectoryData.curvesData[i,j]!=null){
		if(Pathfinding.trajectoryData.curvesData[i,i].tangents.Length>0)
			if(Pathfinding.trajectoryData.curvesData[i,j].tangents.Length>1)
				if(Pathfinding.trajectoryData.curvesData[j,j].tangents.Length>0){
					if(point1Type == point2Type)
					curveC = PathsTypes[path1Type].color;
					else
					curveC = Color.grey;
				Handles.color = curveC;	
						drawPos = Pathfinding.trajectoryData.curvesData[i,j].tangents[0].GetVector3 ();
						
		if(Handles.Button(drawPos,Quaternion.identity,0.7f,0.7f,Handles.SphereCap)){
				curTangent.i = i;
				curTangent.j = j;
				curTangent.tangentIndex = 0;
				handlePos = Vector3.zero;
				Selection.activeObject = null;
					}
						drawPos = Pathfinding.trajectoryData.curvesData[i,j].tangents[1].GetVector3 ();
						
		if(Handles.Button(drawPos,Quaternion.identity,0.7f,0.7f,Handles.SphereCap)){
				curTangent.i = i;
				curTangent.j = j;
				curTangent.tangentIndex = 1;
				handlePos = Vector3.zero;
				Selection.activeObject = null;
					}
			curvePoints[0] = Pathfinding.trajectoryData.curvesData[i,i].tangents[0].GetVector3 ();
			curvePoints[1] = Pathfinding.trajectoryData.curvesData[i,j].tangents[0].GetVector3();
			curvePoints[2] = Pathfinding.trajectoryData.curvesData[i,j].tangents[1].GetVector3();
			curvePoints[3] = Pathfinding.trajectoryData.curvesData[j,j].tangents[0].GetVector3 ();
	Handles.DrawBezier(curvePoints[0],curvePoints[3],curvePoints[1],curvePoints[2],curveC,null,2f);
					}
			}
		  }
		}
		DrawTangentTranslation();
	}
	
	void DrawTangentTranslation(){
		if(curTangent.i<0 || curTangent.j<0 || curTangent.tangentIndex<0)
			return;
		if(handlePos == Vector3.zero)
handlePos = Pathfinding.trajectoryData.curvesData[curTangent.i,curTangent.j].tangents[curTangent.tangentIndex].GetVector3();
	handlePos = Handles.PositionHandle(handlePos,Quaternion.identity);
Pathfinding.trajectoryData.curvesData[curTangent.i,curTangent.j].tangents[curTangent.tangentIndex] =
			new Pathfinding.Vector3S(handlePos);
	}
	
	
	
	void PointPlacing(){
    // if SceneView is not drawing now,then return.
    if (SceneView.currentDrawingSceneView == null){
       Debug.Log ("Select a scene view before adding a waypoint!");
       return;
    }
    RaycastHit hit;
		Quaternion normalRot;
    if (Physics.Raycast (sceneCam.transform.position,sceneCam.transform.forward, out hit, Mathf.Infinity)) {
			normalRot = Quaternion.LookRotation(hit.normal);
			Handles.color = Color.green;
			Handles.CircleCap(0,hit.point,normalRot,pointRadius);
			Handles.color = Color.cyan;
			Handles.DrawLine(hit.point,hit.point+hit.normal*distToSurface);
			Handles.SphereCap(0,hit.point+hit.normal*distToSurface,Quaternion.identity,pointRadius);
			if(curPoint!=null){
			Handles.color = Color.green;
			Handles.DrawLine(curPoint.transform.position,hit.point+hit.normal*distToSurface);
			}
			DefineAction(hit.point+hit.normal*distToSurface);
			SceneView.currentDrawingSceneView.Focus();
    }
}
	
	void DefineAction(Vector3 p){
	Event e  = Event.current;
		GameObject child = null;
				if(e.type == EventType.KeyUp){
//			Debug.Log ("key is pressed");
		if(curPoint == null){
		lastNumber = GetLastNumberInType(curType);  
		if(e.keyCode == KeyCode.Alpha1){
					lastNumber++;
		curPoint = new GameObject();
		curPoint.transform.position = p;
		curPoint.name = lastNumber.ToString ();
		curPoint.transform.parent = Pathfinding.PathsRoot.transform.Find(curType);
		}
		}else {
		if(e.keyCode == KeyCode.Alpha1){
		child = new GameObject();
		child.transform.position = p;
		child.name = curPoint.transform.name;
		child.transform.parent = curPoint.transform;
		curPoint = null;
		}else if(e.keyCode == KeyCode.Alpha2){
					curPoint = null;
				}
		}
		}
	}
	
	int GetLastNumberInType(string type){
		int result = 1;
	Transform typeT = Pathfinding.PathsRoot.transform.Find (type);
		if(typeT){
		result = typeT.childCount;	
		}
		return result;
	}
	


	void OnDisable(){
	SceneView.onSceneGUIDelegate-= this.OnSceneGUI;
	EditorApplication.update-=this.ConstantUpdate;
		SaveData ();
		SaveWindowSettings();
	SceneView.RepaintAll();
		Pathfinding.Reset ();
		if(Pathfinding.PathsRoot!=null){
		DestroyImmediate(Pathfinding.PathsRoot);	
		}
	}
	
	void SaveData(){
		temp = EditorUtility.DisplayDialog("Save navigation data","Do you want to save "+curFileName+" ?","Save","Don't save");
		if(temp){
		SaveNavigationData();
		if(PathsTypes.Length>0)
		SavePathsSettings();
		}		
	}
	
	void SaveWindowSettings(){
	WindowSettings s = new WindowSettings();
		s.pathsRootName = Pathfinding.pathsRootObject;
		s.pointRadius = pointRadius;
		s.distToSurface = distToSurface;
		s.SetBipointColor(bipointC);
		BINS.Save(s,Application.dataPath+"/NAPS","Settings.wst");
	}
	
	void  LoadWindowSettings(WindowSettings s){
		if(s==null)
			return ;
		Pathfinding.pathsRootObject = s.pathsRootName;
		pointRadius = s.pointRadius;
		distToSurface = s.distToSurface;
		bipointC = s.GetBipointColor();
	}
	
	void SavePathsSettings(){
	int i;
	PathsSettings s = new PathsSettings();
		if(PathsTypes.Length<1)
			return;
		s.PathsTypes = new NAPSWindow.PathsSettings.PathType[PathsTypes.Length];
		for(i=0;i<PathsTypes.Length;i++){
		s.PathsTypes[i] = new NAPSWindow.PathsSettings.PathType();
		s.PathsTypes[i].type = PathsTypes[i].type;
		s.PathsTypes[i].color = new ColorS();
		s.PathsTypes[i].color.SetColor(PathsTypes[i].color);
		}
		
		BINS.Save(s,curFilePath,curFileName.Remove(curFileName.IndexOf("."))+".est");
	}
	
	void SaveNavigationData(){
	if(Pathfinding.Paths.Length>0){	
		if(!saveCurves)
	Pathfinding.trajectoryData = new Pathfinding.TrajectoryData();
	Pathfinding.SerializePaths(curFilePath,curFileName);
		}		
	}
	
	void LoadPathsSettings(PathsSettings s){
		if(s==null)
			return;
	int i,j;
		if(PathsTypes.Length>0 && s.PathsTypes.Length>0)
		for(i=0;i<PathsTypes.Length;i++)
			for(j=0;j<s.PathsTypes.Length;j++){
			if(PathsTypes[i].type == s.PathsTypes[j].type)
					PathsTypes[i].color = s.PathsTypes[j].color.ReadColor();
			}
	}
	
	
	void RemovePath(string type){
	int i,j;
		if(!Pathfinding.PathsRoot){
		Debug.Log ("PathsRoot not found in Pathfinding.PathsRoot!");
			return;
		}
	Transform pathType = Pathfinding.PathsRoot.transform.FindChild(type);
		if(!pathType){
		Debug.Log ("Can't find transform of path type "+type+" in PathsRoot!");
			return;
		}
		removePath = type;
		if(pathType.childCount>0){
			DestroyImmediate(pathType.GetChild (pathType.childCount-1).gameObject);
		}else{	
		//crear global path data( remove this path from paths array)
		Pathfinding.Path[] tempPaths = new Pathfinding.Path[Pathfinding.Paths.Length-1];
		string[] _pathsTypes = new string[Pathfinding.pathsTypes.Length-1];
		i=-1;
		for(j=0;j<tempPaths.Length+1;j++){
		if(Pathfinding.Paths[j].type != type){
				i++;
				tempPaths[i] = new Pathfinding.Path();
				tempPaths[i].type = Pathfinding.Paths[j].type;
				tempPaths[i].size = Pathfinding.Paths[j].size;
				tempPaths[i].connectionLength = Pathfinding.Paths[j].connectionLength;
				tempPaths[i].nextPoint = Pathfinding.Paths[j].nextPoint;
				tempPaths[i].pathLength = Pathfinding.Paths[j].pathLength;
				tempPaths[i].Waypoints = Pathfinding.Paths[j].Waypoints;
				_pathsTypes[i] = Pathfinding.pathsTypes[j];
				
			}
		}
		Pathfinding.Paths = new Pathfinding.Path[0];
		Pathfinding.Paths = tempPaths;	
		Pathfinding.pathsTypes = _pathsTypes;
			//ClearAtypicalConnections(type);
			
		//clear the path data of interface
		PathTypeData[] tempPathsTypes = new PathTypeData[PathsTypes.Length-1];
		i=-1;
		for(j=0;j<PathsTypes.Length;j++){
		 if(PathsTypes[j].type!=type){
				i++;
			tempPathsTypes[i] = new PathTypeData();
			tempPathsTypes[i].type = PathsTypes[j].type;
			tempPathsTypes[i].color = PathsTypes[j].color;
			tempPathsTypes[i].show = PathsTypes[j].show;
			}
		}
		PathsTypes = new PathTypeData[0];
		PathsTypes = tempPathsTypes;
		DestroyImmediate(pathType.gameObject);
			removePath = "";
		curTypeId = Mathf.Clamp (curTypeId,0,PathsTypes.Length-1);
	}
	}
	
	void ClearAtypicalConnections(string type){
	int i,j,k;
	ArrayList atypicalConnections = new ArrayList();
		if(Pathfinding.Paths.Length<1)
			return;
		for(i=0;i<Pathfinding.Paths.Length;i++){
		 for(j=0;j<Pathfinding.Paths[i].size;j++){
				if(Pathfinding.Paths[i].Waypoints[j].atypicalConnections.Length>0)
			for(k=0;k<Pathfinding.Paths[i].Waypoints[j].atypicalConnections.Length;k++){
			if(Pathfinding.Paths[i].Waypoints[j].atypicalConnections[k].type!=type)
						atypicalConnections.Add(Pathfinding.Paths[i].Waypoints[j].atypicalConnections[k]);
				}
Pathfinding.Paths[i].Waypoints[j].atypicalConnections = (Pathfinding.AtypicalNode[])atypicalConnections.ToArray(typeof(Pathfinding.AtypicalNode));
			}
		}
		Debug.Log ("atypical connections cleared");
	}
	
	
	void SelectFirstNode(){
		if(Pathfinding.NodeIsEmpty(connectionNodes[0]))
			return;
		Transform nodeTransform = Pathfinding.GetPointTransform(connectionNodes[0].number,connectionNodes[0].type);
		if(nodeTransform){
		Selection.activeGameObject = nodeTransform.gameObject;	
		}
	}
	
	
	public class PathTypeData
	{
	public string type = "";
	public bool show = true;
	public Color color = Color.green;
	}
	
	public class Link
	{
	public string nodeNumber ="";
	public string nodeType = "";
	public string linkWeight ="0";
	public bool linkRemove = false;
	}
	
	[System.Serializable]
	public class WindowSettings
	{
	public string pathsRootName;	
	public float pointRadius,distToSurface;
	public ColorS bipointC = new ColorS();
		
		public void SetBipointColor(Color c){
			bipointC.R = c.r;
			bipointC.G = c.g;
			bipointC.B = c.b;
			bipointC.A = c.a;
		}
		
		public Color GetBipointColor(){
		return new Color(bipointC.R,bipointC.G,bipointC.B,bipointC.A);	
		}

	}
	
	[System.Serializable]
	public class PathsSettings
	{
		[System.Serializable]
		public class PathType
		{
			public string type="";
			public ColorS color;
		}
	public PathType[] PathsTypes = new PathType[0];
	}
	
	[System.Serializable]
	public class ColorS
	{
	public float R,G,B,A;
		public void SetColor(Color c){
			R = c.r;
			G = c.g;
			B = c.b;
			A = c.a;
			}
			
			public Color ReadColor(){
			return new Color(R,G,B,A);	
			}
	}
	
	
	public class HierarchyState
	{
	
		public class PathType
		{
		public int nodeCount = 0;	
		}
	public HierarchyState.PathType[] PathsTypes = new HierarchyState.PathType[0];		
	}
	
	//class to store temporary curve tangent to provide correct translation of tangent 
	public class CurveId
	{
	public int i=-1,j=-1,tangentIndex = -1;	
	}
	
	
}
