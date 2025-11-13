using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LeaderboardManager : MonoBehaviour
{
    public TextMeshProUGUI firstName;
    public TextMeshProUGUI secondName;
    public TextMeshProUGUI thirdName;
    public TextMeshProUGUI fourthName;
    public TextMeshProUGUI fifthName;
    public TextMeshProUGUI sixthName;
    public TextMeshProUGUI firstScore;
    public TextMeshProUGUI secondScore;
    public TextMeshProUGUI thirdScore;
    public TextMeshProUGUI fourthScore;
    public TextMeshProUGUI fifthScore;
    public TextMeshProUGUI sixthScore;
    public List<Score> scores;
    // Start is called before the first frame update
    void Start()
    {
        // Creates a list of the names and score ui's
        List<TextMeshProUGUI> uiName = new List<TextMeshProUGUI> { firstName, secondName, thirdName, fourthName, fifthName, sixthName };
        List<TextMeshProUGUI> uiScore = new List<TextMeshProUGUI> { firstScore, secondScore, thirdScore, fourthScore, fifthScore, sixthScore };
        //Gets player score
        int days = Player.Instance.Day - 1;
        int playerScore = days * 700 + Player.Instance.GetMoney();
        bool placeFound = false;
        int currentScore;
        for (int i = 0; i < scores.Count; i++)
        {
            currentScore = scores[i].score;
            // If the players score is already being displayed offset all of the others scores
            if (placeFound)
            {
                uiName[i + 1].text = scores[i].name;
                uiScore[i + 1].text = currentScore.ToString();
            }
            // If the players spot hasn't been found and this is it
            else if (playerScore >= currentScore)
            {
                uiName[i].text = "You";
                uiScore[i].text = playerScore.ToString();
                placeFound = true;
                i -= 1;
            }
            // If the players spot hasn't been found and this isn't it
            else
            {
                uiName[i].text = scores[i].name;
                uiScore[i].text = currentScore.ToString();
            }
        }
        if (!placeFound)
        {
            uiName[uiName.Count - 1].text = "You";
            uiScore[uiScore.Count - 1].text = playerScore.ToString();
        }
    }

}
