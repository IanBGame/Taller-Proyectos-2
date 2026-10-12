using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public List<PlayerStats> playerList = new List<PlayerStats>();

    void Awake()
    {
        if(instance == null)
        {
            instance = this;

            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public PlayerStats GetPlayerStats(int playerID)
    {
        return playerList.Find(player => player.playerID == playerID);
        
    }
}
