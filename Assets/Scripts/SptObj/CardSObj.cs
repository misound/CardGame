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
    [SerializeField] private int _cardAtk;
    [SerializeField] public int CardAtk
    {
        get { return _cardAtk; } 
        set
        {
            _cardAtk = value;
            if (value < 0)
            {
                _cardAtk = 0;
            }
        }
    }
    [SerializeField] private int _id;
    [SerializeField] public bool Used;

    private void Awake()
    {
        Debug.Log($"�ڬO{100 + _id}���d�P");
    }
    private void OnEnable()
    {
        Debug.Log($"�ڬO{100 + _id}���d�P");
    }
    private void OnValidate()
    {
        Debug.Log($"�ڬO{100 + _id}���d�P�A�ڦ�{CardAtk}�I�����O");
    }
}
