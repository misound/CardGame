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
    [SerializeField] private float Sides;
    [SerializeField] private float Height = 1000f;
    [Tooltip("Hand card left side")]
    [SerializeField] private Vector3 Aposition;
    [Tooltip("Hand card right side")]
    [SerializeField] private Vector3 Bposition;
    [Tooltip("Max Angle")]
    [SerializeField] private float SideEulerAngles;

    [Tooltip("Card Obj")]
    [SerializeField] private GameObject _cardparent;
    [Tooltip("Card trans parents")]
    [SerializeField] private GameObject _pool;
    [Tooltip("Card amount")]
    [SerializeField] public int cards;
    [SerializeField] public int handCards;

    [Header("Scanner")]
    [SerializeField] private Button _button;
    [SerializeField] private List<Card> _cards;
    [SerializeField] private List<Card> _deck;
    [SerializeField] private List<GameObject> cardParent;
    [SerializeField] private List<GameObject> HandcardParent;
    [SerializeField] private List<CardSObj> cardSObjs;
    [SerializeField] private CardObjBase cardObjBase;

    public int Dmg = 1;
    // Start is called before the first frame update
    private void Awake()
    {
        //handCards = 3;
        DeckIns(cards);
    }
    void Start()
    {
        _button.onClick.AddListener(Scan);
        //Scan();
    }
    void Update()
    {

    }
    #region 手牌角度
    /// <summary>
    /// 手牌角度
    /// </summary>
    void HandCardAngle(int handcard)
    {
        //從三點來做手牌的範圍
        //最左邊
        Aposition = new Vector2(-Sides, Height);
        ///最右邊
        Bposition = new Vector2(Sides, Height);
        //取兩點之中點為第三點


        //以AB點Y值為圓心
        //取AB點X值做球形差值的圓周上任兩點
        //1.生產物件，並認canvas作為父節點
        //2.對齊UI上canvas的RectTransform，並用球形插值平均分配於AB區間上，平均點的間距由卡牌的總數決定
        //3.角度:我她媽怎麼得出這公式的?
        //3-1.反正他可以平均分配我的角度，由(最大角度)到-(最大角度)

        for (int i = 1; i < handcard + 1; i++)
        {
            //生
            GameObject temp = Instantiate(_cardparent, _pool.transform);
            //球形插植來對齊每張卡牌
            temp.transform.localPosition =
                Camera.main.WorldToScreenPoint(Vector3.Slerp(Aposition, Bposition, i * (1f / (handcard + 1))))
                - Camera.main.WorldToScreenPoint((Aposition + Bposition) / 2);
            //to do:依照卡牌多寡來做高度增減
            //角度
            temp.transform.localEulerAngles = new Vector3(0f, 0f,
                SideEulerAngles - ((2 * SideEulerAngles) * (i * (1f / (handcard + 1)))));
            //卡牌定位的子物件
            _cards.Add(temp.transform.GetChild(0).GetComponent<Card>());
            //temp.transform.localRotation = quaternion.LookRotationSafe(Vector3.forward, math.lerp(Vector3.up, 2,Time.deltaTime*3));
        }
    }
    #endregion
    /// <summary>
    /// 生產牌組
    /// </summary>
    /// <param name="CardAmount">牌組數目</param>
    void DeckIns(int CardAmount)
    {
        for (int i = 0; i < CardAmount; i++)
        {
            GameObject temp = Instantiate(_cardparent, _pool.transform);
            _deck.Add(temp.transform.GetChild(0).GetComponent<Card>());
            cardParent.Add(temp);
            GetCards();
            temp.SetActive(false);
        }

    }
    /// <summary>
    /// 手牌排序
    /// </summary>
    /// <param name="handcard">手牌張數</param>
    public void HandCard(int handcard)
    {
        //從三點來做手牌的範圍
        //最左邊
        Aposition = new Vector2(-Sides, Height);
        ///最右邊
        Bposition = new Vector2(Sides, Height);
        if (handcard < 0)
            return;
        //複製牌組資訊
        HandcardParent = cardParent.GetRange(0, handcard);
        if (handcard < 2)
            return;
        for (int i = 0; i < handcard; i++)
        {
            HandcardParent[i].gameObject.SetActive(true);
            //生
            //球形插植來對齊每張卡牌
            HandcardParent[i].gameObject.transform.localPosition =
                Camera.main.WorldToScreenPoint(Vector3.Slerp(Aposition, Bposition, i * (1f / (handcard - 1))))
                - Camera.main.WorldToScreenPoint((Aposition + Bposition) / 2);
            //to do:依照卡牌多寡來做高度增減
            //角度
            HandcardParent[i].transform.localEulerAngles = new Vector3(0f, 0f,
                SideEulerAngles - ((2 * SideEulerAngles) * (i * (1f / (handcard - 1)))));
            //卡牌定位的子物件
            //_cards.Add(temp.transform.GetChild(0).GetComponent<Card>());
            //temp.transform.localRotation = quaternion.LookRotationSafe(Vector3.forward, math.lerp(Vector3.up, 2,Time.deltaTime*3));
        }
    }
    #region 另一種手牌排序方法
    void HandCardAngle01()
    {
        //從三點來做手牌的範圍
        //最左邊
        Aposition = new Vector2(-Sides, 1f);
        ///最右邊
        Bposition = new Vector2(Sides, 1f);
        //取兩點之中點為第三點
        //center = (Aposition + Bposition) * 0.5f;
        //center -= new Vector3(0f, Height, 0f);


        for (int i = 1; i < cards + 1; i++)
        {
            //生
            GameObject temp = Instantiate(_cardparent, _pool.transform);
            //對齊物件
            temp.transform.position =
                Camera.main.WorldToScreenPoint(Handcard(i, cards));
            //角度
            temp.transform.localEulerAngles = new Vector3(0f, 0f,
                SideEulerAngles - ((2 * SideEulerAngles) * (i * (1f / (cards + 1)))));
            //temp.transform.localRotation = quaternion.LookRotationSafe(Vector3.forward, math.lerp(Vector3.up, 2,Time.deltaTime*3));
        }
    }

    private Vector3 Handcard(int cardIndex, int cardTotal)
    {
        if (cardTotal % 2 == 0)
        {
            var xPos = Vector3.left * (math.ceil(cardTotal / 2)) + Vector3.right * cardIndex;
            var yPos = Vector3.down * math.abs(math.ceil(cardTotal) / 2 - cardIndex);
            return transform.position + _cardparent.transform.position.x * (xPos)
                + _cardparent.transform.position.y * (yPos);

        }
        else
        {
            var xPos = Vector3.left * ((cardTotal / 2) - .5f) + Vector3.right * cardIndex;
            var yPos = Vector3.down * math.abs(cardTotal / 2 - cardIndex - .5f);
            return transform.position + 10 * (xPos)
                + _cardparent.transform.position.y * (yPos);
        }
    }
    #endregion
    #region 卡牌位置排序
    void Scan()
    {
        _cards = new List<Card>();
        Card[] dis = FindObjectsOfType<Card>();
        _cards.AddRange(dis.ToList());
        BubSort(_cards);
    }
    /// <summary>
    /// 氣泡排序，排序卡牌所在的x值
    /// </summary>
    /// <param name="c">卡牌清單</param>
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
    #endregion
    #region 載入卡牌資訊
    /// <summary>
    /// 載入卡牌ScriptObject資訊
    /// </summary>
    void GetCards()
    {
        //讀取Resources內卡牌
        for (int i = 0; i < 11; i++)
        {
            //載入卡牌資訊
            CardSObj aaa = Resources.Load<CardSObj>("CardID/" + i.ToString());
            if (aaa == null)
            {
                break;
            }

            if (!aaa.Used)
            {
                cardSObjs.Add(aaa);
            }

        }
        //符合列表資訊
        for (int i = 0; i < _deck.Count; i++)
        {
            //todo:幫每個卡牌安置對應的SObj
            _deck[i].CardSobj = cardSObjs[GameDB.rand.Next(cardSObjs.Count)];
        }
        GameDB.LettersList = cardSObjs;
    }
    #endregion
    private void OnGUI()
    {
        if (GUI.Button(new Rect(10, 10, 50, 50), "card++"))
            HandCard(++handCards);
        if (GUI.Button(new Rect(10, 70, 50, 30), "card--"))
            HandCard(--handCards);
    }
}