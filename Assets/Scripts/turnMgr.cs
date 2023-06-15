using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class turnMgr : MonoBehaviour
{
    public bool start = false; //回合開始

    [SerializeField] private float a;
    [SerializeField] private float b;
    [SerializeField] private float c = 1000f;

    [SerializeField] public Vector3 Aposition;
    [SerializeField] public Vector3 Bposition;

    [SerializeField] private float SideEulerAngles;

    public Vector3 center;

    public GameObject card;
    public GameObject pool;

    public int cards;

    public Vector3 aRelCenter;

    public Vector3 bRelCenter;

    // Start is called before the first frame update
    void Start()
    {
        Aposition = new Vector2(a, 1f);
        Bposition = new Vector2(b, 1f);

        center = (Aposition + Bposition) * 0.5f;
        center -= new Vector3(0f, c, 0f);

        aRelCenter = Aposition - center;
        bRelCenter = Bposition - center;
        for (int i = 1; i < cards + 1; i++)
        {
            GameObject temp = Instantiate(card);
            temp.transform.position = Vector3.Slerp(aRelCenter, bRelCenter, i * (1f / (cards + 1)));
            //temp.transform.position -= new Vector3(0f, temp.transform.position.y + c,0f);
            temp.transform.localEulerAngles = new Vector3(0f, 0f,
                SideEulerAngles - ((2 * SideEulerAngles) * (i * (1f / (cards + 1)))));
        }
    }

    void Update()
    {
    }
}