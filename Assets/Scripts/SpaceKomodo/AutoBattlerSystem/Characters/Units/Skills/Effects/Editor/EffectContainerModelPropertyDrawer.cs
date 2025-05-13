using UnityEditor;
using UnityEngine;
using SpaceKomodo.Editor;

namespace SpaceKomodo.AutoBattlerSystem.Characters.Units.Skills.Effects.Editor
{
    [CustomPropertyDrawer(typeof(EffectContainerModel))]
    public class EffectContainerModelPropertyDrawer : ReflectionPropertyDrawer
    {
        private const string ModelNamespace = "SpaceKomodo.AutoBattlerSystem.Characters.Units.Skills.Effects.Models";
        private const string TypePropertyPath = "Type";
        private const string DataPropertyPath = "Data";
        
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, property);
            
            position.height = EditorGUIUtility.singleLineHeight;
            
            var typeProperty = property.FindPropertyRelative(TypePropertyPath);
            var dataProperty = property.FindPropertyRelative(DataPropertyPath);
            
            EditorGUI.PropertyField(position, typeProperty);
            position.y += EditorGUIUtility.singleLineHeight + PropertySpacing;
            
            var enumValue = (EffectType)typeProperty.enumValueIndex;
            var previousEnumValue = typeProperty.hasMultipleDifferentValues ? 
                enumValue : (EffectType)typeProperty.enumValueFlag;
            
            if (previousEnumValue != enumValue)
            {
                dataProperty.stringValue = string.Empty;
                typeProperty.enumValueFlag = (int)enumValue;
            }
            
            var modelType = GetModelTypeForEnum(enumValue, ModelNamespace);
            if (modelType != null)
            {
                DrawJsonSerializedProperties(position, dataProperty, modelType);
            }
            
            EditorGUI.EndProperty();
        }
        
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            var height = EditorGUIUtility.singleLineHeight;
            
            var typeProperty = property.FindPropertyRelative(TypePropertyPath);
            var enumValue = (EffectType)typeProperty.enumValueIndex;
            
            var modelType = GetModelTypeForEnum(enumValue, ModelNamespace);
            height += GetJsonTypeHeight(modelType);
            
            return height;
        }
    }
}