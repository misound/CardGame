using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Card",menuName = "Card")]
public class CardSObj : ScriptableObject
{
    [Header("Data")] 
    [SerializeField] public string Cardname;

    [SerializeField] public string Description;
    [SerializeField] public Sprite Artwork;
    [SerializeField] private int _id;
    [SerializeField] public bool Used;

    private void Awake()
    {
        Debug.Log($"我是{100 + _id}號卡牌");
    }
    private void OnEnable()
    {
        Debug.Log($"我是{100 + _id}號卡牌");
    }
}
