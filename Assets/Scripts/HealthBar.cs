using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private HealthSystem healthSystem;
    private Transform barTransform;
    private Transform separatorContainer;

    private void Awake()
    {
        barTransform = transform.Find("bar");
    }
    private void Start()
    {
        separatorContainer = transform.Find("separatorContainer");
        Transform separatorTemplate = separatorContainer.Find("separatorTemplate");
        separatorTemplate.gameObject.SetActive(false);
        float barSize = 3f;
        float barOneHealthAmountSize=barSize/healthSystem.GetHealthAmountMax();
        int separatorCount = Mathf.FloorToInt(healthSystem.GetHealthAmountMax() / 10);

        for (int i = 0; i < separatorCount; i++)
        {
            Transform separatorTransform = Instantiate(separatorTemplate, separatorContainer);
            separatorTransform.gameObject.SetActive(true);
            separatorTransform.localPosition = new Vector3(barOneHealthAmountSize * i * 10,0,0);
        }

        healthSystem.OnDamaged += HealthSystem_OnDamaged;
        healthSystem.Onhealed += HealthSystem_Onhealed;
        UpdateBar();
        UpdateHealthBarVisible();
        

    }

    private void HealthSystem_Onhealed(object sender, EventArgs e)
    {
        UpdateBar();
        UpdateHealthBarVisible();
    }

    private void HealthSystem_OnDamaged(object sender, EventArgs e)
    {
        UpdateBar();
        UpdateHealthBarVisible();
        
    }

    private void UpdateBar()
    {
        barTransform.localScale = new Vector3(healthSystem.GetHealthAmountNormalized(),1,1);
    }

    private void UpdateHealthBarVisible()
    {
        if (healthSystem.IsFullHealth())
        {
            gameObject.SetActive(false);
        }
        else
        {
            gameObject.SetActive(true);
        }
        gameObject.SetActive(true);
    }

    public  void isDead()
    {


    }
}
