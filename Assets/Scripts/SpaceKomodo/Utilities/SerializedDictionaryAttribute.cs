using System;
using UnityEngine;

namespace SpaceKomodo.Utilities
{
    [AttributeUsage(AttributeTargets.Field)]
    public class SerializedDictionaryAttribute : PropertyAttribute
    {
        public string KeyLabel { get; private set; }
        public string ValueLabel { get; private set; }
        
        public SerializedDictionaryAttribute(string keyLabel, string valueLabel)
        {
            KeyLabel = keyLabel;
            ValueLabel = valueLabel;
        }
    }
}