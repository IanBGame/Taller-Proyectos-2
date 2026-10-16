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

    public int skinID;

    public bool hasIceRounds;
    public bool hasSugarRush;
    public bool hasSnailTrail;
    public bool hasTiny;
    public bool hasBoo;
    public bool hasConfetti;
    public bool hasGambling;
    public bool hasWhereAmI;
    public bool hasDash;
    public bool hasBeam;
    public bool hasRubberRounds;
    public bool hasSpreadShot;
    public bool hasPierce;
    public bool hasCloseUpMagic;
    public bool hasExplodingRounds;
    public bool hasHeal;
    public bool hasStasis;
    public bool hasKaboom;
}
