using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.UI;


public class GameManager : MonoBehaviour
{

    public enum GameState
    {
        None,
        Start,
        Serve,
        Play,
        Score,
        Win
    }

    public float ServeForce;

    public GameObject StartMenuPreFab;
    public GameObject BallPreFab;
    public GameState CurrentGameState { get; private set; }

    public static GameManager Instance()
    {
        return _instance;
    }

    public void ChangeGameState(GameState newState)
    {
        if (CurrentGameState == newState)
        {
            Debug.LogWarning(string.Format("Trying to set GameState to {0} when it is already set to that.", newState.ToString()));
            return;
        }

        Debug.Log(string.Format("Set state to {0}.", newState.ToString()));

        switch (newState)
        {
            case GameState.Start:
               LoadStartMenu();
                break;
            case GameState.Serve:
                ServeBall();

                ChangeGameState(GameState.Play);
                break;
            case GameState.Play:
            case GameState.Score:
            case GameState.Win:
                break;
        }

        CurrentGameState = newState;
    }
    
    private static GameManager _instance;
    private GameObject _startMenu;
    private GameObject _ball;

	// Use this for initialization
	void Start ()
	{
	    CurrentGameState = GameState.None;
	    _instance = this;

	    ChangeGameState(GameState.Start);
	}

    private void LoadStartMenu()
    {
        Assert.IsNotNull(StartMenuPreFab, "StartMenu prefab is null when setting game state to start. Forget to set it in the editor?");
        _startMenu = Instantiate(StartMenuPreFab);

        Assert.IsNotNull(_startMenu);
        Button startGameButton = _startMenu.GetComponentInChildren<Button>();

        Assert.IsNotNull(startGameButton);
        startGameButton.onClick.AddListener(StartGame);
    }

    private void CloseStartMenu()
    {
        Assert.IsNotNull(_startMenu);
        Button startGameButton = _startMenu.GetComponentInChildren<Button>();

        Assert.IsNotNull(startGameButton);
        startGameButton.onClick.RemoveListener(StartGame);

        Destroy(_startMenu);
        _startMenu = null;
    }

    private void StartGame()
    {
        CloseStartMenu();

        ChangeGameState(GameState.Serve);
    }

    private void ServeBall()
    {
        Assert.IsNotNull(BallPreFab);
        Assert.IsNull(_ball);

        // going to spawn at origin with random direction 

        Vector3 spawnPoint = Vector3.zero;
        Vector2 force = new Vector2(Random.Range(-1f,1), Random.Range(-1f, 1));
        force.Normalize();
        force *= ServeForce;

        _ball = Instantiate(BallPreFab, spawnPoint, Quaternion.identity);
        Rigidbody2D rb = _ball.GetComponent<Rigidbody2D>();

        Assert.IsNotNull(rb);
        Debug.Log(string.Format("Adding force of {0} to ball.", force.ToString()));
        rb.AddForce(force, ForceMode2D.Impulse);
    }
}
