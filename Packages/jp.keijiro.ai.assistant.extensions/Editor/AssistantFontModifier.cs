using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace AIAssistantExtensions {

[InitializeOnLoad]
static class AssistantFontModifier
{
    const string FontAssetPath =
      "Packages/jp.keijiro.ai.assistant.extensions/Editor/NotoSansJP-Regular.ttf";

    const string TargetWindowTypeName =
      "Unity.AI.Assistant.UI.Editor.Scripts.AssistantWindow";

    //
    // Constructor and event handlers
    //

    static AssistantFontModifier()
    {
        // Subscribe to window focus change events
        EditorWindow.windowFocusChanged += OnWindowFocusChanged;

        // Double delayCall to wait for Assistant UI to be fully constructed after domain reload
        EditorApplication.delayCall += () =>
          { EditorApplication.delayCall += () => ApplyToAllOpenWindows(); };
    }

    static void OnWindowFocusChanged()
      => EditorApplication.delayCall += () =>
           CheckAndApplyCustomFont(EditorWindow.focusedWindow);

    static void ApplyToAllOpenWindows()
    {
        foreach (var window in Resources.FindObjectsOfTypeAll<EditorWindow>())
            CheckAndApplyCustomFont(window);
    }

    //
    // Font application logic
    //

    static Font _font;

    static bool CheckWindowType(EditorWindow window)
      => window?.GetType()?.FullName == TargetWindowTypeName;

    static void ApplyCustomFont(EditorWindow window)
    {
        var root = window.rootVisualElement;
        var elements = root.Query<TextElement>(className: "unity-text-element").ToList();
        if (elements.Count == 0) return;

        if (_font == null) _font = AssetDatabase.LoadAssetAtPath<Font>(FontAssetPath);
        var unityFont = new StyleFont(_font);

        foreach (var element in elements)
        {
            element.style.unityFontDefinition = StyleKeyword.None;
            element.style.unityFont = unityFont;
        }
    }

    static void CheckAndApplyCustomFont(EditorWindow window)
    {
        if (CheckWindowType(window)) ApplyCustomFont(window);
    }
}

} // namespace AIAssistantExtensions
