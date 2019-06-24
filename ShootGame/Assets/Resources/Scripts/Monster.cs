using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    private int blood;
    private Vector3 monsterPosition;
    private Vector3 nextPosition;
    private float speed;
    private int type;
    private bool patroling;
    private bool dead;
    private float patrolTime;
    private float disappearTime;
    private Animator animator;
    private MonsterFactory monsterFactory;

    public delegate void HitPlayer();
    public static event HitPlayer hitPlayerEvent;

    // Start is called before the first frame update
    void Start()
    {
        blood = 100;
        monsterPosition = new Vector3(0, 0, 0);
        speed = 2.0f;
        type = 1;
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
        monsterPosition = this.transform.position;
        if (monsterPosition.y <= 1.5f)
        {
            animator.SetBool("run", true);
        }
        if (blood < 0)
        {
            die();
        } else
        {
            monsterMove();
        }
    }

    public void monsterMove()
    {
        if (!dead && patroling)
        {
            if (patrolTime > 3.0f)
            {
                float deltaX = Random.Range(-10.0f, 10.0f);
                float deltaZ = Random.Range(-10.0f, 10.0f);
                nextPosition = new Vector3(this.transform.position.x + deltaX, 0, this.transform.position.y + deltaZ);
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
        } else
        {
            disappearTime += Time.deltaTime;
            if (disappearTime > 2.0f)
            {
                monsterFactory.freeMonster(this.gameObject);
                this.gameObject.SetActive(false);
            }
        }
    }

    public void exexplosion()
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
            exexplosion();
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
    /*
    public void setBlood(int newBlood)
    {
        blood = newBlood;
    }

    public void setType(int newType)
    {
        type = newType;
    }

    public void setSpeed(float newSpeed)
    {
        speed = newSpeed;
    }*/

}
