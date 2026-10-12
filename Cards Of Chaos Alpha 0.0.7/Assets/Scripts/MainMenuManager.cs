using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] private TMP_Text[] playerTexts;
    [SerializeField] private Button playButton;

    [SerializeField] private PlayerInputManager inputManager;

    [SerializeField] private int minPlayers = 2;
    [SerializeField] private string combatScene = "Game";

    void Start()
    {
        RefreshPlayers();
    }

    public void RefreshPlayers()
    {
        for (int i = 0; i < playerTexts.Length; i++) 
        {
            if (playerTexts[i] != null)
            {
                playerTexts[i].text =
                    "Jugador " + (i + 1);
            }
        }

        foreach (PlayerInput player in PlayerInput.all) 
        {
            int id = player.playerIndex;

            if(playerTexts[id] != null)
            {
                playerTexts[id].text = "Jugador " + (id + 1) + "unido";
            }
        }

        //HACER START


    }

    public void StartGame()
    {
        if (PlayerInput.all.Count < minPlayers)
        {
            return;
        }

        inputManager.DisableJoining();

        SceneManager.LoadScene(combatScene);
    }
}
