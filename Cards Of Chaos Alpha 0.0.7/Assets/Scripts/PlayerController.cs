using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using Debug = UnityEngine.Debug;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D playerRigidBody;
    private Vector2 movementInput;
    [SerializeField] public float playerSpeed;
    [SerializeField] private GameObject bulletPrefab;

    [SerializeField] private Sprite[] playerSkins;
    [SerializeField] private SpriteRenderer spriteRenderer;

    public int playerID;
    public float currentHealth;

    public float maxHealth;
    public float projectileSize;
    public float shootTime;
    public float shootRange;

    private float shootWaitTime = 0f;
    private Coroutine corrutinaAtaque;

    private bool AttackPulsado = false;

    public void LoadPlayerStats()
    {
        if(GameManager.instance == null)
        {
            return;
        }

        PlayerStats stats = GameManager.instance.GetPlayerStats(playerID);

        if (stats == null)
        {
            return;
        }

        playerSpeed = stats.playerSpeed;
        maxHealth = stats.Health;
        projectileSize = stats.projectileSize;
        shootRange = stats.shootRange;
        shootTime = stats.shootTime;
        currentHealth = maxHealth;

        SetSkin(stats.skinID);
    }

    public void SetSkin(int skinID)
    {
        spriteRenderer.sprite = playerSkins[skinID];
    }

    private void Awake()
    {
        playerRigidBody = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        playerRigidBody.linearVelocity = movementInput * playerSpeed;

        if(shootWaitTime > 0f)
        {
            shootWaitTime -= Time.fixedDeltaTime;
        }
        
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        movementInput = context.ReadValue<Vector2>();
    }

    public void OnAttackUp(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            AttackPulsado = true;

            // Iniciamos la corrutina si no hay una ejecutándose ya
            if (corrutinaAtaque == null)
            {
                corrutinaAtaque = StartCoroutine(ShootBullet(1));
            }
        }

        // Se ejecuta exactamente cuando el jugador suelta la tecla
        if (context.canceled)
        {
            AttackPulsado = false;

            // Detenemos la corrutina de inmediato
            if (corrutinaAtaque != null)
            {
                StopCoroutine(corrutinaAtaque);
                corrutinaAtaque = null;
            }
        }

    }
    public void OnAttackDown(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            AttackPulsado = true;

            // Iniciamos la corrutina si no hay una ejecutándose ya
            if (corrutinaAtaque == null)
            {
                corrutinaAtaque = StartCoroutine(ShootBullet(2));
            }
        }

        // Se ejecuta exactamente cuando el jugador suelta la tecla
        if (context.canceled)
        {
            AttackPulsado = false;

            // Detenemos la corrutina de inmediato
            if (corrutinaAtaque != null)
            {
                StopCoroutine(corrutinaAtaque);
                corrutinaAtaque = null;
            }
        }

    }
    public void OnAttackLeft(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            AttackPulsado = true;

            // Iniciamos la corrutina si no hay una ejecutándose ya
            if (corrutinaAtaque == null)
            {
                corrutinaAtaque = StartCoroutine(ShootBullet(3));
            }
        }

        // Se ejecuta exactamente cuando el jugador suelta la tecla
        if (context.canceled)
        {
            AttackPulsado = false;

            // Detenemos la corrutina de inmediato
            if (corrutinaAtaque != null)
            {
                StopCoroutine(corrutinaAtaque);
                corrutinaAtaque = null;
            }
        }
    }
    public void OnAttackRight(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            AttackPulsado = true;

            // Iniciamos la corrutina si no hay una ejecutándose ya
            if (corrutinaAtaque == null)
            {
                corrutinaAtaque = StartCoroutine(ShootBullet(4));
            }
        }

        // Se ejecuta exactamente cuando el jugador suelta la tecla
        if (context.canceled)
        {
            AttackPulsado = false;

            // Detenemos la corrutina de inmediato
            if (corrutinaAtaque != null)
            {
                StopCoroutine(corrutinaAtaque);
                corrutinaAtaque = null;
            }
        }
    }

    private GameObject InstantiateBullet()
    {
        GameObject bullet = Instantiate(
            bulletPrefab,
            transform.position,
            Quaternion.identity
        );

        bullet.transform.localScale *= projectileSize;

        BulletController bulletController = bullet.GetComponent<BulletController>();

        bulletController.bulletTimer = shootRange;
        bulletController.playerNumber = playerID;

        shootWaitTime = shootTime;
        return bullet;


    }

    private BulletController GetController(GameObject bullet)
    {
        return bullet.GetComponent<BulletController>();
    }

    public IEnumerator ShootBullet(int direction)
    {
        while(AttackPulsado)
        {
            if (shootWaitTime <= 0f)
            {
                GameObject bullet = InstantiateBullet();

                BulletController bulletController = GetController(bullet);

                //bullet.GetComponent<BulletController>().bulletTimer = shootRange;

                if (direction == 1)
                {
                    bulletController.bulletUp = true;
                }
                else if (direction == 2)
                {
                    bulletController.bulletDown = true;
                }
                else if (direction == 3)
                {
                    bulletController.bulletLeft = true;
                }
                else if (direction == 4)
                {
                    bulletController.bulletRight = true;
                }

                shootWaitTime = Mathf.Max(0.05f, shootTime);

            }

            yield return null;
        }

    }

    public void TakeDamage()
    {
        currentHealth--;

        currentHealth = Mathf.Max(0, currentHealth);

        if(currentHealth <= 0)
        {
            PlayerDeath();
        }
    }

    private void PlayerDeath()
    {
        gameObject.SetActive(false);
    }
}
