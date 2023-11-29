using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

public class turnMgr : MonoBehaviour
{
    public bool start = false; //回合開始

    [Header("HandCardSort")]
    [SerializeField] private float LeftSide;
    [SerializeField] private float RightSide;
    [SerializeField] private float Height = 1000f;

    [SerializeField] private Vector3 Aposition;
    [SerializeField] private Vector3 Bposition;

    [SerializeField] private float SideEulerAngles;

    [SerializeField] private Vector3 center;

    [SerializeField] private GameObject _cardparent;
    [SerializeField] private GameObject _pool;

    [SerializeField] private int cards;
    [SerializeField] private Vector3 aRelCenter;
    [SerializeField] private Vector3 bRelCenter;

    [Header("Scanner")]
    [SerializeField] private Button _button;
    [SerializeField] private List<Card> _cards;

    // Start is called before the first frame update
    void Start()
    {
        _button.onClick.AddListener(Scan);
    }


    void Update()
    {
    }

    /// <summary>
    /// 手牌角度
    /// </summary>
    void HandCardAngle()
    {
        Aposition = new Vector2(LeftSide, 1f);
        Bposition = new Vector2(RightSide, 1f);

        center = (Aposition + Bposition) * 0.5f;
        center -= new Vector3(0f, Height, 0f);

        aRelCenter = Aposition - center;
        bRelCenter = Bposition - center;
        for (int i = 1; i < cards + 1; i++)
        {
            GameObject temp = Instantiate(_cardparent, _pool.transform);
            temp.transform.position =
                Camera.main.WorldToScreenPoint(Vector3.Slerp(aRelCenter, bRelCenter, i * (1f / (cards + 1))));
            //temp.transform.position -= new Vector3(0f, temp.transform.position.y + (c * i),0f);
            temp.transform.localEulerAngles = new Vector3(0f, 0f,
                SideEulerAngles - ((2 * SideEulerAngles) * (i * (1f / (cards + 1)))));
            //temp.transform.localRotation = quaternion.LookRotationSafe(Vector3.forward, math.lerp(Vector3.up, 2,Time.deltaTime*3));
        }
    }

    void Scan()
    {
        _cards = new List<Card>();

        Card[] dis = FindObjectsOfType<Card>();
        _cards.AddRange(dis.ToList());

        BubSort(_cards);
    }
    void BubSort(List<Card> c)
    {
        for (int i = c.Count - 1; i > 0; i--)  //每排一次，剩下的无序数减一
        {
            for (int j = 0; j < i; j++)    //一个for循环获得一个最大的数
            {
                if (c[j].transform.position.x > c[j + 1].transform.position.x)  //数值互换
                {
                    var sap = c[j];
                    c[j] = c[j + 1];
                    c[j + 1] = sap;
                }
            }
        }
    }
}