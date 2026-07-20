using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using TMPro;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.UI;

public class ResourcesUI : MonoBehaviour
{
    private ResourceTypeListSO resourceTypeList;
    private Dictionary<ResourceTypeSO, Transform> resourceTypetransformDictionary;
   

    private void Awake()
    {
        resourceTypeList = Resources.Load<ResourceTypeListSO>(typeof(ResourceTypeListSO).Name);

        resourceTypetransformDictionary = new Dictionary<ResourceTypeSO, Transform>();

        Transform resourceTemplate = transform.Find("resourceTemplate");
        resourceTemplate.gameObject.SetActive(false);


        int index = 0;
        foreach (ResourceTypeSO resourceType in resourceTypeList.list)
        {
            Transform resouceTransform = Instantiate(resourceTemplate, transform);
            resouceTransform.gameObject.SetActive(true);

            float offsetAmount = -160f;
            resouceTransform.GetComponent<RectTransform>().anchoredPosition = new Vector2(offsetAmount * index, 0);

            resouceTransform.Find("image").GetComponent<Image>().sprite = resourceType.sprite;

            resourceTypetransformDictionary[resourceType] = resouceTransform;
            index++;
         
            


        }
      

    }
    private void Start()
    {
        ResourceManager.Instance.OnResourceAmountChanged += ResourceManager_OnResourceAmountChanged;
        //开始初始化数量
        UpdateResourceAmount();
    }

    private void ResourceManager_OnResourceAmountChanged(object sender, System.EventArgs e)
    {
        UpdateResourceAmount();
    }

    private void UpdateResourceAmount()
    {
        foreach (ResourceTypeSO resourceType in resourceTypeList.list)
        {
            Transform resourceTransform = resourceTypetransformDictionary[resourceType];
            int resourceAmount = ResourceManager.Instance.GetResourceAmount(resourceType);

            resourceTransform.Find("text").GetComponent<TextMeshProUGUI>().SetText(resourceAmount.ToString());
            
        }
    }
}
