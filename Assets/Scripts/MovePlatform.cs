using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MovePlatform : MonoBehaviour
{
    private Vector3 startPos;
    [SerializeField] private Vector3 endPos;
    [SerializeField] private float speed;
    
    private Vector3 direction;

    private bool forward;
    
    private void Start()
    {
        forward = true;
        startPos = transform.position;
        direction = (endPos - startPos) / 10;
        direction = direction * speed;
    }

    private void Update()
    {
        transform.Translate(direction * Time.deltaTime);
    }

    private void SwitchDirection()
    {
        if (transform.position == endPos)
        {
            direction = (startPos - endPos) / 10;
            direction = direction * speed;
            forward = false;
        }

        if (transform.position == startPos)
        {
            direction = (endPos - startPos) / 10;
            direction = direction * speed;
            forward = true;
        }
    }
    
    /*
    private void Update()
    {
        if (forward)
        {
            transform.position = Vector3.MoveTowards(transform.position, endPos, speed * Time.deltaTime);
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, startPos, speed * Time.deltaTime);
        }
        SwitchDirection();
    }
    
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.transform.parent = transform;
        }
    }
    
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.transform.parent = null;
        }
    }
    */
}
