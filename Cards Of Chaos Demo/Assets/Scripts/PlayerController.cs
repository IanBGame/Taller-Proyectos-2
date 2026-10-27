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

    [SerializeField] private PlayerSkinManager skinManager;
    private Animator playerAnimator;

    public int playerID;
    public float currentHealth;

    public float maxHealth;
    public float projectileSize;
    public float shootTime;
    public float shootRange;
    public float playerSize;

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

    private float shootWaitTime = 0f;
    private Coroutine corrutinaAtaque;

    private bool AttackPulsado = false;
    public bool isAlive = true;
    public bool canShoot = true;

    public void LoadPlayerStats()
    {
        Debug.Log("1. Entrando en LoadPlayerStats");
        if (GameManager.instance == null)
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
        playerSize = stats.playerSize;

        hasIceRounds = stats.hasIceRounds;
        hasSugarRush = stats.hasSugarRush;
        hasSnailTrail = stats.hasSnailTrail;
        hasTiny = stats.hasTiny;
        hasBoo = stats.hasBoo;
        hasConfetti = stats.hasConfetti;
        hasGambling = stats.hasGambling;
        hasWhereAmI = stats.hasWhereAmI;
        hasDash = stats.hasDash;
        hasBeam = stats.hasBeam;
        hasRubberRounds = stats.hasRubberRounds;
        hasSpreadShot = stats.hasSpreadShot;
        hasPierce = stats.hasPierce;
        hasCloseUpMagic = stats.hasCloseUpMagic;
        hasExplodingRounds = stats.hasExplodingRounds;
        hasHeal = stats.hasHeal;
        hasStasis = stats.hasStasis;
        hasKaboom = stats.hasKaboom;

        skinManager.SetSkin(stats.skinID);
        playerAnimator = skinManager.GetAnimator();
    }

    public void RefreshStats()
    {
        PlayerStats stats =
            GameManager.instance.GetPlayerStats(playerID);

        if (stats == null)
            return;

        playerSpeed = stats.playerSpeed;
        maxHealth = stats.Health;
        projectileSize = stats.projectileSize;
        shootRange = stats.shootRange;
        shootTime = stats.shootTime;
        playerSize = stats.playerSize;

        hasIceRounds = stats.hasIceRounds;
        hasSugarRush = stats.hasSugarRush;
        hasSnailTrail = stats.hasSnailTrail;
        hasTiny = stats.hasTiny;
        hasBoo = stats.hasBoo;
        hasConfetti = stats.hasConfetti;
        hasGambling = stats.hasGambling;
        hasWhereAmI = stats.hasWhereAmI;
        hasDash = stats.hasDash;
        hasBeam = stats.hasBeam;
        hasRubberRounds = stats.hasRubberRounds;
        hasSpreadShot = stats.hasSpreadShot;
        hasPierce = stats.hasPierce;
        hasCloseUpMagic = stats.hasCloseUpMagic;
        hasExplodingRounds = stats.hasExplodingRounds;
        hasHeal = stats.hasHeal;
        hasStasis = stats.hasStasis;
        hasKaboom = stats.hasKaboom;
    }

    private void Awake()
    {
        playerRigidBody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if(playerAnimator != null)
        {
            playerAnimator.SetFloat("Speed", movementInput.magnitude);
        }
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

        if (movementInput != Vector2.zero)
        {
            Debug.Log("Jugador " + playerID + " recibe movimiento: " + movementInput);
        }
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
        GameObject bullet = Instantiate(bulletPrefab,transform.position,Quaternion.identity);

        bullet.transform.localScale *= Mathf.Max(1f, projectileSize);

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
            if (canShoot && shootWaitTime <= 0f && !hasSpreadShot)
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
            else if(canShoot  && shootWaitTime <= 0f && hasSpreadShot)
            {
                GameObject bullet1 = InstantiateBullet();
                GameObject bullet2 = InstantiateBullet();
                GameObject bullet3 = InstantiateBullet();

                BulletController bulletController1 = GetController(bullet1);
                BulletController bulletController2 = GetController(bullet2);
                BulletController bulletController3 = GetController(bullet3);

                if(direction == 1)
                {
                    bulletController1.bulletUp = true;
                    bulletController2.bulletUp = true;
                    bulletController3.bulletUp = true;

                    bullet2.transform.eulerAngles = new Vector3(0,0,30);
                }
                else if(direction == 2)
                {
                    bulletController1.bulletDown = true;
                    bulletController2.bulletDown = true;
                    bulletController3.bulletDown = true;
                }
                else if(direction == 3)
                {
                    bulletController1.bulletLeft = true;
                    bulletController2.bulletLeft = true;
                    bulletController3.bulletLeft = true;
                }
                else if(direction == 4)
                {
                    bulletController1.bulletRight = true;
                    bulletController2.bulletRight = true;
                    bulletController3.bulletRight = true;
                }
            }

            yield return null;
        }

    }

    public void TakeDamage()
    {
        if(!canShoot || !isAlive)
        {
            return;
        }

        currentHealth--;

        currentHealth = Mathf.Max(0, currentHealth);

        if(currentHealth <= 0)
        {
            PlayerDeath();
        }
    }

    private void PlayerDeath()
    {
        if(!isAlive)
        {
            return;
        }

        isAlive = false;

        AttackPulsado = false;
        movementInput = Vector2.zero;

        playerRigidBody.linearVelocity = Vector2.zero;
        playerRigidBody.simulated = false;

        skinManager.SetVisible(false);

        enabled = false;

        if (CombatManager.instance != null)
        {
            CombatManager.instance.PlayerDied(playerID);
        }
    }

    public void ResetForNewRound()
    {
        currentHealth = maxHealth;
        isAlive = true;

        AttackPulsado = false;
        corrutinaAtaque = null;
        shootWaitTime = 0f;
        movementInput = Vector2.zero;

        skinManager.SetVisible(true);

        playerRigidBody.simulated = true;
        playerRigidBody.linearVelocity = Vector2.zero;
        playerRigidBody.angularVelocity = 0f;

        enabled = true;
    }

    public void DisableShoot()
    {
        canShoot = false;

        AttackPulsado = false;
        shootWaitTime = 0f;
        movementInput = Vector2.zero;

        if (corrutinaAtaque != null)
        {
            StopCoroutine(corrutinaAtaque);
            corrutinaAtaque = null;
        }
    }
}
