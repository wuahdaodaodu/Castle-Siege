using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System;

public class HealthSystem : MonoBehaviour
{

    public event EventHandler OnDamaged;
    public event EventHandler Onhealed;
    public event EventHandler OnDied;
    private int healthAmount;
    [SerializeField] private int healthAmountMax;
    private void Awake()
    {
        healthAmount = healthAmountMax;
    }

    public void Damage(int damageAmount)
    {
        healthAmount-=damageAmount;
        healthAmount = Mathf.Clamp(healthAmount, 0, healthAmountMax);

        OnDamaged?.Invoke(this,EventArgs.Empty);

        if (IsDead()) { 

            OnDied?.Invoke(this,EventArgs.Empty);
        } 
    }

    public void  heal(int healAmount)
    {
        healAmount += healAmount;
        healthAmount = Mathf.Clamp(healthAmount, 0, healthAmountMax);
        Onhealed?.Invoke(this,EventArgs.Empty);
    }
    public void healFull()
    {
        healthAmount = healthAmountMax;
        Onhealed?.Invoke(this, EventArgs.Empty);
    }
    public bool IsDead()
    {
        return healthAmount == 0;
    }
   public int GetHealthAmount()
    {
        return healthAmount;
    }
    public int GetHealthAmountMax()
    {
        return healthAmountMax;
    }
    public float GetHealthAmountNormalized()
    {
        return (float)healthAmount / healthAmountMax;
    }
    public bool IsFullHealth()
    {
        return  healthAmount== healthAmountMax;
    }

    public void SetHealthAmountMax(int healthAmountMax,bool updatehealthAmount)
    {
        this.healthAmountMax = healthAmountMax;
        if (updatehealthAmount)
        {
            healthAmount = healthAmountMax;
        }
    }
    
}
