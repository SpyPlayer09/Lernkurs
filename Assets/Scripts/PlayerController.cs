using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    private Rigidbody rigbody;

    private Vector2 direction;
    [SerializeField] private int moveSpeed = 10;

    private Vector3 velocity;
    private float rotation;
    
    [SerializeField] private int jumpForce = 2;

    [SerializeField] private GameObject bullet;

    void Awake()
    {
        rigbody = GetComponent<Rigidbody>();
        //bullet = Resources.Load<GameObject>("Bullet");
    }
    
    public void Move(InputAction.CallbackContext ctx)
    {
        direction = ctx.ReadValue<Vector2>();
    }

    public void Look(InputAction.CallbackContext ctx)
    {
        Vector2 inp = ctx.ReadValue<Vector2>();
        rotation = inp.x / 10f;
    }
    
    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 forward = transform.forward;
        Vector3 right = transform.right;

        forward.y = 0f;
        right.y = 0f;
        
        forward.Normalize();
        right.Normalize();

        velocity = forward * direction.y + right * direction.x;
        velocity *= moveSpeed;

        velocity.y = rigbody.velocity.y;

        rigbody.velocity = velocity;
        
        transform.Rotate(Vector3.up, rotation);
    }

    private bool Grounded()
    {
        return Physics.Raycast(transform.position, Vector3.down, 1.05f, 1 << LayerMask.NameToLayer("Default")); //Warum diese Syntax
    }
    
    /* 
    private RaycastHit GetGround()
    {
        RaycastHit hit;
        Physics.Raycast(transform.position, Vector3.down, out hit, 10f, 1 << LayerMask.NameToLayer("Default"));
        return hit;
    }
    */

    public void Jump()
    {
        if (Grounded())
        {
            Vector3 jumpvel = new Vector3(rigbody.velocity.x, jumpForce, rigbody.velocity.z);
            rigbody.velocity = jumpvel;
        }
    }

    private bool canShoot = true;
    
    public void Shoot(InputAction.CallbackContext ctx)
    {
        if (canShoot)
        {
            Instantiate(bullet, transform.position + transform.forward, transform.rotation); //Gibt als Return die Instanz zurück
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
