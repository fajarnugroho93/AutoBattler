using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

namespace SpaceKomodo.Utilities
{
    [CustomPropertyDrawer(typeof(SerializedDictionaryAttribute))]
    public class SerializedDictionaryDrawer : PropertyDrawer
    {
        private ReorderableList _list;
        private SerializedProperty _keyValuePairsProperty;
        private string _keyLabel;
        private string _valueLabel;
        
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, property);
            
            if (_list == null)
            {
                _keyValuePairsProperty = property.FindPropertyRelative("_keyValuePairs");
                
                var serializedDictionaryAttribute = (SerializedDictionaryAttribute)this.attribute;
                _keyLabel = serializedDictionaryAttribute.KeyLabel;
                _valueLabel = serializedDictionaryAttribute.ValueLabel;
                
                _list = new ReorderableList(property.serializedObject, _keyValuePairsProperty, true, true, true, true);
                
                _list.drawHeaderCallback = rect => 
                {
                    var halfWidth = rect.width / 2f;
                    EditorGUI.LabelField(new Rect(rect.x, rect.y, halfWidth, rect.height), _keyLabel);
                    EditorGUI.LabelField(new Rect(rect.x + halfWidth, rect.y, halfWidth, rect.height), _valueLabel);
                };
                
                _list.drawElementCallback = (rect, index, _, _) => 
                {
                    var element = _keyValuePairsProperty.GetArrayElementAtIndex(index);
                    var keyProperty = element.FindPropertyRelative("Key");
                    var valueProperty = element.FindPropertyRelative("Value");
                    
                    var halfWidth = rect.width / 2f;
                    
                    EditorGUI.PropertyField(
                        new Rect(rect.x, rect.y, halfWidth - 5, EditorGUIUtility.singleLineHeight),
                        keyProperty,
                        GUIContent.none);
                        
                    EditorGUI.PropertyField(
                        new Rect(rect.x + halfWidth, rect.y, halfWidth - 5, EditorGUIUtility.singleLineHeight),
                        valueProperty,
                        GUIContent.none);
                };
                
                _list.elementHeightCallback = _ => EditorGUIUtility.singleLineHeight;
            }
            
            position.height = _list.GetHeight();
            _list.DoList(position);
            
            EditorGUI.EndProperty();
        }
        
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            if (_list == null)
            {
                _keyValuePairsProperty = property.FindPropertyRelative("_keyValuePairs");
                
                var serializedDictionaryAttribute = (SerializedDictionaryAttribute)this.attribute;
                _keyLabel = serializedDictionaryAttribute.KeyLabel;
                _valueLabel = serializedDictionaryAttribute.ValueLabel;
                
                _list = new ReorderableList(property.serializedObject, _keyValuePairsProperty, true, true, true, true);
            }
            
            return _list.GetHeight();
        }
    }
}