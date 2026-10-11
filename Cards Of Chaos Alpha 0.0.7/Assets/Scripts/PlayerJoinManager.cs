using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerJoinManager : MonoBehaviour
{
    public void OnPlayerJoined(PlayerInput playerInput)
    {
        PlayerController playerController = playerInput.GetComponent<PlayerController>();

        if (playerController == null) 
        {
            return;
        }

        int playerID = playerInput.playerIndex;

        playerController.playerID = playerID;

        if (GameManager.instance == null) 
        {
            return;
        }

        PlayerStats playerStats = GameManager.instance.GetPlayerStats(playerID);

        if (playerStats == null) 
        {
            playerStats = new PlayerStats();

            playerStats.playerID = playerID;
            playerStats.skinID = playerID % 4;

            GameManager.instance.playerList.Add(playerStats);
        }

        playerController.LoadPlayerStats();
    }
}
