using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState
{
    init = 0,
    begin = 1,
    pause = 2,
    over = 3,
    win = 4
}

public class MyGame : MonoBehaviour
{
        int score;
    public GameState gameState;
    private Scene scene;

    // Start is called before the first frame update
    void Start()
    {
        score = 0;
        gameState = GameState.init;
        scene = Scene.getInstance();
        scene.loadResource();
        Player.deathEvent += gameOver;
        Monster.deathEvent += addScore;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameState == GameState.begin)
        {
            float translationX = Input.GetAxis("Horizontal") * 6;
            float translationZ = Input.GetAxis("Vertical") * 6;
            scene.playerMove(translationX, translationZ);
        }
    }

    public void gamePause()
    {
        gameState = GameState.pause;
    }

    public void gameOver()
    {
        gameState = GameState.over;
    }
    public void gameWin()
    {
        gameState = GameState.win;
    }

    public void gameBegin()
    {
        gameState = GameState.begin;
    }

    public void addScore()
    {
        score++;
        if (score % 10 == 0)
        {
            scene.loadResource();
        }
        if (score == 20)
        {
            scene.playerMove(0.0f, 0.0f);
            gameWin();
        }
    }

    void OnGUI()
    {
        GUIStyle style = new GUIStyle();
        style.normal.background = null;
        style.normal.textColor = new Color(0, 0, 0);
        style.fontSize = 40;
        GUI.TextField(new Rect(0, 0, 350, 50), "Score:" + score, style);

        style.normal.background = null;
        style.normal.textColor = new Color(0, 0, 0);
        style.fontSize = 40;
        GUI.TextField(new Rect(350, 0, 350, 50), "WeapenType:" + scene.playerWeapon(), style);

        style.normal.background = null;
        style.normal.textColor = new Color(0, 0, 0);
        style.fontSize = 40;
        GUI.TextField(new Rect(800, 0, 350, 50), "Blood:" + scene.playerBlood(), style);

        if (gameState == GameState.init)
        {
            if (GUI.Button(new Rect(450, 150, 100, 50), "Start"))
            {
                gameBegin();
            }
        }

        if (gameState == GameState.over)
        {
            style.normal.background = null;
            style.normal.textColor = new Color(0, 0, 0);
            style.fontSize = 40;
            GUI.TextField(new Rect(400, 160, 350, 50), "You Lose!", style);
        }

        if (gameState == GameState.win)
        {
            style.normal.background = null;
            style.normal.textColor = new Color(0, 0, 0);
            style.fontSize = 40;
            GUI.TextField(new Rect(400, 160, 350, 50), "You Win!", style);
        }

    }
}
