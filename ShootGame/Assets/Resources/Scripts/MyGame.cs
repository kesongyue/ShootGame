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
    public static int score;
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

    public static int getScore()
    {
        return score;
    }

    void OnGUI()
    {
        
        GUIStyle style = new GUIStyle();

        if (gameState == GameState.init)
        {
            if (GUI.Button(new Rect(450, 150, 100, 50), "开始游戏"))
            {
                gameBegin();
            }
        }

        if (Input.GetKeyDown(KeyCode.Escape) && gameState == GameState.begin)
        {
            Time.timeScale = 0;
            gameState = GameState.pause;
        }

        if (gameState == GameState.pause)
        {
            if (GUI.Button(new Rect(450, 100, 100, 50), "继续游戏"))
            {
                Time.timeScale = 1;
                gameBegin();
            }
            if (GUI.Button(new Rect(450, 150, 100, 50), "退出游戏"))
            {
                Application.Quit();
                UnityEditor.EditorApplication.isPlaying = false;
            }
        }


        if (gameState == GameState.over)
        {
            style.normal.background = null;
            style.normal.textColor = new Color(0, 0, 0);
            style.fontSize = 40;
            GUI.TextField(new Rect(400, 160, 350, 50), "游戏失败!", style);
            if (GUI.Button(new Rect(450, 100, 100, 50), "退出游戏"))
            {
                Application.Quit();
                UnityEditor.EditorApplication.isPlaying = false;
            }

        }

        if (gameState == GameState.win)
        {
            style.normal.background = null;
            style.normal.textColor = new Color(0, 0, 0);
            style.fontSize = 40;
            GUI.TextField(new Rect(400, 160, 350, 50), "游戏胜利!", style);
            if (GUI.Button(new Rect(450, 100, 100, 50), "退出游戏"))
            {
                Application.Quit();
                UnityEditor.EditorApplication.isPlaying = false;
            }
        }
        
    }
}
