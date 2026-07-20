using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingGhost : MonoBehaviour
{
    private GameObject spriteGameobject;
    private ResourceNearbyOverlap resourceNearbyOverlap;

    
    private void Awake()
    {
            spriteGameobject=transform.Find("sprite").gameObject;
        resourceNearbyOverlap= transform.Find("pfResourceNearbyOverlap").GetComponent<ResourceNearbyOverlap>();
            Hide();
    }
    private void Update()
    {
        transform.position = UtilsClass.GetMouseWorldPosition();
      
    }
    private void Start()
    {
        BuildingManager.Instance.OnActiveBuildingTypeChanged += BuildingManager_OnActiveBuildingTypeChanged;
    }

    private void BuildingManager_OnActiveBuildingTypeChanged(object sender, BuildingManager.OnActiveBuildingTypeChangedEventArgs e)
    {
        if (e.activeBuildingType == null) {
            Hide();
            resourceNearbyOverlap.Hide();
        }
        else
        {
            Show(e.activeBuildingType.sprite);
            if (e.activeBuildingType.hasResourceGeneratorData)
            {
                resourceNearbyOverlap.Show(e.activeBuildingType.resourceGeneratorData);
            }
            else
            {
                resourceNearbyOverlap.Hide();
            }
            
        }
    }

    private void Show(Sprite ghostSprite)
    {
         spriteGameobject.SetActive(true);
        spriteGameobject.GetComponent<SpriteRenderer>().sprite=ghostSprite;
    }
    private void Hide()
    {
        spriteGameobject.SetActive(false);
    }
}
