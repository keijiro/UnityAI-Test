This repository contains a testbed project for the Unity AI 2026 Beta.

## About macOS IME patch

The Unity AI Assistant window (v1.5.0-pre2) has a few issues with handling the
Japanese language:

- Incorrectly detects the return key as being pressed while Japanese text is
  being composed.
- The default font atlas overflows when the window contains many Japanese
  glyphs.

The patch file `mac-ja-fix.patch` contains a hotfix for these issues. You can
apply it with the following steps:

1. Clone the Unity AI Assistant package by pressing the "Customize" button in
   the Package Manager window.
2. Apply the patch from the command line: `patch -p1 < mac-ja-fix.patch`

I've already reported these issues to the development team. You can use this
patch as a workaround until they are fixed.
