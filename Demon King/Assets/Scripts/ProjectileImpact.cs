using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ProjectileImpact : MonoBehaviour
{
    [SerializeField] private UnityEvent OnPlayerImpact;
    
    private void OnCollisionEnter(Collision other)
    {
        if (other.transform.CompareTag("Player"))
        {
            OnPlayerImpact.Invoke();
        }
        Destroy(this.gameObject);
    }
    
}
