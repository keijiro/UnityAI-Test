This repository contains a test bed project for Unity AI 2026 Beta.

## About macOS IME patch

The Unity AI Assistant window (v1.5.0-pre2) has an issue in Japanese text input
that incorrectly detects the return key being pressed while Japanese text
composition.

The patch file `mac-ime-fix.patch` contains a hotfix for this issue. You can
apply it with the following steps:

1. Clone the Unity AI Assistant package by pressing the "Customize" button in
   the Package Manager window.
2. Apply the patch from the command line: `patch < mac-ime-fix.patch`

I've already reported this issue to the development team. You can use this
patch as a workaround until it gets fixed.
