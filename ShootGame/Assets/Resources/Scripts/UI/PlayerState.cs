using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerState : MonoBehaviour
{
    private Image img;
    private Scene scene;
    private Sprite shoot;
    private Sprite run;
    private bool flag = false;
    // Start is called before the first frame update
    void Start()
    {
        shoot = Resources.Load("Textures/shoot", typeof(Sprite)) as Sprite;
        run = Resources.Load("Textures/run", typeof(Sprite)) as Sprite;
        scene = Scene.getInstance();
        img = GetComponent<Image>();
        img.sprite = run;
    }

    // Update is called once per frame
    void Update()
    {
        if ((scene.playerState() == 0 || scene.playerState() == 1) && flag) 
        {
            Debug.Log(run);
            img.sprite = run;
            flag = false;
        }
        else if((scene.playerState() == 2) && !flag)
        {
            Debug.Log(shoot);
            img.sprite = shoot;
            flag = true;
        }
    }
}
