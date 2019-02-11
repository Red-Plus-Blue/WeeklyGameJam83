using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Battery : MonoBehaviour
{
    public float ChargeRate = 1.0f;
    public float UsageRate = 1.0f;

    public float PowerMax = 10.0f;
    public float Power { get; protected set; }

    public bool Active { get; protected set; }

    public GameObject Display;

    protected Vector3 _displayScale;

    private void Awake()
    {
        Power = PowerMax;
        _displayScale = Display.transform.localScale;
    }

    private void Update()
    {
        if(Active)
        {
            var usage = UsageRate * Time.deltaTime;
            Power = Mathf.Max(Power - usage, 0.0f);

            if(Power == 0.0f)
            {
                Active = false;
            }
        }
        else
        {
            var recharge = ChargeRate * Time.deltaTime;
            Power = Mathf.Min(Power + recharge, PowerMax);
        }

        Display.transform.localScale = new Vector3(
            Display.transform.localScale.x,
            (Power / PowerMax) * _displayScale.y,
            Display.transform.localScale.z
        );

        Display.transform.localPosition = new Vector3(
            -((1 - (Power / PowerMax)) * _displayScale.x),
            Display.transform.localPosition.y,
            Display.transform.localPosition.z
        );
    }

    public void Activate()
    {
        Active = true;
    }
}