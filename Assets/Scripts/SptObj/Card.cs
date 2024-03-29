using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Card : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerExitHandler
{
    [Header("Properties")]
    [SerializeField] public CardSObj CardSobj;
    [SerializeField] public Image artwork;
    [SerializeField] public Text title;
    [SerializeField] public Text DescipText;
    [Tooltip("invalid area")]
    [SerializeField] private bool poolnull;
    [Tooltip("sue")]
    [SerializeField] public GameObject nullimage;
    [SerializeField] public GameObject parents;

    [Header("Drag Following")]
    [SerializeField] private GameObject _canvas;
    [SerializeField] private RectTransform _rectTransform;
    [SerializeField] private CanvasGroup _canvasGroup;
    // Start is called before the first frame update
    void Start()
    {
        _rectTransform = GetComponent<RectTransform>();
        _canvasGroup = GetComponent<CanvasGroup>();
        _canvas = GameObject.Find("Canvas");
        GetPorperties();
    }

    // Update is called once per frame
    void Update()
    {
        if (poolnull) //卡牌不是移至活動場地上的處理
        {
            transform.position = Vector3.Lerp(transform.position, parents.transform.position, Time.deltaTime * 10f);
            float Dis = Vector3.Distance(parents.transform.position, transform.position);
            if (Dis < 1f)
            {
                poolnull = false;
                nullimage.SetActive(poolnull);
            }
        }
    }
    /// <summary>
    /// 取得屬性
    /// </summary>
    void GetPorperties()
    {
        artwork.sprite = CardSobj.Artwork;

        title.text = CardSobj.Cardname;

        DescipText.text = CardSobj.Description;
    }
    /// <summary>
    /// 點下時
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerDown(PointerEventData eventData)
    {

        transform.localScale = Vector3.one;
        transform.SetParent(GameObject.Find("Canvas").transform);
        //timer = 0;
        //startAct = false;
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        _canvasGroup.blocksRaycasts = false;
        transform.SetParent(_canvas.transform);
        transform.localEulerAngles = Vector3.zero;
    }
    public void OnDrag(PointerEventData eventData)
    {
        //使物件跟隨符合滑鼠游標速度
        _rectTransform.anchoredPosition += eventData.delta; 
        //__canvasGroup.alpha = .6f; //透明度
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        //判定是否為空場地
        if (eventData.pointerEnter == null) 
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
                _canvasGroup.blocksRaycasts = true;
                _canvasGroup.alpha = 1f;
            }
            else if (eventData.pointerEnter == GameObject.Find("HandCardBlock"))
            {
                poolnull = true; //無效區域
                nullimage.SetActive(poolnull);
                transform.SetParent(parents.transform);
            }
            else if (eventData.pointerEnter.GetComponent<Card>())
            {
                poolnull = true; //無效區域
                nullimage.SetActive(poolnull);
                transform.SetParent(parents.transform);
            }
            else if (eventData.pointerEnter == GameObject.Find("SetBlock"))
            {
                _canvasGroup.blocksRaycasts = true;
                _canvasGroup.alpha = 1f;
            }
        }

        transform.localEulerAngles = Vector3.zero;
        _canvasGroup.blocksRaycasts = true;
        _canvasGroup.alpha = 1f;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (!eventData.dragging)
        {
            transform.SetParent(parents.transform);
            transform.localEulerAngles = Vector3.zero;
        }
    }
}
