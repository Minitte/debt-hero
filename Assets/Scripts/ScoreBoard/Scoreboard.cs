using System;
using System.Collections.Generic;
using UnityEngine;

public class Scoreboard {

    /// <summary>
    /// Player prefs key for score
    /// </summary>
    private static readonly string SCORE_KEY = "scores";

    /// <summary>
    /// Registers the player's score
    /// </summary>
    /// <param name="playerName"></param>
    /// <param name="score"></param>
    public static void RegisterScore(string playerName, int score) {
        List<ScoreEntry> scores = GetScores();

        ScoreEntry entry = new ScoreEntry();

        entry.name = playerName;
    
        entry.score = score;

        scores.Add(entry);

        scores.Sort(delegate(ScoreEntry e1, ScoreEntry e2) { return e1.score.CompareTo(e2.score); });

        SaveScore(scores);
    } 

    /// <summary>
    /// Gets a list of scores
    /// </summary>
    /// <returns></returns>
    public static List<ScoreEntry> GetScores() {
        string jsonStr = PlayerPrefs.GetString(SCORE_KEY);
        ScoreList scoreList = (jsonStr.Length > 0) ? JsonUtility.FromJson<ScoreList>(jsonStr) : null;

        if (scoreList == null) {
            List<ScoreEntry> scores = new List<ScoreEntry>();

            SaveScore(scores);

            return scores;
        }

        return scoreList.scores;
    }

    /// <summary>
    /// Saves the list of scores
    /// </summary>
    /// <param name="scores"></param>
    private static void SaveScore(List<ScoreEntry> scores) {
        ScoreList scoreList = new ScoreList();
        scoreList.scores = scores;

        string json = JsonUtility.ToJson(scoreList);
        
        PlayerPrefs.SetString(SCORE_KEY, json);
    }
}

[Serializable]
class ScoreList {
    public List<ScoreEntry> scores;
}

[Serializable]
public class ScoreEntry {

    /// <summary>
    /// name of player
    /// </summary>
    public string name;

    /// <summary>
    /// Score of the player
    /// </summary>
    public int score;
}