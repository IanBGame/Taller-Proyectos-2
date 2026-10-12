using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using TMPro;

public class CombatManager : MonoBehaviour
{
    public static CombatManager instance;

    [SerializeField] private int totalWin = 2;
    [SerializeField] private float endRoundDelay = 2f;

    [SerializeField] private string shopScene = "Shop";
    [SerializeField] private string menuScene = "Menu";

    [SerializeField] private TMP_Text winnerText;

    private bool roundFinished = false;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        if (winnerText != null)
        {
            winnerText.text = "";
        }
    }

    public void PlayerDied(int playerID)
    {
        if (roundFinished)
        {
            return;
        }

        int aliveCount = 0;
        int winnerID = -1;

        foreach (PlayerInput player in PlayerInput.all)
        {
            PlayerController controller = player.GetComponent<PlayerController>();

            if (controller != null && controller.isAlive)
            {
                aliveCount++;
                winnerID = controller.playerID;
            }

        }

        if (aliveCount <= 1)
        {
            roundFinished = true;

            StartCoroutine(EndRound(winnerID));
        }

    }

    private IEnumerator EndRound(int winnerID)
    {
        if (winnerID == -1)
        {
            ShowMessage("¡EMPATE!");

            yield return new WaitForSeconds(endRoundDelay);

            SceneManager.LoadScene(shopScene);
            yield break;
        }

        PlayerStats winner = GameManager.instance.GetPlayerStats(winnerID);

        if (winner == null)
        {
            yield break;
        }

        winner.roundWins++;

        if (winner.roundWins >= totalWin)
        {
            ShowMessage("¡JUGADOR " + (winnerID + 1) + " GANA LA PARTIDA!");

            yield return new WaitForSeconds(endRoundDelay);

            yield return StartCoroutine(ResetMatch());

            SceneManager.LoadScene(menuScene);
        }
        else
        {
            ShowMessage("¡JUGADOR " + (winnerID + 1) + " GANA LA RONDA!\n" + winner.roundWins + "/" + totalWin + " VICTORIAS");

            yield return new WaitForSeconds(endRoundDelay);

            SceneManager.LoadScene(shopScene);
        }



    }

    private void ShowMessage(string message)
    {
        if (winnerText != null)
        {
            winnerText.text = message;
        }
    }

    private IEnumerator ResetMatch()
    {
        // Copiamos los jugadores antes de destruirlos
        List<PlayerInput> players = new List<PlayerInput>(PlayerInput.all);

        foreach (PlayerInput player in players)
        {
            player.DeactivateInput();
            Destroy(player.gameObject);
        }

        // Esperar a que Unity destruya los objetos
        yield return null;

        // El GameManager se mantiene, pero vaciamos los datos
        if (GameManager.instance != null)
        {
            GameManager.instance.playerList.Clear();
        }
    }

    private void OnDestroy()
    {
        if(instance == this)
        {
            instance = null;
        }
    }
}
