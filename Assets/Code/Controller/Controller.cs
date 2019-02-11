using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class Controller : MonoBehaviour
{
    public Unit Unit;
    public Rigidbody Rigidbody;

    public float InputVertical;
    public float InputHorizontal;

    private void Update()
    {
        RunUpdate();
    }

    protected virtual void RunUpdate()
    {
        var direction = new Vector3(InputHorizontal, Rigidbody.velocity.y, InputVertical);
        Rigidbody.velocity = (direction * Unit.Speed);
    }
}