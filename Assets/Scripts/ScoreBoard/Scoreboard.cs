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

        // find entry
        ScoreEntry entry = FindEntry(scores, playerName);

        // make a new entry if none
        if (entry == null) {
            entry = new ScoreEntry();

            entry.name = playerName;

            scores.Add(entry);
        }

        // set score
        entry.score = score;

        // sort by score
        scores.Sort(delegate(ScoreEntry e1, ScoreEntry e2) { return e2.score.CompareTo(e1.score); });

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

    /// <summary>
    /// Searches for the entry
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    private static ScoreEntry FindEntry(List<ScoreEntry> scores, string name) {
        foreach (ScoreEntry e in scores) {
            if (e.name.Equals(name)) {
                return e;
            }
        }

        return null;
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