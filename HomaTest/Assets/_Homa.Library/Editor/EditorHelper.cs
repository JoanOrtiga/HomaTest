using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace _Homa.Library.Editor
{
    public static class EditorHelper
    {
        public static T Clone<T>(this T scriptableObject) where T : ScriptableObject
        {
            if (scriptableObject == null)
            {
                Debug.LogError($"ScriptableObject was null. Returning default {typeof(T)} object.");
                return (T)ScriptableObject.CreateInstance(typeof(T));
            }

            T instance = Object.Instantiate(scriptableObject);
            instance.name = scriptableObject.name;
            return instance;
        }
        
        /// <summary>
        /// Gets all children of `SerializedProperty` at 1 level depth.
        /// </summary>
        /// <param name="serializedProperty">Parent `SerializedProperty`.</param>
        /// <returns>Collection of `SerializedProperty` children.</returns>
        public static IEnumerable<SerializedProperty> GetChildren(this SerializedProperty serializedProperty)
        {
            SerializedProperty currentProperty = serializedProperty.Copy();
            SerializedProperty nextSiblingProperty = serializedProperty.Copy();
            {
                nextSiblingProperty.Next(false);
            }
 
            if (currentProperty.Next(true))
            {
                do
                {
                    if (SerializedProperty.EqualContents(currentProperty, nextSiblingProperty))
                        break;
 
                    yield return currentProperty;
                }
                while (currentProperty.Next(false));
            }
        }
        
        public static IEnumerable<SerializedProperty> GetDirectChildren(this SerializedProperty parent) {
            int dots = parent.propertyPath.Count(c => c == '.');
            foreach (SerializedProperty inner in parent) {
                bool isDirectChild = inner.propertyPath.Count(c => c == '.') == dots + 1;
                if (isDirectChild)
                    yield return inner;
            }
        }
    }
}