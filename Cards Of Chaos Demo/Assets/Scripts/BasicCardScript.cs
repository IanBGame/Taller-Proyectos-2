using UnityEngine;

public class BasicCardScript : MonoBehaviour
{
    [SerializeField] private CardType cardType;

    [SerializeField] private float hpIncrease = 1.0f;
    [SerializeField] private float speedIncrease = 2.0f;
    [SerializeField] private float fireRateDecrease = 0.05f;
    [SerializeField] private float projectileSizeIncrease = 0.2f;
    [SerializeField] private float rangeIncrease = 0.5f;

    [Header("Referencias Visuales")]
    public SpriteRenderer spriteRenderer;
    public Sprite spriteHPUp;
    public Sprite spriteSpeedUp;
    public Sprite spriteFireRateUp;
    public Sprite spriteProjectileSizeUp;
    public Sprite spriteRangeUp;

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

    public void setSprites()
    {
        if (spriteRenderer == null) return;

        switch (cardType)
        {
            case CardType.HPUp:

                spriteRenderer.sprite = spriteHPUp;
                break;

            case CardType.SpeedUp:

                spriteRenderer.sprite = spriteSpeedUp;
                break;

            case CardType.FireRateUp:

                spriteRenderer.sprite = spriteFireRateUp;
                break;

             case CardType.ProjectileSizeUp:

                spriteRenderer.sprite = spriteProjectileSizeUp;
                break;

             case CardType.RangeUp:
                spriteRenderer.sprite = spriteRangeUp;
                break;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        PlayerController target = other.GetComponent<PlayerController>();
        if(target == null)
        {
            return;
        }

        ShopManager shopManager = FindAnyObjectByType<ShopManager>();

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
