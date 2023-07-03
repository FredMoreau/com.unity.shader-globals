using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.UIElements;
using System.IO;
#endif

public sealed class ShaderGlobals : ScriptableObject
{
    [Serializable]
    public class ShaderGlobal<T>
    {
        public string referenceName;
        public T value;
    }

    // TODO : move to Project Settings Folder
    public const string k_ShaderGlobalsSettingsPath = "Assets/Resources/ShaderGlobals.asset";
    //public const string k_ShaderGlobalsSettingsPath = "ProjectSettings/ShaderGlobals.asset";

    public List<ShaderGlobal<float>> globalFloats = new List<ShaderGlobal<float>>();
    public List<ShaderGlobal<int>> globalIntegers = new List<ShaderGlobal<int>>();
    public List<ShaderGlobal<Color>> globalColors = new List<ShaderGlobal<Color>>();
    public List<ShaderGlobal<Vector4>> globalVectors = new List<ShaderGlobal<Vector4>>();
    public List<ShaderGlobal<Matrix4x4>> globalMatrices = new List<ShaderGlobal<Matrix4x4>>();
    public List<ShaderGlobal<Texture2D>> globalTextures = new List<ShaderGlobal<Texture2D>>();
    public List<ShaderGlobal<List<float>>> globalFloatArrays = new List<ShaderGlobal<List<float>>>();
    public List<ShaderGlobal<List<Vector4>>> globalVectorArrays = new List<ShaderGlobal<List<Vector4>>>();
    public List<ShaderGlobal<List<Matrix4x4>>> globalMatrixArrays = new List<ShaderGlobal<List<Matrix4x4>>>();

#if UNITY_EDITOR
    [InitializeOnLoadMethod, InitializeOnEnterPlayMode]
#endif
    [RuntimeInitializeOnLoadMethod]
    static void SetGlobals()
    {
        var settings = Resources.Load<ShaderGlobals>("ShaderGlobals");
        if (settings is null)
            return;

        // floats
        foreach (var floatGlobal in settings.globalFloats)
            Shader.SetGlobalFloat(floatGlobal.referenceName, floatGlobal.value);

        // float arrays
        foreach (var floatArrayGlobal in settings.globalFloatArrays)
            Shader.SetGlobalFloatArray(floatArrayGlobal.referenceName, floatArrayGlobal.value);

        // colors
        foreach (var colorGlobal in settings.globalColors)
            Shader.SetGlobalColor(colorGlobal.referenceName, colorGlobal.value);

        // integers
        foreach (var intGlobal in settings.globalIntegers)
            Shader.SetGlobalInteger(intGlobal.referenceName, intGlobal.value);

        // matrices
        foreach (var matrixGlobal in settings.globalMatrices)
            Shader.SetGlobalMatrix(matrixGlobal.referenceName, matrixGlobal.value);

        // matrix arrays
        foreach (var matrixArrayGlobal in settings.globalMatrixArrays)
            Shader.SetGlobalMatrixArray(matrixArrayGlobal.referenceName, matrixArrayGlobal.value);

        // textures
        foreach (var textureGlobal in settings.globalTextures)
            Shader.SetGlobalTexture(textureGlobal.referenceName, textureGlobal.value);

        // vectors
        foreach (var vectorGlobal in settings.globalVectors)
            Shader.SetGlobalVector(vectorGlobal.referenceName, vectorGlobal.value);

        // vector arrays
        foreach (var vectorArrayGlobal in settings.globalVectorArrays)
            Shader.SetGlobalVectorArray(vectorArrayGlobal.referenceName, vectorArrayGlobal.value);
    }

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

    [SettingsProvider]
    static SettingsProvider ShaderGlobalsSettingsProvider()
    {
        var provider = new SettingsProvider("Project/Graphics/Shader Globals", SettingsScope.Project)
        {
            label = "Shader Globals",
            activateHandler = (searchContext, rootElement) =>
            {
                var serializedObject = GetSerializedSettings();

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

                settings.Add(MakeListView("globalFloats", "Global Floats"));
                settings.Add(MakeListView("globalIntegers", "Global Integers"));
                settings.Add(MakeListView("globalColors", "Global Colors"));
                settings.Add(MakeListView("globalVectors", "Global Vectors"));
                settings.Add(MakeListView("globalMatrices", "Global Matrices"));
                settings.Add(MakeListView("globalTextures", "Global Textures"));
                settings.Add(MakeListView("globalFloatArrays", "Global Float Arrays"));
                settings.Add(MakeListView("globalVectorArrays", "Global Vector Arrays"));
                settings.Add(MakeListView("globalMatrixArrays", "Global Matrix Arrays"));
            },

            keywords = new HashSet<string>(new[] { "Shader", "Global" })
        };

        return provider;

        // TODO : improve arrays display
        VisualElement MakeListView(string propertyPath, string headerTitle)
        {
            var gPropField = new ListView();
            gPropField.showFoldoutHeader = true;
            gPropField.headerTitle = headerTitle;
            gPropField.virtualizationMethod = CollectionVirtualizationMethod.DynamicHeight;
            gPropField.reorderable = true;
            gPropField.reorderMode = ListViewReorderMode.Animated;
            gPropField.bindingPath = propertyPath;
            gPropField.showAddRemoveFooter = true;
            gPropField.AddToClassList("property-value");
            return gPropField;
        }
    }
#endif
}