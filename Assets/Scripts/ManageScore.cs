using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManageScore : MonoBehaviour
{
    public uint ScoretoWin;
    public Text PlayerOneScoreText;
    public Text PlayerTwoScoreText;

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

        UpdateScoreText();
    }

    public void ResetScore()
    {
        _playerOneScore = 0;
        _playerTwoScore = 0;
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
