using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ResourceGeneratorOverlap : MonoBehaviour
{

    [SerializeField] private ResourceGenerator resourceGenerator;
    private Transform barTransform;

    private void Start()
    {
       ResourceGeneratorData resourceGeneratorData= resourceGenerator.GetResourceGeneratorData();
        barTransform = transform.Find("bar");
        transform.Find("icon").GetComponent<SpriteRenderer>().sprite = resourceGeneratorData.resourceType.sprite;
        transform.Find("text").GetComponent<TextMeshPro>().SetText(resourceGenerator.GetAmountGeneratedPerSecond().ToString("F1"));//保留一位小数


    }
    private void Update()
    { 
        barTransform.localScale = new Vector3(1-resourceGenerator.GetTimeNormalized(), 1, 1);
       
    }
}
