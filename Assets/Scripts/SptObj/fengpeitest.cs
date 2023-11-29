using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class fengpeitest : MonoBehaviour
{
    public List<CardSObj> CardSobj; //�إߥd�P��T��

    public List<Card> Cards; //�d�P�ǰt��H


    private void Awake()
    {
        GetCards();
    }
    void Update()
    {

    }

    void GetCards()
    {
        //Ū��Resources���d�P
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

        //����������d�Pcs�A�}�C������List
        Card[] dis = FindObjectsOfType<Card>();
        Cards.AddRange(dis.ToList());

        System.Random rand = new System.Random();
        //�ŦX�C���T
        for (int i = 0; i < Cards.Count; i++)
        {
            Cards[i].CardSobj = CardSobj[rand.Next(CardSobj.Count)]; //todo:�ѨM���ƼƦr
        }
        GameDB.LettersList = CardSobj;
    }
}
