using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    enum State {Staying, Running, Aiming};
    State state;
    private int blood = 0;
    private Vector3 position;
    private int firearmsType;
    private float speed;
    private float CameraMoveSpeedX = 20.0f;
    Actions actions;
    PlayerController playerController;
    Scene scene;
    [SerializeField] LayerMask whatIsGround = 1 << 8;
    bool locking;

    public delegate void die();
    public static event die deathEvent;

    // Start is called before the first frame update
    void Start()
    {
        //scene = Scene.getInstance();
        blood = 100;
        position = new Vector3(0, 0, 0);
        firearmsType = 0;
        speed = 1.0f;
        actions = GetComponent<Actions>();
        playerController = GetComponent<PlayerController>();
        playerController.SetArsenal("Rifle");
        Monster.hitPlayerEvent += getDamage;
    }

    // Update is called once per frame
    void Update()
    {
        float x = Input.GetAxis("Mouse X") * CameraMoveSpeedX;
        transform.localEulerAngles += new Vector3(-0, x);
        if (transform.position.y < -10)
        {
            blood = 0;
        }
        if (blood <= 0)
        {
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
        if (Input.GetMouseButtonDown(0))
        {
            Attack();
        }       
    }

    public void getDamage()
    {
        blood -= 30;
        Debug.Log(blood);
    }

    public void ChangeSpeed(int speed_)
    {
        speed = speed_;
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
