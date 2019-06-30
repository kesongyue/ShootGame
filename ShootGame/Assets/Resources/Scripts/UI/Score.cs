using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    private Text scoreText;
    private Scene scene;

    // Start is called before the first frame update
    void Start()
    {
        scene = Scene.getInstance();
        scoreText = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text = MyGame.getScore().ToString();
    }
}
