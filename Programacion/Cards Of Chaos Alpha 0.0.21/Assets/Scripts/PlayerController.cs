using System.Diagnostics;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D playerRigidBody;
    private Vector2 movementInput;
    [SerializeField] private float playerSpeed;
    [SerializeField] private GameObject bulletPrefab;

    private void Awake()
    {
        playerRigidBody = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        playerRigidBody.linearVelocity = movementInput;
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        movementInput = context.ReadValue<Vector2>() * playerSpeed;
    }

    /*
     * private void OnAttack(InputAction.CallbackContext context)
    {
        while (context.performed)
        {
            if 
        }
    }
    */

    public void OnAttackUp(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            GameObject bullet = InstantiateBullet();

            BulletController bulletController = GetController(bullet);

            bulletController.bulletUp = true;

        }
    }
    public void OnAttackDown(InputAction.CallbackContext context)
    {
        if (context.performed) 
        {
            GameObject bullet = InstantiateBullet();

            BulletController bulletController = GetController(bullet);

            bulletController.bulletDown = true;
        }


    }
    public void OnAttackLeft(InputAction.CallbackContext context)
    {
        if (context.performed) 
        {
            GameObject bullet = InstantiateBullet();

            BulletController bulletController = GetController(bullet);

            bulletController.bulletLeft = true;
        }       
    }
    public void OnAttackRight(InputAction.CallbackContext context)
    {
        if (context.performed) 
        {
            GameObject bullet = InstantiateBullet();

            BulletController bulletController = GetController(bullet);

            bulletController.bulletRight = true;
        }
    }

    private GameObject InstantiateBullet()
    {
        GameObject bullet = Instantiate(
            bulletPrefab,
            transform.position,
            Quaternion.identity
        );

        return bullet;
    }

    private BulletController GetController(GameObject bullet)
    {
        return bullet.GetComponent<BulletController>();
    }
}
