using System;
using Hirame.Pantheon.Editor;
using UnityEditor;
using UnityEngine;

namespace Hirame.Apollo.Editor
{
    [CustomEditor (typeof (AmbianceArea))]
    public class AmbianceAreaEditor : UnityEditor.Editor
    {
        private Collider attachedCollider;

        public override void OnInspectorGUI ()
        {
            if (!target)
                return;
            
            serializedObject.Update ();
            
            using (var scope = new EditorGUI.ChangeCheckScope ())
            {
                var isGlobalProp = serializedObject.FindProperty ("isGlobal");
                
                if (!isGlobalProp.boolValue)
                {
                    if (!attachedCollider)
                        attachedCollider = (target as AmbianceArea).GetComponent<Collider> ();
                    
                    if (!attachedCollider || !attachedCollider.isTrigger)
                        EditorGUILayout.HelpBox ("A non global Ambiance Area should have a Collider setups as trigger.", MessageType.None);   
                }

                DrawPropertiesExcluding (serializedObject, "m_Script");
                
                if (scope.changed)
                    serializedObject.ApplyModifiedProperties ();
            }
        }

        [MenuItem ("GameObject/Audio/Abiance Area")]
        private static void CreateArea (MenuCommand command)
        {
            var ambianceArea = new GameObject ("Ambiance Volume", typeof (AmbianceArea));
            
            LayersHelper.SetOnLayer (ambianceArea, "Audio");
            
            var parent = command.context as GameObject;
            if (parent != null)
            {
                ambianceArea.transform.SetParent (parent.transform);
                ambianceArea.transform.localPosition = Vector3.zero;
                ambianceArea.transform.localRotation = Quaternion.identity;
                ambianceArea.transform.localScale = Vector3.one;
            }

            Selection.activeGameObject = ambianceArea;
        }
    }

}