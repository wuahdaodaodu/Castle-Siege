using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class TooltipUI : MonoBehaviour
{
    public static TooltipUI Instance {  get; private set; }



    private TextMeshProUGUI textMeshPro;
    private RectTransform backgroundRectTransform;
    private RectTransform rectTransform;
    private TooltipTimer tooltipTimer;
    [SerializeField] private RectTransform canvasRectTransform;

    private void Awake()
    {
        Instance = this;
        rectTransform = GetComponent<RectTransform>();
        textMeshPro= transform.Find("text").GetComponent<TextMeshProUGUI>();
        backgroundRectTransform= transform.Find("background").GetComponent<RectTransform>();
        Hide();
      
    }
    private void Update()
    {
        HandleFollowMouse();
        if (tooltipTimer !=null)
        {
            tooltipTimer.timer -= Time.deltaTime;
            if (tooltipTimer.timer < 0) 
            {
                Hide();
            }
        }

       
    }
    private void HandleFollowMouse()
    {
        Vector2 anchoredPosition = Input.mousePosition / canvasRectTransform.localScale.x;
        if (anchoredPosition.x + backgroundRectTransform.rect.width > canvasRectTransform.rect.width)
        {
            anchoredPosition.x = canvasRectTransform.rect.width - backgroundRectTransform.rect.width;
        }

        if (anchoredPosition.y + backgroundRectTransform.rect.height > canvasRectTransform.rect.height)
        {
            anchoredPosition.y = canvasRectTransform.rect.height - backgroundRectTransform.rect.height;
        }
        rectTransform.anchoredPosition = anchoredPosition;
    }
    private void SetText(string tooltipText)
    {
        textMeshPro.SetText(tooltipText);

        textMeshPro.ForceMeshUpdate();  
        //强制当前
        Vector2 textSize = textMeshPro.GetRenderedValues(false);
        Vector2 padding = new Vector2(8, 8);
        backgroundRectTransform.sizeDelta = textSize+padding;
    }


    public void Show(string tooltipText, TooltipTimer tooltipTimer=null)
    {
        this.tooltipTimer = tooltipTimer;
        gameObject.SetActive(true);
        SetText(tooltipText);
        //激活gameObject.SetActive(true)后，立刻同步执行一次坐标刷新，Tooltip 生成瞬间就落在鼠标当前位置，没有旧位置过渡，不会出现跳转闪烁
        HandleFollowMouse();
    }
    public void Hide() {
        gameObject.SetActive(false);
        

    } 
    public class TooltipTimer
    {
        public float timer;
    }
}
