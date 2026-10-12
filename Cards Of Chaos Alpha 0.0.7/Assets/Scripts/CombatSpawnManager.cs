using System.Diagnostics;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.InputSystem;
using Debug = UnityEngine.Debug;

public class CombatSpawnManager : MonoBehaviour
{
    [SerializeField] Transform[] spawnPoints;

    void Start()
    {
        Debug.Log("COMBAT SPAWN MANAGER SE EJECUTA");
        foreach (PlayerInput player in PlayerInput.all)
        {
            int playerID = player.playerIndex;

            if(playerID < 0 ||  playerID >= spawnPoints.Length)
            {
                continue;
            }

            if(spawnPoints[playerID] == null)
            {
                continue;
            }

            player.transform.position = spawnPoints[playerID].position;

            PlayerSkinManager skinManager = player.GetComponent<PlayerSkinManager>();

            if(skinManager != null)
            {
                skinManager.SetVisible(true);
            }


            PlayerController playerController = player.GetComponent<PlayerController>();

            if (playerController != null) 
            {
                playerController.RefreshStats();
                playerController.ResetForNewRound();
                playerController.canShoot = true;
            }
        }
    }
}
