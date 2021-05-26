using UnityEngine;
using UnityEngine.UI.Ext;
using UnityEditor;

[CustomEditor(typeof(ButtonWrapper))]
public class ButtonWrapperEditor : Editor
{
    private ButtonWrapper m_target;

    private ButtonWrapper Target
    {
        get
        {
            if (m_target != null) return m_target;
            m_target = (ButtonWrapper)target;
            return m_target;
        }
    }

    public override void OnInspectorGUI()
    {
        Target.Label.text = EditorGUILayout.TextField("Label", Target.Label.text);

        if (GUI.changed)
        {
            EditorUtility.SetDirty(Target.Label);
        }
    }

}





