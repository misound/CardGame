using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Tester : MonoBehaviour,IPointerDownHandler,IBeginDragHandler,IEndDragHandler,IDragHandler,IPointerEnterHandler,IPointerExitHandler
{
    [SerializeField] private Canvas canvas;
    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;
    private turnMgr _turnMgr;

    private float _tempx; //卡本身的位置
    private float _tempy;

    [SerializeField] private bool poolnull; //是否放去未互動場地

    public GameObject nullimage; //避免有操作過快的問題，所以在上面放空按鈕

    public GameObject stagepool; //放卡場地

    public GameObject HandCard;
    

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        _turnMgr = FindObjectOfType<turnMgr>();
    }

    private void Update()
    {
        if (poolnull)
        {
            transform.position = Vector3.Lerp( new Vector3(_tempx, _tempy),transform.position,Time.deltaTime*50f);
            float Dis = Vector3.Distance(transform.position, new Vector3(_tempx, _tempy));
            if (Dis < 0.1f)
            {
                poolnull = false;
                nullimage.SetActive(poolnull);
            }
        }
    }


    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("BeginDrag");
        if (!_turnMgr.start)
        {
            return;
        }
        canvasGroup.blocksRaycasts = false;
    }
    
    public void OnDrag(PointerEventData eventData)
    {
        Debug.Log("OnDrag");
        if (!_turnMgr.start)
        {
            return;
        }
        rectTransform.anchoredPosition += eventData.delta;
        canvasGroup.alpha = .6f;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("EndDrag");
        if (!_turnMgr.start)
        {
            return;
        }
        if (eventData.pointerEnter == null)  //判定是否為空場地
        {
            poolnull = true;
            nullimage.SetActive(poolnull);
        }
        else if (eventData.pointerEnter == stagepool) //判定是否為丟牌場地 //目前無效果
        {
            canvasGroup.blocksRaycasts = true;
            canvasGroup.alpha = 1f;
        }
        else if (eventData.pointerEnter == HandCard)  //判定是否為空場地
        {
            poolnull = true;
            nullimage.SetActive(poolnull);
        }
        
        canvasGroup.blocksRaycasts = true;
        canvasGroup.alpha = 1f;

    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("Down");
        _tempx = transform.position.x;
        _tempy = transform.position.y;
        
        transform.localScale = Vector3.one;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        transform.localScale = new Vector3(2, 2);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        transform.localScale = Vector3.one;
    }
}
