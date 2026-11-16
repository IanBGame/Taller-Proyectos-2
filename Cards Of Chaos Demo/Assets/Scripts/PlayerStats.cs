using UnityEngine;

[System.Serializable]

public class PlayerStats
{
    public int playerID;
    public int roundWins = 0;

    public float playerSpeed = 5f;
    public float Health = 3f;
    public float shootTime = 1f;
    public float shootRange = 3f;
    public float projectileSize = 1f;
    public float playerSize = 0.828202f;

    public int skinID;
    public bool hasIceRounds = false;
    public bool hasSugarRush = false;
    public bool hasSnailTrail = false;
    public bool hasTiny = false;
    public bool hasBoo = false;
    public bool hasConfetti = false;
    public bool hasGambling = false;
    public bool hasWhereAmI = false;
    public bool hasDash = false;
    public bool hasBeam = false;
    public bool hasRubberRounds = false;
    public bool hasSpreadShot = false;
    public bool hasPierce = false;
    public bool hasCloseUpMagic = false;
    public bool hasExplodingRounds = false;
    public bool hasHeal = false;
    public bool hasStasis = false;
    public bool hasKaboom = false;

    public void ApplyTiny(PlayerController player)
    {
        CircleCollider2D collider = player.GetComponent<CircleCollider2D>();
        collider.radius = playerSize / 3 * 2;
    }

    public bool HasAdvancedCard(AdvancedCardScript.CardType type)
    {
        switch(type)
        {
            case AdvancedCardScript.CardType.iceRounds:
                return hasIceRounds;

            case AdvancedCardScript.CardType.sugarRush:
                return hasSugarRush;

            case AdvancedCardScript.CardType.snailTrail:
                return hasSnailTrail;

            case AdvancedCardScript.CardType.tiny:
                return hasTiny;

            case AdvancedCardScript.CardType.boo:
                return hasBoo;

            case AdvancedCardScript.CardType.confetti:
                return hasConfetti;

            case AdvancedCardScript.CardType.whereAmI:
                return hasWhereAmI;

            case AdvancedCardScript.CardType.dash:
                return hasDash;

            case AdvancedCardScript.CardType.beam:
                return hasBeam;

            case AdvancedCardScript.CardType.rubberRounds:
                return hasRubberRounds;

            case AdvancedCardScript.CardType.spreadShot:
                return hasSpreadShot;

            case AdvancedCardScript.CardType.pierce:
                return hasPierce;

            case AdvancedCardScript.CardType.closeUpMagic:
                return hasCloseUpMagic;

            case AdvancedCardScript.CardType.explodingRounds:
                return hasExplodingRounds;

            case AdvancedCardScript.CardType.heal:
                return hasHeal;

            case AdvancedCardScript.CardType.stasis:
                return hasStasis;

            case AdvancedCardScript.CardType.kaboom:
                return hasKaboom;

            default:
                return false;
        }
    }
}
