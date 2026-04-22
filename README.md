This repository contains a testbed project for Unity AI Assistant.

## About IME issues

The Unity AI Assistant chat window has a few issues with handling IME input,
especially with the Japanese language:

- Incorrectly detects the return key as being pressed while Japanese text is
  being composed. [UUM-134009]
- The default font atlas overflows when the window contains many Japanese
  glyphs. [UUM-133847]
- "ArgumentOutOfRangeException" error is thrown when editing text with IME.
  [UUM-136872]

[UUM-134009]:
  https://issuetracker.unity3d.com/issues/enter-key-submits-prompt-when-confirming-japanese-ime-composition

[UUM-133847]:
  https://issuetracker.unity3d.com/issues/japanese-glyphs-are-missing-when-text-is-overflown

[UUM-136872]:
  https://issuetracker.unity3d.com/issues/argumentoutofrangeexception-is-thrown-when-using-japanese-ime-and-rewriting-the-field-during-composition

These issues can be worked around by using the *IME Proxy* window included in
the custom package for this project (`jp.keijiro.ai.assistant.extensions`).
You can open it from `Window > AI > IME Proxy`. It automatically copies the
contents of its text field to the AI Assistant chat field and sends it to the
server when you press `⌘ + Enter`.

<img width="600" height="425" alt="IME Proxy" src="https://github.com/user-attachments/assets/a6b5146d-1b63-47dd-ab4d-633357996974" />

I've already reported these issues to the development team. You can use this
extension as a workaround until they are fixed.
