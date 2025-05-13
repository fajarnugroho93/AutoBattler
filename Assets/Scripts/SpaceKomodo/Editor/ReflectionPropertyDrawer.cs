using System;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace SpaceKomodo.Editor
{
    public abstract class ReflectionPropertyDrawer : PropertyDrawer
    {
        protected const float PropertySpacing = 2f;

        private static object DisplayPropertyField(Rect position, string name, object value, Type type)
        {
            if (type == typeof(float))
            {
                return EditorGUI.FloatField(position, name, (float)value);
            }

            if (type == typeof(int))
            {
                return EditorGUI.IntField(position, name, (int)value);
            }
            
            if (type == typeof(bool))
            {
                return EditorGUI.Toggle(position, name, (bool)value);
            }
            
            if (type == typeof(string))
            {
                return EditorGUI.TextField(position, name, (string)value);
            }
            
            if (type.IsEnum)
            {
                return EditorGUI.EnumPopup(position, name, (Enum)value);
            }
            
            if (type == typeof(Vector2))
            {
                return EditorGUI.Vector2Field(position, name, (Vector2)value);
            }
            
            if (type == typeof(Vector3))
            {
                return EditorGUI.Vector3Field(position, name, (Vector3)value);
            }
            
            if (type == typeof(Color))
            {
                return EditorGUI.ColorField(position, name, (Color)value);
            }
            
            EditorGUI.LabelField(position, name, "Unsupported type: " + type.Name);
            return value;
        }
        
        protected Type GetModelTypeForEnum(Enum enumValue, string baseNamespace)
        {
            var modelTypeName = $"{baseNamespace}.{enumValue}Model";
            return Type.GetType(modelTypeName) ?? 
                   Assembly.GetAssembly(enumValue.GetType()).GetType(modelTypeName);
        }
        
        protected static float DrawJsonSerializedProperties(Rect position, SerializedProperty dataProperty, Type modelType)
        {
            var originalY = position.y;
            
            try
            {
                var dataObject = string.IsNullOrEmpty(dataProperty.stringValue) ? 
                    Activator.CreateInstance(modelType) : 
                    JsonUtility.FromJson(dataProperty.stringValue, modelType);
                
                var properties = modelType.GetProperties(BindingFlags.Instance | BindingFlags.Public);
                var fields = modelType.GetFields(BindingFlags.Instance | BindingFlags.Public);
                
                if (properties.Length > 0 || fields.Length > 0)
                {
                    EditorGUI.indentLevel++;
                    
                    foreach (var propertyInfo in properties)
                    {
                        if (propertyInfo.CanRead && propertyInfo.CanWrite)
                        {
                            var value = propertyInfo.GetValue(dataObject);
                            var newValue = DisplayPropertyField(position, propertyInfo.Name, value, propertyInfo.PropertyType);
                            if (newValue != null && !newValue.Equals(value))
                            {
                                propertyInfo.SetValue(dataObject, newValue);
                            }
                            position.y += EditorGUIUtility.singleLineHeight + PropertySpacing;
                        }
                    }
                    
                    foreach (var info in fields)
                    {
                        var value = info.GetValue(dataObject);
                        var newValue = DisplayPropertyField(position, info.Name, value, info.FieldType);
                        if (newValue != null && !newValue.Equals(value))
                        {
                            info.SetValue(dataObject, newValue);
                        }
                        position.y += EditorGUIUtility.singleLineHeight + PropertySpacing;
                    }
                    
                    EditorGUI.indentLevel--;
                }
                
                dataProperty.stringValue = JsonUtility.ToJson(dataObject);
            }
            catch (Exception ex)
            {
                EditorGUI.LabelField(position, "Error: " + ex.Message);
                position.y += EditorGUIUtility.singleLineHeight + PropertySpacing;
            }
            
            return position.y - originalY;
        }
        
        protected static float GetJsonTypeHeight(Type modelType)
        {
            if (modelType == null) return 0;
            
            var properties = modelType.GetProperties(BindingFlags.Instance | BindingFlags.Public);
            var fields = modelType.GetFields(BindingFlags.Instance | BindingFlags.Public);
            
            var totalMembers = properties.Length + fields.Length;
            return (EditorGUIUtility.singleLineHeight + PropertySpacing) * totalMembers;
        }
    }
}