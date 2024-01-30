/*
 * 將你擁有的卡牌彙整成一組牌組
 * 穩定你的手牌的角度
 * 手牌角度可依手牌張數做調整
 *
*/
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CardMgr : MonoBehaviour
{
    //牌組的list
    //手牌的list
    public List<CardSObj> allCards;
    public GameObject cardPrefab;
    private Queue<GameObject> cardPool = new Queue<GameObject>();


    void Start()
    {
        // 初始化所有卡牌
        LoadAllCards();
    }

    void LoadAllCards()
    {
        // 透過資源管理工具載入卡牌資源
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
