using UnityEngine;
using UnityEngine.UI;

namespace UnityEditor.UI
{
    [CustomEditor(typeof(Text_Extend), true)]
    [CanEditMultipleObjects]
    public class Text_ExtendEditor : TextEditor
    {
        SerializedProperty m_OriginText;
        SerializedProperty linkObject;
        SerializedProperty underlineOffsetY;
        SerializedProperty underlineHeightScale;

        protected override void OnEnable()
        {
            base.OnEnable();
            m_OriginText = serializedObject.FindProperty("m_OriginText");
            linkObject = serializedObject.FindProperty("linkObject");
            underlineOffsetY = serializedObject.FindProperty("underlineOffsetY");
            underlineHeightScale = serializedObject.FindProperty("underlineHeightScale");
            
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            EditorGUILayout.PropertyField(m_OriginText);
            EditorGUILayout.PropertyField(linkObject);
            EditorGUILayout.PropertyField(underlineOffsetY);
            EditorGUILayout.PropertyField(underlineHeightScale);
            serializedObject.ApplyModifiedProperties();

            base.OnInspectorGUI();



        }
    }
}