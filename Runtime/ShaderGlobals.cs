using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using NUnit.Framework.Internal;
using UnityEngine.UIElements;
//using static UnityEngine.Rendering.DebugUI;
#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.UIElements;
using System.IO;
#endif

class ShaderGlobals : ScriptableObject
{
    [Serializable]
    public struct ShaderGlobalFloat
    {
        public string referenceName;
        public float value;
    }

    [Serializable]
    public struct ShaderGlobalFloatArray
    {
        public string referenceName;
        public List<float> value;
    }

    [Serializable]
    public struct ShaderGlobalInteger
    {
        public string referenceName;
        public int value;
    }

    [Serializable]
    public struct ShaderGlobalColor
    {
        public string referenceName;
        public Color value;
    }

    [Serializable]
    public struct ShaderGlobalVector
    {
        public string referenceName;
        public Vector4 value;
    }

    [Serializable]
    public struct ShaderGlobalVectorArray
    {
        public string referenceName;
        public List<Vector4> value;
    }

    [Serializable]
    public struct ShaderGlobalMatrix
    {
        public string referenceName;
        public Matrix4x4 value;
    }

    [Serializable]
    public struct ShaderGlobalMatrixArray
    {
        public string referenceName;
        public List<Matrix4x4> value;
    }

    [Serializable]
    public struct ShaderGlobalTexture
    {
        public string referenceName;
        public Texture value;
    }

    [Serializable]
    public struct ShaderGlobalBuffer
    {
        public string referenceName;
        public ComputeBuffer value;
    }

#if UNITY_EDITOR
    [InitializeOnLoadMethod, InitializeOnEnterPlayMode]
#endif
    [RuntimeInitializeOnLoadMethod]
    static void SetGlobals()
    {
        //var settings = GetSerializedSettings().targetObject as ShaderGlobals;
        var settings = Resources.Load<ShaderGlobals>("ShaderGlobals");
        if (settings is null)
            return;

        foreach (var floatGlobal in settings.globalFloats)
            Shader.SetGlobalFloat(floatGlobal.referenceName, floatGlobal.value);
    }

    // TODO : move to Project Settings Folder
    public const string k_ShaderGlobalsSettingsPath = "Assets/Resources/ShaderGlobals.asset";
    //public const string k_ShaderGlobalsSettingsPath = "ProjectSettings/ShaderGlobals.asset";

    public List<ShaderGlobalFloat> globalFloats = new List<ShaderGlobalFloat>();
    public List<ShaderGlobalFloatArray> globalFloatArrays = new List<ShaderGlobalFloatArray>();
    public List<ShaderGlobalInteger> globalIntegers = new List<ShaderGlobalInteger>();

    private void OnValidate()
    {
        SetGlobals();
    }
#if UNITY_EDITOR
    internal static ShaderGlobals GetOrCreateSettings()
    {
        var settings = AssetDatabase.LoadAssetAtPath<ShaderGlobals>(k_ShaderGlobalsSettingsPath);
        if (settings == null)
        {
            var dir = Path.GetDirectoryName(k_ShaderGlobalsSettingsPath);
            if (!Directory.Exists(dir))
                Directory.CreateDirectory(dir);
            settings = ScriptableObject.CreateInstance<ShaderGlobals>();
            AssetDatabase.CreateAsset(settings, k_ShaderGlobalsSettingsPath);
            AssetDatabase.SaveAssets();
        }
        return settings;
    }

    internal static SerializedObject GetSerializedSettings()
    {
        return new SerializedObject(GetOrCreateSettings());
    }

    //[SettingsProvider]
    //public static SettingsProvider ShaderGlobalsSettingsProvider()
    //{
    //    // First parameter is the path in the Settings window.
    //    // Second parameter is the scope of this setting: it only appears in the Project Settings window.
    //    var provider = new SettingsProvider("Project/Shader Globals", SettingsScope.Project)
    //    {
    //        // By default the last token of the path is used as display name if no label is provided.
    //        //label = "Floats",
    //        // Create the SettingsProvider and initialize its drawing (IMGUI) function in place:
    //        guiHandler = (searchContext) =>
    //        {
    //            var settings = GetSerializedSettings();
    //            EditorGUILayout.PropertyField(settings.FindProperty("globalFloats"), new GUIContent("Floats"));
    //        },

    //        // Populate the search keywords to enable smart search filtering and label highlighting:
    //        keywords = new HashSet<string>(new[] { "Shader", "Global" })
    //    };

    //    return provider;
    //}

    [SettingsProvider]
    public static SettingsProvider CreateMyCustomSettingsProvider()
    {
        // First parameter is the path in the Settings window.
        // Second parameter is the scope of this setting: it only appears in the Settings window for the Project scope.
        var provider = new SettingsProvider("Project/Shader Globals", SettingsScope.Project)
        {
            label = "Shader Globals",
            // activateHandler is called when the user clicks on the Settings item in the Settings window.
            activateHandler = (searchContext, rootElement) =>
            {
                var serializedObject = GetSerializedSettings();

                // rootElement is a VisualElement. If you add any children to it, the OnGUI function
                // isn't called because the SettingsProvider uses the UIElements drawing framework.
                //var styleSheet = AssetDatabase.LoadAssetAtPath<StyleSheet>("Assets/Editor/settings_ui.uss");
                //rootElement.styleSheets.Add(styleSheet);
                var title = new Label()
                {
                    text = "Shader Globals"
                };
                title.AddToClassList("title");
                rootElement.Add(title);


                var settings = new VisualElement()
                {
                    style =
                    {
                        flexDirection = FlexDirection.Column
                    }
                };
                settings.AddToClassList("property-list");
                settings.Bind(serializedObject);

                rootElement.Add(settings);

                var gFloatField = new ListView();
                gFloatField.showFoldoutHeader = true;
                gFloatField.headerTitle = "Global Floats";
                gFloatField.virtualizationMethod = CollectionVirtualizationMethod.DynamicHeight;
                gFloatField.reorderable = true;
                gFloatField.reorderMode = ListViewReorderMode.Animated;
                gFloatField.bindingPath = "globalFloats";
                gFloatField.showAddRemoveFooter = true;
                gFloatField.AddToClassList("property-value");
                settings.Add(gFloatField);
            },

            // Populate the search keywords to enable smart search filtering and label highlighting:
            keywords = new HashSet<string>(new[] { "Shader", "Global" })
        };

        return provider;
    }
#endif
}