using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace AIAssistantExtensions {

class AssistantImeProxyWindow : EditorWindow
{
    const int BorderRadius = 8;

    VisualElement _placeholder;
    VisualElement _textFieldContainer;
    TextField _textField;

    [MenuItem("Window/AI/IME Proxy")]
    static void Open()
    {
        var window = GetWindow<AssistantImeProxyWindow>();
        window.titleContent = new GUIContent("IME Proxy");
    }

    void CreateGUI()
    {
        // Root fills the entire window
        rootVisualElement.style.flexGrow = 1;
        rootVisualElement.style.paddingTop = 4;
        rootVisualElement.style.paddingBottom = 4;
        rootVisualElement.style.paddingLeft = 4;
        rootVisualElement.style.paddingRight = 4;

        // Placeholder box shown when no text field is active.
        _placeholder = new VisualElement();
        _placeholder.style.flexGrow = 1;
        _placeholder.style.backgroundColor = new Color(0.2f, 0.2f, 0.2f, 1f);
        _placeholder.style.borderBottomLeftRadius = BorderRadius;
        _placeholder.style.borderBottomRightRadius = BorderRadius;
        _placeholder.style.borderTopLeftRadius = BorderRadius;
        _placeholder.style.borderTopRightRadius = BorderRadius;
        _placeholder.style.justifyContent = Justify.Center;
        _placeholder.style.alignItems = Align.Center;

        var label = new Label("Click here to type a prompt");
        label.style.color = new Color(0.6f, 0.6f, 0.6f, 1f);
        _placeholder.Add(label);

        _placeholder.RegisterCallback<ClickEvent>(_ => ActivateTextField());
        rootVisualElement.Add(_placeholder);
    }

    void ActivateTextField()
    {
        // Hide placeholder
        _placeholder.style.display = DisplayStyle.None;

        // Container with rounded corners and relative positioning for the hint label
        _textFieldContainer = new VisualElement();
        _textFieldContainer.style.flexGrow = 1;
        _textFieldContainer.style.position = Position.Relative;

        // Create a fresh TextField each time — no stale cursor or IME state.
        _textField = new TextField { multiline = true };
        _textField.style.flexGrow = 1;
        _textField.style.borderBottomLeftRadius = BorderRadius;
        _textField.style.borderBottomRightRadius = BorderRadius;
        _textField.style.borderTopLeftRadius = BorderRadius;
        _textField.style.borderTopRightRadius = BorderRadius;
        _textField.style.overflow = Overflow.Hidden;

        // Also round the inner TextInput element
        var textInput = _textField.Q("unity-text-input");
        if (textInput != null)
        {
            textInput.style.borderBottomLeftRadius = BorderRadius;
            textInput.style.borderBottomRightRadius = BorderRadius;
            textInput.style.borderTopLeftRadius = BorderRadius;
            textInput.style.borderTopRightRadius = BorderRadius;
        }

        _textField.RegisterCallback<KeyDownEvent>(OnKeyDown, TrickleDown.TrickleDown);
        _textFieldContainer.Add(_textField);

        // Hint label overlaid at the bottom-right corner
        var hint = new Label("\u2318+Enter to send");
        hint.style.position = Position.Absolute;
        hint.style.bottom = 6;
        hint.style.right = 10;
        hint.style.fontSize = 11;
        hint.style.color = new Color(0.5f, 0.5f, 0.5f, 0.6f);
        hint.pickingMode = PickingMode.Ignore;
        _textFieldContainer.Add(hint);

        rootVisualElement.Add(_textFieldContainer);

        // Focus after the element is attached to the panel
        _textField.schedule.Execute(() => _textField.Focus());
    }

    void DeactivateTextField()
    {
        // Destroy the TextField entirely — all internal state is discarded.
        if (_textFieldContainer != null)
        {
            _textFieldContainer.RemoveFromHierarchy();
            _textFieldContainer = null;
            _textField = null;
        }

        // Show placeholder again
        _placeholder.style.display = DisplayStyle.Flex;
    }

    void OnKeyDown(KeyDownEvent evt)
    {
        if (evt.keyCode is not (KeyCode.Return or KeyCode.KeypadEnter)) return;
        if (!evt.commandKey) return; // Cmd+Enter to send

        var text = _textField.value?.Trim();
        if (string.IsNullOrEmpty(text)) return;

        evt.StopImmediatePropagation();
#pragma warning disable CS0618
        evt.PreventDefault();
#pragma warning restore CS0618

        SendToAssistant(text);

        // Destroy the TextField after the current event processing completes.
        _textField.schedule.Execute(DeactivateTextField);
    }

    void SendToAssistant(string text)
    {
        // Find the AI Assistant window's chat input and action button.
        TextField chatInput = null;
        Button actionButton = null;

        foreach (var w in Resources.FindObjectsOfTypeAll<EditorWindow>())
        {
            if (w == this) continue;
            var typeName = w.GetType().FullName;
            if (typeName == null || !typeName.Contains("Assistant")) continue;

            var root = w.rootVisualElement;
            chatInput = root?.Q<TextField>("input");
            actionButton = root?.Q<Button>("actionButton");

            if (chatInput != null && actionButton != null) break;
            chatInput = null;
            actionButton = null;
        }

        if (chatInput == null || actionButton == null) return;

        // Set the text on the chat input field.
        // This triggers OnTextFieldValueChanged -> OnChatValueChanged,
        // which saves the prompt and enables the submit button.
        chatInput.value = text;

        // Click the action button after the value change has propagated.
        // Using PointerUpEvent avoids the KeyDownEvent -> BubbleUp crash
        // path entirely.
        chatInput.schedule.Execute(() =>
        {
            var guiEvent = new Event
            {
                type = EventType.MouseUp,
                button = 0,
                mousePosition = actionButton.worldBound.center
            };
            using var ptrUp = PointerUpEvent.GetPooled(guiEvent);
            actionButton.SendEvent(ptrUp);
        });
    }
}

} // namespace AIAssistantExtensions
