using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class fengpeitest : MonoBehaviour
{
    public List<CardSObj> CardSobj; //建立卡牌資訊表

    public List<Card> Cards; //卡牌匹配對象


    private void Awake()
    {
        GetCards();
    }
    void Update()
    {

    }

    void GetCards()
    {
        //讀取Resources內卡牌
        for (int i = 0; i < 11; i++)
        {
            CardSObj aaa = Resources.Load<CardSObj>("Letter/" + i.ToString());
            if (aaa == null)
            {
                break;
            }

            if (!aaa.Used)
            {
                CardSobj.Add(aaa);
            }

        }

        //獲取場景內卡牌cs，陣列直接轉List
        Card[] dis = FindObjectsOfType<Card>();
        Cards.AddRange(dis.ToList());

        System.Random rand = new System.Random();
        //符合列表資訊
        for (int i = 0; i < Cards.Count; i++)
        {
            Cards[i].CardSobj = CardSobj[rand.Next(CardSobj.Count)]; //todo:解決重複數字
        }
        GameDB.LettersList = CardSobj;
    }
}
