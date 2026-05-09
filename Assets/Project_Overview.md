# Project Technical Overview: AI Assistant Extensions Test Project

## 1. Project Description
This project serves as a minimal testing and demonstration environment for **Unity 6 (6000.3.14f1)** using the **Universal Render Pipeline (URP)**. Its primary focus is exploring and integrating AI-driven features through the `com.unity.ai.assistant` and `jp.keijiro.ai.assistant.extensions` packages. The project is configured for **Standalone macOS** development and utilizes the modern **UIToolkit** for its interface and the **New Input System** for interaction.

## 2. Gameplay Flow / User Loop
Given the minimal nature of the `Main` scene, the user loop is currently focused on system initialization and UI presentation:
1. **Boot**: The application initializes the Universal Render Pipeline and the New Input System.
2. **Scene Load**: `Main.unity` loads as the entry point.
3. **UI Initialization**: The `UIDocument` on the `UI` GameObject loads `Main.uxml` using `DefaultPanel.asset` settings and `DefaultTheme.tss`.
4. **Interaction**: The user interacts with the UI (currently a placeholder structure) while the URP Camera renders a solid color background, typical of a utility or tool-centric application rather than a traditional game.

## 3. Architecture
The project follows a decoupled, package-based architecture common in modern Unity development:
*   **Rendering**: Managed by URP. The configuration is centralized in `Assets/URP/URP.asset`.
*   **UI Architecture**: Uses **UI Toolkit (UITK)**. Unlike UGUI, this uses a retained-mode approach where the layout (`.uxml`) and styling (`.tss`) are separated from logic.
*   **AI Integration**: Leverages the `Unity.AI.Assistant` and `Unity.AI.Inference` packages. These packages provide the backend for AI interactions and neural network inference within the Unity environment.

## 4. Game Systems & Domain Concepts

### Rendering System
Configured with URP for optimized performance and modern shader support.
*   `UniversalRenderPipelineAsset`: Defines the quality settings and rendering features.
*   `Renderer2DData`: Specifically includes 2D rendering support, even if the primary camera is currently 3D.
`Location: Assets/URP`

### UI Framework
The project uses the UI Toolkit for all interface elements, moving away from the legacy Canvas system.
*   `UIDocument`: The component that links the UXML visual tree to the scene.
*   `PanelSettings`: Defines how the UI is scaled and rendered (e.g., target display, reference resolution).
*   `ThemeStyleSheet`: Controls the visual "skin" of the UI via USS (Unity Style Sheets).
`Location: Assets/UI`

### AI & Inference
While no custom scripts are present in the `Assets` folder, the project is heavily dependent on AI packages.
*   `com.unity.ai.assistant`: Provides the core AI assistant functionality.
*   `com.unity.ai.inference`: Enables running trained models (Sentis/ONNX) within the project.
`Location: Package Cache / Packages`

## 5. Scene Overview
The project contains a single primary scene.
*   **Main.unity**: A minimal setup containing a `Camera` and a `UI` GameObject. The camera is set to `SolidColor` clear flags, suggesting this scene acts as a dashboard or a controlled test environment for the AI extensions.
`Location: Assets/Main.unity`

## 6. UI System
The UI is built entirely using **UI Toolkit**.
*   **Structure**: Defined in `Main.uxml`. Currently, this is a root container for future UI development.
*   **Styling**: Controlled by `DefaultTheme.tss`.
*   **Rendering**: The `UIDocument` on the `UI` GameObject is the entry point for the UI into the scene. To add new screens, one would create new `.uxml` files and swap the `visualTreeAsset` on the `UIDocument` or manage them via script.
`Location: Assets/UI`

## 7. Asset & Data Model
*   **Rendering Data**: Stored as ScriptableObjects in `Assets/URP`.
*   **UI Data**: Visual trees (`.uxml`) and Theme sheets (`.tss`) are stored in `Assets/UI`.
*   **AI Models/Extensions**: Managed via the package manager (`jp.keijiro.ai.assistant.extensions`), which likely provides the core logic and models used by the assistant.

## 8. Notes, Caveats & Gotchas
*   **No Source Code**: There are currently no `.cs` scripts in the `Assets` directory. All logic is either handled by built-in Unity components or provided by imported packages (e.g., `GLTFast` for model loading or `AppUI` for UI utilities).
*   **Unity 6 Requirement**: The project uses version `6000.3.14f1`. Attempting to open this in Unity 2022 or 2021 will likely result in package incompatibilities, especially with the AI Assistant packages.
*   **URP Dependency**: The UI and rendering are strictly tied to URP. Switching to Built-in or HDRP would require a full reconfiguration of the `URP` folder assets.