using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterFactory : MonoBehaviour
{
    public static MonsterFactory instance;
    private List<GameObject> used;
    private List<GameObject> free;

    void Awake()
    {
        instance = this;
        used = new List<GameObject>();
        free = new List<GameObject>();
    }

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public static MonsterFactory getInstance()
    {
        return instance;
    }

    public GameObject getMonster()
    {
        GameObject newMonster;
        if (free.Count > 0)
        {
            newMonster = free[0];
            newMonster.transform.position = new Vector3(UnityEngine.Random.Range(10.0f, 50.0f), 15.0f, UnityEngine.Random.Range(10.0f, 50.0f));
            free.RemoveAt(0);
        } else
        {
            Vector3 monsterPositon = new Vector3(UnityEngine.Random.Range(10.0f, 50.0f), 15.0f, UnityEngine.Random.Range(10.0f, 50.0f));
            float ran = Random.Range(0.0f, 1.0f);
            if (ran > 0.5f)
            {
                newMonster = Instantiate(Resources.Load("Prefabs/SciFiWarriorHP"), monsterPositon, Quaternion.identity) as GameObject;
            }
            else
            {
                newMonster = Instantiate(Resources.Load("Prefabs/SciFiWarriorPBR"), monsterPositon, Quaternion.identity) as GameObject;
            }
            newMonster.AddComponent<Monster>();
        }
        newMonster.SetActive(true);
        used.Add(newMonster);
        return newMonster;
    }

    public void freeMonster(GameObject monster)
    {
        free.Add(monster);
        used.Remove(monster);
    }

}
