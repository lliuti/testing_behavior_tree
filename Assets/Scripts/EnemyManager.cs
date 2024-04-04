using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [SerializeField] private int maxHealth;
    private int _currentHealth;

    void Start()
    {
        _currentHealth = maxHealth;
    }

    public bool TakeHit(int incomingDamage)
    {
        _currentHealth -= incomingDamage;
        
        if (_currentHealth <= 0) 
        {
            Die();
            return true;
        }

        return false;
    }

    private void Die()
    {
        Destroy(gameObject);
    }
}
