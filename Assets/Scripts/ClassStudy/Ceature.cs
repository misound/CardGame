using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;

public class Ceature
{
    public GameObject gameObject = null;

    public void Talk(string name)
    {
        Debug.Log($"我叫做{name}");
    }
    public void ShowG(GameObject gameObject)
    {
        Debug.Log($"這個遊戲物件是{gameObject.name}");
    }

    public void movement()
    {
        gameObject.transform.position = new Vector3(500, 0, 0);
    }
}

public interface ITalkable
{
    public void say(string N);
}
