using System.Diagnostics;
using UnityEngine;
using UnityEngine.InputSystem;
using Debug = UnityEngine.Debug;

public class PlayerJoinManager : MonoBehaviour
{
    public void OnPlayerJoined(PlayerInput playerInput)
    {
        Debug.Log("Se ha unido el jugador: " + playerInput.playerIndex);
        PlayerController playerController = playerInput.GetComponent<PlayerController>();

        if (playerController == null) 
        {
            Debug.Log("NO HAY PLAYERCONTROLLER");
            return;
        }

        int playerID = playerInput.playerIndex;

        playerController.playerID = playerID;

        if (GameManager.instance == null) 
        {
            Debug.Log("NO HAY GAMEMANAGER");
            return;
        }

        PlayerStats playerStats = GameManager.instance.GetPlayerStats(playerID);

        if (playerStats == null) 
        {
            Debug.Log("HAY PLAYERSTATS");
            playerStats = new PlayerStats();

            playerStats.playerID = playerID;
            playerStats.skinID = playerID % 4;

            GameManager.instance.playerList.Add(playerStats);
        }
        Debug.Log("STATS LISTAS");

        playerController.LoadPlayerStats();

        Debug.Log("LOAD PLAYER EJECUTADO");
    }
}
