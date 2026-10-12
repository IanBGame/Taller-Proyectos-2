using System.Diagnostics;
using UnityEngine;
using UnityEngine.InputSystem;
using Debug = UnityEngine.Debug;

public class SpawnPoints : MonoBehaviour
{
    [Header("Puntos de aparición")]
    [SerializeField] private Transform[] spawnPoints;

    private void Start()
    {
        Debug.Log("SpawnPoints iniciado");

        Debug.Log(
            "Jugadores encontrados: " + PlayerInput.all.Count
        );

        foreach (PlayerInput player in PlayerInput.all)
        {
            int playerID = player.playerIndex;

            Debug.Log("Preparando jugador: " + playerID);

            // Comprobar que existe su punto de aparición
            if (playerID < 0 || playerID >= spawnPoints.Length)
            {
                Debug.LogError(
                    "No hay punto de aparición para jugador " + playerID
                );
                continue;
            }

            if (spawnPoints[playerID] == null)
            {
                Debug.LogError("SpawnPoint no asignado");
                continue;
            }

            // Colocar al jugador
            player.transform.position =
                spawnPoints[playerID].position;

            Rigidbody2D rb = player.GetComponent<Rigidbody2D>();

            if (rb != null)
            {
                rb.simulated = true;
                rb.linearVelocity = Vector2.zero;
                rb.angularVelocity = 0f;
            }

            // Volver a mostrar la skin
            PlayerSkinManager skinManager =
                player.GetComponent<PlayerSkinManager>();

            if (skinManager != null)
            {
                skinManager.SetVisible(true);
            }
            else
            {
                Debug.LogError(
                    "Jugador sin PlayerSkinManager: " + playerID
                );
            }

            // Reactivar controlador y restaurar vida
            PlayerController controller = player.GetComponent<PlayerController>();

            if (controller != null)
            {
                controller.RefreshStats();
                controller.DisableShoot();
                controller.enabled = true;
            }
            else
            {
                Debug.LogError(
                    "Jugador sin PlayerController: " + playerID
                );
            }

            Debug.Log("Jugador " + playerID + " preparado");
        }
    }
}