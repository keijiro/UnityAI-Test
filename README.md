This repository contains a testbed project for the Unity AI 2026 Beta.

## About macOS Japanese font issue

The Unity AI Assistant window (v2.0.0-pre.1) has a few issues with handling the
Japanese language:

- Incorrectly detects the return key as being pressed while Japanese text is
  being composed.
- The default font atlas overflows when the window contains many Japanese
  glyphs.

The first issue can be worked around by enabling the "Use ⌘Return to send a
prompt" option.

The second issue can also be worked around by dynamically modifying the UI
style with a custom Editor script. The custom package included in this project
(`jp.keijiro.ai.assistant.extensions`) provides a script that applies a proper
Japanese font to the Assistant window.

I've already reported these issues to the development team. You can use this
project as a workaround until they are fixed.
