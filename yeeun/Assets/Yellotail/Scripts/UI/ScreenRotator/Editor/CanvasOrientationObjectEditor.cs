using UnityEditor;
using UnityEngine;

namespace Yellotail.InGame.EditorClass
{
    [CustomEditor(typeof(CanvasOrientationObject))]
    public class CanvasOrientationObjectEditor : Editor
    {
        CanvasOrientationObject m_This;

        private void OnEnable()
        {
            m_This = target as CanvasOrientationObject;
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            EditorGUILayout.Space();

            bool ret = GUILayout.Button("Set the current status to Portrait", GUILayout.Height(30));
            if (ret)
                m_This.SaveToPortirait();

            ret = GUILayout.Button("Set the current status to Landscape", GUILayout.Height(30));
            if (ret)
                m_This.SaveToLandscape();
        }
    }
}
