using System.Collections;
using System.Collections.Generic;
using UnityEngine.Assertions;
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
    public uint ScoretoWin;

    public GameObject StartMenuPreFab;
    public GameObject BallPreFab;
    public GameObject WinUIPreFab;
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
        CurrentGameState = newState;

        switch (newState)
        {
            case GameState.Start:
               LoadStartMenu();
                //button event changes to next state
                break;
            case GameState.Serve:
                
                ServeBall();
                ChangeGameState(GameState.Play);
                break;
            case GameState.Play:
                //score manager changes to next state
                break;
            case GameState.Score:
                Destroy(_ball);
                _ball = null;
                ChangeGameState(_scoreManager.PlayerOneIsWinner != null ? GameState.Win : GameState.Serve);
                break;
            case GameState.Win:
                OpenWinUI();
                //button changes to next state
                break;
        }

        
    }


    
    private static GameManager _instance;
    private ManageScore _scoreManager;
    private GameObject _startMenu;
    private GameObject _ball;
    private GameObject _winUI;

	// Use this for initialization
	void Start ()
	{
	    CurrentGameState = GameState.None;
	    _instance = this;

	    _scoreManager = GetComponentInChildren<ManageScore>();
        Assert.IsNotNull(_scoreManager);

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

    private void OpenWinUI()
    {
        Assert.IsNull(_winUI);
        Assert.IsTrue(_scoreManager.PlayerOneIsWinner.HasValue);

        Debug.Log(string.Format("{0} won!", _scoreManager.PlayerOneIsWinner.Value ? "Player One" : "Player Two"));

        _winUI = Instantiate(WinUIPreFab);
        Button confirmButton = _winUI.GetComponentInChildren<Button>();
        Assert.IsNotNull(confirmButton);
        confirmButton.onClick.AddListener(HandleWinUIConfirm);

        foreach (var text in _winUI.GetComponentsInChildren<Text>())
        {
            if (text.tag == "RuntimeEditable")
            {
                text.text = _scoreManager.PlayerOneIsWinner.Value ? "One" : "Two";
            }
        }

    }

    private void CloseWinUI()
    {
        Assert.IsNotNull(_winUI);

        Button confirmButton = _winUI.GetComponentInChildren<Button>();
        Assert.IsNotNull(confirmButton);
        confirmButton.onClick.RemoveListener(HandleWinUIConfirm);
        Destroy(_winUI);
        _winUI = null;
    }

    private void HandleWinUIConfirm()
    {
        CloseWinUI();
        _scoreManager.ResetScore();
        ChangeGameState(GameState.Start);
    }
}
