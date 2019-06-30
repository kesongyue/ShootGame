using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    enum State { Staying, Running, Aiming };
    State state;
    private int blood = 0;
    private float speed;
    private float CameraMoveSpeedX = 15.0f;
    Actions actions;
    PlayerController playerController;
    Scene scene;
    [SerializeField] LayerMask whatIsGround = 1 << 8;
    bool locking;
    private LineRenderer lineRenderer;
    public GameObject fire;
    private int demageValue;
    private int weapontype;
    private float time;
    private float interval;
    public delegate void die();
    public static event die deathEvent;

    // Start is called before the first frame update
    void Start()
    {
        //scene = Scene.getInstance();
        blood = 100;
        speed = 1.0f;
        actions = GetComponent<Actions>();
        playerController = GetComponent<PlayerController>();
        playerController.SetArsenal("Rifle");
        Monster.hitPlayerEvent += getDamage;
        demageValue = 15;
        weapontype = 0;
        time = 0;
        interval = 0.2f;
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.startWidth = 0.5f;
        lineRenderer.endWidth = 0.6f;
       
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if (GameObject.FindWithTag("MainCamera").GetComponent<MyGame>().gameState != GameState.begin)
        {
            return;
        }
        float x = Input.GetAxis("Mouse X") * CameraMoveSpeedX;
        transform.localEulerAngles += new Vector3(-0, x);
        if (transform.position.y < -15.0f)
        {
            blood = 0;
        }
        if (blood <= 0)
        {
            Death();
            if (deathEvent != null)
            {
                deathEvent();
            }
        }

        if (Input.GetMouseButtonDown(1))
        {
            if (state == State.Staying || state == State.Running)
            {
                state = State.Aiming;
                Aiming();
                locking = true;
            }
            else
            {
                state = State.Staying;
                Stay();
                locking = false;
            }
        }

        if (locking && Input.GetMouseButtonDown(0))
        {
            if (time > interval)
            {
                shootLine();
                time = 0;
            }
        }
        if (Input.GetMouseButtonUp(0))
        {
            lineRenderer.enabled = false;
        }

    }

    public void shootLine()
    {

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.transform.tag == "Monster1" || hit.transform.tag == "Monster2")
            {
                lineRenderer.enabled = true;
                lineRenderer.SetPosition(0, fire.transform.position);
                lineRenderer.SetPosition(1, hit.transform.position);
                hit.transform.gameObject.GetComponent<Monster>().Hited(demageValue);
            }
        }
    }
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Item")
        {
            Debug.Log("Item");
            weapontype = collision.gameObject.GetComponent<Item>().hitted();
            switch (weapontype)
            {
                case 0:
                    setLineColor(Color.green);
                    demageValue = 15;
                    interval = 0.2f;
                    speed = 1.0f;
                    break;
                case 1:
                    setLineColor(Color.red);
                    demageValue = 50;
                    interval = 1.0f;
                    speed = 0.6f;
                    break;
                case 2:
                    setLineColor(Color.blue);
                    demageValue = 30;
                    interval = 0.5f;
                    speed = 0.8f;
                    break;
                default: break;
            }
        }
    }

    private void setLineColor(Color color)
    {
        lineRenderer.startColor = color;
        lineRenderer.endColor = color;
    }
    public void getDamage()
    {
        blood = blood - 30 > 0 ? blood - 30 : 0;
        Damage();
    }

    public int getBlood()
    {
        return blood;
    }

    public int getState()
    {
        if (state == State.Staying) return 0;
        else if (state == State.Running) return 1;
        else return 2;
    }

    public int getWeaponType()
    {
        return weapontype;
    }

    public void playerMove(float translationX, float translationZ)
    {
        if (translationX == 0 && translationZ == 0)
        {
            if (state != State.Aiming)
            {
                Stay();
                state = State.Staying;
            }
        }
        else
        {
            if (!locking)
            {
                translationX *= Time.deltaTime * speed;
                translationZ *= Time.deltaTime * speed;
                transform.Translate(translationX, 0, translationZ);
                Run();
                state = State.Running;
            }

        }
    }

    public void Stay()
    {
        actions.Stay();
    }

    public void Walk()
    {
        actions.Walk();
    }

    public void Run()
    {
        actions.Run();
    }

    public void Attack()
    {
        actions.Attack();
    }

    public void Death()
    {
        actions.Death();
    }

    public void Damage()
    {
        actions.Damage();
    }

    public void Jump()
    {
        actions.Jump();
    }

    public void Aiming()
    {
        actions.Aiming();
    }

    public void Sitting()
    {
        actions.Sitting();
    }

}
