using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scene : MonoBehaviour
{
    public static Scene instance;
    private GameObject map;
    private GameObject player;
    private MonsterFactory monsterFactory;
    private List<GameObject> monsters;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        map = null;
        player = null;
        monsterFactory = MonsterFactory.getInstance();
        monsters = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public static Scene getInstance()
    {
        return instance;
    }

    public void loadResource()
    {
        map = Instantiate(Resources.Load("Prefabs/Low_Poly_Boat_Yard"), new Vector3(0, -1.0f, 0), Quaternion.identity) as GameObject;
        player = GameObject.FindWithTag("Player");
        for (int i = 0; i < 5; i++)
        {
            monsters.Add(monsterFactory.getMonster());
        }
    }

    public void playerMove(float translationX, float translationZ)
    {
        player.GetComponent<Player>().playerMove(translationX, translationZ);
    }

    public void playerShoot()
    {

    }

    public void changeGun(int gunType)
    {

    }

}
