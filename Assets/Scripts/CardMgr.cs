/*
 * �N�A�֦����d�P�J�㦨�@�յP��
 * í�w�A����P������
 * ��P���ץi�̤�P�i�ư��վ�
 *
*/
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CardMgr : MonoBehaviour
{
    //�P�ժ�list
    //��P��list
    public List<CardSObj> allCards;
    public GameObject cardPrefab;
    private Queue<GameObject> cardPool = new Queue<GameObject>();


    void Start()
    {
        // ��l�ƩҦ��d�P
        LoadAllCards();
    }

    void LoadAllCards()
    {
        // �z�L�귽�޲z�u����J�d�P�귽
        allCards = Resources.LoadAll<CardSObj>("Letter/").ToList();
    }

    public GameObject GetCard()
    {
        if (cardPool.Count == 0)
        {
            GameObject newCard = Instantiate(cardPrefab);
            cardPool.Enqueue(newCard);
        }

        GameObject card = cardPool.Dequeue();
        card.SetActive(true);
        return card;
    }

    public void ReturnCard(GameObject card)
    {
        card.SetActive(false);
        cardPool.Enqueue(card);
    }
}
