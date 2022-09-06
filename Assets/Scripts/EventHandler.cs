using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class EventHandler : MonoBehaviour
{
    private Lernkurs lernkurs;
    
    private InputAction move;
    private InputAction jump;
    private InputAction look;
    private InputAction shoot;

    private PlayerPhysic playerObject;

    public bool shouldJam = true;
    
    void Awake()
    {
        lernkurs = new Lernkurs();
        move = lernkurs.Player.Move;
        jump = lernkurs.Player.Jump;
        look = lernkurs.Player.Look;
        shoot = lernkurs.Player.Fire;
        
        playerObject = GameObject.FindWithTag("Player").GetComponent<PlayerPhysic>();
        
        move.started += playerObject.Move;
        move.canceled += playerObject.Move;
        look.started += playerObject.Look;
        look.canceled += playerObject.Look;
        jump.started += (ctx) => playerObject.Jump();
        shoot.performed += playerObject.Shoot;

        StartCoroutine(WaitAndUnjam());
    }

    private void OnEnable()
    {
        move.Enable();
        jump.Enable();
        look.Enable();
        shoot.Enable();
    }
    
    private void OnDisable()
    {
        move.Disable();
        jump.Disable();
        look.Disable();
        shoot.Disable();
    }

    public void JamWeapon()
    {
        shoot.performed -= playerObject.Shoot;
    }

    public void UnjamWeapon()
    {
        shoot.performed += playerObject.Shoot;
    }
    
    private IEnumerator WaitAndUnjam()
    {
        while (shouldJam)
        {
            Debug.Log("Jammed");
            JamWeapon();
            yield return new WaitForSeconds(3);
            UnjamWeapon();
            Debug.Log("Unjammed");
            yield return new WaitForSeconds(3);
        }
    }
    
    
}
