using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpritePostionSortingOrder : MonoBehaviour
{
    [SerializeField] private bool runOnce;
    [SerializeField] private float positionOffsetY;
    private SpriteRenderer spriteRenderer;
    private void Awake()
    {
             spriteRenderer= GetComponent<SpriteRenderer>();
    }
    private void LateUpdate()
    {
        //根据当前物体的y来确定渲染层级
        float precisionMultiplier = 5f;
        //放大精度，比如一个是2.1，2.9.会都位于2，变量用于精度放大
        spriteRenderer.sortingOrder=(int)(-(transform.position.y+ positionOffsetY) *precisionMultiplier);

        if (runOnce)
        {
            Destroy(this);
        }
    }
}
