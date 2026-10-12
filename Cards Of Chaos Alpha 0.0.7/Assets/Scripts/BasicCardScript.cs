using UnityEngine;

public class BasicCardScript : MonoBehaviour
{
    [SerializeField] private CardType cardType;

    [SerializeField] private float hpIncrease = 1f;
    [SerializeField] private float speedIncrease = 2f;
    [SerializeField] private float fireRateDecrease = 0.05f;
    [SerializeField] private float projectileSizeIncrease = 0.2f;
    [SerializeField] private float rangeIncrease = 0.5f;

    public enum CardType
    {
        HPUp,
        SpeedUp,
        FireRateUp,
        ProjectileSizeUp,
        RangeUp
    }

    public void SetCardType(CardType type)
    {
        cardType = type;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        PlayerController target = other.GetComponent<PlayerController>();
        if(target == null)
        {
            return;
        }

        ShopManager shopManager = FindFirstObjectByType<ShopManager>();

        if(shopManager == null)
        {
            return;
        }

        if(!shopManager.CanTakeCard(target.playerID))
        {
            return;
        }


        if(GameManager.instance == null || GameManager.instance.GetPlayerStats(target.playerID) == null)
        {
            return;
        }

        ApplyCard(target.playerID);

        target.RefreshStats();

        shopManager.PlayerTookCard(target.playerID);
    }

    public void ApplyCard(int playerID)
    {
        if(GameManager.instance == null)
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
            case CardType.HPUp:

                playerStats.Health += hpIncrease;
                break;

            case CardType.SpeedUp:

                playerStats.playerSpeed += speedIncrease;
                break;

            case CardType.FireRateUp:

                playerStats.shootTime = Mathf.Max(0.05f, playerStats.shootTime - fireRateDecrease);
                break;

             case CardType.ProjectileSizeUp:

                playerStats.projectileSize += projectileSizeIncrease;
                break;

             case CardType.RangeUp:
                playerStats.shootRange += rangeIncrease;
                break;
        }

        Destroy(gameObject);
    }
}
