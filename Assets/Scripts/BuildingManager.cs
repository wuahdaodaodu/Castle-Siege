using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System;

public class BuildingManager : MonoBehaviour
{

    public static BuildingManager Instance {  get; private set; }


    public event EventHandler<OnActiveBuildingTypeChangedEventArgs> OnActiveBuildingTypeChanged;


 

    public class OnActiveBuildingTypeChangedEventArgs : EventArgs
    {
        public BuildingTypeSO activeBuildingType;
    }




    private Camera mainCamera;
    private BuildingTypeListSO buildingTypeList;
    private BuildingTypeSO  activeBuildingType;
    [SerializeField] private Building hqBuilding;
    private void Awake()
    {
        Instance = this;
        buildingTypeList = Resources.Load<BuildingTypeListSO>(typeof(BuildingTypeListSO).Name);
        
    }
    private void Start()
    {
        mainCamera = Camera.main;
        hqBuilding.GetComponent<HealthSystem>().OnDied += HQ_OnDied;
      
       
        
    }

    private void HQ_OnDied(object sender, EventArgs e)
    {
        SoundManager.Instance.PlaySound(SoundManager.Sound.GameOver);
        GameOverUI.Instance.Show();
    }

    private void Update()
    {
        //EventSystem 是 UGUI 事件系统，专门检测鼠标是否悬浮 / 点击在 UI 物体（按钮、图片、文字、面板）上
        if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
        {
            if( activeBuildingType != null )
            {
                if (canSpawnBuilding(activeBuildingType, UtilsClass.GetMouseWorldPosition(), out string errorMessage))
                {
                    if (ResourceManager.Instance.CanAfford(activeBuildingType.constructionResourceCostArray))
                    {
                        ResourceManager.Instance.SpendResource(activeBuildingType.constructionResourceCostArray);
                        // Instantiate(activeBuildingType.prefab, UtilsClass.GetMouseWorldPosition(), Quaternion.identity);
                        BuildingConstruction.Create(UtilsClass.GetMouseWorldPosition(), activeBuildingType);
                        SoundManager.Instance.PlaySound(SoundManager.Sound.BuildingPlaced);
                    }
                    else
                    {
                        TooltipUI.Instance.Show("Cannot afford" + activeBuildingType.GetConstructionResourceCostString(),new TooltipUI.TooltipTimer { timer = 2f });
                    }

                }
                else
                {
                    TooltipUI.Instance.Show(errorMessage,new TooltipUI.TooltipTimer { timer=2f});
                }


            }



        }

     


    }
   
    public void SetActiveBuildingType(BuildingTypeSO buildingType)
    {
        //设置激活建筑类型
        activeBuildingType=buildingType;

        //this是谁触发的，当前实例触发，EventArgs事件附带的数据，
        OnActiveBuildingTypeChanged?.Invoke(this, 
            new OnActiveBuildingTypeChangedEventArgs { activeBuildingType= activeBuildingType });
    }
    public BuildingTypeSO GetActiveBuildingType()
    {
        return activeBuildingType;
    }

    private bool canSpawnBuilding(BuildingTypeSO buildingType, Vector3 position,out string errorMessage)
    {
        BoxCollider2D boxCollider2D = buildingType.prefab.GetComponent<BoxCollider2D>();

        //检查目标区域是否可用,有无遮挡物
        Collider2D[] collider2DArray = Physics2D.OverlapBoxAll(position + (Vector3)boxCollider2D.offset, boxCollider2D.size, 0);

        bool isAreaClear = collider2DArray.Length == 0;
        if (!isAreaClear)
        {
            errorMessage = "Area is not clear!";
            return false;
        } 


        //最小区域可用
        collider2DArray = Physics2D.OverlapCircleAll(position, buildingType.minConstructionRadius);

        foreach (Collider2D collider2D in collider2DArray)
        {
            BuildingTypeHolder buildingTypeHolder = collider2D.GetComponent<BuildingTypeHolder>();
            if (buildingTypeHolder != null)
            {
                if (buildingTypeHolder.buildingType == buildingType)
                {
                    errorMessage = "Too close to another building of the same type!";
                    return false;
                }
            }
            ;
        }
        if (buildingType.hasResourceGeneratorData)
        {
            ResourceGeneratorData resourceGeneratorData = buildingType.resourceGeneratorData;
            int nearbyResourceAmount = ResourceGenerator.GetNearbyResourceAmount(resourceGeneratorData, position);
            if (nearbyResourceAmount==0)
            {
                errorMessage = "There are no nearby Resource Nodes";
                return false;
            }
        }
      


        float maxConstructionRadius = 25;
        //最大区域可用
        collider2DArray = Physics2D.OverlapCircleAll(position, maxConstructionRadius);

        foreach (Collider2D collider2D in collider2DArray)
        {
            BuildingTypeHolder buildingTypeHolder = collider2D.GetComponent<BuildingTypeHolder>();
            //最大区域如果存在建筑，可放，不存在就不能
            if (buildingTypeHolder != null)
            {
                errorMessage = "";
                return true;
            }
            ;
        }
        errorMessage = "Too far from any other building!";
        return false;

    }
    public Building GetHQBuilding()
    {
        return hqBuilding;
    }


}
