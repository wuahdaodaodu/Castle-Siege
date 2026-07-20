using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions.Must;

public class ResourceGenerator : MonoBehaviour
{

    public static int GetNearbyResourceAmount( ResourceGeneratorData resourceGeneratorData,Vector3 position)
    {
        Collider2D[] collider2DArray = Physics2D.OverlapCircleAll(position, resourceGeneratorData.resourceDetectionRadius);
        int nearbyResourceAmount = 0;


        foreach (Collider2D collider2D in collider2DArray)
        {
            ResourceNode resourceNode = collider2D.GetComponent<ResourceNode>();

            if (resourceNode != null)
            {
                if (resourceNode.resourceType == resourceGeneratorData.resourceType)
                {
                    nearbyResourceAmount++;
                }

            }
        }

        nearbyResourceAmount = Mathf.Clamp(nearbyResourceAmount, 0, resourceGeneratorData.maxResourceAmount);
        return nearbyResourceAmount;
    }
    private BuildingTypeSO buildingType;
    private ResourceGeneratorData resourceGeneratorData;
    private float timer;
    private float timerMax;



    // Start is called before the first frame update
    void Awake()
    {
        resourceGeneratorData = GetComponent<BuildingTypeHolder>().buildingType.resourceGeneratorData;  
        timerMax = resourceGeneratorData.timerMax;
    }
    private void Start()
    {

       int nearbyResourceAmount= GetNearbyResourceAmount(resourceGeneratorData, transform.position);


        if (nearbyResourceAmount == 0)
        {
            enabled = false;
        }
        else {
            timerMax=(resourceGeneratorData.timerMax/2f)+
                resourceGeneratorData.timerMax* 
                (1-(float)nearbyResourceAmount/resourceGeneratorData.maxResourceAmount);
        
        
        }

            Debug.Log("nearResource" + nearbyResourceAmount+";timeMax"+timerMax);




    }

    // Update is called once per frame
    private  void Update()
    {
       timer-= Time.deltaTime;
        if (timer<=0f)
        {
            timer += timerMax;
             ResourceManager.Instance.AddResource(resourceGeneratorData.resourceType, 1);
         
           
        }
      
    }
   public ResourceGeneratorData GetResourceGeneratorData()
    {
        return resourceGeneratorData;
    }
    public float GetTimeNormalized()
    {
        return timer / timerMax;
    }

    public float GetAmountGeneratedPerSecond()
    {
        return 1 / timerMax;
    }
}
