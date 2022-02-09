using System.Collections;
using System.Collections.Generic;

#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;

// https://github.com/EZhex1991/EZUnity/blob/020f5ef087999196943e53ad3703786fbc764d0b/Assets/EZhex1991/EZUnity/Editor/Attributes/EZMinMaxDrawer.cs

namespace Yellotail
{
    public class MinMaxAttribute : PropertyAttribute
    {
        public readonly bool fixedLimit;
        public float limitMin;
        public float limitMax;

        public MinMaxAttribute()
        {
            // limits will be retrived from zw component of the vector
            fixedLimit = false;
            limitMin = 0;
            limitMax = 1;
        }
        public MinMaxAttribute(float min, float max)
        {
            fixedLimit = true;
            this.limitMin = min;
            this.limitMax = max;
        }
    }

#if UNITY_EDITOR
    [CustomPropertyDrawer(typeof(MinMaxAttribute))]
    public class MinMaxDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            MinMaxAttribute minMaxAttribute = attribute as MinMaxAttribute;
            EditorGUI.BeginProperty(position, label, property);

            if (property.propertyType == SerializedPropertyType.Float)
            {
                property.floatValue = EditorGUILayout.Slider(label, property.floatValue, minMaxAttribute.limitMin, minMaxAttribute.limitMax);
            }
            else if (property.propertyType == SerializedPropertyType.Integer)
            {
                property.intValue = EditorGUILayout.IntSlider(label, property.intValue, (int)minMaxAttribute.limitMin, (int)minMaxAttribute.limitMax);
            }
            else if (property.propertyType == SerializedPropertyType.Vector2 || property.propertyType == SerializedPropertyType.Vector4)
            {
                position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);

                if (property.propertyType == SerializedPropertyType.Vector2)
                {
//                    property.vector2Value = EZEditorGUIUtility.MinMaxSliderV2(position, property.vector2Value, minMaxAttribute.limitMin, minMaxAttribute.limitMax);
                }
                else if (property.propertyType == SerializedPropertyType.Vector4)
                {
                    if (minMaxAttribute.fixedLimit)
                    {
//                        property.vector4Value = EZEditorGUIUtility.MinMaxSliderV4(position, property.vector4Value, minMaxAttribute.limitMin, minMaxAttribute.limitMax);
                    }
                    else
                    {
                        property.isExpanded = EditorGUI.Foldout(new Rect(position) { width = 0 }, property.isExpanded, GUIContent.none, false);
                        if (property.isExpanded)
                        {
                            property.vector4Value = EditorGUI.Vector4Field(position, "", property.vector4Value);
                        }
                        else
                        {
//                            property.vector4Value = EZEditorGUIUtility.MinMaxSliderV4(position, property.vector4Value);
                        }
                    }
                }
            }
            else
            {
                EditorGUI.HelpBox(position, string.Format("MinMaxAttribute not suitable for {0}: {1}", property.type, property.name), MessageType.Warning);
            }

            EditorGUI.EndProperty();
        }
    }
#endif
}
