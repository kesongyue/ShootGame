using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyGame : MonoBehaviour
{
    public enum GameState
    {
        init = 0,
        begin = 1,
        pause = 2,
        over = 3
    }

    private GameState gameState;
    private Scene scene;

    // Start is called before the first frame update
    void Start()
    {
        gameState = GameState.begin;
        scene = Singleton<Scene>.Instance;
        scene.loadResource();
        Player.deathEvent += gameOver;
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
        Debug.Log(gameState);
    }

    public void gameBegin()
    {
        gameState = GameState.begin;
    }
}
