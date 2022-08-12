using UnityEditor;
using UnityEngine;

namespace GameState.Editor
{
    [CustomPropertyDrawer(typeof(SliderAttribute))]
    public class MinMaxPropertyDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            var minmax = ((SliderAttribute)attribute);
            if (property.isArray)
            {
                for (int i = 0; i < property.arraySize; ++i)
                    DrawProperty(minmax, position, property.GetArrayElementAtIndex(i), label);
            } else 
                DrawProperty(minmax, position, property, label);
        }

        private void DrawProperty(SliderAttribute minmax, Rect position, SerializedProperty property, GUIContent label)
        {
            position.height = EditorGUIUtility.singleLineHeight;
            EditorGUI.LabelField(position, label);
            position.y += EditorGUIUtility.singleLineHeight;
            
            const float padding = 20f;
            const float inputSize = 60f;
            float sliderSize = position.width - inputSize - inputSize - padding - padding;
            
            var min = property.FindPropertyRelative("Min").floatValue;
            var max = property.FindPropertyRelative("Max").floatValue;
            
            position.width = inputSize;
            min = EditorGUI.FloatField(position,  min);

            position.x += inputSize + padding;
            position.width = sliderSize;
            
            EditorGUI.MinMaxSlider(position, ref min, ref max, minmax.Min, minmax.Max);

            position.x += sliderSize + padding;
            position.width = inputSize;
            
            max = EditorGUI.FloatField(position, max);
            property.FindPropertyRelative("Min").floatValue = min;
            property.FindPropertyRelative("Max").floatValue = max;
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return EditorGUIUtility.singleLineHeight * 2;
        }
    }
}