using UnityEngine;

public class AdvancedCardScript : MonoBehaviour
{
    private CardType cardType;

    public SpriteRenderer spriteRenderer;

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

    public void setSprites()
    {
        if (spriteRenderer == null)
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


                break;

            case CardType.boo:

                break;
            case CardType.confetti:

                break;
            case CardType.gambling:

                break;
            case CardType.whereAmI:

                break;
            case CardType.dash:

                break;
            case CardType.beam:

                break;
            case CardType.rubberRounds:

                break;
            case CardType.spreadShot:

                break;
            case CardType.pierce:

                break;
            case CardType.closeUpMagic:

                break;
            case CardType.explodingRounds:

                break;
            case CardType.heal:

                break;
            case CardType.stasis:

                break;
            case CardType.kaboom:

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

        ShopManager shopManager = FindFirstObjectByType<ShopManager>();

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

        ApplyCard(target.playerID);

        target.RefreshStats();

        shopManager.PlayerTookCard(target.playerID);
    }

    public void ApplyCard(int playerID)
    {

    }
}