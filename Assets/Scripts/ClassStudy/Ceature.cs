using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;

public class Ceature
{
    public GameObject gameObject = null;

    public void Talk(string name)
    {
        Debug.Log($"�ڥs��{name}");
    }
    public void ShowG(GameObject gameObject)
    {
        Debug.Log($"�o�ӹC������O{gameObject.name}");
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
