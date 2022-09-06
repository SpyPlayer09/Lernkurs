using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    [SerializeField] private int health;

    private void Update()
    {
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }
    
    public void TakeDamage(int damage)
    {
        health -= damage;
    }
}
