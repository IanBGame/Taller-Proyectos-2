using UnityEngine;

public class BulletController : MonoBehaviour
{
    public float bulletSpeed;
    public float bulletTimer;

    public int playerNumber;

    public bool bulletUp;
    public bool bulletDown;
    public bool bulletLeft;
    public bool bulletRight;

    public bool isIceBullet;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (bulletUp)
        {
            this.transform.Translate(this.bulletSpeed * Time.deltaTime * Vector3.up);
        }
        else if (bulletDown)
        {
            this.transform.Translate(this.bulletSpeed * Time.deltaTime * Vector3.down);
        }
        else if (bulletLeft)
        {
            this.transform.Translate(this.bulletSpeed * Time.deltaTime * Vector3.left);
        }
        else if (bulletRight)
        {
            this.transform.Translate(this.bulletSpeed * Time.deltaTime * Vector3.right);
        }

        bulletTimer -= Time.deltaTime;

        if (bulletTimer <= 0)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        PlayerController target = other.GetComponent<PlayerController>();
        if (target == null)
        {
            return;
        }

        if (target.playerID == playerNumber)
        {
            return;
        }

        target.TakeDamage();

        Destroy(gameObject);
    }
}
