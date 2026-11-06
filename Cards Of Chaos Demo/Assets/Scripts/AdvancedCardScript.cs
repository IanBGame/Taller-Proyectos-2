using UnityEngine;

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
            case CardType.rubberRounds:          //Por implementar

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

        ApplyCard(target.playerID, other);

        target.RefreshStats();

        shopManager.PlayerTookCard(target.playerID);
    }

    public void ApplyCard(int playerID, Collider2D other)
    {
        PlayerController target = other.GetComponent<PlayerController>();

        if (GameManager.instance == null)
        {
            return;
        }

        PlayerStats playerStats = GameManager.instance.GetPlayerStats(playerID);

        if (playerStats == null)
        {
            return;
        }


        switch (cardType)
        {
            case CardType.iceRounds:


                break;

            case CardType.sugarRush:


                break;

            case CardType.snailTrail:


                break;

            case CardType.tiny:

                playerStats.hasTiny = true;

                break;

            case CardType.boo:

                break;
            case CardType.confetti:

                break;
            case CardType.gambling:

                break;
            case CardType.whereAmI:

                playerStats.hasWhereAmI = true;

                break;
            case CardType.dash:

                break;
            case CardType.beam:

                break;
            case CardType.rubberRounds:

                break;
            case CardType.spreadShot:

                playerStats.hasSpreadShot = true;

                break;
            case CardType.pierce:

                playerStats.hasPierce = true;
                playerStats.shootTime = Mathf.Max(0.05f, playerStats.shootTime + fireRateDecrease / 2);

                break;
            case CardType.closeUpMagic:

                break;
            case CardType.explodingRounds:

                break;
            case CardType.heal:

                playerStats.hasHeal = true;
                break;

            case CardType.stasis:

                break;
            case CardType.kaboom:

                break;
        }
    }
}