#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
using UnityEditor.UI;

[CustomEditor(typeof(InventorySlot)), CanEditMultipleObjects]
public class InventorySlotEditor : SelectableEditor
{
    InventorySlot inventorySlot;

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        if (inventorySlot == null)
        {
            inventorySlot = (InventorySlot)target;
        }

        EditorGUILayout.PropertyField(serializedObject.FindProperty("occupiedItem"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("count"));

        serializedObject.ApplyModifiedProperties();
    }
}
#endif