using System.Collections.Generic;
using UnityEngine;

public class Scoreboard {

    private static readonly string SCORE_KEY = "scores";

    /// <summary>
    /// Registers the player's score
    /// </summary>
    /// <param name="score"></param>
    public static void RegisterScore(int score) {
        List<int> scores = GetScores();

        scores.Add(score);

        scores.Sort();

        SaveScore(scores);
    } 

    /// <summary>
    /// Gets a list of scores
    /// </summary>
    /// <returns></returns>
    public static List<int> GetScores() {
        string jsonStr = PlayerPrefs.GetString(SCORE_KEY);
        ScoreList scoreList = (jsonStr.Length > 0) ? JsonUtility.FromJson<ScoreList>(jsonStr) : null;

        if (scoreList == null) {
            List<int> scores = new List<int>();

            SaveScore(scores);

            return scores;
        }

        return scoreList.scores;
    }

    /// <summary>
    /// Saves the list of scores
    /// </summary>
    /// <param name="scores"></param>
    private static void SaveScore(List<int> scores) {
        ScoreList scoreList = new ScoreList();
        scoreList.scores = scores;

        string json = JsonUtility.ToJson(scoreList);
        
        PlayerPrefs.SetString(SCORE_KEY, json);
    }
}

class ScoreList {
    public List<int> scores;
}