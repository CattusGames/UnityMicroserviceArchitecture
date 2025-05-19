Documentation Link:
https://docs.google.com/document/d/1-_p0Hw8RbFdXGZXAo7WYQjFjh4ls4jopwjc4U4zrnFw/edit?usp=sharing

Documentation

A small (50px) circle is displayed on the scene. When you click anywhere on the screen,
the object moves to the click location. With several consecutive clicks, the object moves in turn along all the clicks that
occur. In the upper right corner, a slider is displayed to adjust the movement speed
Starting the game through the Initialize scene.

1. Entry Point – BootstrapInstaller. This is the entry point of the application through the ProjectContext. Here we bind all our
    services and the state machine. After that, enter the Bootstrap state.
2. The game's state machine – GameStateMachine. The IStateMachine<T> interface manages transitions between IGameState states. First, Bootstrap state is launched, where we start the initialization logic of the game: we load the game scene. We load the scene through CoroutineRunner, because Addressables.LoadSceneAsync() returns an AsyncOperationHandle that is reproduced in the unity main thread and requires an update loop for progress. After loading, LoadGame state is launched, where we directly create the UI, the player and initialize it.
3. Services
   TouchInput : ITouchInput, ITickable Processes input via InputSystem. Calls OnScreenTouched only when the tap is not on the UI.
   SceneLoader : ISceneLoader Loads the scene via Addressables, uses ICoroutineRunner
   AssetProvider : IAssetProvider Provides centralized access to resources stored in Addressables (HUD, player). Encapsulates the logic of Addressables, it is possible to ensure the reuse of already loaded resources.
   GameFactory : IGameFactory Responsible for creating a 50px player via FitSpriteRendererSize. Also creates a HUD. All prefabs are created through Addressables.
4.Game Logic
   MovementController : MonoBehaviour Inject ITouchInput, subscribes to OnScreenTouched, saves a queue of Queue<Vector3> points and moves between them. The speed is regulated by the _speed field and also through a slider that works as a speed multiplier.
6. Other components
   FitSpriteRendererSize.SetObjectSizeInPixels Scales the SpriteRenderer object so that it occupies exactly N pixels on the screen.
   CoroutineRunner : MonoBehaviour, ICoroutineRunner It is not destroyed between scenes, it is used to run Addressables and scenes.
