using gishadev.gmtk.Core;
using UnityEditor;
using UnityEngine;

namespace gishadev.gmtk.Editor
{
    [CustomEditor(typeof(Location))]
    public class LocationEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            EditorGUILayout.Space();

            if (!GUILayout.Button("Auto-Fill From Children"))
                return;

            var location = (Location)target;
            Undo.RecordObject(location, "Auto-Fill Location");
            location.AutoFillFromChildren();
            EditorUtility.SetDirty(location);
        }
    }
}
