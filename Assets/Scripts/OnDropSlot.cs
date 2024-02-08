using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class OnDropSlot : MonoBehaviour, IDropHandler
{
    public GameObject pool;
    private turnMgr _turnMgr;
    private int myHp = 100;

    private void Awake()
    {
        _turnMgr = FindObjectOfType<turnMgr>();
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (!_turnMgr.start)
        {
            return;
        }
        //轉移子物件
        if (eventData.pointerDrag != null)
        {
            //放置的物件與此物件對齊
            eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = GetComponent<RectTransform>().anchoredPosition;

            if (eventData.pointerDrag.GetComponent<Card>() != null)
            {
                myHp -= eventData.pointerDrag.GetComponent<Card>().CardSobj.CardAtk;
                Debug.Log(myHp);
                eventData.pointerDrag.transform.SetParent(pool.transform);
            }
        }

    }
}