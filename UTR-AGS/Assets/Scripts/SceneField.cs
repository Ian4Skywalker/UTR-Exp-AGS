/*
    SceneField Utility Script

    This script provides a custom way to reference Unity scenes in the Inspector without using raw string inputs.
    It ensures that scenes are referenced safely, reducing the chance of errors due to incorrect scene names.
    Additionally, it includes an editor-only property drawer for better usability inside the Unity Editor.

    The SceneField class allows you to store a scene reference and retrieve its name.
    The SceneFieldPropertyDrawer enables selecting a scene asset in the Inspector.
    jaja xd B-)
*/

using UnityEngine;

#if UNITY_EDITOR
using UnityEditor; // Required for modifying editor properties.
#endif

[System.Serializable] // Marks this class as serializable so it can be used in Unity's Inspector.
public class SceneField
{
    [SerializeField]
    private Object m_SceneAsset; // Stores the scene asset reference.

    [SerializeField]
    private string m_SceneName = ""; // Stores the scene name as a string.

    // Public property to retrieve the scene name.
    public string SceneName
    {
        get { return m_SceneName; }
    }

    // Allows implicit conversion so SceneField can be treated as a string (scene name).
    public static implicit operator string(SceneField sceneField)
    {
        return sceneField.SceneName;
    }
}

#if UNITY_EDITOR
// Creates a custom property drawer for SceneField in the Unity Editor.
[CustomPropertyDrawer(typeof(SceneField))]
public class SceneFieldPropertyDrawer : PropertyDrawer
{
    // Overrides the default UI rendering behavior for the SceneField property.
    public override void OnGUI(Rect _position, SerializedProperty _property, GUIContent _label)
    {
        EditorGUI.BeginProperty(_position, GUIContent.none, _property);

        // Retrieves serialized properties for the scene asset and scene name.
        SerializedProperty sceneAsset = _property.FindPropertyRelative("m_SceneAsset");
        SerializedProperty sceneName = _property.FindPropertyRelative("m_SceneName");

        // Adjusts the label positioning in the Inspector.
        _position = EditorGUI.PrefixLabel(_position, GUIUtility.GetControlID(FocusType.Passive), _label);

        if (sceneAsset != null)
        {
            // Creates an object field in the Inspector for selecting a SceneAsset.
            sceneAsset.objectReferenceValue = EditorGUI.ObjectField(_position, sceneAsset.objectReferenceValue, typeof(SceneAsset), false);

            // If a scene asset is assigned, updates the scene name property with its actual name.
            if (sceneAsset.objectReferenceValue != null)
            {
                sceneName.stringValue = (sceneAsset.objectReferenceValue as SceneAsset).name;
            }
        }

        EditorGUI.EndProperty(); // Ends the property GUI rendering.
    }
}
#endif