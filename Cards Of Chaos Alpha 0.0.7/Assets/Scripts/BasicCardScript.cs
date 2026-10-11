using UnityEngine;

public class BasicCardScript : MonoBehaviour
{
    [SerializeField] private CardType cardType;

    [SerializeField] private float hpIncrease = 20f;
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

    void Awake()
    {
        int randomBasicCard = Random.Range(0, 5);

        cardType = (CardType)randomBasicCard;
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        PlayerController target = other.GetComponent<PlayerController>();
        if(target == null)
        {
            return;
        }

        ApplyCard(target.playerID);

        target.LoadPlayerStats();
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
