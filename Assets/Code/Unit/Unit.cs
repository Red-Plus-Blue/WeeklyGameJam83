using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class Unit : MonoBehaviour
{
    public float HealthMax = 100.0f;
    public float Health { get; protected set; }

    public float Speed = 1.0f;
    public Animator Animator;

    private void Start()
    {
        Health = HealthMax;
    }

    public void TakeDamage(float amount)
    {
        if(Health == 0.0f)
        {
            return;
        }

        Health = Mathf.Max(Health - amount, 0.0f);
        if(Health == 0.0f)
        {
            Die();
        }
    }

    protected void Die()
    {
        var controller = gameObject.GetComponent<Controller>();
        Destroy(controller);

        var rigidBody = gameObject.GetComponent<Rigidbody>();
        rigidBody.velocity = Vector3.zero;

        Animator.SetTrigger("Die");

        GameObject.Destroy(gameObject, 1.0f);
    }
}