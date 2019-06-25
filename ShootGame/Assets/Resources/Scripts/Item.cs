using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public int type;
    public bool dead;

    void Awake()
    {
        float ran = Random.Range(0.0f, 1.0f);
        if (ran < 0.33f)
        {
            type = 0;
        } else if (ran < 0.66f)
        {
            type = 1;
        } else
        {
            type = 2;
        }
        dead = false;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public int hitted()
    {
        dead = true;
        return type;
    }

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            dead = true;
        }
    }

}
