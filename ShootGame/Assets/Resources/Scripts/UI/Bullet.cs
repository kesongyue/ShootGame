using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bullet : MonoBehaviour
{
    private Image img;
    private Sprite green;
    private Sprite red;
    private Sprite blue;
    private int state;
    private Scene scene;
    // Start is called before the first frame update
    void Start()
    {
        green = Resources.Load("Textures/bulletGreen", typeof(Sprite)) as Sprite;
        red = Resources.Load("Textures/bulletRed", typeof(Sprite)) as Sprite;
        blue = Resources.Load("Textures/bulletBlue", typeof(Sprite)) as Sprite;
        scene = Scene.getInstance();
        img = GetComponent<Image>();
        img.sprite = green;
        state = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (scene.playerWeapon() == 0 && state != 0)
        {
            img.sprite = green;
            state = 0;
        }
        if (scene.playerWeapon() == 1 && state != 1)
        {
            img.sprite = red;
            state = 1;
        }
        if (scene.playerWeapon() == 2 && state != 2)
        {
            img.sprite = blue;
            state = 2;
        }
    }
}
