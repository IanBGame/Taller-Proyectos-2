using System.Security.Cryptography;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public float bulletSpeed;
    public float bulletTimer;
    public GameObject shooter;

    public int playerNumber;

    public bool bulletUp;
    public bool bulletDown;
    public bool bulletLeft;
    public bool bulletRight;

    public bool isIceBullet;
    public bool isPierce;

    public bool useCustomDirection = false;
    public Vector2 customDirection;

    public int maxBounces = 3;

    void Start()
    {
        
    }

    void Update()
    {
        if (useCustomDirection)
        {
            transform.position += (Vector3)(customDirection.normalized * bulletSpeed * Time.deltaTime);
        }
        else
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
        }

        bulletTimer -= Time.deltaTime;

        if (bulletTimer <= 0)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<BulletController>() != null) return;

        PlayerController target = other.GetComponent<PlayerController>();

        if (target == null) // Significa que es pared, intentar aplicar rubber rounds
        {
            PlayerController shooterController = shooter != null ? shooter.GetComponent<PlayerController>() : null;

            if (shooterController != null && shooterController.hasRubberRounds && maxBounces > 0)
            {
                maxBounces--;

                Vector2 currentDir = GetCurrentDirection();
                float randomAngle = Random.Range(-60f, 60f);
                customDirection = (Quaternion.Euler(0, 0, randomAngle) * (-currentDir)).normalized;
                useCustomDirection = true;
                transform.position += (Vector3)(customDirection * 0.1f);
            }
            else
            {
                Destroy(gameObject);
            }
            return;
        }

        if (target.playerID == playerNumber)
        {
            return;
        }

        if (shooter != null)
        {
            target.TakeDamage(shooter);
        }

        if (!isPierce)
        {
            Destroy(gameObject);
        }

        
    }

    private Vector2 GetCurrentDirection()
    {
        if (useCustomDirection) return customDirection.normalized;
        if (bulletUp) return Vector2.up;
        if (bulletDown) return Vector2.down;
        if (bulletLeft) return Vector2.left;
        if (bulletRight) return Vector2.right;
        return Vector2.right;
    }

}
