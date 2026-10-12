using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using Debug = UnityEngine.Debug;

public class PlayerJoinManager : MonoBehaviour
{
    [SerializeField] private int minPlayers = 2;
    [SerializeField] private string gameScene = "Game";

    [SerializeField] private PlayerInputManager inputManager;
    [SerializeField] private MainMenuManager menuManager;

    private bool gameStarted = false;

    private Dictionary<PlayerInput, InputAction> startActions = new Dictionary<PlayerInput, InputAction>();

    public void OnPlayerJoined(PlayerInput playerInput)
    {
        Debug.Log("Se ha unido el jugador: " + playerInput.playerIndex);

        PlayerController playerController = playerInput.GetComponent<PlayerController>();

        if (playerController == null)
        {
            Debug.LogError("No existe PlayerController");
            return;
        }

        if (GameManager.instance == null)
        {
            Debug.LogError("No existe GameManager");
            return;
        }

        // Asignar ID
        int playerID = playerInput.playerIndex;
        playerController.playerID = playerID;

        // Buscar estadísticas
        PlayerStats stats =
            GameManager.instance.GetPlayerStats(playerID);

        if (stats == null)
        {
            stats = new PlayerStats();

            stats.playerID = playerID;
            stats.skinID = playerID % 4;

            GameManager.instance.playerList.Add(stats);
        }

        // Conservar el jugador y su mando
        DontDestroyOnLoad(playerInput.gameObject);

        // Cargar estadísticas y skin
        playerController.LoadPlayerStats();

        playerController.canShoot = false;

        PlayerSkinManager skinManager = playerInput.GetComponent<PlayerSkinManager>();

        if (skinManager != null)
        {
            skinManager.SetVisible(false);
        }
        // Desactivar movimiento durante el menú
        playerController.enabled = false;

        Rigidbody2D rb =
            playerInput.GetComponent<Rigidbody2D>();

        if (rb != null)
        {
            rb.linearVelocity = Vector2.zero;
            rb.simulated = false;
        }

        // Obtener la acción StartGame de este jugador
        InputAction startAction =
            playerInput.actions.FindAction("StartGame");

        if (startAction == null)
        {
            Debug.LogError("No existe la acción StartGame");
            return;
        }

        // Esperar a que termine la pulsación de unión
        StartCoroutine(EnableStartAfterRelease(
            playerInput,
            startAction
        ));

        if (menuManager != null)
        {
            menuManager.RefreshPlayers();
        }

        Debug.Log("Jugadores conectados: " + PlayerInput.all.Count);

        Debug.Log(
    "Jugador " + playerInput.playerIndex +
    " - Dispositivos asignados: " + playerInput.devices.Count
);

        foreach (InputDevice device in playerInput.devices)
        {
            Debug.Log(
                "Jugador " + playerInput.playerIndex +
                " - Mando: " + device.displayName +
                " - ID dispositivo: " + device.deviceId
            );
        }
    }

    private IEnumerator EnableStartAfterRelease(
        PlayerInput playerInput,
        InputAction startAction)
    {
        // Evitar que el botón usado para unirse
        // inicie también la partida
        yield return new WaitWhile(() => startAction.IsPressed());

        yield return null;

        startActions[playerInput] = startAction;

        startAction.performed += OnStartGame;
    }

    private void OnStartGame(InputAction.CallbackContext context)
    {
        if (gameStarted)
            return;

        if (PlayerInput.all.Count < minPlayers)
        {
            Debug.Log("Se necesitan al menos 2 jugadores");
            return;
        }

        StartGame();
    }

    private void StartGame()
    {
        gameStarted = true;

        Debug.Log("Comenzando partida");

        if (inputManager != null)
        {
            inputManager.DisableJoining();
        }

        // Desconectar los eventos del menú
        foreach (InputAction action in startActions.Values)
        {
            action.performed -= OnStartGame;
        }

        startActions.Clear();

        SceneManager.LoadScene(gameScene);
    }

    private void OnDestroy()
    {
        foreach (InputAction action in startActions.Values)
        {
            action.performed -= OnStartGame;
        }

        startActions.Clear();
    }
}