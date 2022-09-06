using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class PlayerPhysic : MonoBehaviour
{
    private Rigidbody rb;

    private Vector2 direction;
    [SerializeField] private int moveSpeed = 10;
    [SerializeField] private int jumpForce = 2;

    private Vector3 velocity;

    private float rotation;

    [SerializeField] private GameObject bullet;
    
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        //bullet = Resources.Load<GameObject>("Bullet");
    }
    
    public void Move(InputAction.CallbackContext ctx)
    {
        Vector2 inp = ctx.ReadValue<Vector2>();
        direction = inp;
    }

    public void Look(InputAction.CallbackContext ctx)
    {
        Vector2 inp = ctx.ReadValue<Vector2>();
        rotation = inp.x / 10f;
    }

    private bool Grounded()
    {
        return Physics.Raycast(transform.position, Vector3.down, 1.05f, 1 << LayerMask.NameToLayer("Default")); //Warum diese Syntax
    }

    public void Jump()
    {
        if (Grounded())
        {
            Vector3 jumpvel = new Vector3(rb.velocity.x, jumpForce, rb.velocity.z);
            rb.velocity = jumpvel;
        }
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 forward = transform.forward;
        Vector3 right = transform.right;

        forward.y = 0f;
        right.y = 0f;
        
        forward.Normalize();
        right.Normalize();

        velocity = (forward * direction.y + right * direction.x)  * Time.deltaTime;

        velocity.Normalize();
        velocity *= moveSpeed;

        velocity.y = rb.velocity.y;

        rb.velocity = velocity;
        
        transform.Rotate(Vector3.up, rotation);
    }
    
    private bool canShoot = true;
    
    public void Shoot(InputAction.CallbackContext ctx)
    {
        if (canShoot)
        {
            GameObject bulletInstance = Instantiate(bullet, transform.position + transform.forward, transform.rotation);
            StartCoroutine(ShootDelay());
        }
    }
    
    private IEnumerator ShootDelay()
    {
        canShoot = false;
        yield return new WaitForSeconds(0.3f);
        canShoot = true;
    }
}
