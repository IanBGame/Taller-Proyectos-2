using System.ComponentModel;
using UnityEngine;
using Random = UnityEngine.Random;
using System.Collections.Generic;

public class AdvancedCardScript : MonoBehaviour
{
    private CardType cardType;

    [Header("Referencias Visuales")]
    public SpriteRenderer spriteRenderer;
    public Sprite spriteTiny;
    public Sprite spriteHeal;
    public Sprite spritePierce;
    public Sprite spriteSpreadShot;
    public Sprite spriteWhereAmI; 
    public Sprite spriteRubberRounds; 

    [SerializeField] private float fireRateDecrease = 0.05f;


    public enum CardType
    {
        iceRounds,
        sugarRush,
        snailTrail,
        tiny,
        boo,
        confetti,
        gambling,
        whereAmI,
        dash,
        beam,
        rubberRounds,
        spreadShot,
        pierce,
        closeUpMagic,
        explodingRounds,
        heal,
        stasis,
        kaboom
    }

    public void SetCardType(CardType type)
    {
        cardType = type;
    }

    public void setSprites()
    {
        if (spriteRenderer == null) { 
        return;}

        switch (cardType)
        {
            case CardType.iceRounds:            //Por implementar

                break;

            case CardType.sugarRush:             //Por implementar

                break;

            case CardType.snailTrail:            //Por implementar

                break;

            case CardType.tiny:

                if (spriteTiny != null)
                { spriteRenderer.sprite = spriteTiny; }
                else Debug.LogWarning("No hay Sprites en la carta: Tiny (Adv)");

                break;

            case CardType.boo:                   //Por implementar

                break;
            case CardType.confetti:              //Por implementar

                break;
            case CardType.gambling:              //Por implementar

                break;
            case CardType.whereAmI:
                if (spriteWhereAmI != null)
                { spriteRenderer.sprite = spriteWhereAmI; }
                else Debug.LogWarning("No hay Sprites en la carta: Where am I (Adv)");

                break;
            case CardType.dash:                    //Por implementar

                break;
            case CardType.beam:                  //Por implementar

                break;
            case CardType.rubberRounds:          
                if (spriteRubberRounds != null)
                { spriteRenderer.sprite = spriteRubberRounds; }
                else Debug.LogWarning("No hay Sprites en la carta: Rubber Rounds (Adv)");

                break;
            case CardType.spreadShot:

                if (spriteSpreadShot != null)
                { spriteRenderer.sprite = spriteSpreadShot; }
                else Debug.LogWarning("No hay Sprites en la carta: SpreadShot (Adv)");

                break;
            case CardType.pierce:

                if (spritePierce != null)
                { spriteRenderer.sprite = spritePierce; }
                else Debug.LogWarning("No hay Sprites en la carta: Pierce (Adv)");

                break;
            case CardType.closeUpMagic:                 //Por implementar

                break;
            case CardType.explodingRounds:               //Por implementar

                break;
            case CardType.heal:
                spriteRenderer.sprite = spriteHeal;
                break;
            case CardType.stasis:                        //Por implementar

                break;
            case CardType.kaboom:                         //Por implementar

                break;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        PlayerController target = other.GetComponent<PlayerController>();
        if (target == null)
        {
            return;
        }

        ShopManager shopManager = FindAnyObjectByType<ShopManager>();

        if (shopManager == null)
        {
            return;
        }

        if (!shopManager.CanTakeCard(target.playerID))
        {
            return;
        }


        if (GameManager.instance == null || GameManager.instance.GetPlayerStats(target.playerID) == null)
        {
            return;
        }
        
        PlayerStats stats = GameManager.instance.GetPlayerStats(target.playerID);

        if (stats == null)
        {
            return;
        }

        bool applied = TryApplyCard(stats, target);

        if(!applied)
        {
            return;
        }

        target.RefreshStats();

        Destroy(gameObject);

        shopManager.PlayerTookCard(target.playerID);
    }

    private bool TryApplyCard(PlayerStats stats,PlayerController target)
    {
        if (cardType == CardType.gambling)
        {
            return GiveRandomPower(stats, target);
        }

        return ApplyCard(cardType, stats, target);
    }


    private bool ApplyCard(CardType type, PlayerStats playerStats, PlayerController target)
    {
        // No permitir habilidades avanzadas repetidas
        if (playerStats.HasAdvancedCard(type))
        {
            return false;
        }

        switch (type)
        {
            case CardType.tiny:
                playerStats.hasTiny = true;
                playerStats.ApplyTiny(target);
                break;

            case CardType.spreadShot:
                playerStats.hasSpreadShot = true;
                break;

            case CardType.pierce:
                playerStats.hasPierce = true;
                playerStats.shootTime = Mathf.Max(
                    0.05f,
                    playerStats.shootTime + fireRateDecrease / 2f
                );
                break;

            case CardType.heal:
                playerStats.hasHeal = true;
                break;
                
            case CardType.whereAmI:
                playerStats.hasWhereAmI = true;
                break;
            
            case CardType.rubberRounds:
                playerStats.hasRubberRounds = true;
                break;

            default:
                // Carta todavía sin implementar
                return false;
        }

        return true;
    }


private bool GiveRandomPower(PlayerStats stats, PlayerController target)
    {
        List<BasicCardScript.CardType> basicCards = new List<BasicCardScript.CardType>
        {
           BasicCardScript.CardType.HPUp,
           BasicCardScript.CardType.SpeedUp,
           BasicCardScript.CardType.FireRateUp,
           BasicCardScript.CardType.ProjectileSizeUp,
           BasicCardScript.CardType.RangeUp
        };

        List<CardType> advancedCards = new List<CardType>//AŃADIR AQUI LAS CARTAS QUE PONGAMOS
        {
        CardType.tiny,
        CardType.spreadShot,
        CardType.pierce,
        CardType.heal,
        CardType.whereAmI
        };

        advancedCards.RemoveAll(type => stats.HasAdvancedCard(type));

        int totalCards = basicCards.Count + advancedCards.Count;

        int randomIndex = Random.Range(0, totalCards);

        if(randomIndex < basicCards.Count)
        {
            BasicCardScript.CardType selectedBasic = basicCards[randomIndex];

            ApplyRandomBasic(selectedBasic, stats);

            return true;
        }

        else
        {
            int advancedIndex = randomIndex - basicCards.Count;

            CardType selectedAdvanced = advancedCards[advancedIndex];

            return ApplyCard(selectedAdvanced, stats, target);
        }
    }

    private void ApplyRandomBasic(BasicCardScript.CardType type, PlayerStats stats)
    {
        switch(type)
        {
            case BasicCardScript.CardType.HPUp:
                stats.Health += 1f;
                break;

            case BasicCardScript.CardType.SpeedUp:
                stats.playerSpeed += 1f;
                break;

            case BasicCardScript.CardType.FireRateUp:
                stats.shootTime = Mathf.Max(0.05f, stats.shootTime - 0.05f);
                break;

            case BasicCardScript.CardType.ProjectileSizeUp:
                stats.projectileSize += 0.2f;
                break;

            case BasicCardScript.CardType.RangeUp:
                stats.shootRange += 0.5f;
                break;
        }
    }
}