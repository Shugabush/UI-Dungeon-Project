#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
using UnityEditor.UI;

[CustomEditor(typeof(MerchandiseSlot))]
public class MerchandiseSlotEditor : SelectableEditor
{
    MerchandiseSlot merchandiseSlot;
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        if (merchandiseSlot == null)
        {
            merchandiseSlot = (MerchandiseSlot)target;
        }

        EditorGUI.BeginChangeCheck();

        EditorGUILayout.PropertyField(serializedObject.FindProperty("item"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("icon"));

        serializedObject.ApplyModifiedProperties();

        if (EditorGUI.EndChangeCheck())
        {
            EditorUtility.SetDirty(merchandiseSlot);
        }
    }
}
#endif