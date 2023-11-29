using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClassMgr : MonoBehaviour
{

    public GameObject Player;
    // Start is called before the first frame update
    void Start()
    {
        GameObject temp = Instantiate(Player);
        temp.transform.position = Vector3.zero;
        temp.transform.localScale = Vector3.one;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
