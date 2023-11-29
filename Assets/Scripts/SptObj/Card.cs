using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Card : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [Header("Properties")]
    [SerializeField] public CardSObj CardSobj;
    [SerializeField] public Image artwork;
    [SerializeField] public Text title;
    [SerializeField] public Text DescipText;

    [Header("Drag Following")]
    [SerializeField] private GameObject _canvas;
    [SerializeField] private RectTransform _rectTransform;
    [SerializeField] private CanvasGroup _canvasGroup;
    // Start is called before the first frame update
    void Start()
    {
        _rectTransform = GetComponent<RectTransform>();
        _canvasGroup = GetComponent<CanvasGroup>();
        GetPorperties();
    }

    // Update is called once per frame
    void Update()
    {

    }
    void GetPorperties()
    {
        artwork.sprite = CardSobj.Artwork;

        title.text = CardSobj.Cardname;

        DescipText.text = CardSobj.Description;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        
    }
    public void OnDrag(PointerEventData eventData)
    {
        _rectTransform.anchoredPosition += eventData.delta; //使物件跟隨符合滑鼠游標速度
        //_canvasGroup.alpha = .6f; //透明度
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        
    }


}
