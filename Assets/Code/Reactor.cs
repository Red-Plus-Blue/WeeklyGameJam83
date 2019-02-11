using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reactor : MonoBehaviour
{
    public static Reactor Instance;

    public float MaxHealth;
    public float Health { get; protected set; }

    public UI UI;

    private void Awake()
    {
        Instance = this;
        Health = MaxHealth;
    }

    private void Start()
    {
        UI.UpdateHealth((int)Health, (int)MaxHealth);
    }

    public void TakeDamage(float amount)
    {
        if(Health == 0.0f)
        {
            return;
        }

        Health = Mathf.Max(Health - amount, 0.0f);

        UI.UpdateHealth((int)Health, (int)MaxHealth);

        if(Health == 0.0f)
        {
            Die();
        }
    }

    public void Die()
    {

    }
}
