using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Tester : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler,
    IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private GameObject canvas;
    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;
    private turnMgr _turnMgr;

    private float _tempx; //卡本身的位置
    private float _tempy;

    [SerializeField] private bool poolnull; //是否放去未互動場地

    public GameObject nullimage; //避免有操作過快的問題，所以在上面放空按鈕

    public GameObject parents;

    public Image cardself;

    private float timer;
    private float Actime = 1.6f;
    private bool startAct;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        _turnMgr = FindObjectOfType<turnMgr>();
        canvas = GameObject.Find("Canvas");
    }

    private void Start()
    {
    }

    private void Update()
    {
        if (poolnull) //卡牌不是移至活動場地上的處理
        {
            transform.position = Vector3.Lerp(new Vector3(_tempx, _tempy), transform.position, Time.deltaTime * 50f);
            float Dis = Vector3.Distance(transform.position, new Vector3(_tempx, _tempy));
            if (Dis < 0.1f)
            {
                poolnull = false;
                nullimage.SetActive(poolnull);
            }
        }

        if (startAct) //滑鼠移至卡牌上放大
        {
            timer += Time.deltaTime;
            if (timer >= Actime)
            {
                cardself.raycastPadding = new Vector4(40f, 40, 40, 40);
                transform.SetParent(GameObject.Find("Canvas").transform);
                transform.localEulerAngles = Vector3.zero;
            }
        }
        
        //transform.localRotation = quaternion.LookRotationSafe(Vector3.forward, math.lerp(Vector3.up, 2,Time.deltaTime*3));
    }


    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("BeginDrag");
        if (!_turnMgr.start)
        {
            return;
        }

        canvasGroup.blocksRaycasts = false;
        transform.SetParent(GameObject.Find("Canvas").transform);
        transform.localEulerAngles = Vector3.zero;

        //GetComponent<Image>().raycastPadding = Vector4.one;
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

    /// <summary>
    /// 判斷場地 還有角度及子父物件的設置
    /// </summary>
    /// <param name="eventData"></param>
    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("EndDrag");
        if (!_turnMgr.start)
        {
            return;
        }

        if (eventData.pointerEnter == null) //判定是否為空場地
        {
            poolnull = true;
            nullimage.SetActive(poolnull);
            Debug.Log(poolnull);
            transform.localEulerAngles = Vector3.zero;
            transform.SetParent(parents.transform);
        }
        else
        {
            if (eventData.pointerEnter == GameObject.Find("UseCardBlock"))
            {
                canvasGroup.blocksRaycasts = true;
                canvasGroup.alpha = 1f;
            }

            if (eventData.pointerEnter == GameObject.Find("HandCardBlock"))
            {
                poolnull = true; //無效區域
                nullimage.SetActive(poolnull);
                transform.SetParent(parents.transform);
            }

            if (eventData.pointerEnter.GetComponent<Tester>())
            {
                poolnull = true; //無效區域
                nullimage.SetActive(poolnull);
                transform.SetParent(parents.transform);
            }

            if(eventData.pointerEnter == GameObject.Find("SetBlock"))
            {
                canvasGroup.blocksRaycasts = true;
                canvasGroup.alpha = 1f;
            }

            Debug.Log(eventData.pointerEnter);
        }

        transform.localEulerAngles = Vector3.zero;
        canvasGroup.blocksRaycasts = true;
        canvasGroup.alpha = 1f;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("Down");
        _tempx = transform.position.x;
        _tempy = transform.position.y;

        transform.localScale = Vector3.one;
        transform.SetParent(GameObject.Find("Canvas").transform);
        timer = 0;
        startAct = false;
        //GetComponent<Image>().raycastPadding = Vector4.one;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!eventData.dragging)
        {
            int i = canvas.transform.childCount;
            startAct = true;
            transform.localScale = new Vector3(1.5f, 1.5f);
            cardself.raycastPadding = new Vector4(40f, 0, 0, 0);
            transform.SetSiblingIndex(i - 1); //最上方
            //transform.SetParent(GameObject.Find("Canvas").transform);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        timer = 0;
        transform.localScale = Vector3.one;
        cardself.raycastPadding = Vector4.zero;
        startAct = false;
        if (!eventData.dragging)
        {
            transform.SetParent(parents.transform);
            transform.localEulerAngles = Vector3.zero;
        }
    }
}