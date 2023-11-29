using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, ITalkable
{


    // Start is called before the first frame update
    void Start()
    {
        Ceature c = new Ceature();
        c.Talk("ª±®a");
        c.gameObject = this.gameObject;
        c.ShowG(this.gameObject);

        ITalkable T = new Player();

        T.say("mama");

    }
    public void say(string N)
    {
        Debug.Log(N);
    }
}
