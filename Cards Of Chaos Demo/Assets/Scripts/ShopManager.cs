using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Security.Cryptography;
using UnityEngine;
using Random = UnityEngine.Random;
using Debug = UnityEngine.Debug;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class ShopManager : MonoBehaviour
{
    [SerializeField] private GameObject cardPrefab;
    [SerializeField] private Transform[] cardSpawnPoints;

    [SerializeField] private float shopDuration = 20f;
    [SerializeField] private string gameScene = "Game";

    private HashSet<int> playersWithCard = new HashSet<int>();

    private float remainingShopTime;
    private bool shopOver;

    private void Start()
    {
        GenerateCards();

        remainingShopTime = shopDuration;
    }

    private void Update()
    {
        if(shopOver)
        {
            return;
        }

        remainingShopTime -= Time.deltaTime;

        if (remainingShopTime <= 0f)
        {
            EndShop();
        }
    }

    public bool CanTakeCard(int playerID)
    {
        return !shopOver && !playersWithCard.Contains(playerID);
    }

    public void PlayerTookCard(int playerID)
    {
        playersWithCard.Add(playerID);

        Debug.Log("Jugador " + playerID + " ha elegido carta");

        if (playersWithCard.Count >= PlayerInput.all.Count)
        {
            Debug.Log("Todos han elegido");

            EndShop();
        }
    }

    private void EndShop()
    {
        if (shopOver)
            return;

        shopOver = true;

        SceneManager.LoadScene("Game");
    }

    private void GenerateCards()
    {
        List<BasicCardScript.CardType> availableCards =
            new List<BasicCardScript.CardType>
        {
        BasicCardScript.CardType.HPUp,
        BasicCardScript.CardType.SpeedUp,
        BasicCardScript.CardType.FireRateUp,
        BasicCardScript.CardType.ProjectileSizeUp,
        BasicCardScript.CardType.RangeUp
        };

        foreach (Transform spawnPoint in cardSpawnPoints)
        {
            if (availableCards.Count == 0)
            {
                break;
            }

            if (spawnPoint == null || cardPrefab == null)
            {
                continue;
            }   

            int randomIndex = Random.Range(0, availableCards.Count);

            BasicCardScript.CardType selectedCard = availableCards[randomIndex];

            availableCards.RemoveAt(randomIndex);

            GameObject card = Instantiate(cardPrefab, spawnPoint.position, Quaternion.identity);

            BasicCardScript cardScript = card.GetComponent<BasicCardScript>();

            if (cardScript != null)
            {
                cardScript.SetCardType(selectedCard);
                cardScript.setSprites();
            }
        }
    }
}
