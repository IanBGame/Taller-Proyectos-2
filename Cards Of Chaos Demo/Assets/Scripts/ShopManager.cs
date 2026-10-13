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
    [SerializeField] private GameObject basicCardPrefab;
    [SerializeField] private GameObject advancedCardPrefab;
    [SerializeField] private Transform[] cardSpawnPoints;

    [SerializeField] private float shopDuration = 20f;
    [SerializeField] private string gameScene = "Game";

    //Chances
    [Range(0f, 1f)]
    [SerializeField] private float advancedCardChance = 0.3f;

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
        List<BasicCardScript.CardType> availableBasicCards =
            new List<BasicCardScript.CardType>
        {
        BasicCardScript.CardType.HPUp,
        BasicCardScript.CardType.SpeedUp,
        BasicCardScript.CardType.FireRateUp,
        BasicCardScript.CardType.ProjectileSizeUp,
        BasicCardScript.CardType.RangeUp
        };

        List<AdvancedCardScript.CardType> availableAdvancedCards = new List<AdvancedCardScript.CardType>
        {
        AdvancedCardScript.CardType.iceRounds,
        AdvancedCardScript.CardType.sugarRush,
        AdvancedCardScript.CardType.snailTrail,
        AdvancedCardScript.CardType.tiny,
        AdvancedCardScript.CardType.boo,
        AdvancedCardScript.CardType.confetti,
        AdvancedCardScript.CardType.gambling,
        AdvancedCardScript.CardType.whereAmI,
        AdvancedCardScript.CardType.dash,
        AdvancedCardScript.CardType.beam,
        AdvancedCardScript.CardType.rubberRounds,
        AdvancedCardScript.CardType.spreadShot,
        AdvancedCardScript.CardType.pierce,
        AdvancedCardScript.CardType.closeUpMagic,
        AdvancedCardScript.CardType.explodingRounds,
        AdvancedCardScript.CardType.heal,
        AdvancedCardScript.CardType.stasis,
        AdvancedCardScript.CardType.kaboom
        };

        foreach (Transform spawnPoint in cardSpawnPoints)
        {
            if (spawnPoint != null)
            {
                return;
            }

            bool generateAdvanced = Random.value < advancedCardChance;


            if (availableBasicCards.Count == 0)
            {
                generateAdvanced = false;
            }

            if (availableAdvancedCards.Count == 0)
            {
                generateAdvanced = true;
            }

            if (availableBasicCards.Count == 0 && availableAdvancedCards.Count == 0)
            {
                break;
            }

            if(generateAdvanced)
            {
                int randomIndex = Random.Range(0, availableBasicCards.Count);

                AdvancedCardScript.CardType selectedCard = availableAdvancedCards[randomIndex];

                availableAdvancedCards.RemoveAt(randomIndex);

                GameObject card = Instantiate(advancedCardPrefab, spawnPoint.position, Quaternion.identity);

                AdvancedCardScript cardScript = card.GetComponent<AdvancedCardScript>();

                if(cardScript != null)
                {
                    cardScript.SetCardType(selectedCard);
                }
            }
            else
            {
                int randomIndex = Random.Range(0, availableBasicCards.Count);

                BasicCardScript.CardType selectedCard = availableBasicCards[randomIndex];

                availableBasicCards.RemoveAt(randomIndex);

                GameObject card = Instantiate(basicCardPrefab, spawnPoint.position, Quaternion.identity);

                BasicCardScript cardScript = card.GetComponent<BasicCardScript>();

                if (cardScript != null)
                {
                    cardScript.SetCardType(selectedCard);
                    cardScript.setSprites();
                }
            }
        }
    }
}
