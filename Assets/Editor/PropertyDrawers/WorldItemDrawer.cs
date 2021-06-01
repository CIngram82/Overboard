using UnityEngine;
using UnityEditor;

[CustomPropertyDrawer(typeof(WorldItemAttribute))]
public class WorldItemDrawer : PropertyDrawer
{
    int _choiceIndex = -1;
    

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        if (property.serializedObject.FindProperty("_itemDatabase").objectReferenceValue == null)
            return;
        SerializedObject databaseSO = new SerializedObject(property.serializedObject.FindProperty("_itemDatabase").objectReferenceValue);
        SerializedProperty listProp = databaseSO.FindProperty("_items");

        string[] _choices = new string[listProp.arraySize];
        for (int i = 0; i < listProp.arraySize; i++)
        {
            SerializedProperty nameProp = listProp.GetArrayElementAtIndex(i);
            _choices[i] = nameProp.FindPropertyRelative("_name").stringValue;

            if (_choices[i].Equals(property.stringValue))
            {
                _choiceIndex = i;
            }
        }

        EditorGUI.BeginChangeCheck();
        _choiceIndex = EditorGUI.Popup(position, label.text, _choiceIndex, _choices);

        if (EditorGUI.EndChangeCheck())
        {
            property.stringValue = _choices[_choiceIndex];
        }
    }
}





