using UnityEngine;

[System.Serializable]

public class PlayerStats
{
    public int playerID;
    public int roundWins = 0;

    public float playerSpeed = 5f;
    public float Health = 7f;
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
        collider.radius = 0.828202f / 3 * 2;
    }

}
