using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireTrap : Trap
{
    public float Damage = 1.0f;

    public ParticleSystem[] TrapEffects;

    public override void Activate()
    {
        StartCoroutine(Run());
    }

    public override void Deactivate()
    {
        _active = false;
    }

    private void OnTriggerStay(Collider other)
    {
        if(_active == false)
        {
            return;
        }

        var unit = other.GetComponent<Unit>();
        if(unit == null)
        {
            return;
        }

        unit.TakeDamage(Damage * Time.deltaTime);
    }

    protected IEnumerator Run()
    {
        TurnOn();
        Battery.Activate();
        while(Battery.Active && _active)
        {
            yield return null;
        }
        TurnOff();
    }

    protected void TurnOn()
    {
        TrapEffects.ToList().ForEach(effect => effect.Play());
        _active = true;

    }

    protected void TurnOff()
    {
        TrapEffects.ToList().ForEach(effect => effect.Stop());
        _active = false;
    }
}
