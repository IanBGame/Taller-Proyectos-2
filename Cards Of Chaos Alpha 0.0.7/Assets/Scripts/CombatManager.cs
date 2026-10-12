using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using System.Collections;
using System;

public class CombatManager : MonoBehaviour
{
    public static CombatManager instance;

    [SerializeField] private float endRoundDelay = 2f;
    [SerializeField] private string shopScene = "Shop";

    private bool roundFinished = false;

    private void Awake()
    {
        instance = this;
    }

    public void PlayerDied(int playerID)
    {
        if(roundFinished)
        {
            return;
        }

        int aliveCount = 0;
        int winnerID =- 1;

        foreach(PlayerInput player in PlayerInput.all)
        {
            PlayerController controller = player.GetComponent<PlayerController>();

            if(controller != null && controller.isAlive)
            {
                aliveCount++;
                winnerID = controller.playerID;
            }

        }

        if(aliveCount <= 1)
        {
            roundFinished = true;

            StartCoroutine(EndRound(winnerID));
        }

    }

    private IEnumerator EndRound(int winnerID)
    {
        if(winnerID >= 0)
        {
            PlayerStats winner = GameManager.instance.GetPlayerStats(winnerID);

            if (winner != null)
            {
                winner.roundWins++;
            }
        }


        yield return new WaitForSeconds(endRoundDelay);

        SceneManager.LoadScene(shopScene);
    }

}
