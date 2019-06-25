using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    private int blood;
    private Vector3 nextPosition;
    private float speed;
    private bool patroling;
    private bool dead;
    private float patrolTime;
    private float disappearTime;
    private Animator animator;
    private MonsterFactory monsterFactory;

    public delegate void HitPlayer();
    public static event HitPlayer hitPlayerEvent;

    public delegate void death();
    public static event death deathEvent;

    // Start is called before the first frame update
    void Start()
    {
        blood = 100;
        speed = 2.0f;
        dead = false;
        patroling = true;
        patrolTime = 5.0f;
        disappearTime = 0.0f;
        animator = GetComponent<Animator>();
        animator.SetBool("run", false);
        animator.SetBool("dead", false);
        monsterFactory = MonsterFactory.getInstance();
        Player.deathEvent += die;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameObject.FindWithTag("MainCamera").GetComponent<MyGame>().gameState != GameState.begin)
        {
            return;
        }
        if (transform.position.y <= 1.5f)
        {
            animator.SetBool("run", true);
        }
        if (blood <= 0)
        {
            die();
        } else
        {
            monsterMove();
        }
        if (dead)
        {
            disappearTime += Time.deltaTime;
            if (disappearTime > 2.0f)
            {
                Instantiate(Resources.Load("Prefabs/Exploson5"), transform.position, Quaternion.identity);
                float ran = Random.Range(0.0f, 1.0f);
                if (ran < 0.4f)
                {
                    Instantiate(Resources.Load("Prefabs/Item"), transform.position + new Vector3(0, 0.4f, 0), Quaternion.identity);
                }
                monsterFactory.freeMonster(this.gameObject);
                this.gameObject.SetActive(false);
                if (deathEvent != null)
                {
                    deathEvent();
                }
                blood = 100;
                speed = 2.0f;
                dead = false;
                patroling = true;
                patrolTime = 5.0f;
                disappearTime = 0.0f;
                animator.SetBool("run", false);
                animator.SetBool("dead", false);
            }
        }
    }

    public void monsterMove()
    {
        if (!dead && patroling)
        {
            if (patrolTime > Random.Range(3.0f, 6.0f))
            {
                float deltaX = Random.Range(-100.0f, 100.0f);
                float deltaZ = Random.Range(-100.0f, 100.0f);
                nextPosition = new Vector3(this.transform.position.x + deltaX, this.transform.position.y, this.transform.position.y + deltaZ);
                patrolTime = 0;
            }
            else
            {
                this.transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(nextPosition - this.transform.position), 5.0f);
                this.transform.position = Vector3.MoveTowards(this.transform.position, nextPosition, speed * Time.deltaTime);
                patrolTime += Time.deltaTime;
            }
        } else if (!dead)
        {
            this.transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(nextPosition - this.transform.position), 5.0f);
            nextPosition = GameObject.FindWithTag("Player").transform.position;
            this.transform.position = Vector3.MoveTowards(this.transform.position, nextPosition, speed * Time.deltaTime);
        }
    }

    public void explosion()
    {
        if (dead == false && hitPlayerEvent != null)
        {
            hitPlayerEvent();
        }
        die();
    }

    public void die()
    {
        animator.SetBool("dead", true);
        dead = true;
    }

    void OnCollisionStay(Collision other)
    {
        if (other.gameObject.tag == "Player")
        {
            explosion();
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            patroling = false;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            patroling = true;
        }
    }
    public void Hited(int damage)
    {
        if (!dead)
        {
            blood -= damage;
            Instantiate(Resources.Load("Prefabs/Exploson10"), transform.position, Quaternion.identity);
        }
    }
}
