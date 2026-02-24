# Project Overview: UnityAI-Test

## 1. Project Description
This project, **UnityAI-Test**, appears to be a lightweight technical prototype or bootstrap environment built on Unity 6000.3. It is configured for 2D rendering using the Universal Render Pipeline (URP) and integrates modern Unity AI and Assistant packages. The core focus seems to be providing a clean slate for AI-driven development or testing AI-assistant extensions within a specialized UI environment using Unity's UI Toolkit.

## 2. Gameplay Flow / User Loop
As a technical prototype, the current "loop" is restricted to application initialization:
1.  **Boot**: The application launches into the `Main.unity` scene.
2.  **Initialization**: The Universal Render Pipeline (2D) initializes the rendering context.
3.  **UI Rendering**: The `UIDocument` component on the `UI` GameObject loads `Main.uxml`.
4.  **Display**: A static UI panel (defined in `DefaultPanel.asset`) is rendered to the screen displaying a placeholder label.
5.  **Interaction**: Currently, there is no active gameplay logic; the project serves as a base for further development.

## 3. Architecture
The project follows a standard decoupled Unity architecture, separating rendering configurations from UI and logic.
*   **Entry Point**: The `Main.unity` scene serves as the single entry point.
*   **Rendering Architecture**: Uses URP with a 2D Renderer. Post-processing and environmental effects are managed via a Global Volume.
*   **UI Architecture**: Built entirely on UI Toolkit (UITK) rather than UGUI. It uses `PanelSettings` for global UI scaling and `VisualTreeAsset` (UXML) for layout description.
*   **AI Integration**: The project includes extensive AI-related packages (AI Assistant, AI Inference, AI Generators), suggesting an architecture designed to interface with LLMs or AI-driven generation tools.

## 4. Game Systems & Domain Concepts

### Rendering System (URP 2D)
The project is configured for a 2D-specific graphical pipeline.
*   `UniversalRenderPipelineAsset`: Configures the global quality and performance settings.
*   `Renderer2DData`: Defines how 2D-specific features like 2D Lights or Sorting Layers are handled.
*   `VolumeProfile`: Manages global post-processing effects (Bloom, Color Grading, etc.).
*   `Location:` `Assets/URP/`

### UI System (UITK)
The project utilizes the modern UI Toolkit framework for its interface.
*   `UIDocument`: The bridge component that links the UXML layout to the scene.
*   `VisualTreeAsset (Main.uxml)`: Defines the hierarchical structure of the UI elements.
*   `PanelSettings`: Governs how the UI is projected, scaled, and rendered.
*   `ThemeStyleSheet`: Applies visual styling to the UI components.
*   `Location:` `Assets/UI/`

## 5. Scene Overview
*   **Main**: The primary scene (`Assets/Main.unity`). It contains a basic `Camera` setup (Camera, Transform, AudioListener) and a `UI` GameObject. The camera is configured for URP 2D rendering. This scene acts as a sandbox for the UI and AI components.

## 6. UI System
The UI is built using the **UI Toolkit**.
*   **Structure**: The main layout is defined in `Main.uxml`.
*   **Styling**: Uses `DefaultTheme.tss` and inline styles within the UXML.
*   **Rendering**: Managed by the `UIDocument` component in the `Main` scene.
*   **Binding**: Currently, the UI contains a placeholder label. To add functionality, a `MonoBehaviour` script would typically query the `UIDocument.rootVisualElement` and register event callbacks.
*   `Location:` `Assets/UI/`

## 7. Asset & Data Model
*   **Render Pipeline Settings**: ScriptableObjects in `Assets/URP/` store all graphical configurations.
*   **UI Templates**: UXML and TSS files in `Assets/UI/` define the interface metadata.
*   **Text Assets**: `Prompt.txt` exists at the root, likely serving as a data source or configuration for the AI Assistant packages.
*   **Naming Convention**: Simple, descriptive naming (e.g., `DefaultPanel`, `Main`).

## 8. Project Structure
The project is organized into logical functional folders:
*   `Assets/UI/`: Contains all UI Toolkit assets (UXML, TSS, PanelSettings).
*   `Assets/URP/`: Contains URP configuration assets and 2D Renderer data.
*   `Assets/`: Root contains the main scene and global text files.

## 9. Notes, Caveats & Gotchas
*   **Missing Scripts**: There are currently no custom C# scripts in the `Assets` folder. Logic is likely intended to be driven by the integrated AI packages or is yet to be implemented.
*   **Platform Target**: The project is currently configured for `StandaloneOSX`.
*   **Unity 6 Features**: The project uses Unity 6 (6000.3), which includes new UI Toolkit features and URP 2D enhancements; ensure compatible versions of external tools are used.
*   **Input**: The project still uses the `Legacy Input Manager` despite the modern UI/AI setup.