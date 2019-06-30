using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scene : MonoBehaviour
{
    public static Scene instance;
    private GameObject player;
    private MonsterFactory monsterFactory;
    private List<GameObject> monsters;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
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
        player = GameObject.FindWithTag("Player");
        for (int i = 0; i < 10; i++)
        {
            monsters.Add(monsterFactory.getMonster());
        }
    }

    public void playerMove(float translationX, float translationZ)
    {
        player.GetComponent<Player>().playerMove(translationX, translationZ);
    }

    public int playerBlood()
    {
        return player.GetComponent<Player>().getBlood();
    }

    public int playerWeapon()
    {
        return player.GetComponent<Player>().getWeaponType();
    }

    public int playerState()
    {
        return player.GetComponent<Player>().getState();
    }

}
