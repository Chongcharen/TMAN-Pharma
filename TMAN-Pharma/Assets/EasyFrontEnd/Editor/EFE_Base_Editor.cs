using UnityEngine;
using UnityEditor;

[CustomEditor (typeof(EFE_Base))]
[CanEditMultipleObjects()]
public class EFE_Base_Editor : Editor
{
	private SerializedObject obj;// The EFE_Content_Modifier object
	private SerializedProperty firstPanel;
	private SerializedProperty useFirstPanelTransition;
	private SerializedProperty [] panelList;
	
	private SerializedProperty currentPanel;
	private SerializedProperty previousPanel;
	
	private Texture backgroundImage ;
	
	SerializedProperty j;
	SerializedProperty k;
	
	public void OnEnable()
	{
		backgroundImage = Resources.Load("icon32")as Texture;
		obj = new SerializedObject(target);
		
	} 
	
	public override void OnInspectorGUI()
	{
		serializedObject.Update();
		GUIStyle style1 = new GUIStyle();
		style1.font = EditorStyles.boldFont;
		style1.normal.textColor = new Color (0.4f,0.6f,1,1);
		
		GUIStyle style2 = new GUIStyle();
		style2.font = EditorStyles.miniFont;
		style2.normal.textColor = new Color (0.4f,0.6f,1,1);
		
		
		GUILayout.Label(backgroundImage,GUILayout.ExpandWidth(true));
		
		EditorGUILayout.HelpBox("All panel which you want to use with EFE must be placed in thie list. " +
			"If you need to create a new panel just add another element to the panel list array (Size)" +
			"and drag in you new panel object into the inpector. The ordering of the objects in this list is not important." +
			" Easy!",MessageType.Info);
		
		EditorGUILayout.LabelField("EFE First Panel To Display", style1, null);
		EditorGUILayout.PropertyField(serializedObject.FindProperty("firstPanel"),true);
		EditorGUILayout.PropertyField(serializedObject.FindProperty("useFirstPanelTransition"),true);
		
		EditorGUILayout.LabelField("EFE Panel List", style1, null);
		//EditorGUILayout.PropertyField(GetArrayElementAtIndex( panelList[0] ));
		EditorGUILayout.PropertyField(serializedObject.FindProperty("panelList"),true);
		
		//EditorGUILayout.LabelField("More coming soon..", style2, null);
		
		j = serializedObject.FindProperty("currentPanel");
		
		
		if(j!=null&&j.objectReferenceValue!=null)
		{
			EditorGUILayout.LabelField("EFE Panel Debug", style1, null);
			//Debug.Log(j);
			EditorGUILayout.PropertyField(j);
			
		}
		k = serializedObject.FindProperty("previousPanel");
		
		if(k!=null&&k.objectReferenceValue!=null)
		{
			EditorGUILayout.PropertyField(k);
		}
		
		
		
		serializedObject.ApplyModifiedProperties();
		obj.ApplyModifiedProperties(); 
		
	}
	
	public void OnSceneGUI()
	{
        // Implement what you want to see in scene view here
	}
}