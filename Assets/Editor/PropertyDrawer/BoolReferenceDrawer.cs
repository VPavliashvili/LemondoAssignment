using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

[CustomPropertyDrawer(typeof(BoolReference))]
class BoolReferenceDrawer : PropertyDrawer {

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {

        label = EditorGUI.BeginProperty(position, label, property);
        position = EditorGUI.PrefixLabel(position, label);

        EditorGUI.BeginChangeCheck();

        // Get properties
        SerializedProperty variable = property.FindPropertyRelative("variable");


        EditorGUI.PropertyField(position, variable, GUIContent.none);

        if (EditorGUI.EndChangeCheck())
            property.serializedObject.ApplyModifiedProperties();

        EditorGUI.EndProperty();
    }

}
