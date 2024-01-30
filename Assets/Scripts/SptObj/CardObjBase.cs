using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New CardCollection", menuName = "CardCollection")]
public class CardObjBase : ScriptableObject
{
    [SerializeField] public List<CardSObj> CardList;
}
