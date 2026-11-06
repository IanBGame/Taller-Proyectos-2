    using System.Collections;
using System.Collections.Specialized;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.GraphicsBuffer;
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
    [SerializeField] private float bulletAngle = 20f; //10-20-30-45


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

        if (hasTiny)
        {
            stats.ApplyTiny(this);
        }
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
        TeleportController tp = GetComponent<TeleportController>();
        CircleCollider2D collider = GetComponent<CircleCollider2D>();
        tp.checkRadius = collider.radius;

        if (playerAnimator != null)
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

    public void OnDash(InputAction.CallbackContext context)
    {
        if(context.started && hasDash)
        {

        }
    }

    private GameObject InstantiateBullet()
    {
        GameObject bullet = Instantiate(bulletPrefab,transform.position,Quaternion.identity);

        bullet.transform.localScale *= Mathf.Max(1f, projectileSize);

        BulletController bulletController = bullet.GetComponent<BulletController>();

        bulletController.bulletTimer = shootRange;
        bulletController.playerNumber = playerID;

        if(hasPierce)
        {
            bulletController.isPierce = true;
        }

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
            if (canShoot && shootWaitTime <= 0f)
            {
                if (hasSpreadShot)
                {
                    SpreadShot(direction);
                }
                else
                {


                    GameObject bullet = InstantiateBullet();

                    BulletController bulletController = GetController(bullet);

                    if (bulletController != null)
                    {
                        bulletController.shooter = this.gameObject;

                        switch(direction)
                        {
                            case 1:
                                bulletController.bulletUp = true;
                                break;
                            case 2:
                                bulletController.bulletDown = true;
                                break;
                            case 3:
                                bulletController.bulletLeft = true;
                                break;
                            case 4:
                                bulletController.bulletRight = true;
                                break;
                        }
                    }
                }
                    shootWaitTime = Mathf.Max(0.05f, shootTime);
            }
            yield return null;
        }

    }

    public void TakeDamage(GameObject shooter)
    {
        if(!canShoot || !isAlive)
        {
            return;
        }

        if (hasWhereAmI)
        {
            TeleportController tp = GetComponent<TeleportController>();
            tp.Teleport();
        }

        currentHealth--;

        currentHealth = Mathf.Max(0, currentHealth);

        if(currentHealth <= 0)
        {
            PlayerDeath(shooter);
        }
    }

    private void PlayerDeath(GameObject shooter)
    {
        PlayerController player = shooter.GetComponent<PlayerController>();

        if (!isAlive)
        {
            return;
        }

        if (player.hasHeal)
        {
            player.ApplyHeal();
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

    public void ApplyHeal()
    {
        currentHealth++;
    }

    private Vector2 GetShootDirection(int direction)
    {
        switch(direction)
        {
            case 1:
                return Vector2.up;

            case 2:
                return Vector2.down;
            
            case 3:
                return Vector2.left;

            case 4:
                return Vector2.right;

            default:
                return Vector2.zero;
        }
    }

    private void SpreadShot(int direction)
    {
        Vector2 shootDirection = GetShootDirection(direction);

        if(shootDirection == Vector2.zero)
        {
            return;
        }

        GameObject bullet1 = InstantiateBullet();
        GameObject bullet2 = InstantiateBullet();
        GameObject bullet3 = InstantiateBullet();

        // Obtener sus controladores
        BulletController controller1 = GetController(bullet1);
        BulletController controller2 = GetController(bullet2);
        BulletController controller3 = GetController(bullet3);

        Vector2 centerDirection = shootDirection;

        Vector2 leftDirection = Quaternion.Euler(0, 0, bulletAngle) * shootDirection;

        Vector2 rightDirection = Quaternion.Euler(0, 0, -bulletAngle) * shootDirection;

        bullet2.transform.rotation = Quaternion.Euler(0, 0, bulletAngle);

        bullet3.transform.rotation = Quaternion.Euler(0, 0, -bulletAngle);

        controller1.useCustomDirection = true;
        controller2.useCustomDirection = true;
        controller3.useCustomDirection = true;

        controller1.customDirection = centerDirection;
        controller2.customDirection = leftDirection;
        controller3.customDirection = rightDirection;

        controller1.shooter = gameObject;
        controller2.shooter = gameObject;
        controller3.shooter = gameObject;

        shootWaitTime = Mathf.Max(0.05f, shootTime);
    }

    private void WhereAmI()
    {
        //GameObject otherPlayer = gameObject.GetComponent<>;
    }
}
