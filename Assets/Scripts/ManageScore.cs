using UnityEngine;
using UnityEngine.UI;

public class ManageScore : MonoBehaviour
{
    
    public Text PlayerOneScoreText;
    public Text PlayerTwoScoreText;
    public bool? PlayerOneIsWinner { get; private set; }

    private uint _playerOneScore = 0;
    private uint _playerTwoScore = 0;

    public void AddScore(bool isPlayerOne)
    {
        if (isPlayerOne)
        {
            ++_playerOneScore;
        }
        else
        {
            ++_playerTwoScore;
        }

        if (_playerOneScore == GameManager.Instance().ScoretoWin ||
            _playerTwoScore == GameManager.Instance().ScoretoWin)
        {
            PlayerOneIsWinner = _playerOneScore > _playerTwoScore;
        }

        UpdateScoreText();

        GameManager.Instance().ChangeGameState(GameManager.GameState.Score);
    }

    public void ResetScore()
    {
        _playerOneScore = 0;
        _playerTwoScore = 0;
        PlayerOneIsWinner = null;
        UpdateScoreText();
    }

	// Use this for initialization
	void Start ()
	{
	    ResetScore();
	}

    private void UpdateScoreText()
    {
        PlayerOneScoreText.text = _playerOneScore.ToString();
        PlayerTwoScoreText.text = _playerTwoScore.ToString();
    }

}
