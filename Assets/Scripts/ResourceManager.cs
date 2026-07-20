using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.Windows;
using System;

public class ResourceManager : MonoBehaviour
{
    public static ResourceManager Instance { get; private set; }

    public event EventHandler OnResourceAmountChanged;

    [SerializeField] private List<ResourceAmount> startingResourceAmountList;


    private Dictionary<ResourceTypeSO, int> resourceAmountDictionary;


    //获取自身挂载的组件（GetComponent）
    //初始化本脚本自己的私有变量、容器
    //单例 Mono 管理器赋值 Instance（行业标准写在 Awake）
    //资源预加载、读取本地配置 SO（不依赖其他外部对象）只读取 Resources、本地文件，不需要别的物体配合。

    private void Awake()
    {
          Instance = this;
        resourceAmountDictionary = new Dictionary<ResourceTypeSO, int>();
        //resources资源
        ResourceTypeListSO resourceTypeList = Resources.Load<ResourceTypeListSO>(typeof(ResourceTypeListSO).Name);
        foreach (ResourceTypeSO resourceType in resourceTypeList.list)
        {   //初始化资源为0
            resourceAmountDictionary[resourceType] = 0;

        }
        foreach (ResourceAmount resourceAmount  in startingResourceAmountList)
        {
            AddResource(resourceAmount.resourceType,resourceAmount.amount);
        }
       

    }
   
    
    public void AddResource(ResourceTypeSO resourceType,int amount)
    {
        resourceAmountDictionary[resourceType] += amount;
        OnResourceAmountChanged?.Invoke(this,EventArgs.Empty);
        
        
    }
    public int GetResourceAmount(ResourceTypeSO resourceType) {     
        
        return resourceAmountDictionary[resourceType];
            }


    public bool  CanAfford(ResourceAmount[] resourceAmountArray)
    {
        foreach (ResourceAmount resourceAmount in resourceAmountArray)
        {
            if (GetResourceAmount(resourceAmount.resourceType) >= resourceAmount.amount)
            {
                return true;
            }
            else
            {

                return false;
            }
        }
        return true;
    }

    public void SpendResource(ResourceAmount[] resourceAmountArray)
    {
        foreach (ResourceAmount resourceAmount in resourceAmountArray)
        {
            resourceAmountDictionary[resourceAmount.resourceType]-= resourceAmount.amount;
          
        }
       
    }
}
