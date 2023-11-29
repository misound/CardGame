using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class OnDropSlot : MonoBehaviour, IDropHandler
{
    public GameObject pool;
    private turnMgr _turnMgr;

    private void Awake()
    {
        _turnMgr = FindObjectOfType<turnMgr>();
    }

    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log("drop");
        if (!_turnMgr.start)
        {
            return;
        }

        if (eventData.pointerDrag != null) //轉移子物件
        {
            //放置的物件與此物件對齊
            eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = GetComponent<RectTransform>().anchoredPosition;


            if (eventData.pointerDrag.GetComponent<Tester>() != null)
            {
                eventData.pointerDrag.transform.SetParent(pool.transform);
            }
        }

    }
}