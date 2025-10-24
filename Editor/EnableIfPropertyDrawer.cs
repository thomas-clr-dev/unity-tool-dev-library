using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace Vhrtz.EditorOnly
{
    [CustomPropertyDrawer(typeof(EnableIfAttribute))]
    public class EnableIfPropertyDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EnableIfAttribute enableIfAttribute = attribute as EnableIfAttribute;
            SerializedProperty activatorProperty = property.serializedObject.FindProperty(enableIfAttribute.FieldName);
            bool isEnabled = false;

            if (activatorProperty != null)
            {
                isEnabled = activatorProperty.boolValue;
            }
            else
            {
                bool didFindValue = false;
                foreach (FieldInfo fieldInfo in property.serializedObject.targetObject.GetType().GetFields(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance))
                {
                    if (fieldInfo.Name == enableIfAttribute.FieldName)
                    {
                        isEnabled = (bool)fieldInfo.GetValue(property.serializedObject.targetObject);
                        didFindValue = true;
                        break;
                    }
                }

                if (!didFindValue)
                {
                    foreach (PropertyInfo propertyInfo in property.serializedObject.targetObject.GetType().GetProperties(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance))
                    {
                        if (propertyInfo.Name == enableIfAttribute.FieldName)
                        {
                            isEnabled = (bool)propertyInfo.GetValue(property.serializedObject.targetObject);
                            didFindValue = true;
                            break;
                        }
                    }
                }

                if (!didFindValue)
                {
                    Debug.LogWarning("Missing value " + enableIfAttribute.FieldName);
                }
            }

            GUI.enabled = isEnabled;
            EditorGUI.PropertyField(position, property, label);
            GUI.enabled = true;
        }
    }
}
