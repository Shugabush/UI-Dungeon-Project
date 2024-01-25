#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
using UnityEditor.UI;

[CustomEditor(typeof(DungeonRoomButton)), CanEditMultipleObjects]
public class DungeonRoomButtonEditor : SelectableEditor
{
    DungeonRoomButton dungeonRoomButton;

    public override void OnInspectorGUI()
    {
        if (dungeonRoomButton == null)
        {
            dungeonRoomButton = (DungeonRoomButton)target;
        }

        base.OnInspectorGUI();

        EditorGUI.BeginChangeCheck();

        EditorGUILayout.PropertyField(serializedObject.FindProperty("nextDungeonRooms"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("minDungeonCompleteRequirement"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("unlocked"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("completed"));

        serializedObject.ApplyModifiedProperties();

        if (EditorGUI.EndChangeCheck())
        {
            EditorUtility.SetDirty(dungeonRoomButton);
        }
    }
}
#endif